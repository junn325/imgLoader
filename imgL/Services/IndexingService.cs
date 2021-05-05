using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using imgL.Windows;

namespace imgL.Services
{
    internal class IndexingService
    {
        private const int Interval = 3000;

        private readonly ImgL _sender;
        private readonly FileSystemWatcher _watcher = new();

        private bool _stop;
        private bool _pause;

        public IndexingService(ImgL sender)
        {
            _sender = sender;

            new Thread(() => {
                var t = new Task(DoIndex);
                t.Start();
                t.Wait();

                if (_sender.Index.Count != 0) _sender.Dispatcher.Invoke(() => _sender.BlockIdx.Visibility = System.Windows.Visibility.Hidden);
                _sender.PgSvc.Paginate();
                _sender.ShowItemsCnt();

                {
                    _watcher.Path = Core.Route;
                    _watcher.NotifyFilter = NotifyFilters.FileName
                                            | NotifyFilters.Size
                                            | NotifyFilters.DirectoryName
                                            | NotifyFilters.FileName
                                            | NotifyFilters.LastWrite;

                    _watcher.Filter = $"*.{Core.InfoExt}";

                    _watcher.Created += AddIndex;
                    _watcher.Deleted += RemoveIndex;
                    _watcher.Changed += RefreshIndex;
                    _watcher.Renamed += RenamedIndex;

                    _watcher.IncludeSubdirectories = true;
                }

                var service = new Thread(() =>
                {
                    while (!_stop)
                    {
                        Thread.Sleep(Interval);

                        if (!File.Exists(Core.FilesRoute + Core.RouteFile) || _pause || Properties.Settings.Default.NoIndex)
                        {
                            _watcher.EnableRaisingEvents = false;
                            continue;
                        }

                        if (!Directory.Exists(Core.Route)) continue;
                        _watcher.Path                = Core.Route;
                        _watcher.EnableRaisingEvents = true;
                    }
                });
                service.Name = "IdxSvc";
                service.IsBackground = true;

                service.Start();
            }) { Name = "IdxSvc.ctor" }.Start();
        }

        private void AddIndex(object sender, FileSystemEventArgs e)
        {
            var item = GetItemFromInfo(e.FullPath);

            if (!item.Show) return;
            if (item.ImgCount != Directory.EnumerateFiles(Core.Dir.GetDirFromFile(e.FullPath), "*").Count(i => !i.Contains($".{Core.InfoExt}"))) return;

            _sender.Index.Add(item);
            _sender.List.Add(item);
            _sender.Dispatcher.Invoke(() =>
            {
                using var disableprocessing = _sender.Dispatcher.DisableProcessing();

                _sender.Sorter.SortRefresh((Sorter.SortOption)_sender.CondInd.SortItem.Option);
                _sender.PgSvc.Clear();
                _sender.PgSvc.Paginate(disableprocessing);
            });
        }

        private void RemoveIndex(object sender, FileSystemEventArgs e)
        {
            var item = FindItem(_sender.Index, e.FullPath);
            _sender.Index.Remove(item);
            _sender.List.Remove(item);

            _sender.Dispatcher.Invoke(() =>
            {
                using var disableprocessing = _sender.Dispatcher.DisableProcessing();
                _sender.PgSvc.Remove(item);
            });
        }

        private void RefreshIndex(object sender, FileSystemEventArgs e)
        {
            RefreshToInfo(e.FullPath);
        }

        private void RenamedIndex(object sender, RenamedEventArgs e)
        {
            RefreshToInfo(e.OldFullPath, e.FullPath);
        }

