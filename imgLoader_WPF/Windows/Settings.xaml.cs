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
        //private readonly List<IndexItem> _index;
        private readonly ImgLoader _sender;
        //private readonly ScrollViewer _scroll;

        private const string TextPath = "Double click to select a path or enter directly";
        private const string TextOpen = "Double click to select a program or enter directly";

        internal Settings(ImgLoader sender)
        {
            InitializeComponent();
            _sender = sender;
            //_index = index;
            //_scroll = scroll;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TxtPath.Text = Core.Route.Length == 0 ? TextPath : Core.Route;
            TxtOpen.Text = Core.OpenWith.Length == 0 ? TextOpen : Core.OpenWith;

            CheckAuthorName.IsChecked = Properties.Settings.Default.ShowAuthor_Folder;
            //CheckFolder.IsChecked = Properties.Settings.Default.BookMark_Name;
            CheckDupl.IsChecked = Properties.Settings.Default.NoAsk_Dupl;
            CheckImmid.IsChecked = Properties.Settings.Default.Down_Immid;
            //CheckScroll.IsChecked = Properties.Settings.Default.NoScrollTag;
            //CheckNoIndex.IsChecked = Properties.Settings.Default.NoIndex;

            ChkOpenBasic_Change();
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var b = new System.Windows.Forms.FolderBrowserDialog();
            if (b.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                TxtPath.Text = b.SelectedPath;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtPath.Text == TextPath || Core.Route == TxtPath.Text) return;
            if (!Directory.Exists(TxtPath.Text)) return;

            Core.Route = TxtPath.Text;

            _sender.BlockIdx.Dispatcher.Invoke(() => _sender.BlockIdx.Visibility = Visibility.Visible);

            _sender.CondInd.Clear();

            _sender.Index.Clear();
            _sender.List.Clear();

            _sender.Scroll.ScrollToTop();
            _sender.IdxSvc.Pause();

            new Thread(() =>
            {
                _sender.IdxSvc.DoIndex();

                new Thread(() =>
                {
                    _sender.Dispatcher.Invoke(() =>
                    {
                        _sender.PgSvc.Clear();
                        _sender.BlockIdx.Visibility = Visibility.Hidden;
                        _sender.ShowItemsCnt();
                    });

                    _sender.PgSvc.Paginate();
                    _sender.IdxSvc.Resume();
                }).Start();
            }).Start();

            if (File.Exists(Path.GetTempPath() + Core.RouteFile))
            {
                if (File.ReadAllText(Path.GetTempPath() + Core.RouteFile) != Core.Route)
                {
                    File.WriteAllText(Path.GetTempPath() + Core.RouteFile, Core.Route);
                }
            }
            else
            {
                File.WriteAllText(Path.GetTempPath() + Core.RouteFile, Core.Route);
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

        private void ChkOpenBasic_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ChkOpenBasic_Click(object sender, RoutedEventArgs e)
        {
            ChkOpenBasic_Change();
            Properties.Settings.Default.UseBasicViewer = ChkOpenBasic.IsChecked.GetValueOrDefault();
            Properties.Settings.Default.Save();
        }

        private void ChkOpenBasic_Change()
        {
            TxtOpen.Visibility = ChkOpenBasic.IsChecked == true ? Visibility.Collapsed : Visibility.Visible;
            Grid.SetColumn(ChkOpenBasic, ChkOpenBasic.IsChecked == true ? 0 : 1);
        }

        private void TxtOpen_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtOpen.Text == TextOpen || Core.OpenWith == TxtOpen.Text) return;
            if (!File.Exists(TxtOpen.Text)) return;

            Core.OpenWith = TxtOpen.Text;

            if (File.Exists(Path.GetTempPath() + Core.OpenFile))
            {
                if (File.ReadAllText(Path.GetTempPath() + Core.OpenFile) != Core.OpenWith)
                {
                    File.WriteAllText(Path.GetTempPath() + Core.OpenFile, Core.OpenWith);
                }
            }
            else
            {
                File.WriteAllText(Path.GetTempPath() + Core.OpenFile, Core.OpenWith);
            }

        }

        private void TxtOpen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var b = new System.Windows.Forms.OpenFileDialog();
            b.Filter = "Executable files (*.exe)|*.exe";
            if (b.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                TxtPath.Text = b.FileName;
        }
    }
}
