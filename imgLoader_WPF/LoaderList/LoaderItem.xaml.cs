using System.Windows;
using System.Windows.Controls;

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
            get => title.Content.ToString();
            set => title.Content = value;
        }
        public string Author
        {
            get => author.Content.ToString();
            set => author.Content = value;
        }
        public string ImgCount
        {
            get => imgCount.Content.ToString();
            set => imgCount.Content = value + "장";
        }
        public string SiteName
        {
            get => siteName.Content.ToString();
            set => siteName.Content = value;
        }
        public string Route
        {
            get; set;
        }
        public string Number
        {
            get => number.Content.ToString();
            set => number.Content = value;
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
    }
}
