using imgLoader_WPF.LoaderListCtrl;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace imgLoader_WPF
{
    internal class VoteSavingService
    {
        private const int Interval = 3000;
        private bool _stop;

        internal void Start(LoaderList list)
        {
            _stop = false;

            var service = new Thread(() =>
            {
                while (!_stop)
                {
                    ObservableCollection<IndexingService.IndexItem> idx = null;
                    Thread.Sleep(Interval);

                    if (Properties.Settings.Default.NoIndex) continue;

                    list.Dispatcher.Invoke(() => idx = ((IndexingService)list.DataContext).Index);
                    if (idx == null) continue;

                    foreach (var item in idx)
                    {
                        var path = $@"{Core.GetDirectoryFromFile(item.Route)}\{item.Number}.{Core.InfoExt}";

                        if (!Directory.Exists(Core.GetDirectoryFromFile(item.Route))) continue;

                        if (File.Exists(path))
                        {
                            var sr = new StreamReader(DelayStream(path, FileMode.Open, FileAccess.Read));
                            var info = sr.ReadToEnd();
                            sr.Close();

                            if (string.IsNullOrEmpty(info)) continue;

                            while (info.Split('\n').Length < 7) info += '\n';

                            var tt = int.TryParse(info.Split('\n')[6].Trim(), out var temp);
                            if (tt && temp == item.Vote) continue;

                            info = info.Substring(0, info.LastIndexOf('\n')) + $"\n{item.Vote}";                                       //\n의 마지막 인덱스. 정보 추가시 수정 필요

                            using var ds = DelayStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                            var sw = new StreamWriter(ds);
                            sw.Write(info);

                            sw.Flush();
                            ds.SetLength(ds.Position);

                            sw.Close();
                            ds.Close();

                            Debug.WriteLine($"{item.Number} complete");
                        }
                        else
                        {
                            File.WriteAllText(path, item.Vote.ToString());
                        }
                    }
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
            FileStream file = null;

            var temp = false;
            var thres = 0;

            while (!temp)
            {
                try
                {
                    file = new FileStream(route, mode, access);
                    temp = true;
                }
                catch
                {
                    if (thres++ > 100) break;

                    temp = false;
                    Debug.WriteLine($"{route} wait");
                    Thread.Sleep(20);
                }
            }

            if (file == null) throw new Exception("stream is null");

            return file;
        }
    }
}
