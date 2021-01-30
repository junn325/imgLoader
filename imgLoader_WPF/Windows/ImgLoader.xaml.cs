using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using imgLoader_WPF.LoaderList;

namespace imgLoader_WPF.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, string> index;
        int i;

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

            index = Core.Index(route);

            //using var d = Dispatcher.DisableProcessing();
            foreach (var (path, info) in index)
            {
                var file = info.Split("\n");
                var lItem = new LoaderItem(file[1], file[2], file[3], file[0], path, LList.Width);
                LList.Children.Add(lItem);
            }
        }

        private void ImgLoader_WPF_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //foreach (UIElement child in LList.Children)
            //{
            //    if (child.DependencyObjectType.Name == "Grid") continue;
            //    ((LoaderItem)child).Width = System.Windows.SystemParameters.WorkArea.Width;
            //}

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var temp = (index.Keys.ToArray()[(int)(index.Keys.ToArray().Length / (((double)DateTime.Now.Ticks) % 10))]);
            Process.Start("explorer.exe", temp.Substring(0, temp.IndexOf(temp.Split('\\').Last())));
        }
    }
}
