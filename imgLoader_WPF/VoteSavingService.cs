using System;
using imgLoader_WPF.LoaderListCtrl;

using System.IO;
using System.Linq;
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
                            var path = $@"{Core.GetDirectoryFromFile(item.Route)}\{item.Number}.{Core.InfoExt}";

                            if (!Directory.Exists(Core.GetDirectoryFromFile(item.Route))) continue;

                            if (File.Exists(path))
                            {
                                var info = File.ReadAllText(path);

                                if (!string.IsNullOrEmpty(info)
                                    && int.TryParse(info.Split('\n')[6].Trim(), out var temp)
                                    && temp == item.Vote) continue;

                                info = info.Substring(0,)//todo: 여기에 vote 수정 넣을것

                                new StreamWriter(DelayStream(path, FileMode.Append, FileAccess.Write)).Write(info);
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

        private static FileStream DelayStream(string route, FileMode mode, FileAccess access)
        {
            var temp = false;
            FileStream file = null;

            while (!temp)
            {
                try
                {
                    file = new FileStream(route, mode, access);
                    temp = true;
                }
                catch
                {
                    temp = false;
                }
            }

            if (file == null) throw new Exception("stream is null");

            return file;
        }
    }
}
