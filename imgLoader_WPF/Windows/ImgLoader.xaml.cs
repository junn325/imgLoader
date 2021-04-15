using imgLoader_WPF.LoaderListCtrl;
using imgLoader_WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using static imgLoader_WPF.Services.Sorter;

namespace imgLoader_WPF.Windows
{
    //작품 -> 항목으로 표시할 것

    //todo: 완전히 같은 이미지 탐색
    //todo: 배경색깔 강제 통일 기능 (https://hiyobi.me/reader/1847608)
    //조회수
    //todo: 여러 폴더를 탭으로 동시에 관리
    //폴더 두 개를 열고 없는 항목 체크
    //todo: 조건이 있는 랜덤 -> List에서만 고름으로 달성?
    //뷰어: 계속 다시 로드하지 말고 배열에 이미지를 담아놓을것
    //최근 본 날짜 + 검색

    //todo: 여러 작품이 하나로 나오는 것 처리 (예시: Gakuen Rankou (jairou))
    //todo: 작업 표시줄에 프로그래스바 
    //numericupdown 같은것으로 작품별로 순위 매기는 시스템
    //todo: 작가, 태그 등으로 자동으로 폴더로 나눠주는 시스템
    //todo: 항상 위로 상태로 떠 있다가 인터넷 창에서 누르면 자동으로 해당 작품 다운로드 
    //todo: 작가/태그 분포, 주로 보는 작품 등 분석 기능
    //todo: 여러 폴더를 지정해 동시에 관리
    //todo: 아무 값 없는 분류(그냥 빨주노초파남보) 분류 기능
    //우클릭 시 해당 항목 작가명/기타로 검색
    //todo: 더블클릭으로 열기
    //todo: 드래그로 사용자 정의 순서
    //todo: CondInd 객체 새로고침, 수정 추가
    //todo: 5항목마다 한칸씩 공백 삽입(아무 컨트롤 없는 LoaderItem 삽입?)

    //  검색
    //todo: 검색 조건에 AND, OR 추가
    //todo: 작가명에 ;| 없는것들 고칠것
    //todo: 그룹 검색 제대로 지원 ("|그룹명"은 | 바로 옆에 있는 그룹밖에 못 찾음)

    //  관리
    //todo: 자체 탐색기 만들기(메뉴-관리)
    //todo: 정보 복구
    //todo: 정보 직접 수정
    //todo: 특정 정보 자동 치환 (ex: 작가명이 A에서 B로 바뀜 -> A = B로 자동 치환)
    //todo: 작가별 트리식 정렬
    //todo: 특정 이미지 숨기기(삭제x)
    //todo: 단행본 나누기
    //todo: 서로 다른 항목 자동 연결

    public partial class ImgLoader
    {
        internal InfoSavingService InfSvc;
        internal IndexingService IdxSvc;
        internal PaginationService PgSvc;

        internal Sorter Sorter;
        internal Searcher Searcher;
        internal ConditionIndicator CondInd;

        private Settings _winSetting;

        internal readonly List<IndexItem> Index = new();                             //인덱싱 결과
        internal readonly List<IndexItem> List = new();                              //표시되어야 할 총 항목
        internal readonly ObservableCollection<IndexItem> ShowItems = new();         //실제 표시되는 항목

        private IndexItem _clickedItem;

