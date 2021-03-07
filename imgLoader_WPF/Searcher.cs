using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using imgLoader_WPF.Windows;

namespace imgLoader_WPF
{
    internal class Searcher
    {
        internal Dictionary<string, Dictionary<int, IndexItem>> SearchList = new();
        private readonly ImgLoader _sender;
        private readonly List<IndexItem> _list;

        public Searcher(ImgLoader sender, List<IndexItem> list)
        {
            _sender = sender;
            _list = list;
        }

        internal void Search(string search)
        {
            if (SearchList.ContainsKey(search)) return;

            var removedItem = new Dictionary<int, IndexItem>();

            _sender.Scroll.ScrollToTop();
            _sender.Sorter.ClearSort();
            SearchFromAll(_list, search, _list, removedItem);
            SearchList.Add(search, removedItem);

            _sender.CondInd.Add(search, ConditionIndicator.Condition.Search);
        }

        internal void Remove(string search)
        {
            var searchTxt = search.Replace("Search:", "");

            Dictionary<int, IndexItem> removed;
            try
            {
                removed = SearchList[searchTxt];
            }
            catch
            {
                return;
            }

            SearchList.Remove(searchTxt);

            if (SearchList.Count != 0)
            {
                var searches = SearchList.Keys.ToArray();
                foreach (var s in searches)
                {
                    removed = removed.Where(i => i.Value.)
                }
            }

            foreach (var (key, value) in removed)
            {
                if (_sender.List.Count < key)
                {
                    _sender.List.Add(value);
                    continue;
                }

                _sender.List.Insert(key, value);
            }
            _sender.ShowItems.Clear();
            _sender.PgSvc.Paginate();
        }

        //todo:void Search() 를 구현한 후 SearchFromAll이 그것을 호출하는것으로 변경할 것

        private void SearchFromAll(List<IndexItem> searchFrom, string search, ICollection<IndexItem> destination, Dictionary<int, IndexItem> removeItem)
        {
            var sb = new StringBuilder();

            var temp = new string[searchFrom.Count];
            var searchResult = new List<IndexItem>(searchFrom);

            for (var i = 0; i < searchFrom.Count; i++)
            {
                var item = searchFrom[i];

                sb.Append(item.Author).Append(item.Number).Append(item.SiteName).Append(item.Title);
                foreach (var tag in item.Tags) sb.Append(tag);

                temp[i] = sb.ToString();
                sb.Clear();
            }

            for (var i = 0; i < searchFrom.Count; i++)
            {
                foreach (var srch in search.Split(','))
                {
                    if (!temp[i].Contains(srch, StringComparison.OrdinalIgnoreCase))
                    {
                        removeItem.Add(i, searchFrom[i]);
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
    }
}
