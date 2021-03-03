
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using imgLoader_WPF.Windows;

namespace imgLoader_WPF
{
    internal class InfoSavingService
    {
        private const int Interval = 3000;

        private bool _stop;
        private Thread _service;
        private readonly ImgLoader _sender;

        private delegate void ServDele(bool stop, ImgLoader sender);
        private readonly ServDele _func = ((stop, sender) =>
        {
            while (!stop)
            {
                Debug.WriteLine("VtSvc");

                Thread.Sleep(Interval);

                Save(sender);
            }
        });

        public InfoSavingService(ImgLoader sender)
        {
            _sender = sender;
        }

        internal static void Save(Windows.ImgLoader sender)
        {
            IndexItem[] idx = null;

            if (Properties.Settings.Default.NoIndex) return;

            try
            {
                if (sender.ItemCtrl.ItemsSource == null) return;
                idx = sender.ItemCtrl.ItemsSource.Cast<IndexItem>().ToArray();
            }
            catch (OperationCanceledException) { }

            //if (idx == null) return;

            foreach (var item in idx)
            {
                if (string.IsNullOrWhiteSpace(item.Route)) continue;

                var path = $@"{Core.GetDirectoryFromFile(item.Route)}\{Core.EHNumForDir(item.Number)}.{Core.InfoExt}";
                if (!Directory.Exists(Core.GetDirectoryFromFile(item.Route))) continue;

                if (File.Exists(path))
                {
                    using (var sr = new StreamReader(Core.DelayStream(path, FileMode.Open, FileAccess.Read)))
                    {
                        var info = sr.ReadToEnd();
                        sr.Close();

                        if (string.IsNullOrEmpty(info)) continue;

                        while (info.Split('\n').Length < 8) info += '\n';

                        if (string.IsNullOrEmpty(info.Split('\n')[6]) || string.IsNullOrEmpty(info.Split('\n')[7])) goto infoContent;

                        if (int.TryParse(info.Split('\n')[6], out var temp) && temp == item.Vote
                            && (info.Split('\n')[7] == "1") == item.Show) continue;

                        infoContent:
                        info = $"{info.Substring(0, info.CountIndexOf('\n', 5))}\n{item.Vote}\n{(item.Show ? 1 : 0)}";

                        using var ds = Core.DelayStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                        var sw = new StreamWriter(ds);
                        sw.Write(info);

                        sw.Flush();
                        ds.SetLength(ds.Position);

                        sw.Close();
                        ds.Close();
                    }

                    Debug.WriteLine($"{item.Number} complete");
                }
                else
                {
                    File.WriteAllText(path, item.Vote.ToString());
                }
            }

        }
        internal void Start()
        {
            _stop = false;

            if (_service == null || _service.ThreadState != System.Threading.ThreadState.Running)
            {
                Debug.WriteLine(_service?.ThreadState);

                _service = new Thread(() => _func(_stop, _sender));
                _service.Name = "VtSvc";

                _service.Start();
            }
        }

        internal void Stop()
        {
            _stop = true;
        }
    }
}
