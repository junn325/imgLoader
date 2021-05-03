using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using imgL.Windows;

namespace imgL.Services
{
    internal class ConditionIndicator
    {
        internal IndItem SortItem = new() { Condition = Condition.Sort, Content = "", Option = -1, Panel = null };
        internal readonly List<IndItem> SearchList = new();

        private readonly ImgLoader _sender;

        public ConditionIndicator(ImgLoader sender)
        {
            _sender = sender;
        }

        internal void Add(string srchText, Condition cond, int option)
        {
            if (cond == Condition.Sort && option == SortItem.Option)
            {
                var enumLength = Enum.GetValues(typeof(Sorter.SortOption)).Length;
                option += enumLength;

                if (option >= enumLength - 1) option -= (enumLength / 2);
            }

            if (!DoRequest(srchText, cond, option)) return;
            AddIndicator(srchText, cond, option);
        }

        internal void Add(string srchText, Condition cond, int option, string label)
        {
            if (option == SortItem.Option)
            {
                var enumLength = Enum.GetValues(typeof(Sorter.SortOption)).Length;
                option += enumLength;

                if (option >= enumLength - 1) option -= (enumLength / 2);
            }

            if (!DoRequest(srchText, cond, option)) return;
            AddIndicator(srchText, cond, option, label);
        }

        private bool DoRequest(string srchText, Condition cond, int option)
        {
            if (SearchList.Any(indItem => indItem.Content == srchText && indItem.Option == option)) return false;

            switch (cond)
            {
                case Condition.Sort:
                    _sender.Sorter.SortRefresh((Sorter.SortOption)option);
                    break;

                case Condition.Search:
                    _sender.Searcher.SearchRefresh(srchText, (Searcher.SearchOption)option);
                    break;
            }

            return true;
        }

        private void AddIndicator(string srchText, Condition cond, int option, string label)
        {
            var item = new IndItem();

            var tb = new TextBlock
            {
                Text                = label,
                Height              = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Center,
                Margin              = new Thickness(2, 1, 2, 1),
                Padding             = new Thickness(2, 1, 2, 1),
            };

            if (cond == Condition.Search)
            {
                tb.MouseUp += SrchClickHandler;
            }
            else
            {
                tb.MouseUp += SortClickHandler;
            }


            tb.Measure(_sender.PnlCond.DesiredSize);

            item.Panel = new DockPanel
            {
                Margin = new Thickness(2, 1, 2, 1),

                Background = cond switch
                {
                    Condition.Sort   => Brushes.Turquoise,
                    Condition.Search => Brushes.CornflowerBlue,
                    _                => Brushes.Gray
                },
            };

            item.Content   = srchText;
            item.Condition = cond;
            item.Option    = option;

            item.Panel.Children.Add(tb);
            _sender.PnlCond.Children.Add(item.Panel);

            if (cond == Condition.Search)
            {
                SearchList.Add(item);
            }
            else
            {
                _sender.PnlCond.Children.Remove(SortItem.Panel);
                SortItem = item;
            }
        }

        private void AddIndicator(string srchText, Condition cond, int option)
        {
            var item = new IndItem();
            var tag = cond switch
            {
                Condition.Search => option switch
                {
                    -1 => "All:",
                    0  => "Author:",
                    1  => "Number:",
                    2  => "Tag:",
                    3  => "SiteName:",
                    4  => "Title:",
                    5  => "ImgCount:",
                    6  => "Vote:",
                    _  => ""
                },
                Condition.Sort => option switch
                {
                    >= 0 and <= 7  => "Sort:",
                    >= 8 and <= 15 => "Sort:←",
                    _              => ""
                },

                _ => ""
            };
            var tb = new TextBlock
            {
                Text                = tag + srchText,
                Height              = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Center,
                Margin              = new Thickness(2, 1, 2, 1),
                Padding             = new Thickness(2, 1, 2, 1),
            };

            if (cond == Condition.Search)
            {
                tb.MouseUp += SrchClickHandler;
            }
            else
            {
                tb.MouseUp += SortClickHandler;
            }

            tb.Measure(_sender.PnlCond.DesiredSize);

            item.Panel = new DockPanel
            {
                Margin = new Thickness(2, 1, 2, 1),

                Background = cond switch
                {
                    Condition.Sort   => Brushes.Turquoise,
                    Condition.Search => Brushes.CornflowerBlue,
                    _                => Brushes.Gray
                },
            };

            item.Content   = srchText;
            item.Condition = cond;
            item.Option    = option;

            item.Panel.Children.Add(tb);
            _sender.PnlCond.Children.Add(item.Panel);

            if (cond == Condition.Search)
            {
                SearchList.Add(item);
            }
            else
            {
                _sender.PnlCond.Children.Remove(SortItem.Panel);
                SortItem = item;
            }
        }

        private void SortClickHandler(object sender, MouseEventArgs e)
        {
            RemoveSort(SortItem);
        }

        private void SrchClickHandler(object sender, MouseEventArgs e)
        {
            RemoveSrch(SearchList.First(indItem => (TextBlock)indItem.Panel.Children[0] == (TextBlock)sender));
        }

        internal void RemoveSort(IndItem item)
        {
            _sender.PnlCond.Children.Remove(item.Panel);
            SortItem.Option  = -1;
            SortItem.Panel   = null;
            SortItem.Content = "";

            var disableProcessing = _sender.Dispatcher.DisableProcessing();
            _sender.Sorter.SortRefresh(Sorter.SortOption.Title, disableProcessing);
        }

        internal void RemoveSrch(IndItem item)
        {
            _sender.PnlCond.Children.Remove(item.Panel);
            SearchList.Remove(item);

            var disableProcessing = _sender.Dispatcher.DisableProcessing();

            var searchItem = SearchList.Where(indItem => indItem.Condition == Condition.Search).ToArray();
            if (searchItem.Length == 0)
            {
                _sender.List.Clear();

                foreach (var indexItem in _sender.Index)
                {
                    _sender.List.Add(indexItem);
                }
            }
            else
            {
                var searchIndex = Searcher.SearchIndex(_sender.Index);
                var result = Searcher.SearchFrom(_sender.Index, searchIndex, searchItem[0].Content, (Searcher.SearchOption)searchItem[0].Option);

                for (var i = 1; i < searchItem.Length; i++)
                {
                    result = Searcher.SearchFrom(result, searchIndex, searchItem[i].Content, (Searcher.SearchOption)searchItem[i].Option);
                }

                _sender.List.Clear();
                foreach (var indexItem in result)
                {
                    if (indexItem == null) continue;

                    _sender.List.Add(indexItem);
                }
            }

            _sender.Sorter.SortRefresh((Sorter.SortOption)SortItem.Option, disableProcessing);
        }

        internal void Clear()
        {
            var disableProcessing = _sender.Dispatcher.DisableProcessing();

            SearchList.Clear();

            SortItem.Option  = -1;
            SortItem.Panel   = null;
            SortItem.Content = "";

            _sender.PnlCond.Children.Clear();
            _sender.List.Clear();

            for (var i = 0; i < _sender.Index.Count; i++)
            {
                _sender.List.Add(_sender.Index[i]);
            }

            _sender.Sorter.SortRefresh(Sorter.SortOption.Title, disableProcessing);
        }

        internal struct IndItem
        {
            internal string Content;
            internal DockPanel Panel;
            internal Condition Condition;
            internal int Option;
        }

        internal enum Condition
        {
            Sort,
            Search,
        }
    }
}