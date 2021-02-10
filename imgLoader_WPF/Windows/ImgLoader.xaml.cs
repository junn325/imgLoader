﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using imgLoader_WPF.LoaderListCtrl;

namespace imgLoader_WPF.Windows
{
    //todo: 이미 본것을 표시 (프로그램 실행 시 초기화)
    //todo: 서로 다른 작품 자동 연결
    //todo: 자체 탐색기 만들기
    //todo: 모든 객체에 dispose

    public partial class MainWindow
    {
        private VoteSavingService _vsSvc;
        private IndexingService _idxSvc;
        private ItemRefreshService _rfshSvc;

        private Settings _winSetting = new();
        private Dictionary<string, string> _index = new();
        int i;
        int j;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = new LoaderItem($"Test_test_{i}", $"imgL_{i}", i++.ToString(), "Hiyobi", "C:\\test", "000000", 0);
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

            _vsSvc = new VoteSavingService();
            //_vsSvc.Start(LList);

            _idxSvc = new IndexingService(_index, LList);
            _idxSvc.Start();

            //_rfshSvc = new ItemRefreshService(_index, LList, LblCount);
            //_rfshSvc.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //var temp = _index.Keys.ToArray()[new Random().Next(0, _index.Count)];
            //Process.Start("explorer.exe", temp.Substring(0, temp.IndexOf(temp.Split('\\').Last(), StringComparison.Ordinal)));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _winSetting.ShowDialog();
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
            //LList.Children.Clear();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
        }

        private void TxtUrl_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton != System.Windows.Input.MouseButtonState.Released) return;
            if (TxtUrl.Text == "주소 입력 후 Enter 키로 다운로드 시작") TxtUrl.Text = "";
        }

        private void ImgLoader_WPF_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _vsSvc.Stop();
            _idxSvc.Stop();
            _rfshSvc.Stop();

            _winSetting.Close();
            _winSetting.Dispatcher.BeginInvokeShutdown(DispatcherPriority.Normal);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (i++ % 2 == 0) _idxSvc.Stop();
            else _idxSvc.Start();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if (j++ % 2 == 0) _rfshSvc.Stop();
            else _rfshSvc.Start();
        }
    }
}