        private void RefreshToInfo(string infoPath)
        {
            var indexIdx = FindItemIndex(_sender.Index, infoPath);
            if (indexIdx == -1) return;

            var item = GetItemFromInfo(infoPath);

            _sender.Index[indexIdx].Author       = item.Author;
            _sender.Index[indexIdx].Date         = item.Date;
            _sender.Index[indexIdx].ImgCount     = item.ImgCount;
            _sender.Index[indexIdx].LastViewDate = item.LastViewDate;
            _sender.Index[indexIdx].SiteName     = item.SiteName;
            _sender.Index[indexIdx].Tags         = item.Tags;
            _sender.Index[indexIdx].Title        = item.Title;
            _sender.Index[indexIdx].View         = item.View;
            _sender.Index[indexIdx].Vote         = item.Vote;
            _sender.Index[indexIdx].Show         = item.Show;
            _sender.Index[indexIdx].IsCntValid   = Directory.GetFiles(Core.Dir.GetDirFromFile(item.Route), "*").Length == item.ImgCount + 1;
            _sender.Index[indexIdx].Route        = item.Route;
            _sender.Index[indexIdx].Number       = item.Number;

            var listIdx = FindItemIndex(_sender.List, infoPath);
            if (listIdx == -1) return;

            if (item.Show)
            {
                _sender.List[listIdx].Author       = item.Author;
                _sender.List[listIdx].Date         = item.Date;
                _sender.List[listIdx].ImgCount     = item.ImgCount;
                _sender.List[listIdx].LastViewDate = item.LastViewDate;
                _sender.List[listIdx].SiteName     = item.SiteName;
                _sender.List[listIdx].Tags         = item.Tags;
                _sender.List[listIdx].Title        = item.Title;
                _sender.List[listIdx].View         = item.View;
                _sender.List[listIdx].Vote         = item.Vote;
                _sender.List[listIdx].IsCntValid   = Directory.GetFiles(Core.Dir.GetDirFromFile(item.Route), "*").Length == item.ImgCount + 1;
                _sender.List[listIdx].Route        = item.Route;
                _sender.List[listIdx].Number       = item.Number;
            }
            else
            {
                _sender.List.RemoveAt(listIdx);
            }

            var itemIdx = FindItemIndex(_sender.ShowItems, infoPath);
            if (itemIdx != -1)
            {
                while (_sender.ShowItems[itemIdx].RefreshInfo == null)
                {
                    Thread.Sleep(50);
                }
                _sender.ShowItems[itemIdx].RefreshInfo();
                return;
            }
        }

        private void RefreshToInfo(string beforeChanged, string afterChanged)
        {
            var indexIdx = FindItemIndex(_sender.Index, beforeChanged);
            if (indexIdx == -1) return;

            var item = GetItemFromInfo(afterChanged);

            _sender.Index[indexIdx].Author       = item.Author;
            _sender.Index[indexIdx].Date         = item.Date;
            _sender.Index[indexIdx].ImgCount     = item.ImgCount;
            _sender.Index[indexIdx].LastViewDate = item.LastViewDate;
            _sender.Index[indexIdx].SiteName     = item.SiteName;
            _sender.Index[indexIdx].Tags         = item.Tags;
            _sender.Index[indexIdx].Title        = item.Title;
            _sender.Index[indexIdx].View         = item.View;
            _sender.Index[indexIdx].Vote         = item.Vote;
            _sender.Index[indexIdx].Show         = item.Show;
            _sender.Index[indexIdx].IsCntValid   = Directory.GetFiles(Core.Dir.GetDirFromFile(item.Route), "*").Length == item.ImgCount + 1;
            _sender.Index[indexIdx].Route        = item.Route;
            _sender.Index[indexIdx].Number       = item.Number;

            var listIdx = FindItemIndex(_sender.List, afterChanged);
            if (listIdx == -1) return;

            if (item.Show)
            {
                _sender.List[listIdx].Author       = item.Author;
                _sender.List[listIdx].Date         = item.Date;
                _sender.List[listIdx].ImgCount     = item.ImgCount;
                _sender.List[listIdx].LastViewDate = item.LastViewDate;
                _sender.List[listIdx].SiteName     = item.SiteName;
                _sender.List[listIdx].Tags         = item.Tags;
                _sender.List[listIdx].Title        = item.Title;
                _sender.List[listIdx].View         = item.View;
                _sender.List[listIdx].Vote         = item.Vote;
                _sender.List[listIdx].IsCntValid   = Directory.GetFiles(Core.Dir.GetDirFromFile(item.Route), "*").Length == item.ImgCount + 1;
                _sender.List[listIdx].Route        = item.Route;
                _sender.List[listIdx].Number       = item.Number;
            }
            else
            {
                _sender.List.RemoveAt(listIdx);
            }

            var itemIdx = FindItemIndex(_sender.ShowItems, afterChanged);
            if (itemIdx != -1)
            {
                while (_sender.ShowItems[itemIdx].RefreshInfo == null)
                {
                    Thread.Sleep(50);
                }
                _sender.ShowItems[itemIdx].RefreshInfo();
                return;
            }
        }

