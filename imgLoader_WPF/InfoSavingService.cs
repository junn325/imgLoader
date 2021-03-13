
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
        internal Thread _service;
        private readonly ImgLoader _sender;

        private const int InfoCount = 9;
        public InfoSavingService(ImgLoader sender)
        {
            _sender = sender;
        }

        internal void Save(Windows.ImgLoader sender)
        {
            IndexItem[] idx = null;

            if (Properties.Settings.Default.NoIndex) return;

            try
            {
                if (_stop || sender.ItemCtrl.ItemsSource == null) return;
                idx = sender.ItemCtrl.ItemsSource.Cast<IndexItem>().ToArray();
            }
            catch (OperationCanceledException) { }

            //if (idx == null) return;

            foreach (var item in idx)
            {
                if (_stop) return;

                if (string.IsNullOrWhiteSpace(item.Route)) continue;

                var path = $@"{Core.GetDirectoryFromFile(item.Route)}\{Core.EHNumForDir(item.Number)}.{Core.InfoExt}";
                if (!Directory.Exists(Core.GetDirectoryFromFile(item.Route))) continue;

                if (File.Exists(path))
                {
                    using (var sr = new StreamReader(Core.DelayStream(path, FileMode.Open, FileAccess.Read)))
                    {
                        var infoString = sr.ReadToEnd();
                        sr.Close();
                        if (string.IsNullOrEmpty(infoString)) continue;

                        var infoSplit = infoString.Split('\n');
                        var info = Core.InitializeArray(InfoCount, infoSplit);

                        if (string.IsNullOrEmpty(infoSplit[6]) || string.IsNullOrEmpty(infoSplit[7])) goto infoContent;

                        if (int.TryParse(infoSplit[6], out var temp) && temp == item.Vote && (infoSplit[7] == "1") == item.Show) continue;

                        infoContent:
                        infoString = $"{infoString.Substring(0, infoString.CountIndexOf('\n', 5))}\n{item.Vote}\n{(item.Show ? 1 : 0)}";

                        if (_stop) return;
                        using var ds = Core.DelayStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                        if (_stop) return;

                        var sw = new StreamWriter(ds);
                        sw.Write(infoString);

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
            Debug.WriteLine("vtsvc: start");
            _stop = false;

            if (_service == null || _service.ThreadState != System.Threading.ThreadState.Running)
            {
                Debug.WriteLine(_service?.ThreadState);

                _service = new Thread(() =>
                {
                    while (!_stop)
                    {
                        //Debug.WriteLine("VtSvc");

                        Thread.Sleep(Interval);

                        Save(_sender);
                    }
                });
                _service.Name = "VtSvc";

                _service.Start();
            }
        }

        internal void Stop()
        {
            _stop = true;
            while (_service.IsAlive) Thread.Sleep(100);
        }
    }
}
