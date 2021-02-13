using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace imgLoader_WPF
{
    internal class IndexingService //index: <path, content>
    {
        private const int interval = 2000;
        private bool _stop;
        public ObservableCollection<IndexItem> Index;
        //private readonly ItemsControl _list;
        private Windows.ImgLoader _sender;

        public IndexingService(ObservableCollection<IndexItem> index, Windows.ImgLoader sender)
        {
            //Debug.WriteLine("indexing init");

            Index = index;
            //_list = list;
            _sender = sender;

            DoIndex();
        }

        private void DoIndex()
        {
            //Debug.WriteLine("DoIndex()");

            //const string countSeparator = "/**/";
            //const string itemSeparator = "-**-";

            //var tempPath = Path.GetTempPath();
            if (!Directory.Exists(Core.Route)) return;
            var infoFiles = Directory.GetFiles(Core.Route, $"*.{Core.InfoExt}", SearchOption.AllDirectories);

            //var sb = new StringBuilder();
            //var infos = new Dictionary<string, string>(infoFiles.Length);

            //var tasks = new Task[infoFiles.Length];
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

            //await File.WriteAllTextAsync($"{tempPath}{Core.IndexFile}.txt", $"{index.Count}{countSeparator}{sb}", Encoding.UTF8);
        }

        internal void Start()
        {
            _stop = false;
            //Debug.WriteLine("indexing Start()");

            var service = new Thread(() =>
            {
                while (!_stop)
                {
                    Thread.Sleep(interval);

                    if (Properties.Settings.Default.NoIndex) continue;
                    //if (temp.Count == Directory.GetFiles(_route, $"*.{Core.InfoExt}", SearchOption.AllDirectories).Length) continue;

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
