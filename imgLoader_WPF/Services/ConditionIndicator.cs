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
        private readonly List<IndItem> _list = new();
        private readonly ImgLoader _sender;

        public ConditionIndicator(ImgLoader sender)
        {
            _sender = sender;
        }

        public void Add(string label, Condition cond, int option)
        {
            if (_list.Any(indItem => indItem.Condition == cond && indItem.Content == label && indItem.Option == option)) return;

            switch (cond)
            {
                case Condition.Sort:
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

            _list.Add(item);
        }

        public void Remove(object sender, MouseEventArgs e)
        {
            var item = new IndItem();
            foreach (var indItem in _list)
            {
                if ((TextBlock)indItem.Panel.Children[0] == (TextBlock)sender)
                {
                    item = indItem;
                    break;
                }
            }

            _sender.CondPanel.Children.Remove(item.Panel);
            _list.Remove(item);

            if (_list.Count == 0)
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
            var tempList = new List<IndItem>(_list);

            //todo: 속도를 위해 같은 searchoption끼리 "검색어,검색어,검색어" 식으로 묶어서 넘기는것 구현할것
            var searchText = "";
            foreach (var indItem in tempList.Where(indItem => indItem.Condition == Condition.Search))
            {
                _sender.Searcher.Search(indItem.Content, (Searcher.SearchOption)indItem.Option);
            }

            foreach (var indItem in tempList.Where(indItem => indItem.Condition == Condition.Sort))
            {
                _sender.Sorter.Sort((Sorter.SortOption)indItem.Option);
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
