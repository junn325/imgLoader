using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace imgLoader_CLI
{
    static class Program
    {
        private static void Main(string[] args)
        {
            if (File.Exists($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt") && Directory.Exists(File.ReadAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt")))
            {
                Core.Route = File.ReadAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt");
            }

            //todo: 경로 설정 추가

            Console.WriteLine($"\nimgLoader_CLI {Assembly.GetExecutingAssembly().GetName().Version}");

            if (args.Length == 0)
            {
                Console.Write("\nURL: ");

                Processor psr = new Processor();
                psr.Initialize(new string[]{ Console.ReadLine() });
            }
            else
            {
                Processor psr = new Processor();
                psr.Initialize(args);
            }
        }
    }

    class Processor
    {
        internal const string InitString_Item = "0개 항목";
        internal const string InitString_NUM = "*/*";

        internal const ushort FORM_WIDTH = 825;
        internal const ushort FORM_HEIGHT = 495;

        private readonly Dictionary<string, string> failed = new Dictionary<string, string>();
        private List<Task> tasks = new List<Task>();
        private bool _stop = false;

        private Thread thrDownStart;

        private int _done;

        internal void Initialize(string[] url)
        {
            thrDownStart = new Thread(() => Download(url));
            thrDownStart.Start();
        }

        internal void Download(string[] url)
        {
            try
            {
                var sw = Stopwatch.StartNew();
                Trace.WriteLine($"Download: start: {sw.ElapsedMilliseconds}ms");

                Thread thStop = new Thread(Stopping);
                long millisec = sw.ElapsedMilliseconds;

                foreach (var link in url)
                {
                    if (url.Length == 0) break;

                    ISite site;
                    Dictionary<string, string> imgList;

                    string artist, title, route;

                    try
                    {
                        millisec = sw.ElapsedMilliseconds;

                        site = Core.LoadSite(link);
                        if (site?.IsValidated() != true) { Console.WriteLine("오류: 로드 실패"); return; }

                        Console.Write("이미지 리스트 추출: ");
                        imgList = site.GetImgUrls();
                        Console.WriteLine($"{imgList.Count}개 이미지");

                        Console.Write("작가명 추출: ");
                        artist = site.GetArtist();
                        Console.WriteLine($"{artist}");

                        Console.Write("작품명 추출: ");
                        title = Core.DirFilter(site.GetTitle());
                        Console.WriteLine($"{title}");

                        route =
                            artist == "N/A"
                                ? $@"{Core.Route}\{title} ({artist})"
                                : $@"{Core.Route}\{title}";
                    }
                    catch
                    {
                        Console.WriteLine("오류: 연결 실패");
                        continue;
                    }

                    try
                    {
                        if (!Directory.Exists(route)) Directory.CreateDirectory(route);
                        Core.CreateInfo(route, Core.GetNumber(link), site);
                    }
                    catch (DirectoryNotFoundException)
                    {
                        Console.WriteLine("오류: 디렉토리를 찾을 수 없음");
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("오류: 파일을 찾을 수 없음");
                    }

                    _done = 0;

                    tasks = new List<Task>();

                    AllocDown(route, imgList);
                    Console.Write("\n|");

                    Trace.WriteLine($"AllocDown: {sw.ElapsedMilliseconds - millisec}ms");

                    while (_done < imgList.Count - failed.Count) Thread.Sleep(Core.WAIT_TIME);

                    _done = 0;

                    HandleFail(route);
                    while (_done < failed.Count) Thread.Sleep(Core.WAIT_TIME);

                    Console.Write("|\n");

                    Core.Log($"Item:Complete: {link}");
                    Console.WriteLine("\n다운로드 완료.\n");
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
                    Core.Log($"실패:null: {uri}");
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

        internal void HandleFail(string route)
        {
            if (failed.Count == 0) return;

            int cnt = Core.FAIL_RETRY;
            var failCopy = new Dictionary<string, string>(failed);

            AllocDown(route, failCopy);

            while (_done < failCopy.Count - failed.Count) Thread.Sleep(Core.WAIT_TIME);

            while (failed.Count != 0 && cnt > 0)
            {
                cnt--;
                HandleFail(route);
            }

            failed.Clear();
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
