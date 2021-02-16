using imgLoader_WPF.LoaderListCtrl;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace imgLoader_WPF
{
    internal class Processor
    {
        private readonly Dictionary<string, string> _failed = new();

        private Task[] _tasks;

        private bool _stop;
        private readonly LoaderItem _item;

        internal string Route { get; }
        internal string Url { get; }
        internal string Artist { get; }
        internal string Title { get; }
        internal string[] Info { get; }
        internal Dictionary<string, string> ImgUrl { get; }
        internal ISite Site { get; }
        internal bool IsValidated { get; }

        public Processor(string url, LoaderItem item)
        {
            try
            {
                if (string.IsNullOrEmpty(url)) throw new NullReferenceException("url was empty");

                item.Dispatcher.Invoke(() => item.ProgPanel.Visibility = Visibility.Visible);

                Url = url;

                Site = Load(url);
                ImgUrl = Site.GetImgUrls();

                Artist = GetArtist(Site);
                Title = GetTitle(Site.GetTitle());
                Route = Getpath(Artist, Title);
                Info = Site.ReturnInfo();

                item.Dispatcher.Invoke(() =>
                {
                    //item.ImgCount = ImgUrl.Count.ToString();

                    //item.Author = Artist;
                    //item.Title = Title;
                    //item.Route = Route;
                    //item.SiteName = Site.GetType().Name;
                    //item.Number = Core.GetNumber(url);

                    item.TagPanel.Visibility = Visibility.Hidden;
                    item.Tags = Info[4].Split("tags:")[1].Split('\n')[0].Split(';');
                });

                if (!Site.IsValidated()) return;

                _item = item;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to Initialize: Processor: {ex.StackTrace}");
            }

            IsValidated = Site.IsValidated();
        }

        internal void Load()
        {
            var temp = CreateInfo(Url);

            if (temp != Error.End)
            {
                if (temp == Error.Cancel) return;

                throw new Exception("Failed to Load: Processor.Load()");
            }

            AllocTask(Route, ImgUrl);
            Stopping();
        }

        /// <summary>
        ///  Can be null
        /// </summary>
        private static ISite Load(string url)
        {
            try
            {
                var site = Core.LoadSite(url);

                return !site.IsValidated()
                            ? null
                            : site;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///  Can be null
        /// </summary>
        private static string GetArtist(ISite site)
        {
            var sb = new StringBuilder();
            string temp;

            if (site.GetArtist() == "|") return null;
            if (site.GetArtist().Split('|')[0].Length != 0)
            {
                foreach (var s in site.GetArtist().Split('|')[0].Split(';'))
                {
                    if (s.Length == 0) continue;
                    sb.Append(s).Append(", ");
                }
                temp = sb.ToString().Substring(0, sb.Length - 2);

                sb.Clear();

                foreach (var s in site.GetArtist().Split('|')[1].Split(';'))
                {
                    if (s.Length == 0) continue;
                    sb.Append(s).Append(", ");
                }

                temp =
                    sb.Length != 0
                        ? $"{temp} ({sb.ToString().Substring(0, sb.Length - 2)})"
                        : temp;
            }
            else
            {
                foreach (var s in site.GetArtist().Split('|')[1].Split(';'))
                {
                    if (s.Length == 0) continue;
                    sb.Append(s).Append(", ");
                }

                temp = sb.ToString().Substring(0, sb.Length - 2);
            }

            return temp;
        }

        private static string GetTitle(string title)
        {
            var temp = Core.DirFilter(title);

            return Encoding.Unicode.GetByteCount(temp) > 255
                        ? temp.Substring(0, 85)
                        : temp;
        }

        private static string Getpath(string artist, string title)
        {
            var temp =
                artist == "N/A"
                    ? $"{title}"
                    : $"{title} ({artist})";

            temp =
                Encoding.Unicode.GetByteCount(artist + title + Core.Route) + 4 > 4096 || Encoding.Unicode.GetByteCount(artist + title) + 3 > 255
                    ? temp.Replace(title, title.Substring(0, 80) + "...")
                    : temp;

            return $@"{Core.Route}\{temp}";
        }       //returns folder name

        private Error CreateInfo(string url)
        {
            try
            {
                var temp = Core.GetNumber(url);
                var infopath = $"{Route}\\{(temp.Contains('/') ? temp.Split('/')[0] : temp)}.{Core.InfoExt}";

                if (!CheckDupl())
                {
                    Directory.CreateDirectory(Route);
                }
                else
                {
                    MessageBox.Show("Test");
                }

                Core.CreateInfo(infopath, Site);

                return Error.End;
            }
            catch (DirectoryNotFoundException)
            {
                return Error.NoDir;
            }
            catch (FileNotFoundException)
            {
                return Error.NoFile;
            }
        }

        internal bool CheckDupl()
        {
            if (!Directory.Exists(Route)) return false;

            if (ImgUrl.Count.ToString() == File.ReadAllText($"{Route}\\{Site.Number}.ilif").Split('\n')[3])
            {
                return true;
            }

            return false;
        }

        private void AllocTask(string path, Dictionary<string, string> imgList)
        {
            _tasks = new Task[imgList.Count];

            _item.Dispatcher.Invoke(() => _item.ProgBar.Maximum = imgList.Count);

            AllocDown(path, imgList);

            Task.WaitAll(_tasks);

            var success = HandleFail(path);

            Core.Log($"Item:Complete: {path}");
            if (success)
            {
                _item.Dispatcher.Invoke(() => _item.ProgPanel.Visibility = Visibility.Hidden);
                _item.Dispatcher.Invoke(() => _item.TagPanel.Visibility = Visibility.Visible);
            }
            else
            {
                Console.Write("\n Download failed.\n");
            }
        }

        private void AllocDown(string path, Dictionary<string, string> urlList)
        {
            _failed.Clear();

            var i = 0;
            foreach (var (key, value) in urlList) _tasks[i++] = Task.Factory.StartNew(() => ThrDownload(value, path, key));
        }

        private void ThrDownload(string uri, string path, string fileName)
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

                using var fs = new FileStream($"{path}\\{fileName}", FileMode.Create);
                do
                {
                    count = br.Read(buff, 0, buff.Length);
                    fs.Write(buff, 0, count);
                } while (count > 0);

                fileSize = fs.Length;
            }

            if (fileSize == resp.ContentLength)
            {
                _item.Dispatcher.Invoke(() => _item.ProgBar.Value++);
                //_item.Dispatcher.Invoke(() => _item.CurrentCount++);
            }
            else
            {
                _failed.Add(fileName, uri);
            }

            req.Abort();
            resp.Close();
        }

        private bool HandleFail(string path)
        {
            if (_failed.Count == 0) return true;

            var failCopy = new Dictionary<string, string>(_failed);
            AllocDown(path, failCopy);

            Task.WaitAll(_tasks);

            if (_failed.Count != 0) HandleFail(path);

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
                Task.WaitAll(_tasks);
                _stop = false;

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