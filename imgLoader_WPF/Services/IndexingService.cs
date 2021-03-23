using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using imgLoader_WPF.Windows;

namespace imgLoader_WPF.Services
{
    //컬렉션은 IndexItem을 담고있음                        
    //매 /interval/ 밀리초마다 인덱싱

    internal class IndexingService
    {
        private const int Interval = 3000;
        private const int InfoCount = 9;

        private readonly Thread _service;
        private bool _stop;

        public List<IndexItem> Index;

        private readonly ImgLoader _sender;

        public IndexingService(List<IndexItem> index, ImgLoader sender)
        {
            Index = index;
            _sender = sender;
            var route = Core.Route;

            _service = new Thread(() =>
            {
                while (!_stop)
                {
                    //Debug.WriteLine("IdxSvc");

                    Thread.Sleep(Interval);

                    if (Properties.Settings.Default.NoIndex) continue;

                    //var sb = new StringBuilder();
                    if (!string.Equals(route, Core.Route, StringComparison.OrdinalIgnoreCase))
                    {
                        route = Core.Route;

                        DoIndex();
                        foreach (var item in Index)
                        {
                            _sender.List.Add(item);
                        }

                        _sender.PgSvc.Paginate();
                    }
                    else
                    {
                        DoIndex();
                    }

                    //RefreshAll();
                }
            });
            _service.Name = "IdxSvc";

            DoIndex();
        }

        internal void DoIndex()
        {
            if (!Directory.Exists(Core.Route)) return;

            var infoFiles = Directory.GetFiles(Core.Route, $"*.{Core.InfoExt}", SearchOption.AllDirectories);
            foreach (var item in new List<IndexItem>(Index))
            {
                if (item.IsDownloading)
                    continue;
                if (infoFiles.Contains(item.Route)) continue;

                Debug.WriteLine($"IdxSvc: remove {item.Number}");
                Index.Remove(item);
            }

            foreach (var infoRoute in infoFiles.Where(item => Index.All(i => i.Route != item)))
            {
                if (!File.Exists(infoRoute)) continue;

                using var sr = new StreamReader(Core.DelayStream(infoRoute, FileMode.Open, FileAccess.Read), Encoding.UTF8);
                var infos = sr.ReadToEnd().Replace("\r\n", "\n");
                sr.Close();
                if (string.IsNullOrWhiteSpace(infos)) continue;

                var info = Core.InitializeArray(InfoCount, infos.Split('\n'));

                //if (info.Length != InfoCount)
                //{
                //    Debug.WriteLine($"Insufficient Info: {infoRoute.Split('\\')[^1].Split('.')[0]}, info.length: {info.Length}");
                //    continue;
                //}

                //if (info.Length > 7 && info[7] == "0") //목록에서만 제거 처리
                //{
                //    var temp = Index.Where(t => t.Number == infoRoute.Split('\\')[^1].Split('.')[0]).ToArray();

                //    if (temp.Length > 0) foreach (var item in temp) Index.Remove(item);

                //    continue;
                //}

                Index.Add(
                new IndexItem
                {
                    SiteName = info[0],
                    Title = info[1],
                    Author = info[2],
                    ImgCount = info[3],
                    Tags = info[4].Split("tags:")[1].Split('\n')[0].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries),
                    Date = info[5],
                    Vote = info[6] != null ? int.Parse(info[6]) : 0,
                    Show = info[7] == null || info[7] == "1",
                    View = info[8] != null ? int.Parse(info[8]) : 0,
                    Number = Core.EHNumFromRoute(infoRoute.Split('\\')[^1].Split('.')[0]),
                    Route = infoRoute
                }
                );
                //sb.Clear();
                _sender.IdxBlock.Dispatcher.Invoke(() => _sender.IdxBlock.Visibility = System.Windows.Visibility.Hidden);
            }
        }

        internal void RefreshAll()
        {
            foreach (var item in Index)
            {
                item.RefreshInfo?.Invoke();
            }
        }
        internal void Start()
        {
            _stop = false;
            _service.Start();
        }

        internal void Stop()
        {
            _stop = true;
            while (_service.IsAlive) Thread.Sleep(100);
        }
    }

    internal class IndexItem
    {
        public delegate void NoParam();
        public delegate void IntOne(int value);
        public delegate void DblOne(double value);
        public delegate void VisOne(System.Windows.Visibility value);

        public Processor Proc;

        public string Title { get; set; }
        public string Author { get; set; }
        public string SiteName { get; set; }
        public string ImgCount { get; set; }
        public string Number { get; set; }
        public string[] Tags { get; set; }
        public int Vote { get; set; }
        public int View { get; set; }
        public bool IsRead { get; set; }
        public string Route { get; set; }

        public string Date;

        //public bool Selected = false;
        public bool Show = true;
        public bool IsDownloading;

        public NoParam RefreshInfo;

        public NoParam ShownChang;

        public VisOne ProgPanelVis;
        //public VisOne TagPanelVis;

        public IntOne ProgBarMax;
        public NoParam ProgBarVal;

        public DblOne SizeChange;
    }

}
