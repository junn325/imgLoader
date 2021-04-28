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

        public Sorter(ImgLoader sender)
        {
            _sender = sender;
        }

        internal void DoSortList(SortOption sortOption)
        {
            //_sender.Scroll.ScrollToTop();
            Core.ShowDate = false;
            Core.ShowLastDate = false;

            List<IndexItem> temp;
            switch (sortOption)
            {
                //todo: OrderBy로 새 리스트를 만들지 말고 insert로 구현하는 정렬 알고리즘 제작할 것

                case SortOption.Number:
                    temp = new List<IndexItem>(_sender.List.OrderBy(i => int.TryParse(i.Number, out var result) ? result : int.MaxValue));
                    break;
                case SortOption.Title:
                    temp = new List<IndexItem>(_sender.List.OrderBy(i => i.Title, StringComparer.OrdinalIgnoreCase));
                    break;
                case SortOption.Page:
                    temp = new List<IndexItem>(_sender.List.OrderBy(i => i.ImgCount));
                    break;
                case SortOption.Author:
                    temp = new List<IndexItem>(_sender.List.OrderBy(i => i.Author, StringComparer.OrdinalIgnoreCase));
                    break;
                case SortOption.Date:
                    Core.ShowDate = true;
                    temp = new List<IndexItem>(_sender.List.OrderByDescending(i => i.Date));
                    break;
                case SortOption.Vote:
                    temp = new List<IndexItem>(_sender.List.OrderByDescending(i => i.Vote));
                    break;
                case SortOption.View:
                    temp = new List<IndexItem>(_sender.List.OrderByDescending(i => i.View));
                    break;
                case SortOption.LastAccess:
                    Core.ShowLastDate = true;
                    temp = new List<IndexItem>(_sender.List.OrderByDescending(i => i.LastViewDate));
                    break;

                case SortOption.RTitle:
                    temp = new List<IndexItem>(_sender.List.OrderByDescending(i => i.Title, StringComparer.OrdinalIgnoreCase));
                    break;
                case SortOption.RAuthor:
                    temp = new List<IndexItem>(_sender.List.OrderByDescending(i => i.Author, StringComparer.OrdinalIgnoreCase));
                    break;
                case SortOption.RNumber:
                    temp = new List<IndexItem>(_sender.List.OrderByDescending(i => int.TryParse(i.Number, out var result) ? result : int.MaxValue));
                    break;
                case SortOption.RVote:
                    temp = new List<IndexItem>(_sender.List.OrderBy(i => i.Vote));
                    break;
                case SortOption.RPage:
                    temp = new List<IndexItem>(_sender.List.OrderByDescending(i => i.ImgCount));
                    break;
                case SortOption.RDate:
                    Core.ShowDate = true;
                    temp = new List<IndexItem>(_sender.List.OrderBy(i => i.Date));
                    break;
                case SortOption.RView:
                    temp = new List<IndexItem>(_sender.List.OrderBy(i => i.View));
                    break;
                case SortOption.RLastAcess:
                    Core.ShowLastDate = true;
                    temp  = new List<IndexItem>(_sender.List.OrderBy(i => i.LastViewDate));
                    break;

                default:
                    return;
            }

            _sender.List.Clear();

            foreach (var item in temp)
            {
                _sender.List.Add(item);
            }

            _sender.PgSvc.RefreshCounter();
        }

        internal void SortRefresh(SortOption sortOption, DispatcherProcessingDisabled disableProcessing)
        {
            DoSortList(sortOption);
            _sender.PgSvc.Clear();
            _sender.PgSvc.Paginate(disableProcessing);
            _sender.ShowItemsCnt();
        }

        internal void SortRefresh(SortOption sortOption)
        {
            DoSortList(sortOption);
            _sender.Dispatcher.Invoke(() =>
            {
                var disableProcessing = Dispatcher.CurrentDispatcher.DisableProcessing();
                _sender.PgSvc.Clear();
                _sender.PgSvc.Paginate(disableProcessing);
                _sender.ShowItemsCnt();
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
            LastAccess,
            RTitle,
            RAuthor,
            RNumber,
            RVote,
            RPage,
            RDate,
            RView,
            RLastAcess
        }
    }
}