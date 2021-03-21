using imgLoader_WPF.Services;
using imgLoader_WPF.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace imgLoader_WPF
{
    internal class Searcher
    {
        private const int IndexCount = 6;
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

            var index = SearchIndex(_list);
            SearchFrom(_list, index, search, _list, option);
        }

        //internal void Search(string search, List<IndexItem> where, SearchOption option)
        //{
        //    _sender.Scroll.ScrollToTop();

        //    SearchFrom(where, search, _list, option);
        //}

        internal string[,] SearchIndex(IReadOnlyList<IndexItem> searchFrom)
        {
            var result = new string[searchFrom.Count, IndexCount];
            var sb = new StringBuilder();

            for (var i = 0; i < searchFrom.Count; i++)
            {
                var indexItem = searchFrom[i];
                result[i, 0] = indexItem.Author;
                result[i, 1] = indexItem.Number;

                foreach (var tag in indexItem.Tags) sb.Append(tag).Append(';');
                result[i, 2] = sb.ToString();
                sb.Clear();

                result[i, 3] = indexItem.SiteName;
                result[i, 4] = indexItem.Title;
                result[i, 5] = indexItem.ImgCount;
            }

            return result;
        }

        internal void SearchFrom(List<IndexItem> searchFrom, string[,] index, string search, ICollection<IndexItem> destination, SearchOption option)
        {
            var searchResult = Core.InitializeArray(searchFrom.Count, searchFrom.ToArray());

            switch (option)
            {
                case SearchOption.All:  //이미지 장수는 제외
                    foreach (var srch in search.Split(','))
                    {
                        for (var i = 0; i < index.Length / IndexCount; i++)
                        {
                            if (!index[i, 0].Contains(srch, StringComparison.OrdinalIgnoreCase)
                                && !index[i, 1].Contains(srch, StringComparison.OrdinalIgnoreCase)
                                && !index[i, 2].Contains(srch, StringComparison.OrdinalIgnoreCase)
                                && !index[i, 3].Contains(srch, StringComparison.OrdinalIgnoreCase)
                                && !index[i, 4].Contains(srch, StringComparison.OrdinalIgnoreCase))
                            {
                                searchResult[i] = null;
                            }
                        }
                    }

                    break;
                case SearchOption.Title:
                    foreach (var srch in search.Split(',')) for (var i = 0; i < index.Length / IndexCount; i++) if (!index[i, 4].Contains(srch, StringComparison.OrdinalIgnoreCase)) searchResult[i] = null;
                    break;

                case SearchOption.Author:
                    foreach (var srch in search.Split(',')) for (var i = 0; i < index.Length / IndexCount; i++) if (!index[i, 0].Contains(srch, StringComparison.OrdinalIgnoreCase)) searchResult[i] = null;
                    break;

                case SearchOption.Tag:
                    foreach (var srch in search.Split(',')) for (var i = 0; i < index.Length / IndexCount; i++) if (!index[i, 2].Contains(srch, StringComparison.OrdinalIgnoreCase)) searchResult[i] = null;
                    break;

                case SearchOption.Number:
                    foreach (var srch in search.Split(',')) for (var i = 0; i < index.Length / IndexCount; i++) if (!index[i, 1].Contains(srch, StringComparison.OrdinalIgnoreCase)) searchResult[i] = null;
                    break;

                case SearchOption.ImgCount:
                    foreach (var srch in search.Split(',')) for (var i = 0; i < index.Length / IndexCount; i++) if (!index[i, 5].Contains(srch, StringComparison.OrdinalIgnoreCase)) searchResult[i] = null;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(option), option, null);
            }

            destination.Clear();
            foreach (var item in searchResult)
            {
                if (item == null) continue;
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
