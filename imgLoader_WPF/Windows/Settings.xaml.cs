using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using imgLoader_WPF.Services;
using Ookii.Dialogs.Wpf;

namespace imgLoader_WPF.Windows
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private readonly List<IndexItem> _index;
        private readonly ImgLoader _sender;
        private ScrollViewer _scroll;

        internal Settings(Windows.ImgLoader sender, ScrollViewer scroll, List<IndexItem> index)
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
            CheckFolder.IsChecked = Properties.Settings.Default.BookMark_Name;
            CheckDupl.IsChecked = Properties.Settings.Default.NoAsk_Dupl;
            CheckImmid.IsChecked = Properties.Settings.Default.Down_Immid;
            CheckScroll.IsChecked = Properties.Settings.Default.NoScrollTag;
            CheckNoIndex.IsChecked = Properties.Settings.Default.NoIndex;
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var a = new VistaFolderBrowserDialog();
            if (a.ShowDialog() == true)
                TxtPath.Text = a.SelectedPath;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtPath.Text == "더블클릭하여 다운로드 경로를 선택하거나 직접 입력" || Core.Route == TxtPath.Text) return;

            Core.Route = TxtPath.Text;

            var a = _index.Count;

            _sender.IdxBlock.Dispatcher.Invoke(() => _sender.IdxBlock.Visibility = Visibility.Visible);

            _sender.List.Clear();
            _sender.ShowItems.Clear();
            _scroll.ScrollToTop();

            //_sender.PgSvc.Paginate();

            while (a == _index.Count)
            {
                Task.Delay(500).Wait();
                Debug.WriteLine("Settings: wait");
            }

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
            Properties.Settings.Default.BookMark_Name = CheckFolder.IsChecked.GetValueOrDefault();
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
            Properties.Settings.Default.NoScrollTag = CheckScroll.IsChecked.GetValueOrDefault();
            Properties.Settings.Default.Save();
        }

        private void CheckNoIndex_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.NoIndex = CheckNoIndex.IsChecked.GetValueOrDefault();
            Properties.Settings.Default.Save();
        }
    }
}
