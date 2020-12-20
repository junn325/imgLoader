using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace imgLoader_CLI
{
    internal class Processor
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

            while (!temp) Thread.Sleep(Core.WAIT_TIME * 20);
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
                        Console.Write("Load info: ");
                        site = Core.LoadSite(link);

                        if (site?.IsValidated() != true) { Console.Write("Error: Failed to load\n"); continue; }
                        Console.WriteLine($"{site.GetType().Name}:");

                        Console.Write("Count: ");
                        imgList = site.GetImgUrls();
                        Console.WriteLine($"{imgList.Count} images");

                        Console.Write("Artist name: ");
                        artist = site.GetArtist();
                        Console.WriteLine($"{artist}");

                        Console.Write("Title: ");
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
                        Console.Write("Error: Connection failed\n");
                        continue;
                    }

                    try
                    {
                        var infoRoute = $"{route}\\{Core.GetNumber(link)}.{site.GetType().Name}";

                        if (!Directory.Exists(route)) Directory.CreateDirectory(route);
                        else
                        {
                            if (File.Exists(infoRoute))
                            {
                                var fileTmp = File.ReadAllText(infoRoute);
                                if (fileTmp.Length != 0)
                                {
                                    var temp = int.Parse(fileTmp.Split('\n')[2]);
                                    if (temp == imgList.Count)
                                    {
                                        Console.WriteLine("\nAlready exists. Download again? Y/N");
                                        a: string result = Console.ReadLine().ToLower();
                                        if (result == "n") continue;
                                        if (result != "y") goto a;
                                    }
                                }
                            }
                        }
                        Core.CreateInfo(infoRoute);
                    }
                    catch (DirectoryNotFoundException)
                    {
                        Console.Write(" Error: No such directory");
                    }
                    catch (FileNotFoundException)
                    {
                        Console.Write(" Error: No such file");
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
                        Console.WriteLine("\n Download complete.\n");
                    }
                    else
                    {
                        Console.Write("\n Download failed.\n");
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
                    Core.Log($"Fail:NoResponse: {uri} {fileName}");
                    failed.Add(fileName, uri);
                    return;
                }

                Core.Log($"Fail:{((HttpWebResponse)we.Response).StatusCode}: {uri} {fileName}");
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
