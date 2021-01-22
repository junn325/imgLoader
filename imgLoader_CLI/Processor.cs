using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace imgLoader_CLI
{
    internal class Processor
    {
        private readonly Dictionary<string, string> _failed = new Dictionary<string, string>();

        private Task[] _tasks;

        private bool _stop;
        private byte _separator;

        internal void Initialize(string[] url)
        {
            Console.Write('\n');
            Process(url);
        }

        private void Process(string[] url)
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

                    if (site?.IsValidated() != true)
                    {
                        Console.Write("Error: Failed to load\n"); 
                        continue;
                    }

                    Console.WriteLine($"{site.GetType().Name}");

                    Console.Write("Count:\t");
                    imgList = site.GetImgUrls();
                    Console.WriteLine($"{imgList.Count} images");

                    Console.Write("Title:\t");
                    title = site.GetTitle();
                    Console.WriteLine($"{title}");

                    Console.Write("Artist:\t");

                    artist = "N/A";
                    var sb = new StringBuilder();

                    if (site.GetArtist() != "|")
                    {
                        if (site.GetArtist().Split('|')[0] != string.Empty)
                        {
                            foreach (var s in site.GetArtist().Split('|')[0].Split(';'))
                            {
                                if (s?.Length == 0) continue;
                                sb.Append(s).Append(", ");
                            }
                            artist = sb.ToString().Substring(0, sb.Length - 2);

                            sb.Clear();

                            foreach (var s in site.GetArtist().Split('|')[1].Split(';'))
                            {
                                if (s.Length == 0) continue;
                                sb.Append(s).Append(", ");
                            }

                            artist =
                                sb.Length != 0
                                    ? $"{artist} ({sb.ToString().Substring(0, sb.Length - 2)})"
                                    : artist;
                        }
                        else
                        {
                            foreach (var s in site.GetArtist().Split('|')[1].Split(';'))
                            {
                                if (s.Length == 0) continue;
                                sb.Append(s).Append(", ");
                            }

                            artist = sb.ToString().Substring(0, sb.Length - 2);
                        }
                    }

                    Console.WriteLine($"{artist}");

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
                    var temp = Core.GetNumber(link);
                    var infoRoute = $"{route}\\{(temp.Contains('/') ? temp.Split('/')[0] : temp)}.{site.GetType().Name}";

                    if (!Directory.Exists(route)) Directory.CreateDirectory(route);
                    else
                    {
                        Console.WriteLine("\nAlready exists. Download again? Y/N");
                        a: var result = Console.ReadLine()?.ToLower();
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


                _tasks = new Task[imgList.Count];

                Console.Write("\n[");
                AllocDown(route, imgList);

                foreach (var task in _tasks) task.Wait();

                var success = HandleFail(route);

                Core.Log($"Item:Complete: {link}");
                if (success)
                {
                    Console.Write("]\n");
                    Console.WriteLine("\n Download complete.\n");
                }
                else
                {
                    Console.Write("\n Download failed.\n");
                }
                Debug.Write(sw.Elapsed.Ticks);
            }
            Stopping();
        }

        private void AllocDown(string route, Dictionary<string, string> urlList)
        {
            _failed.Clear();

            var i = 0;
            foreach (var item in urlList) _tasks[i++] = Task.Factory.StartNew(() => ThrDownload(item.Value, route, item.Key));
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

                Core.Log($"Fail:{(we.Response as HttpWebResponse).StatusCode}: {uri} {fileName}");
                _failed.Add(fileName, uri);
                return;
            }

            long fileSize;

            using (var br = resp.GetResponseStream())
            {
                int count;
                var buff = new byte[1024];

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
                        Console.Write("|");
                        _separator = 0;
                        break;
                }

                Console.Write("■");
            }
            else
            {
                _failed.Add(fileName, uri);
            }

            _separator++;
            req.Abort();
            resp.Close();
        }

        private bool HandleFail(string route)
        {
            if (_failed.Count == 0) return true;

            var failCopy = new Dictionary<string, string>(_failed);
            AllocDown(route, failCopy);

            foreach (var task in _tasks) task.Wait();

            if (_failed.Count != 0) HandleFail(route);

            _failed.Clear();

            return true;
        }

        private void Stopping()
        {
            new Thread(() =>
            {
                _failed.Clear();

                if (_tasks == null) return;

                _stop = true;
                foreach (var task in _tasks) task.Wait();
                _stop = false;

                _tasks = null;
            }).Start();
        }

    }
}
