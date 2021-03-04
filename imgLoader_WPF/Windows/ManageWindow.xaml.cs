using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace imgLoader_WPF.Windows
{
    /// <summary>
    /// Interaction logic for ManageWindow.xaml
    /// </summary>
    public partial class ManageWindow : Window
    {
        private string _route;
        private List<BitmapImage> _imgList = new();
        public ManageWindow(string route)
        {
            InitializeComponent();
            _route = route;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var imgs = Directory.GetFiles(_route).Where((i) => !i.Contains(Core.InfoExt)).ToArray();

            var threads = new Thread[imgs.Length];

            for (var i = 0; i < imgs.Length; i++)
            {
                var i1 = i;
                threads[i] = new Thread(() => LoadImage(imgs[i1]));
                threads[i].SetApartmentState(ApartmentState.STA);
                threads[i].Start();
            }
        }

        private void LoadImage(string uri)
        {
            ContainerPanel.Dispatcher.Invoke(() =>
            {
                var pnel = new DockPanel();
                pnel.MaxWidth = 210;
                //pnel.MaxHeight = 110;
                pnel.Background = Brushes.Aqua;
                pnel.VerticalAlignment = VerticalAlignment.Top;
                pnel.HorizontalAlignment = HorizontalAlignment.Left;

                var img = new Image();
                img.Source = new BitmapImage(new Uri(uri));
                img.Width = 200;
                img.Height = 200;
                img.HorizontalAlignment = HorizontalAlignment.Center;
                img.VerticalAlignment = VerticalAlignment.Top;
                img.Margin = new Thickness(5, 2, 5, 2);

                var label = new TextBlock();
                label.Text = "test";

                pnel.Children.Add(img);
                pnel.Children.Add(label);

                ContainerPanel.Children.Add(pnel);
            });
        }
    }
}
