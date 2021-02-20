using imgLoader_WPF.Tag;

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

        //private readonly Stopwatch sw = new Stopwatch();

        public LoaderItem()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((IndexingService.IndexItem)this.DataContext).ShownChang = ShownChanged;
            TitleBlock.Width = this.ActualWidth - NumBlock.ActualWidth - 10;
            AuthorBlock.Width = this.ActualWidth - VoteGrid.ActualWidth - 10;

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

        private void ShownChanged()
        {
            Background = ((IndexingService.IndexItem)this.DataContext).IsRead ? Brushes.LightGray : Brushes.White;
        }

        private void UpVote_Click(object sender, RoutedEventArgs e)
        {
            ((IndexingService.IndexItem)this.DataContext).Vote++;
        }
        private void DownVote_Click(object sender, RoutedEventArgs e)
        {
            ((IndexingService.IndexItem)this.DataContext).Vote--;
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
            TitleBlock.MaxWidth = ActualWidth - NumBlock.ActualWidth - 15;
            AuthorBlock.MaxWidth = ActualWidth - 10;
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
