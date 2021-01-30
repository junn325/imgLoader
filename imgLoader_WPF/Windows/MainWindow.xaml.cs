using System.Diagnostics;
using System.Linq;
using System.Windows;
using imgLoader_WPF.LoaderList;

namespace imgLoader_WPF.Windows
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
            const string route = "D:\\문서\\사진\\Saved Pictures\\고니\\manga";

            var sw = new Stopwatch();
            sw.Start();

            var index = Core.Index(route);
            Debug.WriteLine(sw.Elapsed.Ticks);
            sw.Restart();

            //using var d = Dispatcher.DisableProcessing();
            foreach (var (path, info) in index)
            {
                var file = info.Split("\n");
                var lItem = new LoaderItem(file[0], file[1], file[2], path.Split('.').Last(), path, LList.Width);
                LList.Children.Add(lItem);
            }
            Debug.WriteLine(sw.Elapsed.Ticks);
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
