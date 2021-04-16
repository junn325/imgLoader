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
        private readonly ObservableCollection<IndexItem> _showItems;
        private readonly List<IndexItem> _list;

        private readonly IndexItem _separator;

        public double ScrollHeight;
        private int _counter;
        private int _separatorCount;

        public PaginationService(Windows.ImgLoader sender, double scrollHeight, ObservableCollection<IndexItem> showItems, List<IndexItem> list)
        {
            _sender = sender;
            _showItems = showItems;
            _list = list;
            ScrollHeight = scrollHeight;

            _separator = new IndexItem { View = -1, ImgCount = -1, IsSeparator = true };
        }

        internal void Paginate(DispatcherProcessingDisabled disableProcessing)
        {
            var num = (int)Math.Ceiling(ScrollHeight / LoaderItem.MHeight);

            _sender.Dispatcher.BeginInvoke(() =>
            {
                var oriCnt = _showItems.Count;

                var listCpy = new List<IndexItem>(_list);
                for (var i = 0; i < num; i++)
                {
                    if (oriCnt + i - _separatorCount >= listCpy.Count) break;
                    if (listCpy[oriCnt + i - _separatorCount] == null) continue;

                    _counter++;
                    _showItems.Add(listCpy[oriCnt + i - _separatorCount]);

                    if (_counter == 5)
                    {
                        _counter = 0;
                        _showItems.Add(_separator);
                        _separatorCount++;
                        //Debug.WriteLine($"sCnt: {_separatorCount}");
                    }
                }
                _sender.Scroll.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
            });

            disableProcessing.Dispose();
        }

        internal void Paginate()
        {
            if (_service != null && _service.ThreadState != ThreadState.Stopped) return;

            _service = new Thread(() =>
            {
                var oriCnt = _showItems.Count;

                _sender.Dispatcher.Invoke(() =>
                {
                    for (var i = 0; i < Math.Ceiling(ScrollHeight / LoaderItem.MHeight); i++)
                    {
                        if (oriCnt + i - _separatorCount >= _list.Count)
                            return;

                        _counter++;
                        _showItems.Add(_list[oriCnt + i - _separatorCount]);

                        if (_counter == 5)
                        {
                            _counter = 0;
                            _showItems.Add(_separator);
                            _separatorCount++;
                            //Debug.WriteLine($"sCnt: {_separatorCount}");
                        }
                        //Debug.Assert(_showItems.Count <= _sender.List.Count);
                    }
                });
            });
            _service.Name = "PgSvc_NoDisableDispatcher";
            _service.IsBackground = true;
            _service.Start();
        }

        internal void RefreshCounter()
        {
            _counter = 0;
            _separatorCount = 0;
            //Debug.WriteLine($"sCnt: {_separatorCount}");
        }

        internal int GetCntWoSep()
        {
            return _showItems.Count - _separatorCount;
        }

        internal void Clear()
        {
            _sender.Scroll.ScrollToTop();
            _showItems.Clear();
        }

        internal void Remove(IndexItem item)
        {
            if (_sender.ItemCtrl.Dispatcher.CheckAccess())
            {
                _sender.ItemCtrl.Dispatcher.Invoke(() => _showItems.Remove(item));
            }
            else
            {
                _showItems.Remove(item);
            }
        }

        internal void Insert(int index, IndexItem item)
        {
            if (_sender.ItemCtrl.Dispatcher.CheckAccess())
            {
                _sender.ItemCtrl.Dispatcher.Invoke(() => _showItems.Insert(index, item));
            }
            else
            {
                _showItems.Insert(index, item);
            }
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
