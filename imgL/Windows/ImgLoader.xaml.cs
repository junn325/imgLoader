//사용된 외부 프로젝트: https://github.com/ValdikSS/GoodbyeDPI

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using imgL.LoaderListCtrl;
using imgL.Services;

namespace imgL.Windows
{
    public partial class ImgLoader
    {
        internal InfoSavingService InfSvc;
        internal IndexingService IdxSvc;
        internal PaginationService PgSvc;

        internal Sorter Sorter;
        internal Searcher Searcher;
        internal ConditionIndicator CondInd;
        internal Categorizer Categorizer;

        private Settings _winSetting;

        internal readonly List<IndexItem> Index = new();                      
        internal readonly List<IndexItem> List = new();                         
        internal readonly ObservableCollection<IndexItem> ShowItems = new();   

        private IndexItem _clickedItem;

        public ImgLoader()
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth  = SystemParameters.MaximizedPrimaryScreenWidth;
        }

        private void ImgLoader_WPF_Loaded(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.Name = "Main";

            new Thread(() =>
            {
                var gbd = Array.Find(Directory.GetFiles(Directory.GetCurrentDirectory(), "*", SearchOption.AllDirectories), i => i.Contains("goodbyedpi"));

                Process process = null;
                if (gbd != null
                    && File.Exists(gbd)
                    && File.Exists(Core.Dir.GetDirFromFile(gbd) + "\\WinDivert.dll")
                    && File.Exists(Core.Dir.GetDirFromFile(gbd) + "\\WinDivert32.sys")
                    && File.Exists(Core.Dir.GetDirFromFile(gbd) + "\\WinDivert64.sys"))
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName               = gbd,
                        RedirectStandardOutput = true,
                        RedirectStandardError  = true,
                        UseShellExecute        = false,
                        CreateNoWindow         = true,
                    };

                    process = new Process
                    {
                        StartInfo           = startInfo,
                        EnableRaisingEvents = true,
                    };

                    process.Start();
                }

                while (Dispatcher.Thread.IsAlive)
                {
                    Thread.Sleep(1000);
                }

                process?.Kill();
            }).Start();

            _winSetting = new Settings(this);
            Properties.Settings.Default.Upgrade();
            Menu.Focus();   

            if (!Directory.Exists(Core.FilesRoute)) Directory.CreateDirectory(Core.FilesRoute);

            if (Core.Route.Length == 0 && File.Exists(Core.FilesRoute + Core.RouteFile) && Directory.Exists(File.ReadAllText(Core.FilesRoute + Core.RouteFile)))
            {
                Core.Route = File.ReadAllText(Core.FilesRoute + Core.RouteFile);
            }
            else
            {
                BlockIdx.Visibility = Visibility.Hidden;
                _winSetting.ShowOnFirst();
            }

            if (Core.OpenWith.Length == 0 && File.Exists(Core.FilesRoute + Core.OpenFile) && File.Exists(File.ReadAllText(Core.FilesRoute + Core.OpenFile)))
            {
                Core.OpenWith = File.ReadAllText(Core.FilesRoute + Core.OpenFile);
            }
            else
            {
                Core.OpenWith = "";
            }

            foreach (RadioButton radio in PnlRadio.Children) radio.PreviewKeyUp += TxtSrchAll_KeyUp;

            Title = Core.Route;

            ItemCtrl.ItemsSource = ShowItems;

            CondInd     = new ConditionIndicator(this);
            Sorter      = new Sorter(this);
            Searcher    = new Searcher(this);
            PgSvc       = new PaginationService(this);
            InfSvc      = new InfoSavingService();
            IdxSvc      = new IndexingService(this);
            Categorizer = new Categorizer();
        }

        internal void AddItem(string url)
        {
            var lItem = new IndexItem { Author = "Connecting...", ImgCount = -1, View = -1, Number = Core.GetNumber(url) };

            var service = new Thread(() =>
            {
                PgSvc.Insert(0, lItem);
                Index.Insert(0, lItem);
                List.Insert(0, lItem);

                lItem.Proc = new Processor(url, lItem);
                if (!lItem.Proc.LoadInfo())
                {
                    MessageBox.Show("Address Unreachable.");
                    PgSvc.Remove(lItem);
                    Index.Remove(lItem);
                    List.Remove(lItem);
                    return;
                }

                lItem.Proc.Pause = lItem.Proc.Pause || !Properties.Settings.Default.Down_Immid;

                if (!lItem.Proc.IsValidated)
                {
                    PgSvc.Remove(lItem);
                    Index.Remove(lItem);
                    List.Remove(lItem);
                    return;
                }

                if (lItem.Proc.CheckDupl())
                {
                    MessageBox.Show("Already Exists.");
                    PgSvc.Remove(lItem);
                    Index.Remove(lItem);
                    List.Remove(lItem);
                    return;
                }

                while (lItem.RefreshInfo == null)
                {
                    Task.Delay(100).Wait();
                }

                if (lItem.Proc.IsStop)
                {
                    PgSvc.Remove(lItem);
                    Index.Remove(lItem);
                    List.Remove(lItem);
                    return;
                }

                lItem.RefreshInfo();
                if (!lItem.Proc.StartDownload())
                {
                    PgSvc.Remove(lItem);
                    Index.Remove(lItem);
                    List.Remove(lItem);
                    return;
                }

                List.Remove(lItem);
                List.Insert(Core.GetFutureIndexOnList(List, lItem, (Sorter.SortOption)CondInd.SortItem.Option), lItem);
                PgSvc.Refresh();

                lItem.IsCntValid = Directory.GetFiles(Core.Dir.GetDirFromFile(lItem.Route), "*").Length == lItem.ImgCount + 1;
                lItem.Proc       = null;
            });

            service.Name = "AddItem";
            service.SetApartmentState(ApartmentState.STA);
            service.Start();
        }

        private void AddRecent(string searchTxt, int option)
        {
            var tag = option switch
            {
                -1 => "All:",
                0  => "Author:",
                1  => "Number:",
                2  => "Tag:",
                3  => "SiteName:",
                4  => "Title:",
                5  => "ImgCount:",
                6  => "Vote:",
                _  => ""
            };

            var block = new TextBlock
            {
                Text       = tag + searchTxt,
                Background = Brushes.White,

                Margin  = new Thickness(2),
                Padding = new Thickness(4, 2, 4, 2),

                VerticalAlignment   = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            block.MouseDown += Block_ClickHandler;

            PnlRecent.Children.Insert(1, block);
        }

        private void DeleteItemDir(IndexItem item)
        {
            if (!File.Exists(item.Route)) return;

            item.Show          = false;
            item.IsDownloading = false;

            new Thread(() =>
            {
                item.Proc?.DoStop();

                Core.Log($"Delete: try delete {item.Route}");

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

                List.Remove(item);
                PgSvc.Remove(item);
            }).Start();
        }

        private void HideBorder(UIElement border, TextBox txtB, TextBlock label)
        {
            border.Visibility = Visibility.Hidden;
            Focus();

            txtB.Text        = "";
            label.Visibility = Visibility.Visible;
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

        internal void ShowItemsCnt()
        {
            if (Dispatcher.CheckAccess())
            {
                BlockCnt.Text = $"{List.Count} items | {Core.Route}";
            }
            else
            {
                Dispatcher.Invoke(() => BlockCnt.Text = $"{List.Count} items | {Core.Route}");
            }
        }

        private void ShowInfo(string content)
        {
        }

        private void ShowError(string content)
        {
        }

        private void OpenItem()
        {
            if (Properties.Settings.Default.UseBasicViewer)
            {
                Core.Dir.OpenOnCanvas(Core.Dir.GetDirFromFile(_clickedItem.Route), _clickedItem.Title, _clickedItem.Author, this);
            }
            else
            {
                Core.Dir.OpenOn(Directory.EnumerateFiles(Core.Dir.GetDirFromFile(_clickedItem.Route), "*").First(i => !i.Contains($".{Core.InfoExt}")));
            }

            _clickedItem.LastViewDate = DateTime.Now;
            _clickedItem.View++;
            _clickedItem.IsRead = true;
            _clickedItem.ShownChang.Invoke();
            InfSvc.Save(_clickedItem);
        }

        private void TxtUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key              != Key.Enter) return;
            if (TxtUrl.Text.Length == 0) return;

            var url = TxtUrl.Text;
            TxtUrl.Text         = "";
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
            if (e.Key                  != Key.Enter) return;
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

            finalText = finalText[(finalText.Split(':')[0].Length + 1)..];

            TxtSrchAll.Text += (TxtSrchAll.Text.Length == 0 ? "" : ",") + finalText;
            TxtSrchAll.Select(TxtSrchAll.Text.Length, 0);
            e.Handled = true;
        }

        private void Scroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (PgSvc == null || Index.Count <= PgSvc?.GetCntItemOnly() || List.Count == 0 || !(Math.Abs(e.VerticalOffset - Scroll.ScrollableHeight) < 1)) return;

            var disableProcessing = Dispatcher.DisableProcessing();
            PgSvc.Paginate(disableProcessing);
        }

        private void ImgLoader_WPF_Closing(object sender, CancelEventArgs e)
        {
            _winSetting.Close();
            _winSetting.Dispatcher.BeginInvokeShutdown(DispatcherPriority.Normal);

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
                        if (_clickedItem.Proc.Pause) item.IsEnabled = true;
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

                    case "MenuDelete":
                        if (_clickedItem.IsDownloading) item.IsEnabled = false;
                        break;

                    default:
                        item.IsEnabled = true;
                        break;
                }
            }

        }

        private void LList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in ItemCtrl.ContextMenu?.Items)
            {
                if (item.GetType() == typeof(System.Windows.Controls.Separator)) continue;

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
        private void OpenExplorer_Click(object sender, RoutedEventArgs e)
        {
            Core.Dir.OpenDir(Core.Dir.GetDirFromFile(_clickedItem.Route));
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenItem();
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
            Search(_clickedItem.Tags, (int)Searcher.SearchOption.Tag);
        }

        private void SearchSManual_Click(object sender, RoutedEventArgs e)
        {
            BdrSrch.Visibility = Visibility.Visible;
            TxtSrchAll.Focus();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _clickedItem.Proc?.DoStop();

            DeleteItemDir(_clickedItem);

            PgSvc.Remove(_clickedItem);
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
                case "Pixiv":
                    Clipboard.SetText($"https://www.pixiv.net/artworks/{_clickedItem.Number}");
                    break;
            }
        }

        private void TitleSort_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Add("Title", ConditionIndicator.Condition.Sort, (int)Sorter.SortOption.Title);
        }

        private void AuthorSort_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Add("Author", ConditionIndicator.Condition.Sort, (int)Sorter.SortOption.Author);
        }

        private void PageSort_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Add("Page", ConditionIndicator.Condition.Sort, (int)Sorter.SortOption.Page);
        }

        private void NumberSort_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Add("Number", ConditionIndicator.Condition.Sort, (int)Sorter.SortOption.Number);
        }

        private void Manage_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(_clickedItem.Route)) return;
        }

        private void DateSort_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Add("Date", ConditionIndicator.Condition.Sort, (int)Sorter.SortOption.Date);
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

                if (rand >= List.Count) return;
            } while (List[rand].IsRead);

            List[rand].IsRead = true;
            List[rand].View++;
            List[rand].LastViewDate = DateTime.Now;

            InfSvc.Save(List[rand]);

            if (PgSvc.GetCntItemOnly() >= rand + 1) List[rand].ShownChang?.Invoke();

            if (Properties.Settings.Default.UseBasicViewer)
            {
                Core.Dir.OpenOnCanvas(Core.Dir.GetDirFromFile(List[rand].Route), List[rand].Title, List[rand].Author, this);
            }
            else
            {
                Core.Dir.OpenOn(List[rand].Route);
            }
        }

        private void VoteSort_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Add("Vote", ConditionIndicator.Condition.Sort, (int)Sorter.SortOption.Vote);
        }

        private void ViewSort_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Add("View", ConditionIndicator.Condition.Sort, (int)Sorter.SortOption.View);
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            _winSetting.ShowDialog();
        }

        private void MenuSortLastDate_Click(object sender, RoutedEventArgs e)
        {
            CondInd.Add("LastViewed", ConditionIndicator.Condition.Sort, (int)Sorter.SortOption.LastAccess);
        }

        #endregion

        private void AddBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) HideBorder(BdrAdd, TxtUrl, BlockAdd);
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void SrchBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) HideBorder(BdrSrch, TxtSrchAll, BlockSrchLbl);
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

        private void LoaderItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenItem();
        }

        private void LoaderItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var temp = ((LoaderItem)sender).DataContext;

            if (temp is not IndexItem item) return;

            _clickedItem = item;
        }
    }
}