using System.Collections.Generic;
using System.Threading;
using imgL.Windows;

namespace imgL.Services
{
    internal class ImgCacheService
    {
        internal const int Interval = 1000;

        private Viewer _sender;

        private bool _stop;
        private bool _pause;

        private readonly Queue<int> _cacheQueue = new();

        public ImgCacheService(Viewer sender)
        {
            _sender = sender;

            var service = new Thread(() =>
            {
                while (!_stop)
                {
                    if (_pause || _cacheQueue.Count == 0)
                    {
                        Thread.Sleep(Interval);
                        continue;
                    }

                    PerformCache(_cacheQueue.Peek());
                    _cacheQueue.Dequeue();
                }
            });
            service.Name         = "vtSvc";
            service.IsBackground = true;

            service.Start();
        }

        private void PerformCache(int index)
        {
            _sender.LoadImage(index);
        }

        internal void Add(int index)
        {
            _cacheQueue.Enqueue(index);
        }

        internal void Resume()
        {
            _pause = false;
        }

        internal void Pause()
        {
            _pause = true;
        }

        internal void Stop()
        {
            _stop = true;
        }
    }
}