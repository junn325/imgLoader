using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace imgLoader_CLI
{
    internal class Processor
    {
        private readonly Dictionary<string, string> _failed = new Dictionary<string, string>();
        private List<Task> _tasks = new List<Task>();
        private Thread _thrDownStart;

        private bool _stop;
        private int _done;
        private byte _separator;

        internal void Initialize(string[] url)
        {
            var sw = new Stopwatch();
            sw.Start();

            Console.Write("\n");

            bool temp = false;
            _thrDownStart = new Thread(() => { Process(url); temp = true; });
            _thrDownStart.Start();

            while (!temp) Thread.Sleep(Core.WAIT_TIME * 20);

            Debug.Write(sw.ElapsedMilliseconds + "\n");
        }

        private void Process(string[] url)
        {
            try
            {
                foreach (var link in url)
                {
                    if (string.IsNullOrEmpty(link)) continue;

                    ISite site;
                    Dictionary<string, string> imgList;

                    var sw = new Stopwatch();
                    sw.Start();

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

                        Console.Write("Title: ");
                        title = site.GetTitle();
                        Console.WriteLine($"{title}");

                        Console.Write("Artist name: ");
                        artist = site.GetArtist();
                        Console.WriteLine($"{artist}");

                        title = Core.DirFilter(title);

                        route =
                            artist == "N/A"
                                ? $@"{Core.Route}\{title}"
                                : $@"{Core.Route}\{title} ({artist})";

                        Debug.Write(sw.Elapsed.Ticks);
                    }
                    catch
                    {
                        Console.Write("Error: Connection failed\n");
                        continue;
                    }

                    try
                    {
                        var temp = Core.GetNumber(link);
                        var infoRoute = $"{route}\\{(temp.Contains('/') ? temp.Split('/')[0] : temp)}.{site.GetType().Name}";

                        if (!Directory.Exists(route)) Directory.CreateDirectory(route);
                        else
                        {
                            Console.WriteLine("\nAlready exists. Download again? Y/N");
                            a: string result = Console.ReadLine().ToLower();
                            if (result == "n") continue;
                            if (result != "y") goto a;
                        }
                        Core.CreateInfo(infoRoute, site);
                    }
                    catch (DirectoryNotFoundException)
                    {
                        Console.Write(" Error: No such directory\n");
                        continue;
                    }
                    catch (FileNotFoundException)
                    {
                        Console.Write(" Error: No such file\n");
                        continue;
                    }

                    _done = 0;

                    _tasks = new List<Task>();

                    Console.Write("\n|");
                    AllocDown(route, imgList);

                    while (_done < imgList.Count - _failed.Count) Thread.Sleep(Core.WAIT_TIME * 2);

                    _done = 0;

                    var success = HandleFail(route);
                    while (_done < _failed.Count) Thread.Sleep(Core.WAIT_TIME * 2);
                    foreach (var item in _tasks) while (item.Status != TaskStatus.RanToCompletion) Thread.Sleep(Core.WAIT_TIME);

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

                Stopping();
            }
            catch (ThreadInterruptedException) { }
        }

        private void AllocDown(string route, Dictionary<string, string> urlList)
        {
            _failed.Clear();

            foreach (var item in urlList)
            {
                _tasks.Add(Task.Factory.StartNew(() => ThrDownload(item.Value, route, item.Key)));
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
                    _failed.Add(fileName, uri);
                    return;
                }

                Core.Log($"Fail:{((HttpWebResponse)we.Response).StatusCode}: {uri} {fileName}");
                _failed.Add(fileName, uri);
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
                switch (_separator)
                {
                    case 50:
                        Console.Write(":");
                        break;
                    case 100:
                        Console.Write("::");
                        _separator = 0;
                        break;
                }

                Console.Write("■");
            }
            else
            {
                _failed.Add(fileName, uri);
            }

            _done++;
            _separator++;
            req.Abort();
            resp.Close();
        }

        private bool HandleFail(string route)
        {
            if (_failed.Count == 0) return true;

            var failCopy = new Dictionary<string, string>(_failed);

            AllocDown(route, failCopy);

            int wait = Core.FAIL_RETRY * 60;
            while (_done < failCopy.Count - _failed.Count && wait != 0)
            {
                wait--;
                Thread.Sleep(Core.WAIT_TIME);
            }

            while (_failed.Count != 0)
            {
                Thread.Sleep(Core.WAIT_TIME * 40);
                HandleFail(route);
            }

            _failed.Clear();
            return true;
        }

        private void Stopping()
        {
            new Thread(() =>
            {
                if (_thrDownStart == null) return;

                while (_thrDownStart.ThreadState == System.Threading.ThreadState.Running)
                {
                    _thrDownStart.Interrupt();
                    Thread.Sleep(Core.WAIT_TIME);
                }

                _stop = true;

                foreach (var item in _tasks) while (item.Status != TaskStatus.RanToCompletion) Thread.Sleep(Core.WAIT_TIME);

                _stop = false;

                _failed.Clear();
                _tasks.Clear();
            }).Start();
        }
    }
}
