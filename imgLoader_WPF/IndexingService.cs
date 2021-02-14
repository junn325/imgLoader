using imgLoader_WPF.Windows;

using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace imgLoader_WPF
{
    //collection contains IndexingService.IndexItem items                            
    //get info data every /interval/ milliseconds

    internal class IndexingService
    {
        private const int interval = 2000;

        private bool _stop;
        public ObservableCollection<IndexItem> Index;
        private readonly ImgLoader _sender;

        public IndexingService(ObservableCollection<IndexItem> index, ImgLoader sender)
        {
            Index = index;
            _sender = sender;

            DoIndex();
        }

        private void DoIndex()
        {
            if (!Directory.Exists(Core.Route)) return;

            var infoFiles = Directory.GetFiles(Core.Route, $"*.{Core.InfoExt}", SearchOption.AllDirectories);
            foreach (var item in new ObservableCollection<IndexItem>(Index))
            {
                if (infoFiles.Contains(item.Route)) continue;

                Index.Remove(item);
            }

            foreach (var infoRoute in infoFiles)
            {
                if (Index.Any(idx => idx.Route == infoRoute)) continue;

                using var sr = new StreamReader(new FileStream(infoRoute, FileMode.Open, FileAccess.Read), Encoding.UTF8);
                var info = (sr.ReadToEndAsync().ConfigureAwait(false));

                var temp = info.GetAwaiter().GetResult().Split('\n');
                Index.Add(new IndexItem { Title = temp[1], Author = temp[2], SiteName = temp[0], ImgCount = temp[3], Number = infoRoute.Split('\\')[^1].Split('.')[0], Route = infoRoute });
                sr.Close();
            }

            _sender.ItemCtrl.Dispatcher.Invoke(() => _sender.ItemCtrl.ItemsSource = this.Index);
        }

        internal void Start()
        {
            _stop = false;

            var service = new Thread(() =>
            {
                while (!_stop)
                {
                    Thread.Sleep(interval);

                    if (Properties.Settings.Default.NoIndex) continue;

                    DoIndex();
                }
            });

            service.Name = "IdxSvc";
            service.Start();
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
        }
    }
}
