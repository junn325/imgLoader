using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int i = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = new LoaderItem($"Test_test_{i}", $"imgL_{i}", i++.ToString(), "Hiyobi", "C:\\test");
            LList.Children.Add(item);
        }

        private void ImgLoader_WPF_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ImgLoader_WPF_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (UIElement child in LList.Children)
            {
                if (child.DependencyObjectType.Name == "Grid") continue;
                ((LoaderItem)child).Width = System.Windows.SystemParameters.WorkArea.Width;
            }

        }
    }
}