        public ImgLoader()
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ;
        }
        private void ImgLoader_WPF_Loaded(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.Name = "Main";

            new Thread(() =>
                {
                    while (true)
                    {
                        Debug.WriteLine($"_index:{Index.Count}/_list:{List.Count}/_showitems:{ShowItems.Count}");
                        Thread.Sleep(1000);
                    }
                })
            { IsBackground = true }.Start();

            Properties.Settings.Default.Upgrade();

            Menu.Focus(); //메뉴 미리 로드

            _winSetting = new Settings(this, Scroll, Index);

            if (Core.Route.Length == 0 && File.Exists($"{Path.GetTempPath()}{Core.RouteFile}.txt") && Directory.Exists(File.ReadAllText($"{Path.GetTempPath()}{Core.RouteFile}.txt")))
            {
                Core.Route = File.ReadAllText($"{Path.GetTempPath()}{Core.RouteFile}.txt");
            }
            else
            {
                BlockIdx.Visibility = Visibility.Hidden;
                _winSetting.Show();
            }

#if DEBUG
            Core.Route = "F:\\문서\\사진\\Saved Pictures\\고니\\i\\새 폴더 (5)";
#endif
#if !DEBUG
            D_Stop.IsEnabled = false;
            D_Else1.IsEnabled = false;
            D_Stop.Visibility = Visibility.Collapsed;
            D_Else1.Visibility = Visibility.Collapsed;
#endif
            foreach (RadioButton radio in PnlRadio.Children)
            {
                radio.PreviewKeyUp += TxtSrchAll_KeyUp;
            }

            Title = Core.Route;

            _winSetting = new Settings(this, Scroll, Index);

            ItemCtrl.ItemsSource = ShowItems;

            CondInd = new ConditionIndicator(this);
            Sorter = new Sorter(this, List);
            Searcher = new Searcher(this, List);

            PgSvc = new PaginationService(this, Scroll.ActualHeight, ShowItems, List);
            InfSvc = new InfoSavingService();
            IdxSvc = new IndexingService(this);

            //var disableProcessing = Dispatcher.DisableProcessing();
            InfSvc.Start();
            //IdxSvc.Start();
            //PgSvc.Paginate(disableProcessing);
        }
        private void HideBorder(UIElement border, TextBox txtB, TextBlock label)
        {
            border.Visibility = Visibility.Hidden;
            Focus();

            txtB.Text = "";
            label.Visibility = Visibility.Visible;
        }
        private void DeleteItemDir(IndexItem item)
        {
            if (!File.Exists(item.Route)) return;

            item.Show = false;
            item.IsDownloading = false;

            new Thread(() =>
            {
                item.Proc?.DoStop();

                var wait = true;
                do
                {
                    if (item.Proc == null) break;

                    foreach (var isLoading in item.Proc.IsImgLoading)
                    {
                        if (isLoading)
                        {
                            wait = true;
                            Thread.Sleep(50);
                            break;
                        }

                        wait = false;
                    }
                } while (wait);

                Directory.Delete(Core.Dir.GetDirFromFile(item.Route), true);

                //var result = IdxSvc.DoIndex();
                //IdxSvc.Refresh(result.infoFiles, result.newFiles);

                List.Remove(_clickedItem);
                Dispatcher.Invoke(() => ShowItems.Remove(_clickedItem));
            }).Start();
        }
        internal void ShowItemCount()
        {
            Debug.WriteLine("ShowItemCount: Call");

            BlockCnt.Text = $"{List.Count} items | {Core.Route}";
        }
        internal void AddItem(string url)
        {
            var lItem = new IndexItem() { Author = "준비 중...", ImgCount = -1, View = -1, Number = Core.GetNum(url) };

            var service = new Thread(() =>
            {
                ItemCtrl.Dispatcher.Invoke(() => ShowItems.Insert(0, lItem));
                Index.Insert(0, lItem);
                List.Insert(0, lItem);

                lItem.Proc = new Processor(url, lItem);
                lItem.Proc.LoadInfo();

                lItem.Proc.Pause = lItem.Proc.Pause || !Properties.Settings.Default.Down_Immid;

                if (!lItem.Proc.IsValidated)
                {
                    ItemCtrl.Dispatcher.Invoke(() => ShowItems.Remove(lItem));
                    Index.Remove(lItem);
                    List.Remove(lItem);
                    return;
                }

                if (lItem.Proc.CheckDupl())
                {
                    MessageBox.Show("Already Exists.");
                    ItemCtrl.Dispatcher.Invoke(() => ShowItems.Remove(lItem));
                    Index.Remove(lItem);
                    List.Remove(lItem);
                    return;
                }

                while (lItem.RefreshInfo == null)
                {
                    Task.Delay(100).Wait();
                    Debug.WriteLine("Main: TxtUrl_KeyUp: Wait");
                }

                if (lItem.Proc.IsStop)
                {
                    ItemCtrl.Dispatcher.Invoke(() => ShowItems.Remove(lItem));
                    Index.Remove(lItem);
                    List.Remove(lItem);
                    return;
                }

                lItem.RefreshInfo();
                lItem.Proc.StartDownload();

                Sorter.SortRefresh((SortOption)CondInd.IndicatorList.Find(i => i.Condition == ConditionIndicator.Condition.Sort).Option);  //todo: 재정렬을 하지 말고 정렬될 위치에 끼워넣는식으로 바꿀것

                Dispatcher.Invoke(ShowItemCount);
                lItem.Proc = null;
            });

            service.Name = "AddItem";
            service.SetApartmentState(ApartmentState.STA);
            service.Start();
        }
        private void Search(string searchTxt, int option)
        {
            CondInd.Add(searchTxt, ConditionIndicator.Condition.Search, option);

            AddRecent(searchTxt, option);
        }
        private void Search(string[] searchTags, int option)
        {
            foreach (var itemTag in searchTags)
            {
                CondInd.Add(itemTag, ConditionIndicator.Condition.Search, option);

                AddRecent(itemTag, option);
            }
        }
        private void Search(string searchTxt, int option, string label)
        {
            CondInd.Add(searchTxt, ConditionIndicator.Condition.Search, option, label);
            AddRecent(searchTxt, option);
        }
        private void AddRecent(string searchTxt, int option)
        {
            var tag = option switch
            {
                -1 => "All:",
                0 => "Author:",
                1 => "Number:",
                2 => "Tag:",
                3 => "SiteName:",
                4 => "Title:",
                5 => "ImgCount:",
                6 => "Vote:",
                _ => ""
            };

            var block = new TextBlock
            {
                Text = tag + searchTxt,
                Background = Brushes.White,

                Margin = new Thickness(2),
                Padding = new Thickness(4, 2, 4, 2),

                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            block.MouseDown += Block_ClickHandler;

            PnlRecent.Children.Insert(1, block);
        }
        private void ShowInfo(string content)
        {
            //프로그램 오른쪽 아래에 작게 표시
        }
        private void ShowError(string content)
        {
            //메시지 전용 패널에 표시
        }
        //

        private void TxtUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (TxtUrl.Text.Length == 0) return;

            var url = TxtUrl.Text;
            TxtUrl.Text = "";
            BlockAdd.Visibility = Visibility.Visible;

            AddItem(url);
        }
        private void TxtUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtUrl.Text.Length == 0)
            {
                BlockAdd.Visibility = Visibility.Visible;
                return;
            }

            BlockAdd.Visibility = Visibility.Collapsed;
        }
        private void TxtUrl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        private void TxtSrchAll_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtSrchAll.Text.Length == 0)
            {
                BlockSrchLbl.Visibility = Visibility.Visible;
                return;
            }

            BlockSrchLbl.Visibility = Visibility.Collapsed;
        }
        private void TxtSrchAll_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (TxtSrchAll.Text.Length == 0) return;

            Search(TxtSrchAll.Text,
                (int)(
                    RadioAll.IsChecked.Value
                        ? Searcher.SearchOption.All
                        : RadioAuthor.IsChecked.Value
                            ? Searcher.SearchOption.Author
                            : RadioTag.IsChecked.Value
                                ? Searcher.SearchOption.Tag
                                : RadioNum.IsChecked.Value
                                    ? Searcher.SearchOption.Number
                                    : Searcher.SearchOption.Title
                ));

            TxtSrchAll.Text = "";
            TxtSrchAll.Focus();
        }
        private void Block_ClickHandler(object sender, MouseButtonEventArgs e)
        {
            if (sender == null) return;

            if (e.RightButton == MouseButtonState.Pressed)
            {
                PnlRecent.Children.Remove((TextBlock)sender);
                return;
            }

            var finalText = ((TextBlock)sender).Text;
            if (finalText.Contains(':'))
            {
                switch (finalText.Split(':')[0])
                {
                    case "All":
                        RadioAll.IsChecked = true;
                        break;

                    case "Author":
                        RadioAuthor.IsChecked = true;
                        break;

                    case "Number":
                        RadioNum.IsChecked = true;
                        break;

                    case "Tag":
                        RadioTag.IsChecked = true;
                        break;

                    case "SiteName":
                        break;

                    case "Title":
                        RadioTitle.IsChecked = true;
                        break;

                    case "ImgCount":
                        break;

                    case "Vote":
                        break;
                }
            }

            finalText = finalText.Split(':')[1];

            TxtSrchAll.Text += (TxtSrchAll.Text.Length == 0 ? "" : ",") + finalText;
            TxtSrchAll.Select(TxtSrchAll.Text.Length, 0);
            //TxtSrchAll.Focus();

            e.Handled = true;
        }
        private void Scroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //if (e.VerticalChange == 0 && e.ExtentHeightChange == 0) return;
            if (!(Math.Abs(e.VerticalOffset - Scroll.ScrollableHeight) < 1) || Index.Count <= ShowItems.Count || List.Count == 0) return;

            var disableProcessing = Dispatcher.DisableProcessing();
            PgSvc.ScrollHeight = Scroll.ActualHeight;
            PgSvc.Paginate(disableProcessing);
        }
        private void ImgLoader_WPF_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _winSetting.Close();
            _winSetting.Dispatcher.BeginInvokeShutdown(DispatcherPriority.Normal);

            //InfSvc.Stop();
            //IdxSvc.Stop();
        }
        private void LItem_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender == null || ItemCtrl.ContextMenu == null) return;

            var temp = ((LoaderItem)sender).DataContext;

            if (temp == null) return;

            _clickedItem = (IndexItem)temp;

            foreach (var i in ItemCtrl.ContextMenu.Items)
            {
                if (i.GetType() == typeof(Separator)) continue;

                var item = (MenuItem)i;

                switch (item.Name)
                {
                    case "MenuOpen":
                        item.IsEnabled = !_clickedItem.IsDownloading;
                        break;

                    case "MenuCancel":
                        item.IsEnabled = _clickedItem.IsDownloading;
                        break;

                    case "MenuResume":
                        if (!_clickedItem.IsDownloading)
                        {
                            item.IsEnabled = false;
                            break;
                        }

                        if (_clickedItem.Proc == null) continue;
                        if (_clickedItem.Proc.Pause)
                        {
                            item.IsEnabled = true;
                        }
                        break;

                    case "MenuPause":
                        if (!_clickedItem.IsDownloading)
                        {
                            item.IsEnabled = false;
                            break;
                        }

                        if (_clickedItem.Proc == null) continue;
                        if (_clickedItem.Proc.Pause)
                        {
                            item.IsEnabled = false;
                            break;
                        }

                        item.IsEnabled = true;
                        break;

                    case "MenuManage":
                        item.IsEnabled = false;
                        break;

                    default:
                        item.IsEnabled = true;
                        break;
                }
            }

            //e.Handled = true;
        }
        private void LList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in ItemCtrl.ContextMenu?.Items)
            {
                if (item.GetType() == typeof(Separator)) continue;

                switch (((MenuItem)item).Name)
                {
                    case "MenuSetting":
                    case "MenuRand":
                    case "MenuAdd":
                    case "MenuSrch":
                        ((MenuItem)item).IsEnabled = true;
                        break;
                    default:
                        ((MenuItem)item).IsEnabled = false;
                        break;
                }
            }
        }

        #region MenuItem
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Are you sure want to delete {_clickedItem.Number}?", "Confirm", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;

            DeleteItemDir(_clickedItem);
        }
        //private void RemoveOnlyList_Click(object sender, RoutedEventArgs e)
        //{
        //    _clickedItem.Show = false;

        //    InfSvc.Save(_clickedItem);
        //    _index.Remove(_clickedItem);

        //    _idxSvc.DoIndex(_sb);
        //}
        private void OpenExplorer_Click(object sender, RoutedEventArgs e)
        {
            Core.Dir.OpenDir(Core.Dir.GetDirFromFile(_clickedItem.Route));
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Core.Dir.OpenOnCanvas(Core.Dir.GetDirFromFile(_clickedItem.Route), _clickedItem.Title, _clickedItem.Author);
            //Process.Start(@"C:\Program Files\Honeyview\Honeyview.exe", Directory.GetFiles(Core.Dir.GetDirFromFile(_clickedItem.Route), "*").First(i => !i.Contains(".ilif")));
            //Process.Start(@"C:\Program Files\Honeyview\Honeyview.exe", Core.Dir.GetDirFromFile(_clickedItem.Route));

            _clickedItem.LastViewDate = DateTime.Now;
            _clickedItem.View++;
            _clickedItem.IsRead = true;
            _clickedItem.ShownChang.Invoke();
            InfSvc.Save(_clickedItem);
        }
        private void AuthorSrch_Click(object sender, RoutedEventArgs e)
        {
            Search(_clickedItem.Author, (int)Searcher.SearchOption.Author, "Author:" + Core.GetArtistFromRaw(_clickedItem.Author));
        }
        private void SiteSrch_Click(object sender, RoutedEventArgs e)
        {
            Search(_clickedItem.SiteName, (int)Searcher.SearchOption.SiteName);
        }
        private void TagSrch_Click(object sender, RoutedEventArgs e)
        {
            Search(_clickedItem.Tags, (int) Searcher.SearchOption.Tag);
        }
        private void SearchSMenu_Click(object sender, RoutedEventArgs e)
        {
            BdrSrch.Visibility = Visibility.Visible;
            TxtSrchAll.Focus();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _clickedItem.Proc?.DoStop();

            DeleteItemDir(_clickedItem);

            ShowItems.Remove(_clickedItem);
            List.Remove(_clickedItem);
            Index.Remove(_clickedItem);
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
            BdrAdd.Visibility = Visibility.Visible;
            TxtUrl.Focus();
        }
        private void CopyAddress_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(_clickedItem.Route)) return;

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
        private void TitleSort_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Add("Title", ConditionIndicator.Condition.Sort, (int)SortOption.Title);
        }
        private void AuthorSort_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Add("Author", ConditionIndicator.Condition.Sort, (int)SortOption.Author);
        }
        private void PageSort_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Add("Page", ConditionIndicator.Condition.Sort, (int)SortOption.Page);
        }
        private void NumberSort_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Add("Number", ConditionIndicator.Condition.Sort, (int)SortOption.Number);
        }
        private void Manage_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(_clickedItem.Route)) return;

        }
        private void DateSort_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Add("Date", ConditionIndicator.Condition.Sort, (int)SortOption.Date);
        }
        private void Random_Click(object sender, RoutedEventArgs e)
        {
            var rng = new Random();

            int rand;
            do
            {
                rand = rng.Next(0, List.Count);

                if (!List[rand].IsRead) continue;
                rand++;

                if (rand >= List.Count)
                {
                    return;
                }
            }
            while (List[rand].IsRead);

            Debug.WriteLine("Main: Random_Click: " + List[rand].Title);

            List[rand].IsRead = true;
            List[rand].View++;
            List[rand].LastViewDate = DateTime.Now;

            InfSvc.Save(List[rand]);

            if (ShowItems.Count >= rand + 1)
            {
                List[rand].ShownChang?.Invoke();
            }

            Core.Dir.OpenOnCanvas(Core.Dir.GetDirFromFile(List[rand].Route), List[rand].Title, List[rand].Author);
        }
        private void VoteSort_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Add("Vote", ConditionIndicator.Condition.Sort, (int)SortOption.Vote);
        }
        private void ViewSort_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Add("View", ConditionIndicator.Condition.Sort, (int)SortOption.View);
        }
        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            _winSetting.ShowDialog();
        }
        private void MenuSortLastDate_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Add("LastAccess", ConditionIndicator.Condition.Sort, (int)SortOption.LastAccess);
        }

        #endregion
        private void AddBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                HideBorder(BdrAdd, TxtUrl, BlockAdd);
            }
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"{this.Left}, {this.Top}");
            var disableProcessing = ItemCtrl.Dispatcher.DisableProcessing();

            ShowItems.Clear();
            List.Clear();
            foreach (var item in Index)
            {
                List.Add(item);
            }
            PgSvc.Paginate(disableProcessing);
        }

        private void SrchBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                HideBorder(BdrSrch, TxtSrchAll, BlockSrchLbl);
            }
        }

        private void DockPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LabelBlock_Srch_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TxtSrchAll.Focus();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void AllRadio_Click(object sender, RoutedEventArgs e)
        {
            TxtSrchAll.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var temp = PnlRecent.Children.Cast<UIElement>().ToList();
            foreach (UIElement child in temp)
            {
                if (child.GetType() == typeof(Button)) continue;
                PnlRecent.Children.Remove(child);
            }
        }
    }
}