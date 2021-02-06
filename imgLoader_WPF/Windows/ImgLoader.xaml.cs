using imgLoader_WPF.LoaderList;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace imgLoader_WPF.Windows
{
    //todo: 이미 본것을 표시 (프로그램 실행 시 초기화)
    //todo: 서로 다른 작품 자동 연결
    //todo: 자체 탐색기 만들기

    public partial class MainWindow
    {
        private Dictionary<string, string> _index;
        private readonly Settings _winSetting = new();
        int i;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = new LoaderItem($"Test_test_{i}", $"imgL_{i}", i++.ToString(), "Hiyobi", "C:\\test", "000000");
            //for (int j = 0; j < 100; j++)
            //{
            //    var item = new LoaderItem(LList);
            //    LList.Children.Add(item);
            //}

        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ;
        }

        private void ImgLoader_WPF_Loaded(object sender, RoutedEventArgs e)
        {
            if (Core.Route.Length == 0 && File.Exists($"{Path.GetTempPath()}{Core.RouteFile}.txt") && Directory.Exists(File.ReadAllText($"{Path.GetTempPath()}{Core.RouteFile}.txt")))
            {
                Core.Route = File.ReadAllText($"{Path.GetTempPath()}{Core.RouteFile}.txt");
            }
            else
            {
                _winSetting.Show();
            }

#if DEBUG
            Core.Route = "D:\\문서\\사진\\Saved Pictures\\고니\\i\\새 폴더 (5)";
#endif

            this.Title = Core.Route;

            var temp = new Thread(() =>
            {
                _index = Core.Index(Core.Route);

                if (_index == null) return;

                foreach (var (path, info) in _index)
                {
                    if (string.IsNullOrEmpty(info)) continue;
                    var file = info.Split("\n");

                    LList.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        var lItem = new LoaderItem(file[1], file[2], file[3], file[0], path, path.Split('\\').Last().Split('.')[0]);
                        lItem.Tags = file[4].Split("tags:")[1].Split('\n')[0].Split(';');
                        LList.Children.Add(lItem);
                    }));
                }
            });

            temp.Name = "Load List";
            temp.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var temp = _index.Keys.ToArray()[new Random().Next(0, _index.Count)];
            Process.Start("explorer.exe", temp.Substring(0, temp.IndexOf(temp.Split('\\').Last(), StringComparison.Ordinal)));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //winSetting = new Settings();
            _winSetting.Show();
        }

        private void TxtUrl_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;
            if (TxtUrl.Text.Length == 0) return;

            var url = TxtUrl.Text;
            var lItem = new LoaderItem();

            var thrTemp = new Thread(() =>
            {
                var proc = new Processor(url, lItem);

                if (proc.CheckDupl())
                {
                    MessageBox.Show("Already Exists.");
                    return;
                }

                if (!proc.IsValidated) return;

                LList.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => LList.Children.Insert(0, lItem)));
                proc.Load();
            });

            thrTemp.Name = "Add object";
            thrTemp.SetApartmentState(ApartmentState.STA);
            thrTemp.Start();
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
            if (TxtUrl.Text == "주소 입력 후 Enter 키로 다운로드 시작") TxtUrl.Text = "";
        }
    }
}