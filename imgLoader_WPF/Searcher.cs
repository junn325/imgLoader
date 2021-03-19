using imgLoader_WPF.Services;
using imgLoader_WPF.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace imgLoader_WPF
{
    internal class Searcher
    {
        private readonly ImgLoader _sender;
        private readonly List<IndexItem> _list;

        public Searcher(ImgLoader sender, List<IndexItem> list)
        {
            _sender = sender;
            _list = list;
        }

        internal void Search(string search, SearchOption option)
        {
            _sender.Scroll.ScrollToTop();

            SearchFrom(_list, search, _list, null, option);
        }

        internal void Search(string search, List<IndexItem> where, SearchOption option)
        {
            _sender.Scroll.ScrollToTop();

            SearchFrom(where, search, _list, null, option);
        }

        private void SearchFrom(IReadOnlyList<IndexItem> searchFrom, string search, ICollection<IndexItem> destination, Dictionary<int, IndexItem> list_CopyRemoved, SearchOption option)
        {
            var sb = new StringBuilder();

            var temp = new string[searchFrom.Count];
            var searchResult = new List<IndexItem>(searchFrom);

            for (var i = 0; i < searchFrom.Count; i++)
            {
                var item = searchFrom[i];

                switch (option)
                {
                    case SearchOption.All:
                        sb.Append(item.Author).Append(';').Append(item.Number).Append(';').Append(item.SiteName).Append(';').Append(item.Title).Append(';');
                        foreach (var tag in item.Tags) sb.Append(tag).Append(';');
                        temp[i] = sb.ToString();
                        sb.Clear();
                        break;

                    case SearchOption.Author:
                        temp[i] = item.Author;
                        break;

                    case SearchOption.Number:
                        temp[i] = item.Number;
                        break;

                    case SearchOption.ImgCount:
                        temp[i] = item.ImgCount;
                        break;

                    case SearchOption.Title:
                        temp[i] = item.Title;
                        break;

                    case SearchOption.Tag:
                        foreach (var tag in item.Tags) sb.Append(tag).Append(';');
                        temp[i] = sb.ToString();
                        sb.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(option), option, null);
                }
            }

            foreach (var srch in search.Split(','))
            {
                for (var i = 0; i < searchFrom.Count; i++)
                {
                    if (!temp[i].Contains(srch, StringComparison.OrdinalIgnoreCase))
                    {
                        list_CopyRemoved?.Add(i, searchFrom[i]);
                        searchResult.Remove(searchFrom[i]);
                    }
                    else
                    {
                        ;
                    }
                }
            }

            destination.Clear();
            foreach (var item in searchResult)
            {
                destination.Add(item);
            }
        }

        internal enum SearchOption
        {
            All,
            Title,
            Author,
            Tag,
            Number,
            ImgCount
        }
    }
}
