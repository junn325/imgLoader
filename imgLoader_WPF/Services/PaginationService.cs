using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using imgLoader_WPF.LoaderListCtrl;
using ThreadState = System.Threading.ThreadState;

namespace imgLoader_WPF.Services
{
    internal class PaginationService
    {
        private const int Interval = 3000;

        private Thread _service;

        private readonly Windows.ImgLoader _sender;
        private readonly double _scrollHeight;
        private readonly ObservableCollection<IndexItem> _showItems;
        private readonly List<IndexItem> _list;

        public PaginationService(Windows.ImgLoader sender, double scrollHeight, ObservableCollection<IndexItem> showItems, List<IndexItem> list)
        {
            _sender = sender;
            _showItems = showItems;
            _list = list;
            _scrollHeight = scrollHeight;
        }

        internal void Paginate(DispatcherProcessingDisabled disableProcessing)
        {
            if (_service != null && _service.ThreadState != ThreadState.Stopped) return;

            var b = false;
            _service = new Thread(() =>
            {
                var num = (int)Math.Ceiling(_scrollHeight / LoaderItem.MHeight);

                var oriCnt = _showItems.Count;
                var listCount = _list.Count;

                var itemToAdd = new IndexItem[num];
                for (var i = 0; i < num; i++)
                {
                    var i1 = i;
                    if (oriCnt + i1 + 1 > listCount) return;

                    itemToAdd[i] = _list[oriCnt + i1];
                }

                _sender.Dispatcher.BeginInvoke(() =>
                {
                    foreach (var item in itemToAdd)
                    {
                        _showItems.Add(item);
                    }
                });

                b = true;
            });
            _service.Name = "PgSvc";
            _service.IsBackground = true;
            _service.Start();

            while (!b)
            {
                Task.Delay(5).Wait();
                Debug.Write("PgSvc: Wait\n");
            }

            disableProcessing.Dispose();
        }

        internal void Paginate()
        {
            if (_service != null && _service.ThreadState != ThreadState.Stopped) return;

            _service = new Thread(() =>
            {
                var oriCnt = _showItems.Count;
                for (var i = 0; i < Math.Ceiling(_scrollHeight / LoaderItem.MHeight); i++)
                {
                    var i1 = i;
                    if (oriCnt + i1 + 1 > _list.Count) return;

                    var temp = _list[oriCnt + i1];
                    _sender.Dispatcher.Invoke(() => _showItems.Add(temp));
                }
            });
            _service.Name = "PgSvc_NoDisableDispatcher";
            _service.IsBackground = true;
            _service.Start();
        }

        internal void PaginateToEnd()
        {
            if (_service == null) goto page;
            if (_service.ThreadState != ThreadState.Stopped) return;

            page: _service = new Thread(() =>
            {
                _sender.Dispatcher.Invoke(() =>
                {
                    _showItems.Clear();
                    foreach (var item in _list)
                    {
                        _showItems.Add(item);
                    }
                });

            });
            _service.Name = "PgSvc";
            _service.Start();
        }
    }
}
