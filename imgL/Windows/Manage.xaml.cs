using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace imgL.Windows
{
    /// <summary>
    /// Interaction logic for ManageWindow.xaml
    /// </summary>
    public partial class Manage : Window
    {
        private string _route;
        private List<BitmapImage> _imgList = new();

        public Manage(string route)
        {
            InitializeComponent();
            _route = route;
        }
    }
}