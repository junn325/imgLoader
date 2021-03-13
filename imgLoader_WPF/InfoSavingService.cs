
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

        internal void Save()
        {

        }

        private void PerformSave(IndexItem sender)
        {
            if (string.IsNullOrWhiteSpace(sender.Route)) return;

            if (!Directory.Exists(Core.GetDirectoryFromFile(sender.Route))) return;

            if (File.Exists(sender.Route))
            {
                using (var sr = new StreamReader(Core.DelayStream(sender.Route, FileMode.Open, FileAccess.Read)))
                {
                    var infoString = sr.ReadToEnd();
                    sr.Close();
                    if (string.IsNullOrEmpty(infoString)) return;

                    var infoSplit = infoString.Split('\n');
                    var info = Core.InitializeArray(InfoCount, infoSplit);

                    if (string.IsNullOrEmpty(infoSplit[6]) || string.IsNullOrEmpty(infoSplit[7])) goto infoContent;

                    if (int.TryParse(infoSplit[6], out var temp) && temp == sender.Vote && (infoSplit[7] == "1") == sender.Show) return;

                    infoContent:
                    infoString = $"{infoString.Substring(0, infoString.CountIndexOf('\n', 5))}\n{sender.Vote}\n{(sender.Show ? 1 : 0)}";

                    if (_stop) return;
                    using var ds = Core.DelayStream(sender.Route, FileMode.OpenOrCreate, FileAccess.Write);
                    if (_stop) return;

                    var sw = new StreamWriter(ds);
                    sw.Write(infoString);

                    sw.Flush();
                    ds.SetLength(ds.Position);

                    sw.Close();
                    ds.Close();
                }

                Debug.WriteLine($"{sender.Number} complete");
            }
            else
            {
                File.WriteAllText(sender.Route, sender.Vote.ToString());
            }
        }

        internal void Stop()
        {
            _stop = true;
            while (_service.IsAlive) Thread.Sleep(100);
        }
    }
}
