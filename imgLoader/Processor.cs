using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace imgLoader
{
    internal class Processor
    {
        private readonly Dictionary<string, string> _failed = new Dictionary<string, string>();

        private Task[] _tasks;
        private ISite _site;

        private Dictionary<string, string> _imgList;

        private bool _stop;
        private byte _separator;
       
        private string _url, _artist, _title, _route;


        internal string Initialize(string url)
        {
            string error;
            if (string.IsNullOrEmpty(url)) return "urlNull";
            _url = url;

            error = Load();
            if (error != null) return "fail";
            GetArtist();
            GetTitle();
            GetRoute();
            error = CreateInfo();
            if (error != null) return "fail";

            AllocTask();
            //Process(_url);

            Stopping();
            return null;
        }

        private string Load()
        {
            try
            {
                _site = Core.LoadSite(_url);

                if (_site?.IsValidated() != true)
                {
                    return "FailLoad";
                }

                _imgList = _site.GetImgUrls();
                _title = _site.GetTitle();
                _artist = "N/A";

                return null;
            }
            catch
            {
                return "ConnectionFail";
            }
        }

        private void GetArtist()
        {
            var sb = new StringBuilder();

            if (_site.GetArtist() != "|")
            {
                if (_site.GetArtist().Split('|')[0].Length != 0)
                {
                    foreach (var s in _site.GetArtist().Split('|')[0].Split(';'))
                    {
                        if (s.Length == 0) continue;
                        sb.Append(s).Append(", ");
                    }
                    _artist = sb.ToString().Substring(0, sb.Length - 2);

                    sb.Clear();

                    foreach (var s in _site.GetArtist().Split('|')[1].Split(';'))
                    {
                        if (s.Length == 0) continue;
                        sb.Append(s).Append(", ");
                    }

                    _artist =
                        sb.Length != 0
                            ? $"{_artist} ({sb.ToString().Substring(0, sb.Length - 2)})"
                            : _artist;
                }
                else
                {
                    foreach (var s in _site.GetArtist().Split('|')[1].Split(';'))
                    {
                        if (s.Length == 0) continue;
                        sb.Append(s).Append(", ");
                    }

                    _artist = sb.ToString().Substring(0, sb.Length - 2);
                }
            }
        }

        private void GetTitle()
        {
            _title = Core.DirFilter(_title);
        }

        private void GetRoute()
        {
            _route =
                _artist == "N/A"
                    ? $@"{Core.Route}\{_title}"
                    : $@"{Core.Route}\{_title} ({_artist})";
        }

        private string CreateInfo()
        {
            try
            {
                var temp = Core.GetNumber(_url);
                var infoRoute = $"{_route}\\{(temp.Contains('/') ? temp.Split('/')[0] : temp)}.{_site.GetType().Name}";

                if (!Directory.Exists(_route))
                {
                    Directory.CreateDirectory(_route);
                }
                else
                {
                    Console.WriteLine("\nAlready exists. Download again? Y/N");
                    a: var result = Console.ReadLine()?.ToLower();
                    if (result == "n") return null;
                    if (result != "y") goto a;
                }
                Core.CreateInfo(infoRoute, _site);

                return null;
            }
            catch (DirectoryNotFoundException)
            {
                return "NoDir";
            }
            catch (FileNotFoundException)
            {
                return "NoFile";
            }
        }

        private void AllocTask()
        {
            _tasks = new Task[_imgList.Count];

            Console.Write("\n[");
            AllocDown(_route, _imgList);

            foreach (var task in _tasks) task.Wait();

            var success = HandleFail(_route);

            Core.Log($"Item:Complete: {_url}");
            if (success)
            {
                Console.Write("]\n");
                Console.WriteLine("\n Download complete.\n");
            }
            else
            {
                Console.Write("\n Download failed.\n");
            }
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

            if (req == null) return;
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

            if (resp == null)
            {
                _failed.Add(fileName, uri);
                return;
            }

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
                _separator++;
                Console.Write("■");

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
            }
            else
            {
                _failed.Add(fileName, uri);
            }

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