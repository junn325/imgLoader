using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using imgL_Sites;

namespace imgL
{
    internal class Processor
    {
        internal bool[] IsImgLoading;
        internal bool Pause, IsStop, IsValidated;

        internal int ProgVal;
        internal int ProgMax;

        private readonly Dictionary<string, string> _failed = new();
        private readonly IndexItem _item;
        private Task[] _tasks;

        private readonly string _url;

        private string _route, _artist, _title, _number, _referer;
        private string[] _info;

        private Dictionary<string, string> _imgUrl;
        private ISite _site;

        public Processor(string url, IndexItem item)
        {
            if (string.IsNullOrEmpty(url)) return;

            _item = item;
            _url  = url;

            item.IsDownloading = true;
        }

        internal bool LoadInfo()
        {
            IsStop              = false;
            _item.IsDownloading = true;

            _site = Load(_url);
            if (_site?.IsValidated() != true) return false;

            _imgUrl      = _site.GetImgUrls();
            _referer     = _site.Referer;
            IsImgLoading = new bool[_imgUrl.Count];
            _number      = Core.GetNumber(_url);
            _artist      = Core.GetArtistFromRaw(_site.GetArtist());
            _title       = GetTitle(_site.GetTitle());
            _route       = GetInfoPath(_artist, _title);
            _info        = _site.GetInfo();

            if (_number == null || _artist == null || _title == null || _route == null || _info == null)
            {
                IsValidated = false;
                return false;
            }

            _item.ImgCount = _imgUrl.Count;
            _item.Author   = _site.GetArtist();
            _item.Title    = _title;
            _item.Route    = _route;
            _item.SiteName = _site.GetType().Name;
            _item.Number   = _number;
            _item.Date     = DateTime.Now;
            _item.Tags     = _info[4].Split("tags:")[1].Split('\n')[0].Split(';', StringSplitOptions.RemoveEmptyEntries);
            _item.Vote     = 0;
            _item.View     = 0;

            IsValidated = _site.IsValidated();

            return true;
        }

        internal bool StartDownload()
        {
            IsStop = false;

            if (!CreateInfo())
            {
                _item.IsDownloading = false;
                return false;
            }

            AllocTask(_route, _imgUrl);
            _item.IsDownloading = false;

            if (IsStop)
            {
                DoStop();
                return false;
            }

            DoStop();

            return true;
        }

        private static ISite Load(string url)
        {
            var site = Core.LoadSite(url);

            return site?.IsValidated() != true
                       ? null
                       : site;
        }

        private static string GetTitle(string title)
        {
            if (title == null) return null;

            var temp = Core.Dir.DirFilter(title);

            return Encoding.Unicode.GetByteCount(temp) > 255
                       ? temp[..85]
                       : temp;
        }

        private string GetInfoPath(string artist, string title)
        {
            if (artist == null || title == null) return null;

            var temp =
                artist.Length == 0
                    ? $"{title}"
                    : $"{title} ({artist})";

            temp =
                Encoding.Unicode.GetByteCount(temp + Core.Route) + 4 > 4096 || Encoding.Unicode.GetByteCount(temp) + 3 > 255
                    ? temp[..79] + "...)"
                    : temp;

            return $@"{Core.Route}\{temp}\{Core.Dir.EHNumFromRaw(_number)}.{Core.InfoExt}";
        }

        private bool CreateInfo()
        {
            if (!CheckDupl())
            {
                Directory.CreateDirectory(Core.Dir.GetDirFromFile(_route));
            }
            else
            {
                return false;
            }

            return Core.CreateInfo(_route, _site);
        }

        internal bool CheckDupl()
        {
            if (!Directory.Exists(Core.Dir.GetDirFromFile(_route))) return false;
            if (!File.Exists(_route)) return false;

            return _imgUrl.Count.ToString() == File.ReadAllText(_route).Split('\n')[3];
        }

        private void AllocTask(string path, Dictionary<string, string> imgList)
        {
            while (_item.ProgBarMax == null)
            {
                Task.Delay(200).Wait();
            }

            _item.ProgPanelVis.Invoke(Visibility.Visible);
            ProgMax = imgList.Count;
            _item.ProgBarMax.Invoke(imgList.Count);

            _tasks = new Task[imgList.Count];

            AllocDown(path, imgList);

            Task.WaitAll(_tasks);

            var success = HandleFail(path, 3);

            Core.Log($"Item:Complete: {path}");
            if (success)
            {
                _item.ProgPanelVis.Invoke(Visibility.Hidden);
            }
        }

        private void AllocDown(string path, Dictionary<string, string> urlList)
        {
            path = Core.Dir.GetDirFromFile(path);
            _failed.Clear();

            var i = 0;
            foreach (var (key, value) in urlList)
            {
                if (IsStop) break;

                while (Pause)
                {
                    Task.Delay(500).Wait();
                }

                var i1 = i;
                _tasks[i++] = Task.Factory.StartNew(() =>
                {
                    IsImgLoading[i1] = true;
                    ThrDownload(value, path, key);
                    IsImgLoading[i1] = false;
                });
            }
        }

        private void ThrDownload(string uri, string path, string fileName)
        {
            if (IsStop) return;

            while (Pause)
            {
                if (IsStop) return;
                Task.Delay(500).Wait();
            }

            HttpWebResponse resp;
            if (WebRequest.Create(uri) is not HttpWebRequest req) return;

            req.Referer   = _referer ?? $"https://{new Uri(uri).Host}";
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

            if (IsStop) return;

            using (var br = resp.GetResponseStream())
            {
                int count;
                var buff = new byte[1024];

                using var fs = new FileStream($"{path}\\{fileName}", FileMode.Create);

                if (IsStop) return;

                do
                {
                    if (IsStop) return;

                    count = br.Read(buff, 0, buff.Length);
                    fs.Write(buff, 0, count);
                } while (count > 0);

                fileSize = fs.Length;
            }

            if (fileSize == resp.ContentLength)
            {
                ProgVal++;
                _item.ProgBarVal.Invoke(ProgVal);
            }
            else
            {
                _failed.Add(fileName, uri);
            }

            req.Abort();
            resp.Close();
        }

        private bool HandleFail(string path, int thres)
        {
            if (_failed.Count == 0) return true;

            var failCopy = new Dictionary<string, string>(_failed);
            AllocDown(path, failCopy);

            if (_tasks == null) return true;

            Task.WaitAll(_tasks);

            if (thres > 0 && _failed.Count != 0) HandleFail(path, --thres);

            _failed.Clear();

            return true;
        }

        internal void DoStop()
        {
            IsStop = true;

            new Thread(() =>
            {
                _failed.Clear();

                if (_tasks == null) return;
                foreach (var task in _tasks)
                {
                    task?.Wait();
                }

                _tasks = null;
            }).Start();
        }
    }
}