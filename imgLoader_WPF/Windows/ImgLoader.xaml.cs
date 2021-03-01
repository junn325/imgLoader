using imgLoader_WPF.LoaderListCtrl;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace imgLoader_WPF.Windows
{
    //todo: 서로 다른 작품 자동 연결
    //todo: 자체 탐색기 만들기
    //todo: 완전히 같은 이미지 탐색
    //todo: 배경색깔 강제 통일 기능 (https://hiyobi.me/reader/1847608)
    //todo: 조회수
    //todo: 여러 폴더를 탭으로 동시에 관리
    //todo: 조건이 있는 랜덤

    public partial class ImgLoader
    {
        private InfoSavingService _infSvc;
        private IndexingService _idxSvc;
        internal PaginationService PgSvc;

        private Settings _winSetting;

        private readonly ObservableCollection<IndexItem> _index = new();   //단순 인덱싱 결과
        internal ObservableCollection<IndexItem> List = new();            //표시되어야 할 총 항목
        internal ObservableCollection<IndexItem> ShowItems = new();        //실제 표시되는 항목

        private IndexItem _clickedItem;
        private readonly StringBuilder _sb = new();

        public ImgLoader()
        {
            InitializeComponent();
        }

        private void HideBorder(Border border, TextBox txtB, TextBlock label)
        {
            border.Visibility = Visibility.Hidden;
            Focus();

            txtB.Text = "";
            label.Visibility = Visibility.Visible;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ;
        }

        private void ImgLoader_WPF_Loaded(object sender, RoutedEventArgs e)
        {
            Menu.Focus(); //메뉴 미리 로드

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

            Title = Core.Route;

            _winSetting = new Settings(this, Scroll, _index);

            ItemCtrl.ItemsSource = ShowItems;

            _infSvc = new InfoSavingService(this);
            _idxSvc = new IndexingService(_index, this);

            foreach (var item in _index)
            {
                List.Add(item);
            }

            PgSvc = new PaginationService(this, Scroll.ActualHeight, ShowItems, ref List);

            _infSvc.Start();
            _idxSvc.Start();
            PgSvc.Paginate();

            new Thread(() =>
            {
                while (true)
                {
                    Debug.WriteLine($"_index:{_index.Count}/_list:{List.Count}/_showitems:{ShowItems.Count}");
                    Thread.Sleep(1000);
                }
            }).Start();
        }

        private void List_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (PgSvc != null && List.Count != 0 && ShowItems.Count == 0)
            {
                Debug.WriteLine("paginate");
                PgSvc.Paginate();
            }
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
            //var item = new LoaderItem($"Test_test_{i}", $"imgL_{i}", i++.ToString(), "Hiyobi", $"C:\\test{j}", "000000", 0);
            //LList.Children.Add();
            //}
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            _winSetting.ShowDialog();
        }

        private void TxtUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (TxtUrl.Text.Length == 0) return;

            var url = TxtUrl.Text;
            var lItem = new IndexItem() { Author = "준비 중...", ImgCount = "\n" }; //imgcount = "\n" => hides "장"

            HideBorder(AddBorder, TxtUrl, LabelBlock_Add);

            var thrTemp = new Thread(() =>
            {
                ItemCtrl.Dispatcher.Invoke(() => _index.Add(lItem));

                _infSvc.Stop();
                lItem.Proc = new Processor(url, lItem);

                if (!lItem.Proc.IsValidated)
                {
                    ItemCtrl.Dispatcher.Invoke(() => _index.Remove(lItem));
                    _infSvc.Start();
                    return;
                }

                if (lItem.Proc.CheckDupl())
                {
                    MessageBox.Show("Already Exists.");
                    _infSvc.Start();
                    ItemCtrl.Dispatcher.Invoke(() => _index.Remove(lItem));
                    return;
                }

                while (lItem.RefreshInfo == null) Task.Delay(100).Wait();

                lItem.RefreshInfo();
                lItem.Proc.Load();

                _infSvc.Start();
            });

            thrTemp.Name = "AddItem";
            thrTemp.SetApartmentState(ApartmentState.STA);
            thrTemp.Start();
        }

        private void ImgLoader_WPF_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _infSvc.Stop();
            _idxSvc.Stop();

            _winSetting.Close();
            _winSetting.Dispatcher.BeginInvokeShutdown(DispatcherPriority.Normal);
        }

        private void LItem_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender == null || ItemCtrl.ContextMenu == null) return;

            _clickedItem = (IndexItem)((LoaderItem)sender).DataContext;

            foreach (var i in ItemCtrl.ContextMenu.Items)
            {
                if (i.GetType() == typeof(Separator)) continue;

                var item = (MenuItem)i;

                switch (item.Name)
                {
                    case "Cancel":
                        break;
                    case "Resume":
                        if (!_clickedItem.IsDownloading)
                        {
                            item.IsEnabled = false;
                        }
                        break;
                    case "Pause":
                        if (_clickedItem.Proc == null) continue;
                        if (_clickedItem.Proc.Pause)
                        {
                            item.IsEnabled = false;
                            ((MenuItem)ItemCtrl.ContextMenu.Items[7]).IsEnabled = false;        //Resume
                            continue;
                        }

                        item.IsEnabled = false;
                        break;
                    default:
                        item.IsEnabled = true;
                        break;
                }
            }

            e.Handled = true;
        }

        private void LList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in ItemCtrl.ContextMenu.Items)
            {
                if (item.GetType() == typeof(Separator)) continue;

                switch (((MenuItem)item).Name)
                {
                    case "Setting":
                    case "Add":
                    case "Search":
                        ((MenuItem)item).IsEnabled = true;
                        break;
                    default:
                        ((MenuItem)item).IsEnabled = false;
                        break;
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            _clickedItem.Show = false;
            _clickedItem.IsDownloading = false;

            Directory.Delete(Core.GetDirectoryFromFile(_clickedItem.Route), true);

            _idxSvc.DoIndex(_sb);
        }

        private void RemoveOnlyList_Click(object sender, RoutedEventArgs e)
        {
            _clickedItem.Show = false;

            InfoSavingService.Save(this);
            _index.Remove(_clickedItem);

            _idxSvc.DoIndex(_sb);
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

            var canvas = new CanvasWindow.CanvasWindow { Image = img, Title = img.UriSource.LocalPath.Split('\\')[^1], FileList = temp };
            canvas.Show();

            _clickedItem.IsRead = true;
            _clickedItem.ShownChang.Invoke();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _clickedItem.Proc.Stop = true;

            _clickedItem.Show = false;
            _clickedItem.IsDownloading = false;

            Directory.Delete(Core.GetDirectoryFromFile(_clickedItem.Route), true);

            _idxSvc.DoIndex(_sb);
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            _clickedItem.Proc.Pause = false;
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            _clickedItem.Proc.Pause = true;
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            AddBorder.Visibility = Visibility.Visible;
            TxtUrl.Focus();
        }

        private void AddBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                HideBorder(AddBorder, TxtUrl, LabelBlock_Add);
            }
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void TxtUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtUrl.Text.Length == 0)
            {
                LabelBlock_Add.Visibility = Visibility.Visible;
                return;
            }

            LabelBlock_Add.Visibility = Visibility.Collapsed;
        }

        private void CopyAddress_Click(object sender, RoutedEventArgs e)
        {
            switch (_clickedItem.SiteName)
            {
                case "Hiyobi":
                    Clipboard.SetText($"https://hiyobi.me/reader/{_clickedItem.Number}");
                    break;
                case "Hitomi":
                    Clipboard.SetText($"https://hitomi.la/galleries/{_clickedItem.Number}.html");
                    break;
                case "EHentai":
                    Clipboard.SetText($"https://e-hentai.org/g/{_clickedItem.Number}/");
                    break;
                case "NHentai":
                    Clipboard.SetText($"https://nhentai.net/g/{_clickedItem.Number}");
                    break;
            }
        }

        private void TxtUrl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Scroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (!(Math.Abs(e.VerticalOffset - Scroll.ScrollableHeight) < 1) || _index.Count <= ShowItems.Count) return;

            var sder = ((ScrollViewer)sender);
            PgSvc.Paginate();
        }

        private void Scroll_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (var item in ShowItems)
            {
                if (item.SizeChange == null) return;
                item.SizeChange(Scroll.ActualWidth - 10.0);
                Debug.WriteLine("size");
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            SrchBorder.Visibility = Visibility.Visible;
            TxtSrchAll.Focus();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //ShowItems.Add(new IndexItem() { Title = "test" });
            PgSvc.Paginate();
        }

        private void TxtSrchAll_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtSrchAll.Text.Length == 0)
            {
                LabelBlock_Srch.Visibility = Visibility.Visible;
                return;
            }

            LabelBlock_Srch.Visibility = Visibility.Collapsed;
        }

        private void TxtSrchAll_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (TxtSrchAll.Text.Length == 0) return;

            List.Clear();
            ShowItems.Clear();

            Core.SearchFromAll(_index, TxtSrchAll.Text, List);
            PgSvc.Paginate();

            TxtSrchAll.Text = "";
            SrchBorder.Visibility = Visibility.Collapsed;
        }

        private void SrchBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                HideBorder(SrchBorder, TxtSrchAll, LabelBlock_Srch);
            }
        }
    }
}