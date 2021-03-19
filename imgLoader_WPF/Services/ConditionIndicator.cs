﻿using System.Collections.Generic;
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
            var item = new IndItem();

            var tag = cond switch
            {
                Condition.Search => option switch
                {
                    0 => "",
                    1 => "Title:",
                    2 => "Author:",
                    3 => "Number:",
                    4 => "ImgCount:",
                    5 => "Tag:",
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
                if ((TextBlock)indItem.Panel.Children[0] == (TextBlock)sender) item = indItem;
            }

            var panel = item.Panel.Background;
            var cond = Condition.Null;

            if (panel == Brushes.Turquoise) cond = Condition.Sort;
            if (panel == Brushes.CornflowerBlue) cond = Condition.Search;

            switch (cond)
            {
                case Condition.Search:
                    _sender.Sorter.ClearSort();
                    _sender.CondPanel.Children.Remove(item.Panel);
                    _sender.Searcher.Remove(item);
                    _list.Remove(item);
                    break;

                case Condition.Sort:
                    if(!_sender.Sorter.ClearSort()) _sender.CondPanel.Children.Remove(item.Panel);
                    _list.Remove(item);
                    break;
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
            Null
        }
    }
}
