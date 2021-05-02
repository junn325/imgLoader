using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
//using imgL_Sites;
using imgL_Sites;

using imgLoader_WPF.Windows;

namespace imgLoader_WPF
{
    internal class Processor
    {
        public bool[] IsImgLoading;
        public bool Pause, IsStop, IsValidated;

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
            //try
            //{
            if (string.IsNullOrEmpty(url)) return;
            _item = item;
            _url  = url;

            item.IsDownloading = true;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception($"Failed to Initialize: Processor: {ex.StackTrace}");
            //}
        }

        internal bool LoadInfo()
        {
            IsStop = false;
            _item.IsDownloading = true;

            _site = Load(_url);
            if (_site == null)
            {
                if (!IsStop) MessageBox.Show("Address Unreachable.");
                return false;
            }

            if (!_site.IsValidated()) return false;

            _imgUrl  = _site.GetImgUrls();
            _referer = _site.Referer;

            IsImgLoading = new bool[_imgUrl.Count];

            _number = Core.GetNum(_url);
            _artist = Core.GetArtistFromRaw(_site.GetArtist());
            _title  = GetTitle(_site.GetTitle());
            _route  = Getpath(_artist, _title);
            _info   = _site.GetInfo();

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

            //}
            //catch (Exception ex)
            //{
            //    throw new Exception($"Failed to Initialize: Processor: {ex.StackTrace}");
            //}

            IsValidated = _site.IsValidated();

            return true;
        }

        internal bool StartDownload()
        {
            IsStop = false;

            var temp = CreateInfo();

            if (temp != Error.End)
            {
                if (temp == Error.Cancel) return false;

                _item.IsDownloading = false;

                return false;
            }

            AllocTask(_route, _imgUrl);
            _item.IsDownloading = false;

            DoStop();

            return !IsStop;
        }

        /// <summary>
        ///     Can be null
        /// </summary>
        private static ISite Load(string url)
        {
            var site = Core.LoadSite(url);

            return site?.IsValidated() != true
                       ? null
                       : site;
        }

        private static string GetTitle(string title)
        {
            var temp = Core.Dir.DirFilter(title);

            return Encoding.Unicode.GetByteCount(temp) > 255
                       ? temp[..85]
                       : temp;
        }

        private string Getpath(string artist, string title)
        {
            var temp =
                artist?.Length == 0
                    ? $"{title}"
                    : $"{title} ({artist})";

            temp =
                Encoding.Unicode.GetByteCount(temp + Core.Route) + 4 > 4096 || Encoding.Unicode.GetByteCount(temp) + 3 > 255
                    ? temp[..79] + "...)"
                    : temp;

            return $@"{Core.Route}\{temp}\{Core.Dir.EHNumFromRaw(_number)}.{Core.InfoExt}";
        } //returns info path

        private Error CreateInfo()
        {
            //try
            //{
            if (!CheckDupl())
            {
                Directory.CreateDirectory(Core.Dir.GetDirFromFile(_route));
            }
            else
            {
                MessageBox.Show("Test");
            }

            Core.CreateInfo(_route, _site);

            return Error.End;
            //}
            //catch (DirectoryNotFoundException)
            //{
            //    return Error.NoDir;
            //}
            //catch (FileNotFoundException)
            //{
            //    return Error.NoFile;
            //}
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
                Debug.WriteLine("Proc: wait");
                Task.Delay(200).Wait();
            }

            _item.ProgPanelVis.Invoke(Visibility.Visible);
            //_item.TagPanelVis.Invoke(Visibility.Hidden);
            ProgMax = imgList.Count;
            _item.ProgBarMax.Invoke(imgList.Count);

            _tasks = new Task[imgList.Count];

            AllocDown(path, imgList);

            Task.WaitAll(_tasks);

            var success = HandleFail(path, 5);  //thres = 5;

            Core.Log($"Item:Complete: {path}");
            if (success)
            {
                _item.ProgPanelVis.Invoke(Visibility.Hidden);
                //_item.TagPanelVis.Invoke(Visibility.Visible);
            }
            else
            {
                Console.Write("\n Download failed.\n");
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
                //_item.Dispatcher.Invoke(() => _item.CurrentCount++);
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
                //_stop = false;

                _tasks = null;
            }).Start();
        }

        private enum Error
        {
            End,
            Cancel,
            NoDir,
            NoFile
        }
    }
}