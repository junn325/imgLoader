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
        private const int InfoCount = 9;

        private bool _stop;
        internal Thread _service;
        private readonly ImgLoader _sender;
        private readonly StringBuilder _sb = new();

        private readonly Queue<IndexItem> _saveQueue = new();
        private readonly Queue<Info> _saveQueue_Info = new();

        public InfoSavingService(ImgLoader sender)
        {
            _sender = sender;
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
                    var info = _saveQueue_Info.Peek();

                    PerformSave(item, _sb, info);

                    _saveQueue.Dequeue();
                    _saveQueue_Info.Dequeue();
                }
            });
            _service.Name = "vtSvc";
        }

        internal void Save(IndexItem item, Info infoToSave)
        {
            _saveQueue.Enqueue(item);
            _saveQueue_Info.Enqueue(infoToSave);
        }

        private void PerformSave(IndexItem item, StringBuilder sb, Info infoToSave)
        {
            if (string.IsNullOrWhiteSpace(item.Route)) return;
            if (!Directory.Exists(Core.GetDirectoryFromFile(item.Route))) return;

            switch (infoToSave)
            {
                case Info.All:
                    {
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

                        break;
                    }

                case Info.Author:
                    {
                        var temp = File.ReadAllText(item.Route);
                        var infos = temp.Split('\n');
                        infos[(int)Info.Author] = item.Author;

                        using var fs = new FileStream(item.Route, FileMode.OpenOrCreate);

                        fs.Position = 3;                        //BOM 스킵
                        for (var i = 0; i < (int)Info.Author; i++)             //작가 위치로 이동
                        {
                            fs.Position += (infos[i].Length + 1);
                        }

                        using TextWriter tw = new StreamWriter(fs, Encoding.UTF8, 1024, true);
                       
                        //tw.Write(item.Author);
                        //tw.Write('\n');                     //writeline은 \r\n을 사용하므로 따로 씀

                        fs.Write(new byte[] {100}, 0, 1);

                        tw.Close();
                        break;
                    }
            }

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
