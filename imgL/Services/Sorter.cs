﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

using imgL.Windows;

using Dispatcher = System.Windows.Threading.Dispatcher;

namespace imgL.Services
{
    internal class Sorter
    {
        private readonly ImgL _sender;

        public Sorter(ImgL sender)
        {
            _sender = sender;
        }

        internal static List<IndexItem> GetSortedList(IEnumerable<IndexItem> list, SortOption sortOption)
        {
            switch (sortOption)
            {
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
                    return new List<IndexItem>(list.OrderByDescending(i => i.Date));

                case SortOption.View:
                    return new List<IndexItem>(list.OrderByDescending(i => i.View));

                case SortOption.LastAccess:
                    Core.ShowDate     = false;
                    Core.ShowLastDate = true;
                    return new List<IndexItem>(list.OrderByDescending(i => i.LastViewDate));

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
                    return new List<IndexItem>(list.OrderBy(i => i.Date));

                case SortOption.RView:
                    return new List<IndexItem>(list.OrderBy(i => i.View));

                case SortOption.RLastAcess:
                    Core.ShowDate     = false;
                    Core.ShowLastDate = true;
                    return new List<IndexItem>(list.OrderBy(i => i.LastViewDate));
            }

            return null;
        }

        private void DoSortList(SortOption sortOption)
        {
            _sender.Scroll.ScrollToTop();
            Core.ShowDate     = true;
            Core.ShowLastDate = false;

            var sortedList = GetSortedList(_sender.List, sortOption);

            if (sortedList == null) return;

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