        private static IndexItem GetItemFromInfo(string fileName)
        {
            using var sr = new StreamReader(Core.Dir.DelayStream(fileName, FileMode.Open, FileAccess.Read), Encoding.UTF8);
            var infos = sr.ReadToEnd().Replace("\r\n", "\n");
            sr.Close();
            if (string.IsNullOrWhiteSpace(infos)) return null;

            var info = Core.InitArray(Core.InfoCount, infos.Split('\n'));

            var item = new IndexItem();
            try
            {
                item.Route  = fileName;
                item.Number = Core.Dir.EHNumFromInternal(fileName.Split('\\')[^1].Split('.')[0]);

                item.SiteName = info[0];
                item.Title    = info[1];
                item.Author   = info[2];
                item.ImgCount = int.TryParse(info[3], out var parse) ? parse : -1;
                item.Tags     = info[4].Split("tags:")[1].Split('\n')[0].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                if (info[5].Contains(' ') && info[5].Contains('/'))
                {
                    var date = info[5].Split(' ');
                    int.TryParse(date[0].Split('/')[0], out var month);
                    int.TryParse(date[0].Split('/')[1], out var day);
                    int.TryParse(date[0].Split('/')[2], out var year);
                    int.TryParse(date[1].Split(':')[0], out var hour);
                    int.TryParse(date[1].Split(':')[1], out var minute);
                    int.TryParse(date[1].Split(':')[2], out var second);
                    item.Date = new DateTime(year, month, day, hour, minute, second);
                }

                item.Vote = info[6] != null ? int.Parse(info[6]) : 0;
                item.Show = info[7] == null || info[7] == "1";
                item.View = info[8] != null ? int.Parse(info[8]) : 0;

                if (info.Length > 9 && info[9]?.Contains(' ') == true && info[9].Contains('/'))
                {
                    var lastDate = info[9].Split(' ');
                    int.TryParse(lastDate[0].Split('/')[0], out var month);
                    int.TryParse(lastDate[0].Split('/')[1], out var day);
                    int.TryParse(lastDate[0].Split('/')[2], out var year);
                    int.TryParse(lastDate[1].Split(':')[0], out var hour);
                    int.TryParse(lastDate[1].Split(':')[1], out var minute);
                    int.TryParse(lastDate[1].Split(':')[2], out var second);
                    item.LastViewDate = new DateTime(year, month, day, hour, minute, second);
                }
            }
            catch
            {
                item.IsError  = true;
                item.Title    = "";
                item.Author   = "Info read error. Recovery is required.";
                item.Vote     = -1;
                item.View     = -1;
                item.ImgCount = -1;
            }

            return item;
        }

