using imgLoader_WPF.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace imgLoader_WPF
{
    internal class InfoSavingService
    {
        private const int Interval = 3000;

        private bool _stop;
        internal Thread _service;
        private readonly StringBuilder _sb = new();

        private readonly Queue<IndexItem> _saveQueue = new();

        public InfoSavingService()
        {
            _service = new Thread(() =>
            {
                while (!_stop)
                {
                    if (_saveQueue.Count == 0)
                    {
                        Thread.Sleep(Interval);
                        continue;
                    }

                    var item = _saveQueue.Peek();

                    PerformSave(item, _sb);

                    _saveQueue.Dequeue();
                }
            });
            _service.Name = "vtSvc";
        }

        internal void Save(IndexItem item)
        {
            _saveQueue.Enqueue(item);
        }

        private void PerformSave(IndexItem item, StringBuilder sb)
        {
            if (string.IsNullOrWhiteSpace(item.Route)) return;
            if (!Directory.Exists(Core.GetDirectoryFromFile(item.Route))) return;

            sb.Append("tags:");
            foreach (var t in item.Tags)
            {
                sb.Append(t).Append(';');
            }
            var tag = sb.ToString();
            sb.Clear();

            sb.Append(item.SiteName).Append('\n')
                .Append(item.Title).Append('\n')
                .Append(item.Author).Append('\n')
                .Append(item.ImgCount).Append('\n')
                .Append(tag).Append('\n')
                .Append(item.Date).Append('\n')
                .Append(item.Vote).Append('\n')
                .Append(item.Show ? "1" : "0").Append('\n')
                .Append(item.View);

            using var sw = new StreamWriter(Core.DelayStream(item.Route, FileMode.OpenOrCreate, FileAccess.ReadWrite));
            sw.Write(sb);
            sb.Clear();
            sw.Close();
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

        internal enum Info
        {
            SiteName,
            Title,
            Author,
            ImgCount,
            Tags,
            Date,
            Vote,
            Show,
            View,
            All = int.MaxValue
        }
    }
}
