using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using imgLoader_WPF.LoaderListCtrl;

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
            _service.Name = "PgSvc";
            _service.IsBackground = true;
            _service.Start();
        }

        internal void PaginateToEnd()
        {
            if (_service == null) goto page;
            if (_service.ThreadState != ThreadState.Stopped) return;

            page: _service = new Thread(() =>
            {
                _sender.Dispatcher.Invoke(() => _showItems.Clear());

                foreach (var item in _list)
                {
                    _sender.Dispatcher.Invoke(() => _showItems.Add(item));
                }
            });
            _service.Name = "PgSvc";
            _service.Start();
        }
    }
}
