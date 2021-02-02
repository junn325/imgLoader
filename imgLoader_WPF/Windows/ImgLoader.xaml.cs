using imgLoader_WPF.LoaderList;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace imgLoader_WPF.Windows
{
    //todo: 이미 본것을 표시 (프로그램 실행 시 초기화)
    //todo: 서로 다른 작품 자동 연결
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, string> index;
        private readonly Settings winSetting = new();
        int i;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = new LoaderItem(LList, $"Test_test_{i}", $"imgL_{i}", i++.ToString(), "Hiyobi", "C:\\test", "000000");
            LList.Children.Add(item);
        }

        private void ImgLoader_WPF_Loaded(object sender, RoutedEventArgs e)
        {
            if (Core.Route.Length == 0 && File.Exists($"{Path.GetTempPath()}{Core.RouteFile}.txt") && Directory.Exists(File.ReadAllText($"{Path.GetTempPath()}{Core.RouteFile}.txt")))
            {
                Core.Route = File.ReadAllText($"{Path.GetTempPath()}{Core.RouteFile}.txt");
            }
            else
            {
                winSetting.Show();
            }

#if DEBUG
            Core.Route = "D:\\문서\\사진\\Saved Pictures\\고니\\i\\새 폴더 (5)";
#endif

            this.Title = Core.Route;

            new Thread(() =>
            {
                index = Core.Index(Core.Route);

                if (index == null) return;

                foreach (var (path, info) in index)
                {
                    var file = info.Split("\n");
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        var lItem = new LoaderItem(file[1], file[2], file[3], file[0], path, path.Split('\\').Last().Split('.')[0], LList.Width);
                        LList.Children.Add(lItem);
                    }));
                }
            }).Start();
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
            var temp = index.Keys.ToArray()[new Random().Next(0, index.Count)];
            Process.Start("explorer.exe", temp.Substring(0, temp.IndexOf(temp.Split('\\').Last(), StringComparison.Ordinal)));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //winSetting = new Settings();
            winSetting.Show();
        }

        private void TxtUrl_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;
            if (TxtUrl.Text.Length == 0) return;

            var url = TxtUrl.Text;

            var proc = new Processor(url);

            if (!proc.IsValidated) return;

            if (proc.CheckDupl()) return;

            var lItem = new LoaderItem(proc.Title, proc.Artist, proc.ImgUrl.Count.ToString(), proc.Site.GetType().Name, proc.Route, Core.GetNumber(url), LList.Width);
            LList.Children.Add(lItem);

            //proc.Load();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
        }

        private void TxtUrl_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton != System.Windows.Input.MouseButtonState.Released) return;

            TxtUrl.SelectAll();

        }
    }
}