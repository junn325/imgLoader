using imgLoader_WPF.Services;
using imgLoader_WPF.Windows;
using System;
using System.Collections.Generic;
using System.Linq;

namespace imgLoader_WPF
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

        internal void Sort(SortOption sortOption)
        {
            //_sender.Scroll.ScrollToTop();

            List<IndexItem> temp;
            switch (sortOption)
            {
                case SortOption.Number:
                    temp = new List<IndexItem>(_list.OrderBy(i => int.TryParse(i.Number, out var result) ? result : int.MaxValue));
                    break;
                case SortOption.Title:
                    temp = new List<IndexItem>(_list.OrderBy(i => i.Title, StringComparer.OrdinalIgnoreCase));
                    break;
                case SortOption.Page:
                    temp = new List<IndexItem>(_list.OrderBy(i => int.TryParse(i.ImgCount, out var result) ? result : int.MaxValue));
                    break;
                case SortOption.Author:
                    temp = new List<IndexItem>(_list.OrderBy(i => i.Author, StringComparer.OrdinalIgnoreCase));
                    break;
                case SortOption.Date:
                    temp = new List<IndexItem>(_list.OrderByDescending(i => i.Date, StringComparer.OrdinalIgnoreCase));
                    break;

                default:
                    return;
            }

            _list.Clear();

            foreach (var item in temp)
            {
                _list.Add(item);
            }

            if (!_sender.Dispatcher.CheckAccess())
            {
                _sender.Dispatcher.Invoke(() => _sender.ShowItems.Clear());
            }
            else
            {
                _sender.ShowItems.Clear();
            }
            _sender.PgSvc.Paginate();
        }

        internal enum SortOption
        {
            Title,
            Number,
            Page,
            Author,
            Date
        }
    }
}