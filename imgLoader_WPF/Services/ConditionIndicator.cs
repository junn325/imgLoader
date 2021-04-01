using System;
using System.Collections.Generic;
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
            if (IndicatorList.Any(indItem => indItem.Condition == cond && indItem.Content == srchText && indItem.Option == option)) return;

            switch (cond)
            {
                case Condition.Sort:
                    var temp = IndicatorList.Where(i => i.Condition == Condition.Sort).ToArray();
                    if (temp.Length > 0)
                    {
                        _sender.CondPanel.Children.Remove(temp[0].Panel);
                        IndicatorList.Remove(temp[0]);
                    }

                    _sender.Sorter.Sort((Sorter.SortOption)option);
                    break;
                case Condition.Search:
                    _sender.Searcher.Search(srchText, (Searcher.SearchOption)option);
                    break;
            }
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
                VerticalAlignment  = VerticalAlignment.Center,
                Margin  = new Thickness(2,1,2,1),
                Padding = new Thickness(2,1,2,1),
            };
            tb.MouseUp += RemoveHandler;

            tb.Measure(_sender.CondPanel.DesiredSize);

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
            _sender.CondPanel.Children.Add(item.Panel);

            IndicatorList.Add(item);
        }
        internal void Add(string srchText, Condition cond, int option, string label)
        {
            if (IndicatorList.Any(indItem => indItem.Condition == cond && indItem.Content == srchText && indItem.Option == option)) return;

            switch (cond)
            {
                case Condition.Sort:
                    var temp = IndicatorList.Where(i => i.Condition == Condition.Sort).ToArray();
                    if (temp.Length > 0)
                    {
                        _sender.CondPanel.Children.Remove(temp[0].Panel);
                        IndicatorList.Remove(temp[0]);
                    }

                    _sender.Sorter.Sort((Sorter.SortOption)option);
                    break;
                case Condition.Search:
                    _sender.Searcher.Search(srchText, (Searcher.SearchOption)option);
                    break;
            }
            var item = new IndItem();

            var tb = new TextBlock
            {
                Text = label,
                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment  = VerticalAlignment.Center,
                Margin  = new Thickness(2,1,2,1),
                Padding = new Thickness(2,1,2,1),
            };
            tb.MouseUp += RemoveHandler;

            tb.Measure(_sender.CondPanel.DesiredSize);

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
            _sender.CondPanel.Children.Add(item.Panel);

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
            _sender.CondPanel.Children.Remove(item.Panel);
            IndicatorList.Remove(item);

            if (item.Condition == Condition.Sort)
            {
                _sender.Sorter.Sort(Sorter.SortOption.Title);
                return;
            }

            _sender.Sorter.Sort((Sorter.SortOption)IndicatorList.Find(i => i.Condition == Condition.Sort).Option);

            var searchItem = IndicatorList.Where(indItem => indItem.Condition == Condition.Search).ToArray();
            if (searchItem.Length == 0)
            {
                _sender.List.Clear();
                _sender.ShowItems.Clear();

                foreach (var indexItem in _sender.Index)
                {
                    _sender.List.Add(indexItem);
                }

                _sender.PgSvc.Paginate();

                return;
            }

            _sender.ShowItems.Clear();
            //var tempList = new List<IndItem>(_list);

            //todo: 속도를 위해 같은 searchoption끼리 "검색어,검색어,검색어" 식으로 묶어서 넘기는것 구현할것
            var index = _sender.Searcher.SearchIndex(_sender.Index);
            foreach (var indItem in searchItem)
            {
                _sender.Searcher.SearchFrom(_sender.Index, index, indItem.Content, _sender.List, (Searcher.SearchOption)indItem.Option);
            }

            _sender.PgSvc.Paginate();
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
