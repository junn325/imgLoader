using System.Linq;
using System.Windows;
using imgLoader_WPF.LoaderList;

namespace imgLoader_WPF.Forms
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

            var index = Core.Index(route);

            using (var d = Dispatcher.DisableProcessing())
            {
                foreach (var (path, info) in index)
                {
                    var file = info.Split("\n");
                    var lItem = new LoaderItem
                    {
                        Title = file[0],
                        Author = file[1],
                        SiteName = path.Split('.').Last(),
                        ImgCount = file[2],
                        Route = path
                    };
                    LList.Children.Add(lItem);
                }

            }
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
