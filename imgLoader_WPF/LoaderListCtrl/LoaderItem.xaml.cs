using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace imgLoader_WPF.LoaderListCtrl
{
    public partial class LoaderItem
    {
        public string[] Tags;

        private int _progMax;
        private int _progVal;

        //private readonly Stopwatch sw = new Stopwatch();

        public LoaderItem()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((IndexItem)DataContext).ShownChang = () => Background = ((IndexItem)DataContext).IsRead ? Brushes.LightGray : Brushes.White;

            ((IndexItem)DataContext).ProgPanelHide = () => Dispatcher.Invoke(() => ProgPanel.Visibility = Visibility.Hidden);
            ((IndexItem)DataContext).ProgPanelShow = () => Dispatcher.Invoke(() => ProgPanel.Visibility = Visibility.Visible);

            ((IndexItem)DataContext).TagPanelHide = () => Dispatcher.Invoke(() => TagPanel.Visibility = Visibility.Hidden);
            ((IndexItem)DataContext).TagPanelShow = () => Dispatcher.Invoke(() => TagPanel.Visibility = Visibility.Visible);

            ((IndexItem)DataContext).ProgBarMax = value => Dispatcher.Invoke(() =>
            {
                _progMax = value;
                ProgBlock.Text = $"0/{value}";
                ProgBar.Maximum = value;
            });
            ((IndexItem)DataContext).ProgBarVal = () => Dispatcher.Invoke(() =>
            {
                _progVal++;
                ProgBar.Value++;
                ProgBlock.Text = $"{_progVal}/{_progMax}";
            });

            if (Tags == null) return;
            foreach (var tag in Tags)
            {
                if (string.IsNullOrEmpty(tag)) return;
                TagPanel.Children.Add(new TextBlock
                {
                    Text = tag.Contains(':') ? tag.Split(':')[1] : tag,

                    Background =
                        tag.Contains(':')
                            ? string.Equals(tag.Split(':')[0], "female", StringComparison.OrdinalIgnoreCase)
                                ? (Brush)new BrushConverter().ConvertFrom("#E86441")
                                : (Brush)new BrushConverter().ConvertFrom("#00A2FF")
                            : (Brush)new BrushConverter().ConvertFrom("#838587")
                });
            }
        }

        private void UpVote_Click(object sender, RoutedEventArgs e)
        {
            ((IndexItem)DataContext).Vote++;
        }
        private void DownVote_Click(object sender, RoutedEventArgs e)
        {
            ((IndexItem)DataContext).Vote--;
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender == null) return;
            var temp = ((ScrollViewer)sender);
            if (Properties.Settings.Default.NoScrollTag && temp.IsEnabled)
            {
                temp.IsEnabled = false;
                return;
            }

            if(!temp.IsEnabled) temp.IsEnabled = true;

            if (e.Delta > 0)
                temp.LineLeft();
            else
                temp.LineRight();

            e.Handled = true;
        }
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TitleBlock.Width = ActualWidth - NumBlock.ActualWidth - 10;
            AuthorBlock.Width = ActualWidth - VoteGrid.ActualWidth - 10;
        }

        private void NumBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TitleBlock.Width = ActualWidth - NumBlock.ActualWidth - 10;
            AuthorBlock.Width = ActualWidth - VoteGrid.ActualWidth - 10;
        }
    }

    public class ValConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((double)value < 50) return 0;

            return (double)value - 50;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
