using System;
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
            get => TxbTitle.Text;
            set => TxbTitle.Text = value;
        }
        public string Author
        {
            get => TxbAuthor.Text;
            set => TxbAuthor.Text = value;
        }
        public string ImgCount
        {
            get => TxbImgCount.Text;
            set => TxbImgCount.Text = value + "장";
        }
        public string SiteName
        {
            get => TxbSiteName.Text;
            set => TxbSiteName.Text = value;
        }

        public string Route;
        public string Number
        {
            get => TxbNumber.Text;
            set => TxbNumber.Text = value;
        }
        public int CurrentCount
        {
            get => _curCnt;
            set
            {
                _curCnt = value;
                ProgLbl.Content = $"{value}/{ImgCount}";
            }
        }
        //public string[] Tags
        //{
        //    get => _tags;
        //    set
        //    {
        //        _tags = value;

        //        if (value == null)
        //        {
        //            TagPanel.Children.Clear();
        //            return;
        //        }

        //        foreach (var tag in value)
        //        {
        //            if (string.IsNullOrEmpty(tag)) return;
        //            TagPanel.Children.Add(new TagItem
        //            {
        //                TagName = tag.Contains(':') ? tag.Split(':')[1] : tag,

        //                Sex =
        //                    tag.Contains(':')
        //                        ? string.Equals(tag.Split(':')[0], "female", StringComparison.OrdinalIgnoreCase)
        //                            ? TagItem.SColor.Female
        //                            : TagItem.SColor.Male
        //                        : TagItem.SColor.None
        //            });
        //        }
        //    }
        //}

        public int Vote
        {
            get => _vote;
            set
            {
                _vote = value;
                LblVote.Content = _vote.ToString();
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
        }

        private void RemoveOnlyList_Click(object sender, RoutedEventArgs e)
        {
            ((LoaderList)((LoaderItem)((ContextMenu)((MenuItem)sender).Parent).PlacementTarget).Parent).Children.Remove(this);
        }

        private void OpenExplorer_Click(object sender, RoutedEventArgs e)
        {
            Core.OpenDir(Route);
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
            TxbTitle.MaxWidth = ActualWidth - TxbNumber.ActualWidth - 15;
            TxbAuthor.MaxWidth = ActualWidth - 10;
            //TagPanel.MaxWidth = ActualWidth - 105;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(Directory.GetFiles(Core.GetDirectoryFromFile(Route),"*.*")[0]);
            img.EndInit();

            var canvas = new Canvas.Canvas { Image = img };
            canvas.Show();

            IsRead = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {

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
            //TagPanel.Dispatcher.Invoke(() => TagPanel.Children.Clear());
            //Tags = null;
            Title = null;
            Author = null;
            ImgCount = null;
            SiteName = null;
            Route = null;
            Number = null;

            _tags = null;
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
