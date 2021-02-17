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
        public string[] Tags
        {
            get => _tags;
            set
            {
                _tags = value;

                if (value == null)
                {
                    TagPanel.Children.Clear();
                    return;
                }

                foreach (var tag in value)
                {
                    if (string.IsNullOrEmpty(tag)) return;
                    TagPanel.Children.Add(new TagItem
                    {
                        TagName = tag.Contains(':') ? tag.Split(':')[1] : tag,

                        Sex =
                            tag.Contains(':')
                                ? string.Equals(tag.Split(':')[0], "female", StringComparison.OrdinalIgnoreCase)
                                    ? TagItem.SColor.Female
                                    : TagItem.SColor.Male
                                : TagItem.SColor.None
                    });
                }
            }
        }
        
        private string[] _tags;

        //private readonly Stopwatch sw = new Stopwatch();

        public LoaderItem()
        {
            InitializeComponent();
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((IndexingService.IndexItem)this.DataContext).shownChang = ShownChanged;
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
