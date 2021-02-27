using imgLoader_WPF.LoaderListCtrl;

using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace imgLoader_WPF
{
    internal class PaginationService
    {
        private const int Interval = 3000;

        private Thread _service;

        private readonly Windows.ImgLoader _sender;
        private readonly ScrollViewer _scroll;
        private readonly ObservableCollection<IndexItem> _showItems;
        private readonly ObservableCollection<IndexItem> _actualIndex;

        public PaginationService(Windows.ImgLoader sender, ScrollViewer scroll, ObservableCollection<IndexItem> showItems, ObservableCollection<IndexItem> actualIndex)
        {
            _sender = sender;
            _showItems = showItems;
            _actualIndex = actualIndex;
            _scroll = scroll;
        }

        internal void Paginate()
        {
            if (_service == null) goto page;
            if (_service.ThreadState != System.Threading.ThreadState.Stopped) return;

            page: _service = new Thread(() =>
            {
                _scroll.Dispatcher.Invoke(() => _scroll.ScrollToTop());

                while (_actualIndex.Count == 0)
                {
                    Task.Delay(500).Wait();
                }

                int oriCnt = _showItems.Count;
                for (int i = 0; i < Math.Ceiling(_scroll.ActualHeight / LoaderItem.MHeight); i++)
                {
                    var i1 = i;
                    if (oriCnt + i1 + 1 > _actualIndex.Count) return;

                    var temp = _actualIndex[oriCnt + i1];
                    _sender.Dispatcher.Invoke(() => _showItems.Add(temp));
                }
            });
            _service.Name = "PgSvc";
            _service.Start();
        }
    }
}
