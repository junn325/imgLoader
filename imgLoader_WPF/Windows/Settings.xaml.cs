using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace imgLoader_WPF.Windows
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private readonly List<IndexItem> _index;
        private readonly ImgLoader _sender;
        private readonly ScrollViewer _scroll;

        internal Settings(ImgLoader sender, ScrollViewer scroll, List<IndexItem> index)
        {
            InitializeComponent();
            _sender = sender;
            _index = index;
            _scroll = scroll;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TxtPath.Text = Core.Route.Length == 0 ? TxtPath.Text : Core.Route;

            CheckAuthorName.IsChecked = Properties.Settings.Default.ShowAuthor_Folder;
            //CheckFolder.IsChecked = Properties.Settings.Default.BookMark_Name;
            CheckDupl.IsChecked = Properties.Settings.Default.NoAsk_Dupl;
            CheckImmid.IsChecked = Properties.Settings.Default.Down_Immid;
            //CheckScroll.IsChecked = Properties.Settings.Default.NoScrollTag;
            //CheckNoIndex.IsChecked = Properties.Settings.Default.NoIndex;
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var b = new System.Windows.Forms.FolderBrowserDialog();
            if (b.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                TxtPath.Text = b.SelectedPath;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtPath.Text == "더블클릭하여 다운로드 경로를 선택하거나 직접 입력" || Core.Route == TxtPath.Text) return;
            if (!Directory.Exists(TxtPath.Text)) return;

            Core.Route = TxtPath.Text;

            _sender.BlockIdx.Dispatcher.Invoke(() => _sender.BlockIdx.Visibility = Visibility.Visible);

            _sender.CondInd.Clear();

            _sender.Index.Clear();
            _sender.List.Clear();

            _scroll.ScrollToTop();
            _sender.IdxSvc.Pause();

            new Thread(() =>
            {
                _sender.IdxSvc.DoIndex();

                new Thread(() =>
                {
                    _sender.Dispatcher.Invoke(() =>
                    {
                        _sender.ShowItems.Clear();
                        _sender.BlockIdx.Visibility = Visibility.Hidden;
                        _sender.ShowItemCount();
                    });

                    _sender.PgSvc.Paginate();
                    _sender.IdxSvc.Resume();
                }).Start();
            }).Start();

            if (File.Exists($"{Path.GetTempPath()}{Core.RouteFile}.txt"))
            {
                if (File.ReadAllText($"{Path.GetTempPath()}{Core.RouteFile}.txt") != Core.Route)
                {
                    File.WriteAllText($"{Path.GetTempPath()}{Core.RouteFile}.txt", Core.Route);
                }
            }
            else
            {
                File.WriteAllText($"{Path.GetTempPath()}{Core.RouteFile}.txt", Core.Route);
            }
        }

        private void CheckFolder_Click(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.BookMark_Name = CheckFolder.IsChecked.GetValueOrDefault();
            Properties.Settings.Default.Save();
        }

        private void CheckAuthorName_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ShowAuthor_Folder = CheckAuthorName.IsChecked.GetValueOrDefault();
            Properties.Settings.Default.Save();
        }

        private void CheckImmid_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Down_Immid = CheckImmid.IsChecked.GetValueOrDefault();
            Properties.Settings.Default.Save();
        }

        private void CheckDupl_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.NoAsk_Dupl = CheckDupl.IsChecked.GetValueOrDefault();
            Properties.Settings.Default.Save();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void CheckScroll_Click(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.NoScrollTag = CheckScroll.IsChecked.GetValueOrDefault();
            Properties.Settings.Default.Save();
        }

        private void CheckNoIndex_Click(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.NoIndex = CheckNoIndex.IsChecked.GetValueOrDefault();
            //Properties.Settings.Default.Save();
        }

        private void CheckSearch_Click(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.see = CheckNoIndex.IsChecked.GetValueOrDefault();
            //Properties.Settings.Default.Save();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Upgrade();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var compareWindow = new CompareDir(_sender);
            compareWindow.BlockPath1.Text = TxtPath.Text;
            compareWindow.Show();
        }

        private void ChkPauseCompareAdd_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.PauseCmpAdd = ChkPauseCmpAdd.IsChecked.GetValueOrDefault();
            Properties.Settings.Default.Save();
        }
    }
}
