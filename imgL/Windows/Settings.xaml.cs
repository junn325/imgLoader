using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace imgL.Windows
{
    public partial class Settings : Window
    {
        private readonly ImgL _sender;

        private bool _isOnFirst;

        private const string TextPath = "Double click to select a path or input directly and press enter";
        private const string TextOpen = "Double click to select a program or input directly and press enter";

        internal Settings(ImgL sender)
        {
            InitializeComponent();
            _sender   = sender;
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth  = SystemParameters.MaximizedPrimaryScreenWidth;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TxtPath.Text = Core.Route.Length == 0 ? TextPath : Core.Route;
            TxtOpen.Text = Core.OpenWith.Length == 0 ? TextOpen : Core.OpenWith;

            ChkOpenBasic_Change();
        }

        internal void ShowOnFirst()
        {
            _isOnFirst = true;
            this.ShowDialog();
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            TxtPath.Text = fbd.SelectedPath;

            if (TxtPath.Text == TextPath || Core.Route == TxtPath.Text) return;
            if (!Directory.Exists(TxtPath.Text)) return;

            Core.Route = TxtPath.Text;

            if (_isOnFirst)
            {
                _isOnFirst = false;
                this.Close();
                return;
            }

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

            if (File.Exists(Core.FilesRoute + Core.RouteFile))
            {
                if (File.ReadAllText(Core.FilesRoute + Core.RouteFile) != Core.Route)
                {
                    File.WriteAllText(Core.FilesRoute + Core.RouteFile, Core.Route);
                }
            }
            else
            {
                File.WriteAllText(Core.FilesRoute + Core.RouteFile, Core.Route);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
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

        private void TxtOpen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "Executable files (*.exe)|*.exe";
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            if (TxtOpen.Text == TextOpen || Core.OpenWith == TxtOpen.Text) return;
            if (!File.Exists(TxtOpen.Text)) return;

            Core.OpenWith = TxtOpen.Text;

            if (File.Exists(Core.FilesRoute + Core.OpenFile))
            {
                File.WriteAllText(Core.FilesRoute + Core.OpenFile, Core.OpenWith);
            }
            else
            {
                File.WriteAllText(Core.FilesRoute + Core.OpenFile, Core.OpenWith);
            }
        }

        private void TxtPath_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (TxtPath.Text == TextPath || Core.Route == TxtPath.Text) return;
            if (!Directory.Exists(TxtPath.Text)) return;

            Core.Route = TxtPath.Text;

            if (_isOnFirst)
            {
                _isOnFirst = false;
                this.Close();
                return;
            }

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

            if (File.Exists(Core.FilesRoute + Core.RouteFile))
            {
                if (File.ReadAllText(Core.FilesRoute + Core.RouteFile) != Core.Route)
                {
                    File.WriteAllText(Core.FilesRoute + Core.RouteFile, Core.Route);
                }
            }
            else
            {
                File.WriteAllText(Core.FilesRoute + Core.RouteFile, Core.Route);
            }

        }

        private void TxtOpen_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (TxtOpen.Text == TextOpen || Core.OpenWith == TxtOpen.Text) return;
            if (!File.Exists(TxtOpen.Text)) return;

            Core.OpenWith = TxtOpen.Text;

            if (File.Exists(Core.FilesRoute + Core.OpenFile))
            {
                File.WriteAllText(Core.FilesRoute + Core.OpenFile, Core.OpenWith);
            }
            else
            {
                File.WriteAllText(Core.FilesRoute + Core.OpenFile, Core.OpenWith);
            }
        }
    }
}
