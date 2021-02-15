using System;
using System.Collections.Generic;
using imgLoader_WPF.Windows;

using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace imgLoader_WPF
{
    //컬렉션은 IndexingService.IndexItem을 담고있음                        
    //매 /interval/ 밀리초마다 인덱싱

    internal class IndexingService
    {
        private const int Interval = 2000;

        public ObservableCollection<IndexItem> Index;

        private bool _stop;
        private readonly ImgLoader _sender;
        private Thread _service;

        public IndexingService(ObservableCollection<IndexItem> index, ImgLoader sender)
        {
            Index = index;
            _sender = sender;

            DoIndex();
        }

        internal void DoIndex()
        {
            if (!Directory.Exists(Core.Route)) return;

            var infoFiles = Directory.GetFiles(Core.Route, $"*.{Core.InfoExt}", SearchOption.AllDirectories);
            foreach (var item in new ObservableCollection<IndexItem>(Index))
            {
                if (infoFiles.Contains(item.Route)) continue;

                _sender.Dispatcher.Invoke(() => Index.Remove(item));
            }

            foreach (var infoRoute in infoFiles)
            {
                using var sr = new StreamReader(Core.DelayStream(infoRoute, FileMode.Open, FileAccess.Read), Encoding.UTF8);
                var infos = sr.ReadToEnd();
                sr.Close();

                var info = infos.Split('\n');

                if (info[7] == "0") //목록에서만 제거 처리
                {
                    var temp = Index.Where(t => t.Number == infoRoute.Split('\\')[^1].Split('.')[0]).ToArray();

                    if (temp.Length > 0)
                    {
                        foreach (var item in temp)
                        {
                            _sender.Dispatcher.Invoke(() => Index.Remove(item));
                        }
                    }

                    continue;
                }

                if (Index.Any(idx => idx.Route == infoRoute)) continue;

                _sender.Dispatcher.Invoke(() => Index.Add(new IndexItem { Title = info[1], Author = info[2], SiteName = info[0], ImgCount = info[3], Number = infoRoute.Split('\\')[^1].Split('.')[0], Route = infoRoute }));
            }

            try
            {
                _sender.ItemCtrl.Dispatcher.Invoke(() => _sender.ItemCtrl.ItemsSource = this.Index);
            }
            catch (TaskCanceledException)
            {
            }
        }

        internal void Start()
        {
            _stop = false;

            _service = new Thread(() =>
            {
                while (!_stop)
                {
                    Thread.Sleep(Interval);

                    if (Properties.Settings.Default.NoIndex) continue;

                    DoIndex();
                }
            });
            _service.Name = "IdxSvc";

            _service.Start();
        }

        internal void Stop()
        {
            _stop = true;
        }

        internal class IndexItem
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string SiteName { get; set; }
            public string ImgCount { get; set; }
            public string Number { get; set; }
            public string[] Tags { get; set; }
            public int Vote { get; set; }

            public string Route { get; set; }
            public bool Show = true;
        }
    }
}
