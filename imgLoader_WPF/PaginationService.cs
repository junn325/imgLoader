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
        private readonly double _scrollHeight;
        private readonly ObservableCollection<IndexItem> _showItems;
        internal readonly ObservableCollection<IndexItem> _list;

        public PaginationService(Windows.ImgLoader sender, double scrollHeight, ObservableCollection<IndexItem> showItems, ObservableCollection<IndexItem> list)
        {
            _sender = sender;
            _showItems = showItems;
            _list = list;
            _scrollHeight = scrollHeight;
        }

        internal void Paginate()
        {
            if (_service == null) goto page;
            if (_service.ThreadState != ThreadState.Stopped) return;

            page: _service = new Thread(() =>
            {
                //while (_list.Count == 0)
                //{
                //    Task.Delay(500).Wait();
                //}

                int oriCnt = _showItems.Count;
                for (int i = 0; i < Math.Ceiling(_scrollHeight / LoaderItem.MHeight); i++)
                {
                    var i1 = i;
                    if (oriCnt + i1 + 1 > _list.Count) return;

                    var temp = _list[oriCnt + i1];
                    _sender.Dispatcher.Invoke(() => _showItems.Add(temp));
                }
            });
            _service.Name = "PgSvc";
            _service.Start();
        }
    }
}
