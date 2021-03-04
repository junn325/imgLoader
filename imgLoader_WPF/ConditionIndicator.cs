using imgLoader_WPF.Windows;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace imgLoader_WPF
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

            //_sender.CondGrid.Children.Remove((DockPanel)item.Parent);

            switch (item.Text.Split(':')[0])
            {
                case "Search":
                    _sender.Searcher.Remove(item.Text);
                    break;

                case "Sort":
                    _sender.Sorter.ClearSort();
                    break;
            }
        }

        internal enum Condition
        {
            Sort,
            Search
        }
    }
}
