using imgLoader_WPF.LoaderListCtrl;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace imgLoader_WPF.Windows
{
    //todo: 이미 본것을 표시 (프로그램 실행 시 초기화)
    //todo: 서로 다른 작품 자동 연결
    //todo: 자체 탐색기 만들기
    //todo: 완전히 같은 이미지 탐색
    //todo: 배경색깔 강제 통일 기능 (https://hiyobi.me/reader/1847608)
    //todo: 페이지네이션
    //todo: 조회수

    public partial class ImgLoader
    {
        private InfoSavingService _infSvc;
        private IndexingService _idxSvc;

        private readonly Settings _winSetting = new();
        private ObservableCollection<IndexItem> _index = new();

        private IndexItem _clickedItem;
        private readonly StringBuilder sb = new();

        int i;
        int j;

        public ImgLoader()
        {
            InitializeComponent();
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
            ItemCtrl.ItemsSource = _index;

            _infSvc = new InfoSavingService();
            _infSvc.Start(this);

            _idxSvc = new IndexingService(_index, this);
            _idxSvc.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //var temp = _index.Keys.ToArray()[new Random().Next(0, _index.Count)];
            //Process.Start("explorer.exe", temp.Substring(0, temp.IndexOf(temp.Split('\\').Last(), StringComparison.Ordinal)));
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _winSetting.ShowDialog();
        }

        private void TxtUrl_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;
            if (TxtUrl.Text.Length == 0) return;

            var url = TxtUrl.Text;
            var lItem = new IndexItem();

            var thrTemp = new Thread(() =>
            {
                lItem.Proc = new Processor(url, lItem);

                if (!lItem.Proc.IsValidated) return;

                if (lItem.Proc.CheckDupl())
                {
                    MessageBox.Show("Already Exists.");
                    return;
                }

                ItemCtrl.Dispatcher.Invoke(() => _index.Add(lItem));
                lItem.Proc.Load();
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
        }

        private void TxtUrl_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton != System.Windows.Input.MouseButtonState.Released) return;
            if (TxtUrl.Text == "주소 입력 후 Enter 키로 다운로드 시작") TxtUrl.Text = "";
        }

        private void ImgLoader_WPF_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _infSvc.Stop();
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
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.NoIndex = true;
            Properties.Settings.Default.NoIndex = false;
        }

        private void LItem_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender == null) return;

            _clickedItem = (IndexItem)((LoaderItem)sender).DataContext;

            for (var j = 0; j < ItemCtrl.ContextMenu.Items.Count; j++)
            {
                var item = ItemCtrl.ContextMenu.Items[j];
                if (item.GetType() == typeof(Separator)) continue;

                if (!_clickedItem.IsDownloading && (j == 3 || j == 4 || j == 5))
                {
                    ((MenuItem)item).IsEnabled = false;
                    continue;
                }

                if (j == 5)
                {
                    if (_clickedItem.Proc.Pause)
                    {
                        ((MenuItem)item).IsEnabled = true;
                        ((MenuItem)ItemCtrl.ContextMenu.Items[4]).IsEnabled = false;
                        continue;
                    }
                    else
                    {
                        ((MenuItem)item).IsEnabled = false;
                        continue;
                    }
                }

                ((MenuItem)item).IsEnabled = true;
            }

            e.Handled = true;
        }

        private void LList_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            foreach (var item in ItemCtrl.ContextMenu.Items)
            {
                if (item.GetType() == typeof(Separator)) continue;
                ((MenuItem)item).IsEnabled = false;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            _clickedItem.Show = false;

            Directory.Delete(Core.GetDirectoryFromFile(_clickedItem.Route), true);

            _idxSvc.DoIndex(sb);
        }

        private void RemoveOnlyList_Click(object sender, RoutedEventArgs e)
        {
            _clickedItem.Show = false;

            InfoSavingService.Save(this);
            _index.Remove(_clickedItem);

            _idxSvc.DoIndex(sb);
        }

        private void OpenExplorer_Click(object sender, RoutedEventArgs e)
        {
            Core.OpenDir(Core.GetDirectoryFromFile(_clickedItem.Route));
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var img = new BitmapImage();
            var temp = Directory.GetFiles(Core.GetDirectoryFromFile(_clickedItem.Route), "*.*").Where(f => !f.Contains(".ilif")).ToArray();

            img.BeginInit();
            img.UriSource = new Uri(temp[0]);
            img.EndInit();

            var canvas = new CanvasWindow.CanvasWindow { Image = img, Title = img.UriSource.LocalPath.Split('\\')[^1], FileList = temp};
            canvas.Show();

            _clickedItem.IsRead = true;
            _clickedItem.ShownChang.Invoke();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _clickedItem.Proc.Stop = true;

            _clickedItem.Show = false;
            _clickedItem.IsDownloading = false;

            Directory.Delete(Core.GetDirectoryFromFile(_clickedItem.Route), true); //todo

            _idxSvc.DoIndex(sb);
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            _clickedItem.Proc.Pause = false;
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            _clickedItem.Proc.Pause = true;
        }
    }
}