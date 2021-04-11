using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using imgLoader_WPF.Windows;

namespace imgLoader_WPF.Services
{
    internal class ConditionIndicator
    {
        internal readonly List<IndItem> IndicatorList = new();
        private readonly ImgLoader _sender;

        public ConditionIndicator(ImgLoader sender)
        {
            _sender = sender;
        }

        internal void Add(string srchText, Condition cond, int option)
        {
            DoRequest(srchText, cond, option);
            AddIndicator(srchText, cond, option);
        }
        internal void Add(string srchText, Condition cond, int option, string label)
        {
            DoRequest(srchText, cond, option);
            AddIndicator(srchText, cond, option, label);
        }
        private void DoRequest(string srchText, Condition cond, int option)
        {
            if (IndicatorList.Any(indItem => indItem.Condition == cond && indItem.Content == srchText && indItem.Option == option)) return;

            //var disableProcessing = System.Windows.Threading.Dispatcher.CurrentDispatcher.DisableProcessing();
            switch (cond)
            {
                case Condition.Sort:
                    var temp = IndicatorList.Where(i => i.Condition == Condition.Sort).ToArray();
                    if (temp.Length > 0)
                    {
                        _sender.PnlCond.Children.Remove(temp[0].Panel);
                        IndicatorList.Remove(temp[0]);
                    }

                    _sender.Sorter.SortRefresh((Sorter.SortOption)option);
                    break;

                case Condition.Search:
                    _sender.Searcher.SearchRefresh(srchText, (Searcher.SearchOption)option);
                    break;
            }
        }
        private void AddIndicator(string srchText, Condition cond, int option, string label)
        {
            var item = new IndItem();

            var tb = new TextBlock
            {
                Text = label,
                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2, 1, 2, 1),
                Padding = new Thickness(2, 1, 2, 1),
            };
            tb.MouseUp += RemoveHandler;

            tb.Measure(_sender.PnlCond.DesiredSize);

            item.Panel = new DockPanel
            {
                Margin = new Thickness(2, 1, 2, 1),

                Background = cond switch
                {
                    Condition.Sort => Brushes.Turquoise,
                    Condition.Search => Brushes.CornflowerBlue,
                    _ => Brushes.Gray
                },
            };

            item.Content = srchText;
            item.Condition = cond;
            item.Option = option;

            item.Panel.Children.Add(tb);
            _sender.PnlCond.Children.Add(item.Panel);

            IndicatorList.Add(item);
        }
        private void AddIndicator(string srchText, Condition cond, int option)
        {
            var item = new IndItem();
            var tag = cond switch
            {
                Condition.Search => option switch
                {
                    -1 => "All:",
                    0 => "Author:",
                    1 => "Number:",
                    2 => "Tag:",
                    3 => "SiteName:",
                    4 => "Title:",
                    5 => "ImgCount:",
                    6 => "Vote:",
                    _ => ""
                },
                Condition.Sort => "Sort:",
                _ => ""
            };
            var tb = new TextBlock
            {
                Text = tag + srchText,
                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2, 1, 2, 1),
                Padding = new Thickness(2, 1, 2, 1),
            };
            tb.MouseUp += RemoveHandler;

            tb.Measure(_sender.PnlCond.DesiredSize);

            item.Panel = new DockPanel
            {
                Margin = new Thickness(2, 1, 2, 1),

                Background = cond switch
                {
                    Condition.Sort => Brushes.Turquoise,
                    Condition.Search => Brushes.CornflowerBlue,
                    _ => Brushes.Gray
                },
            };

            item.Content = srchText;
            item.Condition = cond;
            item.Option = option;

            item.Panel.Children.Add(tb);
            _sender.PnlCond.Children.Add(item.Panel);

            IndicatorList.Add(item);

        }
        private void RemoveHandler(object sender, MouseEventArgs e)
        {
            var item = new IndItem();
            foreach (var indItem in IndicatorList)
            {
                if ((TextBlock)indItem.Panel.Children[0] == (TextBlock)sender)
                {
                    item = indItem;
                    break;
                }
            }

            Remove(item);
        }

        internal void Remove(IndItem item)
        {
            _sender.PnlCond.Children.Remove(item.Panel);
            IndicatorList.Remove(item);

            var disableProcessing = _sender.Dispatcher.DisableProcessing();

            if (item.Condition == Condition.Sort)
            {
                _sender.Sorter.SortRefresh(Sorter.SortOption.Title, disableProcessing);
                return;
            }

            var searchItem = IndicatorList.Where(indItem => indItem.Condition == Condition.Search).ToArray();
            _sender.ShowItems.Clear();

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
                //todo: 속도를 위해 같은 searchoption끼리 "검색어,검색어,검색어" 식으로 묶어서 넘기는것 구현할것
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

            _sender.Sorter.DoSortList((Sorter.SortOption)IndicatorList.Find(i => i.Condition == Condition.Sort).Option);
            _sender.PgSvc.Paginate(disableProcessing);
            _sender.ShowItemCount();
        }

        internal void Clear()
        {
            foreach (var indItem in new List<IndItem>(IndicatorList))
            {
                Remove(indItem);
            }
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
