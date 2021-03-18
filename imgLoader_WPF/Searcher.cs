using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using imgLoader_WPF.Services;
using imgLoader_WPF.Windows;

namespace imgLoader_WPF
{
    internal class Searcher
    {
        internal List<SearchItem> SearchList = new();
        private readonly ImgLoader _sender;
        private readonly List<IndexItem> _list;

        internal struct SearchItem
        {
            internal string SearchText;
            internal SearchOption Option;
            internal Dictionary<int, IndexItem> Dict;
        }

        public Searcher(ImgLoader sender, List<IndexItem> list)
        {
            _sender = sender;
            _list = list;
        }

        internal void Search(string search, SearchOption option)
        {
            if (List_IsContains(search, SearchList)) return;

            var removedItem = new Dictionary<int, IndexItem>();

            _sender.Scroll.ScrollToTop();
            _sender.Sorter.ClearSort();
            SearchFrom(_list, search, _list, removedItem, option);

            var temp = new SearchItem {SearchText = search, Option = option, Dict = removedItem};
            SearchList.Add(temp);

            var label = option switch
            {
                SearchOption.All => "",
                SearchOption.Title => "Title:",
                SearchOption.Author => "Author:",
                SearchOption.Number => "Number:",
                SearchOption.ImgCount => "ImgCount:",
                _ => "Test:"
            };

            _sender.CondInd.Add(label + search, ConditionIndicator.Condition.Search, (int)option);
        }

        internal void Remove(ConditionIndicator.IndItem search)
        {
            var result = new List<IndexItem>(_sender.Index);

            List_Remove(search.Content, SearchList);
            if (SearchList.Count != 0)
            {
                var sb = new StringBuilder();

                foreach (var item in SearchList)
                {
                    var temp = item.SearchText.Split(':');
                    if (temp.Length == 1)   //재탐색할 항목이 "모든 항목에서 검색" 일 경우
                    {
                        SearchFrom(result, item.SearchText, result, null, SearchOption.All);
                    }
                    else
                    {

                    }
                }
            }

            _sender.List.Clear();
            foreach (var item in result)
            {
                _sender.List.Add(item);
            }

            _sender.ShowItems.Clear();
            _sender.PgSvc.Paginate();
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
                        sb.Append(item.Author).Append(item.Number).Append(item.SiteName).Append(item.Title);
                        foreach (var tag in item.Tags) sb.Append(tag);
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
                }
            }

            for (var i = 0; i < searchFrom.Count; i++)
            {
                foreach (var srch in search.Split(','))
                {
                    if (!temp[i].Contains(srch, StringComparison.OrdinalIgnoreCase))
                    {
                        list_CopyRemoved?.Add(i, searchFrom[i]);
                        searchResult.Remove(searchFrom[i]);
                    }
                }
            }

            destination.Clear();
            foreach (var item in searchResult)
            {
                destination.Add(item);
            }
        }

        private SearchItem List_SearchForText(string searchText, SearchOption option, List<SearchItem> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i].SearchText == searchText && list[i].Option == option) return list[i];
            }

            return new SearchItem { SearchText = null };
        }

        private bool List_IsContains(string searchText, List<SearchItem> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i].SearchText == searchText) return true;
            }

            return false;
        }

        private void List_Remove(string searchText, List<SearchItem> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i].SearchText == searchText) list.RemoveAt(i);
            }
        }
        internal enum SearchOption
        {
            All,
            Title,
            Author,
            Number,
            ImgCount
        }
    }
}
