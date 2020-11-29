using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace imgLoader_CLI
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine($"\n\n{Core.PROJECT_NAME} {Assembly.GetExecutingAssembly().GetName().Version}");

            if (File.Exists($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt") && Directory.Exists(File.ReadAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt")))
            {
                Core.Route = File.ReadAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt");
                Console.WriteLine($"\n현재 경로:{Core.Route}");
            }

            if (args.Length == 0) Console.WriteLine("\n작업 취소: exit    경로 재설정: R\n");

            while (true)
            {
                if (Core.Route.Length == 0)
                {
                    Console.Write("\n경로: ");

                    string path = Console.ReadLine();

                    if (string.Equals(path, "exit", StringComparison.OrdinalIgnoreCase)) { Core.Route = File.ReadAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt"); continue; }

                    if (Directory.Exists(path)) { Core.Route = path; File.WriteAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt", path); }
                    else { Console.WriteLine("\n 존재하지 않는 경로\n"); continue; }
                }

                if (args.Length == 0)
                {
                    Console.Write("\nURL: ");

                    string temp = Console.ReadLine();
                    if (string.Compare(temp, "exit", StringComparison.OrdinalIgnoreCase) == 0) break;
                    if (string.Compare(temp, "R", StringComparison.OrdinalIgnoreCase) == 0) { Core.Route = ""; continue; }

                    Processor psr = new Processor();
                    psr.Initialize(new string[] { temp });
                }
                else
                {
                    Processor psr = new Processor();
                    psr.Initialize(args);
                    break;
                }
            }
        }
    }

    class Processor
    {
        private readonly Dictionary<string, string> failed = new Dictionary<string, string>();
        private List<Task> tasks = new List<Task>();
        private bool _stop = false;

        private Thread thrDownStart;

        private int _done;

        internal void Initialize(string[] url)
        {
            Console.Write("\n");

            bool temp = false;
            thrDownStart = new Thread(() => { Process(url); temp = true; });
            thrDownStart.Start();

            while (!temp) Thread.Sleep(Core.WAIT_TIME *4); 
        }

        internal void Process(string[] url)
        {
            try
            {
                Thread thStop = new Thread(Stopping);
                foreach (var link in url)
                {
                    if (url.Length == 0) break;

                    ISite site;
                    Dictionary<string, string> imgList;

                    string artist, title, route;

                    try
                    {
                        site = Core.LoadSite(link);
                        if (site?.IsValidated() != true) { Console.Write("오류: 로드 실패\n"); return; }

                        Console.Write("이미지 리스트 추출: ");
                        imgList = site.GetImgUrls();
                        Console.WriteLine($"{imgList.Count}개 이미지");

                        Console.Write("작가명 추출: ");
                        artist = site.GetArtist();
                        Console.WriteLine($"{artist}");

                        Console.Write("작품명 추출: ");
                        title = site.GetTitle();
                        Console.WriteLine($"{title}");

                        title = Core.DirFilter(title);

                        route =
                            artist == "N/A"
                                ? $@"{Core.Route}\{title}"
                                : $@"{Core.Route}\{title} ({artist})";
                    }
                    catch
                    {
                        Console.Write(" 오류: 연결 실패");
                        continue;
                    }

                    try
                    {
                        if (!Directory.Exists(route)) Directory.CreateDirectory(route);
                        Core.CreateInfo(route, Core.GetNumber(link), site);
                    }
                    catch (DirectoryNotFoundException)
                    {
                        Console.Write(" 오류: 디렉토리를 찾을 수 없음");
                    }
                    catch (FileNotFoundException)
                    {
                        Console.Write(" 오류: 파일을 찾을 수 없음");
                    }

                    _done = 0;

                    tasks = new List<Task>();

                    AllocDown(route, imgList);
                    Console.Write("\n|");

                    while (_done < imgList.Count - failed.Count) Thread.Sleep(Core.WAIT_TIME *2);

                    _done = 0;

                    var success = HandleFail(route);
                    while (_done < failed.Count) Thread.Sleep(Core.WAIT_TIME *2);

                    Core.Log($"Item:Complete: {link}");
                    if (success)
                    {
                        Console.Write("|\n");
                        Console.WriteLine("\n다운로드 완료.\n");
                    }
                    else
                    {
                        Console.Write("\n다운로드 실패.\n");
                    }
                }

                thStop.Start();
            }
            catch (ThreadInterruptedException) { }
        }

        internal void AllocDown(string route, Dictionary<string, string> urlList)
        {
            failed.Clear();

            foreach (var item in urlList)
            {
                tasks.Add(Task.Factory.StartNew(() => ThrDownload(item.Value, route, item.Key)));
            }
        }

        internal void ThrDownload(string uri, string route, string fileName)
        {
            if (_stop)
            {
                return;
            }

            var req = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse resp;

            req.Referer = $"https://{new Uri(uri).Host}";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36";

            try
            {
                resp = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException we)
            {
                if (we.Response == null)
                {
                    Core.Log($"실패:응답없음: {uri}");
                    failed.Add(fileName, uri);
                    return;
                }

                Core.Log($"실패:{((HttpWebResponse)we.Response).StatusCode}: {uri}");
                failed.Add(fileName, uri);
                return;
            }

            long fileSize;

            using (var br = resp.GetResponseStream())
            {
                int count;
                byte[] buff = new byte[1024];

                using var fs = new FileStream($"{route}\\{fileName}", FileMode.Create);
                do
                {
                    count = br.Read(buff, 0, buff.Length);
                    fs.Write(buff, 0, count);

                } while (count > 0);

                fileSize = fs.Length;
            }

            if (fileSize == resp.ContentLength)
            {
                Console.Write("■");
            }
            else
            {
                failed.Add(fileName, uri);
            }

            _done++;
        }

        internal bool HandleFail(string route)
        {
            if (failed.Count == 0) return true;

            int cnt = Core.FAIL_RETRY;
            var failCopy = new Dictionary<string, string>(failed);

            AllocDown(route, failCopy);

            int wait = Core.FAIL_RETRY * 60;
            while (_done < failCopy.Count - failed.Count || wait != 0)
            {
                wait--;
                Thread.Sleep(Core.WAIT_TIME);
            }

            while (failed.Count != 0 && cnt > 0)
            {
                cnt--;
                HandleFail(route);
            }

            failed.Clear();
            return true;
        }

        internal void Stopping()
        {
            new Thread(() =>
            {
                if (thrDownStart == null) return;

                while (thrDownStart.ThreadState == System.Threading.ThreadState.Running)
                {
                    thrDownStart.Interrupt();
                    Thread.Sleep(Core.WAIT_TIME);
                }

                _stop = true;

                foreach (var item in tasks) while (item.Status != TaskStatus.RanToCompletion) Thread.Sleep(Core.WAIT_TIME);

                _stop = false;

                failed.Clear();
                tasks.Clear();
            }).Start();
        }
    }
}
