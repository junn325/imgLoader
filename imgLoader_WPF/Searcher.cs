using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgLoader_WPF
{
    internal class Searcher
    {
        internal Dictionary<string, Dictionary<int, IndexItem>> SearchList = new();
        private Windows.ImgLoader _sender;
        private List<IndexItem> _srchInd;
        private List<IndexItem> _destInd;

        public Searcher(Windows.ImgLoader sender, List<IndexItem> searchIndex, List<IndexItem> destIndex)
        {
            _sender = sender;
            _srchInd = searchIndex;
            _destInd = destIndex;
        }

        internal void Search(string search)
        {
            var removedItem = new Dictionary<int, IndexItem>();

            SearchFromAll(_destInd, search, _destInd, removedItem);
            //SearchFromAll(_srchInd, search, _destInd, removedItem);
            SearchList.Add(search, removedItem);

            _sender.CondInd.Add(search, ConditionIndicator.Condition.Search);
        }

        internal void Remove(string search)
        {
            Dictionary<int, IndexItem> remove;

            try
            {
                remove = SearchList[search];
            }
            catch
            {
                return;
            }

            foreach (var (key, value) in remove)
            {
                _sender.List.Insert(key, value);
            }
            _sender.ShowItems.Clear();

            SearchList.Remove(search);
        }

        private void SearchFromAll(List<IndexItem> searchIndex, string search, List<IndexItem> destIndex, Dictionary<int, IndexItem> removeItem)
        {
            var sb = new StringBuilder();

            var temp = new string[searchIndex.Count];
            var searchResult = new List<IndexItem>(searchIndex);

            for (var i = 0; i < searchIndex.Count; i++)
            {
                var item = searchIndex[i];

                sb.Append(item.Author).Append(item.Number).Append(item.SiteName).Append(item.Title);
                foreach (var tag in item.Tags) sb.Append(tag);

                temp[i] = sb.ToString();
                sb.Clear();
            }

            for (var i = 0; i < searchIndex.Count; i++)
            {
                foreach (var srch in search.Split(','))             //검색어 나열
                {
                    if (!temp[i].Contains(srch, StringComparison.OrdinalIgnoreCase))
                    {
                        removeItem.Add(i, searchIndex[i]);
                        searchResult.Remove(searchIndex[i]);
                    }
                }
            }

            destIndex.Clear();
            foreach (var item in searchResult)
            {
                destIndex.Add(item);
            }
        }
    }
}
