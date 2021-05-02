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

        internal static List<IndexItem> GetSortedList(List<IndexItem> list, SortOption sortOption)
        {
            switch (sortOption)
            {
                //todo: OrderBy로 새 리스트를 만들지 말고 insert로 정렬하도록 변경할 것
                case SortOption.Title:
                    return new List<IndexItem>(list.OrderBy(i => i.Title, StringComparer.OrdinalIgnoreCase));

                case SortOption.Author:
                    return new List<IndexItem>(list.OrderBy(i => i.Author, StringComparer.OrdinalIgnoreCase));

                case SortOption.Number:
                    return new List<IndexItem>(list.OrderBy(i => int.TryParse(i.Number, out var result) ? result : int.MaxValue));

                case SortOption.Vote:
                    return new List<IndexItem>(list.OrderByDescending(i => i.Vote));

                case SortOption.Page:
                    return new List<IndexItem>(list.OrderBy(i => i.ImgCount));

                case SortOption.Date:
                    Core.ShowDate = true;
                    return new List<IndexItem>(list.OrderByDescending(i => i.Date));

                case SortOption.View:
                    return new List<IndexItem>(list.OrderByDescending(i => i.View));

                case SortOption.LastAccess:
                    Core.ShowLastDate = true;
                    return new List<IndexItem>(list.OrderByDescending(i => i.LastViewDate));

                    //
                case SortOption.RTitle:
                    return new List<IndexItem>(list.OrderByDescending(i => i.Title, StringComparer.OrdinalIgnoreCase));

                case SortOption.RAuthor:
                    return new List<IndexItem>(list.OrderByDescending(i => i.Author, StringComparer.OrdinalIgnoreCase));

                case SortOption.RNumber:
                    return new List<IndexItem>(list.OrderByDescending(i => int.TryParse(i.Number, out var result) ? result : int.MaxValue));

                case SortOption.RVote:
                    return new List<IndexItem>(list.OrderBy(i => i.Vote));

                case SortOption.RPage:
                    return new List<IndexItem>(list.OrderByDescending(i => i.ImgCount));

                case SortOption.RDate:
                    Core.ShowDate = true;
                    return new List<IndexItem>(list.OrderBy(i => i.Date));

                case SortOption.RView:
                    return new List<IndexItem>(list.OrderBy(i => i.View));

                case SortOption.RLastAcess:
                    Core.ShowLastDate = true;
                    return new List<IndexItem>(list.OrderBy(i => i.LastViewDate));
            }

            return null;
        }

        internal void DoSortList(SortOption sortOption)
        {
            //_sender.Scroll.ScrollToTop();
            Core.ShowDate = false;
            Core.ShowLastDate = false;

            var sortedList = GetSortedList(_sender.List, sortOption);

            _sender.List.Clear();
            foreach (var item in sortedList)
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
            Title = -1,
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