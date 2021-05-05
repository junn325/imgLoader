using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace imgL.Services
{
    internal class DelayStream
    {
        private const int Interval = 20;

        private bool _stop = false;
        private int test = 0;

        private readonly Queue<(string, Task<FileStream>)> _streamQueue = new();

        public DelayStream()
        {
            var service = new Thread(() =>
            {
                while (!_stop)
                {
                    if (_streamQueue.Count == 0)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            if (_streamQueue.Count != 0) break;
                            Thread.Sleep(Interval / 20);
                        }
                        continue;
                    }

                    var (route, task) = _streamQueue.Peek();
                    Debug.Assert(task != null);
                    task.Start();
                    _streamQueue.Dequeue();
                }
            });
            service.Name = "DelStream";
            service.IsBackground = true;

            service.Start();
        }

        internal Task<FileStream> RequestStream(string route, FileMode mode, FileAccess access)
        {
            test++;
            var result = new Task<FileStream>(() => new FileStream(route, mode, access));

            lock (_streamQueue)
            {
                _streamQueue.Enqueue((route, result));
            }
            return result;
        }
    }
}
