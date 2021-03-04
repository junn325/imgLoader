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
                Text = label,
                //Width = ,
                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment  = VerticalAlignment.Center,
                Margin  = new Thickness(2,1,2,1),
                Padding = new Thickness(2,1,2,1),
            };

            tb.MouseUp += Remove;

            //var bdr = new Border
            //{
            //    //Margin = new Thickness(2, 1, 2, 1),
            //    //Child = tb,
            //    BorderBrush = Brushes.Red,
            //    BorderThickness = new Thickness(0.1),
            //    CornerRadius = new CornerRadius(0.01)
            //};

            tb.Measure(_sender.CondGrid.DesiredSize);

            //var close = new Button
            //{
            //    Style = _sender.FindResource(ToolBar.ButtonStyleKey) as Style,
            //    Content = "x",
            //    Width = 15,
            //    Height = 15,
            //    FontSize = 12,
            //    HorizontalAlignment = HorizontalAlignment.Right,
            //    VerticalAlignment = VerticalAlignment.Center,
            //    Margin = new Thickness(2, 1, 2, 1),
            //    Padding = new Thickness(0)
            //};

            var item = new DockPanel
            {
                Margin = new Thickness(2, 1, 2, 1),

                //OpacityMask = new VisualBrush(bdr),
                Background = cond switch
                {
                    Condition.Sort => Brushes.Turquoise,
                    Condition.Search => Brushes.CornflowerBlue,
                    _ => Brushes.Gray
                },
            };

            item.Children.Add(tb);
            //item.Children.Add(close);

            //var b = new Border
            //{
            //    Margin = new Thickness(2, 1, 2, 1),
            //    Child = item,
            //    //BorderBrush = Brushes.Red,
            //    //BorderThickness = new Thickness(1),
            //    //CornerRadius = new CornerRadius(5)
            //};

            _sender.CondGrid.Children.Add(item);
            //_list.Add(label, item);
        }

        public void Remove(object sender, MouseEventArgs e)
        {
            var item = (TextBlock)sender;

            //_list.Remove(item.Text, out _);
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
