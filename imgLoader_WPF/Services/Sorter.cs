using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

using imgLoader_WPF.Windows;

using Dispatcher = System.Windows.Threading.Dispatcher;

namespace imgLoader_WPF.Services
{
    internal class Sorter
    {
        private readonly ImgLoader _sender;
        private readonly List<IndexItem> _list;

        public Sorter(ImgLoader sender, List<IndexItem> list)
        {
            _list = list;
            _sender = sender;
        }

        internal void DoSortList(SortOption sortOption)
        {
            //_sender.Scroll.ScrollToTop();
            Core.ShowDate = false;

            List<IndexItem> temp;
            switch (sortOption)
            {
                //todo: OrderBy로 새 리스트를 만들지 말고 insert로 구현하는 정렬 알고리즘 제작할 것

                case SortOption.Number:
                    temp = new List<IndexItem>(_list.OrderBy(i => int.TryParse(i.Number, out var result) ? result : int.MaxValue));
                    break;
                case SortOption.Title:
                    temp = new List<IndexItem>(_list.OrderBy(i => i.Title, StringComparer.OrdinalIgnoreCase));
                    break;
                case SortOption.Page:
                    temp = new List<IndexItem>(_list.OrderBy(i => i.ImgCount));
                    break;
                case SortOption.Author:
                    temp = new List<IndexItem>(_list.OrderBy(i => i.Author, StringComparer.OrdinalIgnoreCase));
                    break;
                case SortOption.Date:
                    Core.ShowDate = true;
                    temp = new List<IndexItem>(_list.OrderByDescending(i => i.Date));
                    break;
                case SortOption.Vote:
                    temp = new List<IndexItem>(_list.OrderByDescending(i => i.Vote));
                    break;
                case SortOption.View:
                    temp = new List<IndexItem>(_list.OrderByDescending(i => i.View));
                    break;
                case SortOption.LastAccess:
                    Core.ShowLastDate = true;
                    temp = new List<IndexItem>(_list.OrderByDescending(i => i.LastViewDate));
                    break;

                default:
                    return;
            }

            _list.Clear();

            foreach (var item in temp)
            {
                _list.Add(item);
            }

            _sender.PgSvc.RefreshCounter();
        }

        internal void SortRefresh(SortOption sortOption, DispatcherProcessingDisabled disableProcessing)
        {
            DoSortList(sortOption);
            _sender.PgSvc.Clear();
            _sender.PgSvc.Paginate(disableProcessing);
            _sender.ShowItemCount();
        }

        internal void SortRefresh(SortOption sortOption)
        {
            DoSortList(sortOption);
            _sender.Dispatcher.Invoke(() =>
            {
                var disableProcessing = Dispatcher.CurrentDispatcher.DisableProcessing();
                _sender.PgSvc.Clear();
                _sender.PgSvc.Paginate(disableProcessing);
                _sender.ShowItemCount();
            });
        }

        internal enum SortOption
        {
            Title,
            Author,
            Number,
            Vote,
            Page,
            Date,
            View,
            LastAccess
        }
    }
}