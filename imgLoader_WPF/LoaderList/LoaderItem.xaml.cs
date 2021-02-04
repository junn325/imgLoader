using System.Windows.Controls;
using System.Diagnostics;

namespace imgLoader_WPF.LoaderList
{
    /// <summary>
    /// Interaction logic for LoaderItem.xaml
    /// </summary>
    public partial class LoaderItem
    {
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
            get => route.Content.ToString();
            set => route.Content = value;
        }

        public string Number
        {
            get => number.Content.ToString();
            set => number.Content = value;
        }

        public LoaderList ParentList { get; }

        private readonly Stopwatch sw = new Stopwatch();
        
        public LoaderItem()
        {
            InitializeComponent();
        }

        public LoaderItem(LoaderList parent)
        {
            InitializeComponent();
            ParentList = parent;
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
        public LoaderItem(string title, string author, string count, string site, string route, string number, double width)
        {
            InitializeComponent();

            Title = title;
            Author = author;
            ImgCount = count;
            SiteName = site;
            Route = route;
            Number = number;

            //this.Width = width;
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            sw.Restart();
            ((LoaderList)((LoaderItem)((ContextMenu)((MenuItem)sender).Parent).PlacementTarget).Parent).Children.Remove(this);
            //ParentList.Children.Remove(this);
            Debug.WriteLine(sw.Elapsed.Ticks);
            //.Items.Remove(this);
        }
    }
}
