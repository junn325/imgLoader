using imgLoader_WPF.Windows;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace imgLoader_WPF
{
    internal class Sorter
    {
        internal SortOption Option = SortOption.Title;
        internal bool IsSorting;

        private readonly ImgLoader _sender;
        private List<IndexItem> _oriItem;
        private readonly List<IndexItem> _list;

        public Sorter(ImgLoader sender, List<IndexItem> list)
        {
            _list = list;
            _sender = sender;
        }

        internal void Sort(SortOption sortOption)
        {
            ClearSort();

            _oriItem = new List<IndexItem>(_list);

            IsSorting = true;
            Option = sortOption;

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
                    temp = new List<IndexItem>(_list.OrderBy(i => i.Date, StringComparer.OrdinalIgnoreCase));
                    break;

                default:
                    return;
            }

            _list.Clear();

            foreach (var item in temp)
            {
                _list.Add(item);
            }

            _sender.CondInd.Add(
                sortOption switch
                {
                    SortOption.Number => "Number",
                    SortOption.Page => "Page",
                    SortOption.Title => "Title",
                    SortOption.Author => "Author",
                    SortOption.Date => "Date",
                    _ => " -Error"
                }, ConditionIndicator.Condition.Sort);
        }

        internal bool ClearSort()
        {
            if (_oriItem == null) return false;

            _list.Clear();
            _sender.ShowItems.Clear();

            foreach (var item in _oriItem)
            {
                _list.Add(item);
            }

            var temp = new DockPanel[_sender.CondPanel.Children.Count];
            _sender.CondPanel.Children.CopyTo(temp, 0);

            foreach (DockPanel item in temp)
            {
                if (!((TextBlock)item.Children[0]).Text.Contains("Sort")) continue;

                _sender.CondPanel.Children.Remove(item);
            }

            IsSorting = false;
            Option = SortOption.Null;
            _oriItem = null;

            return true;
        }

        internal enum SortOption
        {
            Number,
            Page,
            Title,
            Author,
            Date,
            Null
        }
    }
}