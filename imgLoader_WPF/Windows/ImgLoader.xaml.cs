using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using imgLoader_WPF.LoaderListCtrl;

namespace imgLoader_WPF.Windows
{
    //todo: 이미 본것을 표시 (프로그램 실행 시 초기화)
    //todo: 서로 다른 작품 자동 연결
    //todo: 자체 탐색기 만들기
    //todo: 모든 객체에 dispose
    //todo: 완전히 같은 이미지 탐색

    public partial class ImgLoader
    {
        private VoteSavingService _vsSvc;
        private IndexingService _idxSvc;
        //private ItemRefreshService _rfshSvc;

        public static string[] dd = { "ddd", "dd", "D" };

        private Settings _winSetting = new();
        private ObservableCollection<IndexingService.IndexItem> _index = new();
        int i;
        int j;

        public ImgLoader()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //for (int j = 0; j < 700; j++)
            //{
            i++;
            //var item = new LoaderItem($"Test_test_{i}", $"imgL_{i}", i++.ToString(), "Hiyobi", $"C:\\test{j}", "000000", 0);
            //LList.Children.Add();
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

            _idxSvc = new IndexingService(_index, this);
            _idxSvc.Start();

            ItemCtrl.DataContext = _idxSvc;


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

                //LList.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => LList.Children.Insert(0, lItem)));
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
            //LList.Children.Clear();
            GC.Collect();

            ;
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

            _winSetting.Close();
            _winSetting.Dispatcher.BeginInvokeShutdown(DispatcherPriority.Normal);
        }

        private void IndexingStop(object sender, RoutedEventArgs e)
        {
            if (i++ % 2 == 0) _idxSvc.Stop();
            else _idxSvc.Start();
        }

        private void Test1(object sender, RoutedEventArgs e)
        {
            //ItemCtrl.DataContext = _idxSvc;
            ItemCtrl.ItemsSource = ((IndexingService)ItemCtrl.DataContext).Index;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.NoIndex = true;
            Properties.Settings.Default.NoIndex = false;
        }

        private LoaderItem _clickedItem;
        private void LList_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //var temp = (FrameworkElement)LList.InputHitTest(e.GetPosition((LoaderList)sender));
            //if (temp.TemplatedParent == null)
            //{
            //    temp = (FrameworkElement)temp.Parent;
            //    do
            //    {
            //        temp = (FrameworkElement)temp.Parent;
            //    } while (temp != null && temp.GetType().Name != "LoaderItem");

            //    _clickedItem = (LoaderItem)temp;
            //}
            //else
            //{
            //    _clickedItem = (LoaderItem)temp.TemplatedParent;
            //}


            //if (LList.ContextMenu == null) return;

            //var temp1 = LList.InputHitTest(e.GetPosition((LoaderList)sender));
            //var name = temp1.GetType().Name;

            //foreach (var item in LList.ContextMenu.Items)
            //{
            //    if (item.GetType().Name == "Separator") continue;
            //    ((MenuItem)item).IsEnabled = name != "LoaderList";
            //}
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
        }

        private void RemoveOnlyList_Click(object sender, RoutedEventArgs e)
        {
            //LList.Children.Remove(_clickedItem);
            _clickedItem.Dispatcher.InvokeShutdown();
            ;
        }

        private void OpenExplorer_Click(object sender, RoutedEventArgs e)
        {
            Core.OpenDir(_clickedItem.Route);
        }


        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var img = new BitmapImage();
            img.BeginInit();
            //img.UriSource = new Uri(Directory.GetFiles(Core.GetDirectoryFromFile(Route), "*.*")[0]);
            img.EndInit();

            var canvas = new Canvas.Canvas { Image = img };
            canvas.Show();

            //IsRead = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}