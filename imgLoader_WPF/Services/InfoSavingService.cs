using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

using imgLoader_WPF.Windows;

namespace imgLoader_WPF.Services
{
    internal class InfoSavingService
    {
        private const int Interval = 1000;

        private bool _stop = false;

        private readonly StringBuilder _sb = new();
        private readonly ImgLoader _sender;

        private readonly Queue<IndexItem> _saveQueue = new();

        public InfoSavingService(ImgLoader sender)
        {
            _sender = sender;

            var service = new Thread(() =>
            {
                while (!_stop)
                {
                    if (_saveQueue.Count == 0)
                    {
                        Thread.Sleep(Interval);
                        continue;
                    }

                    PerformSave(_saveQueue.Peek(), _sb);
                    _saveQueue.Dequeue();
                }
            });
            service.Name = "vtSvc";
            service.IsBackground = true;

            service.Start();
        }

        internal void Save(IndexItem item)
        {
            _saveQueue.Enqueue(item);
        }

        private void PerformSave(IndexItem item, StringBuilder sb)
        {
            if (string.IsNullOrWhiteSpace(item.Route)) return;
            if (!Directory.Exists(Core.Dir.GetDirFromFile(item.Route))) return;

            if (item.IsError) return;

            sb.Append("tags:");

            if (item.Tags != null)
            {
                foreach (var t in item.Tags)
                {
                    sb.Append(t).Append(';');
                }
            }

            var tags = sb.ToString();
            sb.Clear();

            sb.Append(item.SiteName).Append('\n')
                .Append(item.Title).Append('\n')
                .Append(item.Author).Append('\n')
                .Append(item.ImgCount).Append('\n')
                .Append(tags).Append('\n')
                .Append(item.Date.ToString(CultureInfo.InvariantCulture)).Append('\n')
                .Append(item.Vote).Append('\n')
                .Append(item.Show ? "1" : "0").Append('\n')
                .Append(item.View).Append('\n')
                .Append(item.LastViewDate.ToString(CultureInfo.InvariantCulture));

            using var fs = Core.Dir.DelayStream(item.Route, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using var sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Write(sb.ToString());

            sw.Flush();                 //┐
            fs.SetLength(fs.Position);  //오류 등으로 원래 담겨야할 줄 수 이상의 줄이 있을 때 지움

            sb.Clear();
        }
    }
}
