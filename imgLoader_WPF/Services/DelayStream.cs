using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace imgLoader_WPF.Services
{
    internal class DelayStream
    {
        private const int Interval = 1000;

        private bool _stop = false;

        private readonly Queue<(string, string)> _fileQueue = new();

        public DelayStream()
        {
            var service = new Thread(() =>
            {
                while (!_stop)
                {
                    if (_fileQueue.Count == 0)
                    {
                        Thread.Sleep(Interval);
                        continue;
                    }

                    var (route, content) = _fileQueue.Peek();
                    PerformWrite(route, content);
                    _fileQueue.Dequeue();
                }
            });
            service.Name = "DelStream";
            service.IsBackground = true;

            service.Start();
        }

        internal void Save(string route, string content)
        {
            _fileQueue.Enqueue((route, content));
        }

        private static void PerformWrite(string route, string content)
        {
            using var fs = new FileStream(route, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using var sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Write(content);

            sw.Flush();                //┐
            fs.SetLength(fs.Position); //오류 등으로 원래 담겨야할 줄 수 이상의 줄이 있을 때 지움

            sw.Close();
        }
    }
}