        internal void DoIndex()
        {
            if (!Directory.Exists(Core.Route)) return;

            foreach (var infoRoute in Core.Dir.GetFiles(Core.Route, Core.InfoExt).Where(File.Exists))
            {
                var infos = File.ReadAllText(infoRoute, Encoding.UTF8).Replace("\r\n", "\n");
                if (string.IsNullOrWhiteSpace(infos)) continue;

                var info = Core.InitArray(Core.InfoCount, infos.Split('\n'));
                var item = new IndexItem();

                try
                {
                    item.Route  = infoRoute;
                    item.Number = Core.Dir.EHNumFromInternal(infoRoute.Split('\\')[^1].Split('.')[0]);

                    item.SiteName   = info[0];
                    item.Title      = info[1];
                    item.Author     = info[2];
                    item.ImgCount   = int.TryParse(info[3], out var parse) ? parse : -1;
                    item.IsCntValid = Directory.GetFiles(Core.Dir.GetDirFromFile(infoRoute), "*").Length == item.ImgCount + 1;
                    item.Tags       = info[4].Split("tags:")[1].Split('\n')[0].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                    if (info[5].Contains(' ') && info[5].Contains('/'))
                    {
                        var date = info[5].Split(' ');
                        int.TryParse(date[0].Split('/')[0], out var month);
                        int.TryParse(date[0].Split('/')[1], out var day);
                        int.TryParse(date[0].Split('/')[2], out var year);
                        int.TryParse(date[1].Split(':')[0], out var hour);
                        int.TryParse(date[1].Split(':')[1], out var minute);
                        int.TryParse(date[1].Split(':')[2], out var second);
                        item.Date = new DateTime(year, month, day, hour, minute, second);
                    }

                    item.Vote = string.IsNullOrWhiteSpace(info[6]) ? 0 : int.Parse(info[6]);
                    item.Show = string.IsNullOrWhiteSpace(info[7]) || info[7] == "1";
                    item.View = string.IsNullOrWhiteSpace(info[8]) ? 0 : int.Parse(info[8]);

                    if (info.Length > 9 && info[9]?.Contains(' ') == true && info[9].Contains('/'))
                    {
                        var lastDate = info[9].Split(' ');
                        int.TryParse(lastDate[0].Split('/')[0], out var month);
                        int.TryParse(lastDate[0].Split('/')[1], out var day);
                        int.TryParse(lastDate[0].Split('/')[2], out var year);
                        int.TryParse(lastDate[1].Split(':')[0], out var hour);
                        int.TryParse(lastDate[1].Split(':')[1], out var minute);
                        int.TryParse(lastDate[1].Split(':')[2], out var second);
                        item.LastViewDate = new DateTime(year, month, day, hour, minute, second);
                    }
                }
                catch
                {
                    item.IsError  = true;
                    item.Title    = "";
                    item.Author   = "Info read error. Recovery is required.";
                    item.Vote     = -1;
                    item.View     = -1;
                    item.ImgCount = -1;
                }

                _sender.Index.Add(item);

                if (info[7] == null || info[7] == "0")
                    continue;

                _sender.List.Add(item);
            }
        }

        internal void Refresh(string[] infoFiles, string[] newFiles)
        {
            if (infoFiles == null || newFiles == null) return;

            foreach (var item in new List<IndexItem>(_sender.Index))
            {
                if (item.IsDownloading) continue;
                if (infoFiles.Contains(item.Route)) continue;
                if (item.ImgCount == -1) continue;

                _sender.Index.Remove(item);
                _sender.List.Remove(item);
                _sender.Dispatcher.Invoke(() =>
                {
                    _sender.Scroll.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Visible;
                    _sender.PgSvc.Remove(item);
                    _sender.ShowItemsCnt();
                });
            }

            if (newFiles.Length != 0)
            {
                _sender.Dispatcher.Invoke(() =>
                {
                    _sender.BlockIdx.Visibility = System.Windows.Visibility.Hidden;
                    var disableProcessing = _sender.Dispatcher.DisableProcessing();
                    var temp = (Sorter.SortOption)_sender.CondInd.SortItem.Option;
                    _sender.Sorter.SortRefresh(temp, disableProcessing);
                });
            }
        }

        private static IndexItem FindItem(List<IndexItem> collection, string infoPath)
        {
            var temp = collection.Where(i => i.Route == infoPath).ToArray();
            return temp.Length != 0 ? temp[0] : null;
        }

        private static int FindItemIndex(IEnumerable<IndexItem> collection, string infoPath)
        {
            int i = 0;
            foreach (var item in collection)
            {
                if (item.Route == infoPath) return i;
                i++;
            }

            return -1;
        }

        internal void RefreshAll()
        {
            foreach (var item in _sender.Index)
            {
                item.RefreshInfo?.Invoke();
            }
        }

        internal void Pause()
        {
            _pause = true;
        }

        internal void Resume()
        {
            _pause = false;
        }
    }
}