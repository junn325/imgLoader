using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace imgLoader_WPF
{
    internal class ParallelStream
    {
        private string _result;
        public string ReadResult
        {
            get
            {
                var temp = _result;
                _result = null;
                return temp;
            }
            private set => _result = value;
        }

        private bool _stop;
        readonly Queue<string> _writeQueue = new();
        readonly Queue<string> _readQueue = new();

        public ParallelStream()
        {
            //_stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            var thrStream = new Thread(() =>
            {
                while (!_stop)
                {
                    if (_writeQueue.Count == 0 && _readQueue.Count == 0)
                    {
                        Thread.Sleep(500);
                        continue;
                    }

                    if (_readQueue.Count != 0)
                    {
                        var temp = _readQueue.Peek();
                        var sw = new StreamReader(new FileStream(temp, FileMode.Open, FileAccess.Read), Encoding.UTF8);
                        ReadResult = sw.ReadToEnd();
                        _readQueue.Dequeue();
                    }

                    if (_writeQueue.Count != 0)
                    {
                        var temp = _writeQueue.Peek().Split('\n');
                        var sw = new StreamWriter(new FileStream(temp[0], FileMode.OpenOrCreate, FileAccess.ReadWrite),Encoding.UTF8);
                        sw.Write(temp[1]);
                        _writeQueue.Dequeue();
                    }
                }
            });

            thrStream.Name = "PrllStream";
            thrStream.Start();
        }

        internal void Write(string path, string value)
        {
            _writeQueue.Enqueue($"{path}\n{value}");
        }

        internal void Read(string path)
        {
            _readQueue.Enqueue(path);
        }

        internal void StopStream()
        {
            _stop = true;
        }
    }
}
