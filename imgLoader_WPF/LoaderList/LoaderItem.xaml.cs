using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using imgLoader_WPF.Tag;

namespace imgLoader_WPF.LoaderList
{
    /// <summary>
    /// Interaction logic for LoaderItem.xaml
    /// </summary>
    public partial class LoaderItem
    {
        #region "Prop"
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
        public string Route
        {
            get; set;
        }
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

        private int _curCnt;
        #endregion

        //private readonly Stopwatch sw = new Stopwatch();

        public LoaderItem()
        {
            InitializeComponent();
        }

        public LoaderItem(string title, string author, string count, string site, string route, string number)
        {
            InitializeComponent();

            Title = title;
            Author = author;
            ImgCount = count;
            SiteName = site;
            Route = route;
            Number = number;

            //this.Width = sender.Width;
        }
        public LoaderItem(string title, string author, string count, string site, string route, string number, string[] tags)
        {
            InitializeComponent();

            Title = title;
            Author = author;
            ImgCount = count;
            SiteName = site;
            Route = route;
            Number = number;

            foreach (var tag in tags)
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
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
        }

        private void RemoveOnlyList_Click(object sender, RoutedEventArgs e)
        {
            ((LoaderList)((LoaderItem)((ContextMenu)((MenuItem)sender).Parent).PlacementTarget).Parent).Children.Remove(this);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Core.OpenDir(Route);
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
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
    }
}
