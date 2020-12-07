using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace imgLoader_CLI
{
    class Processor
    {
        private readonly Dictionary<string, string> failed = new Dictionary<string, string>();
        private List<Task> tasks = new List<Task>();
        private bool _stop;

        private Thread thrDownStart;

        private int _done;

        internal void Initialize(string[] url)
        {
            Console.Write("\n");

            bool temp = false;
            thrDownStart = new Thread(() => { Process(url); temp = true; });
            thrDownStart.Start();

            while (!temp) Thread.Sleep(Core.WAIT_TIME * 4);
        }

        private void Process(string[] url)
        {
            try
            {
                Thread thStop = new Thread(Stopping);
                foreach (var link in url)
                {
                    if (string.IsNullOrEmpty(link)) continue;

                    ISite site;
                    Dictionary<string, string> imgList;

                    string artist, title, route;

                    try
                    {
                        Console.Write("작품 정보 로드: ");
                        site = Core.LoadSite(link);

                        if (site == null || !site.IsValidated()) { Console.Write("오류: 로드 실패\n"); continue; }
                        Console.WriteLine($"{site.GetType().Name}:");

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
                        else
                        {
                            if (File.Exists($"{route}\\{Core.GetNumber(link)}.{site.GetType().Name}"))
                            {
                                var fileTmp = File.ReadAllText($"{route}\\{Core.GetNumber(link)}.{site.GetType().Name}");
                                if (fileTmp.Length != 0)
                                {
                                    int.TryParse(fileTmp.Split('\n')[2], out var temp);
                                    if (temp == imgList.Count)
                                    {
                                        Console.WriteLine("\n해당 작품이 이미 존재합니다. 다시 다운로드하시겠습니까? Y/N");
                                        a: string result = Console.ReadLine().ToLower();
                                        if (result == "n") continue;
                                        if (result != "y") goto a;
                                    }
                                }
                            }
                        }
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

                    Console.Write("\n|");
                    AllocDown(route, imgList);

                    while (_done < imgList.Count - failed.Count) Thread.Sleep(Core.WAIT_TIME * 2);

                    _done = 0;

                    var success = HandleFail(route);
                    while (_done < failed.Count) Thread.Sleep(Core.WAIT_TIME * 2);
                    foreach (var item in tasks) while (item.Status != TaskStatus.RanToCompletion) Thread.Sleep(Core.WAIT_TIME);

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

        private void AllocDown(string route, Dictionary<string, string> urlList)
        {
            failed.Clear();

            foreach (var item in urlList)
            {
                tasks.Add(Task.Factory.StartNew(() => ThrDownload(item.Value, route, item.Key)));
            }
        }

        private void ThrDownload(string uri, string route, string fileName)
        {
            if (_stop)
            {
                return;
            }

            var req = WebRequest.Create(uri) as HttpWebRequest;
            HttpWebResponse resp;

            req.Referer = $"https://{new Uri(uri).Host}";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36";

            try
            {
                resp = req.GetResponse() as HttpWebResponse;
            }
            catch (WebException we)
            {
                if (we.Response == null)
                {
                    Core.Log($"실패:응답없음: {uri} {fileName}");
                    failed.Add(fileName, uri);
                    return;
                }

                Core.Log($"실패:{((HttpWebResponse)we.Response).StatusCode}: {uri} {fileName}");
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
            req.Abort();
            resp.Close();
        }

        private bool HandleFail(string route)
        {
            if (failed.Count == 0) return true;

            var failCopy = new Dictionary<string, string>(failed);

            AllocDown(route, failCopy);

            int wait = Core.FAIL_RETRY * 60;
            while (_done < failCopy.Count - failed.Count && wait != 0)
            {
                wait--;
                Thread.Sleep(Core.WAIT_TIME);
            }

            while (failed.Count != 0)
            {
                Thread.Sleep(Core.WAIT_TIME * 40);
                HandleFail(route);
            }

            failed.Clear();
            return true;
        }

        private void Stopping()
        {
            new Thread(() =>
            {
                if (thrDownStart == null) return;

                while (thrDownStart.ThreadState == ThreadState.Running)
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
