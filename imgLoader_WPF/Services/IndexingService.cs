using imgLoader_WPF.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace imgLoader_WPF.Services
{
    //컬렉션은 IndexItem을 담고있음                        
    //매 /interval/ 밀리초마다 인덱싱

    internal class IndexingService
    {
        private const int Interval = 3000;

        //private readonly Thread _service;
        private bool _stop;
        private bool _pause;

        //public readonly List<IndexItem> Index;

        private readonly ImgLoader _sender;

        public IndexingService(ImgLoader sender)
        {
            _sender = sender;

            var service = new Thread(() =>
            {
                while (!_stop)
                {
                    //Debug.WriteLine("IdxSvc");

                    Thread.Sleep(Interval);
                    if (_pause) continue;

                    if (Properties.Settings.Default.NoIndex) continue;

                    Indexing();
                }
            });
            service.Name = "IdxSvc";
            service.IsBackground = true;

            Indexing();
        }

        private void Indexing()
        {
            var (infoFiles, newFiles) = DoIndex();
            Refresh(infoFiles, newFiles);
        }

        internal (string[], string[]) DoIndex()
        {
            if (!Directory.Exists(Core.Route)) return (null, null);

            var infoFiles = Directory.GetFiles(Core.Route, $"*.{Core.InfoExt}", SearchOption.AllDirectories);
            var newFiles = infoFiles.Where(item => _sender.Index.All(i => i.Route != item)).ToArray();
            foreach (var infoRoute in newFiles)
            {
                if (!File.Exists(infoRoute)) continue;

                using var sr = new StreamReader(Core.Dir.DelayStream(infoRoute, FileMode.Open, FileAccess.Read), Encoding.UTF8);
                var infos = sr.ReadToEnd().Replace("\r\n", "\n");
                sr.Close();
                if (string.IsNullOrWhiteSpace(infos)) continue;

                var info = Core.InitArray(Core.InfoCount, infos.Split('\n'));

                var item = new IndexItem();
                try
                {
                    item.Route = infoRoute;
                    item.Number = Core.Dir.EHNumFromInternal(infoRoute.Split('\\')[^1].Split('.')[0]);

                    item.SiteName = info[0];
                    item.Title = info[1];
                    item.Author = info[2];
                    item.ImgCount = int.TryParse(info[3], out var parse) ? parse : -1;
                    item.Tags = info[4].Split("tags:")[1].Split('\n')[0].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

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

                    if (info.Length > 9 && info[9] != null && info[9].Contains(' ') && info[9].Contains('/'))
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
                    item.IsError = true;
                    item.Title = "";
                    item.Author = "Info read error. Recovery is required.";
                    item.Vote = -1;
                    item.View = -1;
                    item.ImgCount = -1;
                }

                _sender.Index.Add(item);
                _sender.List.Add(item);
            }

            return (infoFiles, newFiles);
        }
        internal void Refresh(string[] infoFiles, string[] newFiles)
        {
            foreach (var item in new List<IndexItem>(_sender.Index))
            {
                if (item.IsDownloading) continue;
                if (infoFiles.Contains(item.Route)) continue;
                if (item.ImgCount == -1) continue;                //새로 다운로드 중인 항목 무시

                Debug.WriteLine($"IdxSvc: remove {item.Number}");

                _sender.Index.Remove(item);
                _sender.List.Remove(item);
                _sender.Dispatcher.Invoke(() =>
                {
                    _sender.Scroll.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Visible;
                    _sender.PgSvc.Remove(item);
                    _sender.ShowItemCount();
                });
            }

            if (newFiles.Length != 0)
            {
                _sender.Dispatcher.Invoke(() =>
                {
                    _sender.BlockIdx.Visibility = System.Windows.Visibility.Hidden;
                    var disableProcessing = _sender.Dispatcher.DisableProcessing();
                    var temp = (Sorter.SortOption)_sender.CondInd.IndicatorList.Find(i => i.Condition == ConditionIndicator.Condition.Sort).Option;
                    _sender.Sorter.SortRefresh(temp, disableProcessing);
                });

            }
        }

        internal void RefreshAll()
        {
            foreach (var item in _sender.Index)
            {
                item.RefreshInfo?.Invoke();
            }
        }
        //internal void Start()
        //{
        //    _stop = false;
        //    _service.Start();
        //}

        internal void Pause()
        {
            _pause = true;
        }

        internal void Resume()
        {
            _pause = false;
        }

        //internal void Stop()
        //{
        //    _stop = true;
        //    while (_service.IsAlive) Thread.Sleep(100);
        //}
    }
}
