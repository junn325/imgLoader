using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
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

        //private readonly IndexItem _separator;

        //private int _counter;
        //private int _separatorCount;

        public PaginationService(Windows.ImgLoader sender)
        {
            _sender = sender;

            //_separator = new IndexItem { View = -1, ImgCount = -1, IsSeparator = true };
        }

        internal void Paginate(DispatcherProcessingDisabled disableProcessing)
        {
            var num = (int)Math.Ceiling(_sender.Scroll.ActualHeight / LoaderItem.MHeight);

            _sender.Dispatcher.BeginInvoke(() =>
            {
                var oriCnt = GetCntItemOnly();

                var listCpy = new List<IndexItem>(_sender.List);
                for (var i = 0; i < num; i++)
                {
                    if (oriCnt + i >= listCpy.Count)
                        break;

                    if (listCpy[oriCnt + i] == null)
                        continue;

                    _sender.ShowItems.Add(listCpy[oriCnt + i]);

                    //_counter++;

                    //if (_counter == 5)
                    //{
                    //    _counter = 0;
                    //    _showItems.Add(_separator);
                    //    _separatorCount++;
                    //    //Debug.WriteLine($"sCnt: {_separatorCount}");
                    //}
                }

                //_sender.ShowItemsCnt();
                _sender.Scroll.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto;
            });

            disableProcessing.Dispose();
        }

        internal void Paginate()
        {
            if (_service != null && _service.ThreadState != ThreadState.Stopped) return;

            _service = new Thread(() =>
            {
                var oriCnt = GetCntItemOnly();

                _sender.Dispatcher.Invoke(() =>
                {
                    for (var i = 0; i < Math.Ceiling(_sender.Scroll.ActualHeight / LoaderItem.MHeight); i++)
                    {
                        if (oriCnt + i >= _sender.List.Count)
                            return;

                        _sender.ShowItems.Add(_sender.List[oriCnt + i]);
                        //_counter++;

                        //if (_counter == 5)
                        //{
                        //    _counter = 0;
                        //    _showItems.Add(_separator);
                        //    _separatorCount++;
                        //    //Debug.WriteLine($"sCnt: {_separatorCount}");
                        //}
                        //Debug.Assert(_showItems.Count <= _sender.List.Count);
                    }

                    //_sender.ShowItemsCnt();
                });
            });
            _service.Name = "PgSvc_NoDisableDispatcher";
            _service.IsBackground = true;
            _service.Start();
        }

        internal void RefreshCounter()
        {
            //_counter = 0;
            //_separatorCount = 0;
            //Debug.WriteLine($"sCnt: {_separatorCount}");
        }

        internal int GetCntItemOnly()
        {
            return _sender.ShowItems.Count /*- _separatorCount*/;
        }

        internal void Clear()
        {
            //var disableProcessing = _sender.Dispatcher.DisableProcessing();

            RefreshCounter();
            if (_sender.Scroll.Dispatcher.CheckAccess())
            {
                _sender.Scroll.ScrollToTop();
                _sender.ShowItems.Clear();
            }
            else
            {
                _sender.Scroll.Dispatcher.Invoke(() =>
                {
                    _sender.Scroll.ScrollToTop();
                    _sender.ShowItems.Clear();
                });
            }

            _sender.ShowItemsCnt();
            //return disableProcessing;
        }

        internal void Remove(IndexItem item)
        {
            if (_sender.ItemCtrl.Dispatcher.CheckAccess())
            {
                _sender.ShowItems.Remove(item);
            }
            else
            {
                _sender.ItemCtrl.Dispatcher.Invoke(() => _sender.ShowItems.Remove(item));
            }

            _sender.ShowItemsCnt();
        }

        internal void Insert(int index, IndexItem item)
        {
            if (_sender.ItemCtrl.Dispatcher.CheckAccess())
            {
                _sender.ShowItems.Insert(index, item);
            }
            else
            {
                _sender.ItemCtrl.Dispatcher.Invoke(() => _sender.ShowItems.Insert(index, item));
            }
        }

        internal void Modify(int index)
        {
            //if (_sender.ItemCtrl.Dispatcher.CheckAccess())
            //{
            //    _showItems[index].Author
            //}
            //else
            //{
            //    _sender.ItemCtrl.Dispatcher.Invoke(() => _showItems.Insert(index, item));
            //}
        }

        internal void PaginateToEnd()
        {
            if (_service == null) goto page;
            if (_service.ThreadState != ThreadState.Stopped) return;

            page: _service = new Thread(() =>
            {
                _sender.Dispatcher.Invoke(() =>
                {
                    _sender.ShowItems.Clear();
                    foreach (var item in _sender.List)
                    {
                        _sender.ShowItems.Add(item);
                    }
                });

            });
            _service.Name = "PgSvc";
            _service.Start();
        }
    }
}
