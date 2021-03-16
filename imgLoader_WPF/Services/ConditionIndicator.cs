﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using imgLoader_WPF.Windows;

namespace imgLoader_WPF.Services
{
    internal class ConditionIndicator
    {
        //private Dictionary<string, DockPanel> _list = new();
        private readonly ImgLoader _sender;

        public ConditionIndicator(ImgLoader sender)
        {
            _sender = sender;
        }

        public void Add(string label, Condition cond)
        {
            var tb = new TextBlock
            {
                Text = label,
                    //cond == Condition.Search
                    //    ? $"Search:{label}"
                    //    : $"Sort:{label}",

                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment  = VerticalAlignment.Center,
                Margin  = new Thickness(2,1,2,1),
                Padding = new Thickness(2,1,2,1),
            };
            tb.MouseUp += Remove;

            tb.Measure(_sender.CondPanel.DesiredSize);

            var item = new DockPanel
            {
                Margin = new Thickness(2, 1, 2, 1),

                Background = cond switch
                {
                    Condition.Sort => Brushes.Turquoise,
                    Condition.Search => Brushes.CornflowerBlue,
                    _ => Brushes.Gray
                },
            };

            item.Children.Add(tb);
            _sender.CondPanel.Children.Add(item);
        }

        public void Remove(object sender, MouseEventArgs e)
        {
            var item = (TextBlock)sender;
            var panel = ((DockPanel)item.Parent).Background;
            var cond = Condition.Null;

            if (panel == Brushes.Turquoise) cond = Condition.Sort;
            if (panel == Brushes.CornflowerBlue) cond = Condition.Search;

            switch (cond)
            {
                case Condition.Search:
                    _sender.Sorter.ClearSort();
                    _sender.CondPanel.Children.Remove((DockPanel)item.Parent);
                    _sender.Searcher.Remove(item.Text);
                    break;

                case Condition.Sort:
                    if(!_sender.Sorter.ClearSort()) _sender.CondPanel.Children.Remove((DockPanel)item.Parent);
                    break;
            }
        }

        internal enum Condition
        {
            Sort,
            Search,
            Null
        }
    }
}