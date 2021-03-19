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
            //internal Dictionary<int, IndexItem> Dict;
        }

        public Searcher(ImgLoader sender, List<IndexItem> list)
        {
            _sender = sender;
            _list = list;
        }

        internal void Search(string search, SearchOption option)
        {
            if (List_IsContains(search, SearchList)) return;

            //var removedItem = new Dictionary<int, IndexItem>();

            _sender.Scroll.ScrollToTop();
            _sender.Sorter.ClearSort();
            SearchFrom(_list, search, _list, null, option);

            var temp = new SearchItem {SearchText = search, Option = option};
            SearchList.Add(temp);

            _sender.CondInd.Add(search, ConditionIndicator.Condition.Search, (int)option);
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
            //var option = searchText.Split(':')[0] switch
            //{
            //    "Tag" => SearchOption.Tag,
            //    "Title" => SearchOption.Title,
            //    "Author" => SearchOption.Author,

            //        SearchOption.All => "",
            //    SearchOption.Title => "Title:",
            //    SearchOption.Author => "Author:",
            //    SearchOption.Number => "Number:",
            //    SearchOption.ImgCount => "ImgCount:",
            //    SearchOption.Tag => "Tag:",
            //    _ => "Test:"
            //}
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
            Tag,
            Number,
            ImgCount
        }
    }
}
