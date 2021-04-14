﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

using imgLoader_WPF.Windows;

namespace imgLoader_WPF.Services
{
    internal class ImgCacheService
    {
        internal const int Interval = 1000;

        private Canvas _sender;

        private bool _stop;
        private bool _pause;

        private readonly Queue<int> _cacheQueue = new();

        public ImgCacheService(Canvas sender)
        {
            _sender = sender;

            var service = new Thread(() =>
            {
                //var sb = new StringBuilder();

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
            service.Name = "vtSvc";
            service.IsBackground = true;

            service.Start();
        }

        private void PerformCache(int index)
        {
            //if (_imgList[index] != null) return;

            //while (_size >= Properties.Settings.Default.CacheSize)
            //{
            //    Task.Delay(200).Wait();
            //}

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
