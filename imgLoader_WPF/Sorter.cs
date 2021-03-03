using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace imgLoader_WPF
{
    public class Sorter
    {
        internal SortOption Option = SortOption.Title;
        internal bool IsSorting;
        internal void Sort(List<IndexItem> collection, SortOption sortOption)
        {
            Option = sortOption;
            ObservableCollection<IndexItem> temp;
            switch (sortOption)
            {
                case SortOption.Number:
                    temp = new ObservableCollection<IndexItem>(collection.OrderBy(i => int.TryParse(i.Number, out var result) ? result : int.MaxValue));
                    break;
                case SortOption.Title:
                    temp = new ObservableCollection<IndexItem>(collection.OrderBy(i => i.Title, StringComparer.OrdinalIgnoreCase));
                    break;
                case SortOption.Page:
                    temp = new ObservableCollection<IndexItem>(collection.OrderBy(i => int.TryParse(i.ImgCount, out var result) ? result : int.MaxValue));
                    break;
                case SortOption.Author:
                    temp = new ObservableCollection<IndexItem>(collection.OrderBy(i => i.Author, StringComparer.OrdinalIgnoreCase));
                    break;

                default:
                    return;
            }

            collection.Clear();

            foreach (var item in temp)
            {
                collection.Add(item);
            }
        }

        internal void ClearSort()
        {

        }

        internal enum SortOption
        {
            Number,
            Page,
            Title,
            Author
        }
    }
}