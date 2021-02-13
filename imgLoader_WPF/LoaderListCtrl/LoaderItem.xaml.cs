using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using imgLoader_WPF.Tag;

namespace imgLoader_WPF.LoaderListCtrl
{
    public partial class LoaderItem
    {
        public string Title
        {
            get => TitleBlock.Text;
            set => TitleBlock.Text = value;
        }
        public string Author
        {
            get => AuthorBlock.Text;
            set => AuthorBlock.Text = value;
        }
        public string ImgCount
        {
            get => CountBlock.Text;
            set => CountBlock.Text = value + "장";
        }
        public string SiteName
        {
            get => SiteBlock.Text;
            set => SiteBlock.Text = value;
        }

        public string Route;
        public string Number
        {
            get => NumBlock.Text;
            set => NumBlock.Text = value;
        }
        public int CurrentCount
        {
            get => _curCnt;
            set
            {
                _curCnt = value;
                ProgLbl.Text = $"{value}/{ImgCount}";
            }
        }
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

        public int Vote
        {
            get => _vote;
            set
            {
                _vote = value;
                LblVote.Text = _vote.ToString();
            }
        }

        public bool IsRead
        {
            get => _isRead;
            set
            {
                _isRead = value;
                Background = IsRead ? Brushes.LightGray : Brushes.White;
            }
        }

        private int _vote;
        private bool _isRead;
        private int _curCnt;
        private string[] _tags;

        //private readonly Stopwatch sw = new Stopwatch();

        public LoaderItem()
        {
            InitializeComponent();
        }
        public LoaderItem(string title, string author, string count, string site, string route, string number, int vote)
        {
            InitializeComponent();

            Title = title;
            Author = author;
            ImgCount = count;
            SiteName = site;
            Route = route;
            Number = number;
            Vote = vote;
        }
        private void UpVote_Click(object sender, RoutedEventArgs e)
        {
            Vote++;
        }
        private void DownVote_Click(object sender, RoutedEventArgs e)
        {
            Vote--;
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            TagPanel.Dispatcher.Invoke(() => TagPanel.Children.Clear());
            Tags = null;
            Title = null;
            Author = null;
            ImgCount = null;
            SiteName = null;
            Route = null;
            Number = null;

            _tags = null;
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender == null) return;
            if (Properties.Settings.Default.NoScrollTag)
            {
                if (e.Delta > 0)
                    ((ScrollViewer)((LoaderList)((LoaderItem)((Border)((Grid)((ScrollViewer)sender).Parent).Parent).Parent).Parent).Parent).LineUp();
                else
                    ((ScrollViewer)((LoaderList)((LoaderItem)((Border)((Grid)((ScrollViewer)sender).Parent).Parent).Parent).Parent).Parent).LineDown();

                e.Handled = true;
                return;
            }

            if (sender is not ScrollViewer sv) return;

            if (e.Delta > 0)
                sv.LineLeft();
            else
                sv.LineRight();
            e.Handled = true;
        }
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TitleBlock.MaxWidth = ActualWidth - NumBlock.ActualWidth - 15;
            AuthorBlock.MaxWidth = ActualWidth - 10;
            //TagPanel.MaxWidth = ActualWidth - 105;
        }
        public void MyDispose()
        {
            Tags = null;
            Title = null;
            Author = null;
            ImgCount = null;
            SiteName = null;
            Route = null;
            Number = null;

            _tags = null;
            _curCnt = 0;
            _isRead = false;
            _vote = 0;
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
