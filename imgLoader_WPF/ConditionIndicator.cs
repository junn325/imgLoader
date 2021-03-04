using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace imgLoader_WPF
{
    internal class ConditionIndicator
    {
        private Dictionary<string, DockPanel> _list = new();
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
                Text = 
                    cond == Condition.Search
                        ? $"Search:{label}"
                        : $"Sort:{label}",

                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment  = VerticalAlignment.Center,
                Margin  = new Thickness(2,1,2,1),
                Padding = new Thickness(2,1,2,1),
            };
            tb.MouseUp += Remove;

            tb.Measure(_sender.CondGrid.DesiredSize);

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
            _sender.CondGrid.Children.Add(item);
        }

        public void Remove(object sender, MouseEventArgs e)
        {
            var item = (TextBlock)sender;

            _sender.CondGrid.Children.Remove((DockPanel)item.Parent);
            _sender.Searcher.Remove(item.Text);
        }

        internal enum Condition
        {
            Sort,
            Search
        }
    }
}
