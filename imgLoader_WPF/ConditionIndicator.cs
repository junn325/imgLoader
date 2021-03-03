using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace imgLoader_WPF
{
    internal class ConditionIndicator
    {
        private List<Border> _list = new();
        private Windows.ImgLoader _sender;
        //private Rectangle _item;
        public ConditionIndicator(Windows.ImgLoader sender)
        {
            _sender = sender;
        }

        public void Add(string label, Condition cond)
        {
            var tb = new TextBlock
            {
                Text = label,
                //Width = ,
                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment  = VerticalAlignment.Center,
                Margin  = new Thickness(2,1,2,1),
                Padding = new Thickness(2,1,2,1)
            };

            tb.Measure(_sender.CondGrid.DesiredSize);

            var close = new Button
            {
                Style = _sender.FindResource(ToolBar.ButtonStyleKey) as Style,
                Content = "x",
                Width = 15,
                Height = 15,
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2, 1, 2, 1),
                Padding = new Thickness(0)
            };

            var item = new DockPanel
            {
                Background = cond switch
                {
                    Condition.Sort => Brushes.Turquoise,
                    Condition.Search => Brushes.Azure,
                    _ => Brushes.Gray
                },
            };

            item.Children.Add(tb);
            item.Children.Add(close);

            var b = new Border { Child = item, CornerRadius = new CornerRadius(500) };

            _sender.CondGrid.Children.Add(b);
            _list.Add(b);
        }

        internal enum Condition
        {
            Sort,
            Search
        }
    }
}
