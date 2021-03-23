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

        public void Add(string label, Condition cond, int option)
        {
            if (IndicatorList.Any(indItem => indItem.Condition == cond && indItem.Content == label && indItem.Option == option)) return;

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
                    _sender.Searcher.Search(label, (Searcher.SearchOption)option);
                    break;
            }
            var item = new IndItem();

            var tag = cond switch
            {
                Condition.Search => option switch
                {
                    0 => "",
                    1 => "Title:",
                    2 => "Author:",
                    3 => "Tag:",
                    4 => "Number:",
                    5 => "ImgCount:",
                    _ => ""
                },
                Condition.Sort => "Sort:",
                _ => ""
            };

            var tb = new TextBlock
            {
                Text = tag + label,
                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment  = VerticalAlignment.Center,
                Margin  = new Thickness(2,1,2,1),
                Padding = new Thickness(2,1,2,1),
            };
            tb.MouseUp += Remove;

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

            item.Content = label;
            item.Condition = cond;
            item.Option = option;

            item.Panel.Children.Add(tb);
            _sender.CondPanel.Children.Add(item.Panel);

            IndicatorList.Add(item);
        }

        public void Remove(object sender, MouseEventArgs e)
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

            _sender.CondPanel.Children.Remove(item.Panel);
            IndicatorList.Remove(item);

            if (item.Condition == Condition.Sort)
            {
                _sender.Sorter.Sort(Sorter.SortOption.Title);
                return;
            }

            if (IndicatorList.Count == 0)
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
            foreach (var indItem in IndicatorList.Where(indItem => indItem.Condition == Condition.Search))
            {
                _sender.Searcher.SearchFrom(_sender.Index, index, indItem.Content, _sender.List, (Searcher.SearchOption)indItem.Option);
            }
            _sender.PgSvc.Paginate();
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
