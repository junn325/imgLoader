using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace imgLoader_WPF
{
    /// <summary>
    /// Interaction logic for LoaderItem.xaml
    /// </summary>
    public partial class LoaderItem : UserControl
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

        //public string Size
        //{
        //    get => this.Height
        //    set => route.Content = value;
        //}

        public LoaderItem()
        {
            InitializeComponent();
        }

        public LoaderItem(string title, string author, string count, string site, string route)
        {
            InitializeComponent();

            Title = title;
            Author = author;
            ImgCount = count;
            SiteName = site;
            Route = route;

            this.Width = 696;
        }
        public LoaderItem(string title, string author, string count, string site, string route, double width)
        {
            InitializeComponent();

            Title = title;
            Author = author;
            ImgCount = count;
            SiteName = site;
            Route = route;

            this.Width = width;
        }
    }
}
