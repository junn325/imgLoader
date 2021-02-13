using imgLoader_WPF.LoaderListCtrl;

using System.IO;
using System.Threading;

namespace imgLoader_WPF
{
    internal class VoteSavingService
    {
        private const int interval = 3000;
        private bool _stop;

        internal void Start(LoaderList list)
        {
            _stop = false;

            var service = new Thread(() =>
            {
                while (!_stop)
                {
                    Thread.Sleep(interval);

                    if (Properties.Settings.Default.NoIndex) continue;

                    list.Dispatcher.Invoke(() =>
                    {
                        foreach (var item in ((IndexingService)list.DataContext).Index)
                        {
                            var path = $@"{Core.GetDirectoryFromFile(item.Route)}\{item.Number}.{Core.VoteExt}";

                            if (!Directory.Exists(Core.GetDirectoryFromFile(item.Route))) continue;

                            if (File.Exists(path))
                            {
                                var info = File.ReadAllText(path);

                                if (!string.IsNullOrEmpty(info) && int.Parse(info.Trim()) == item.Vote) continue;

                                info = item.Vote.ToString();
                                File.WriteAllText(path, info);
                            }
                            else
                            {
                                File.WriteAllText(path, item.Vote.ToString());
                            }
                        }
                    });
                }
            });

            service.Name = "VtSvc";
            service.Start();
        }

        internal void Stop()
        {
            _stop = true;
        }
    }
}
