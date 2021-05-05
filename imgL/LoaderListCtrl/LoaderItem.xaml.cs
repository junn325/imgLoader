using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace imgL.LoaderListCtrl
{
    public partial class LoaderItem
    {
        private Windows.ImgL _sender;
        private bool _isVoteClick;

        public static int MHeight { get; } = 53;
        public LoaderItem()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var data = (IndexItem)DataContext;

            if (((IndexItem)DataContext).IsSeparator)
            {
                using (Dispatcher.DisableProcessing())
                {
                    MainGrid.Children.Clear();
                    this.Height = 1;
                }

                return;
            }

            _sender = (Windows.ImgL)Window.GetWindow(this);

            if (data.IsDownloading) VoteGrid.Visibility = Visibility.Hidden;

            data.ShownChang = () =>
            {
                Background        = data.IsRead ? Brushes.LightGray : Brushes.White;
                ViewCntBlock.Text = data.View == -1 ? "" : $"{data.View} Views";
            };

            data.ProgPanelVis = (v) => Dispatcher.Invoke(() =>
            {
                ProgPanel.Visibility    = v;
                ViewCntBlock.Visibility = v == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
                VoteGrid.Visibility     = v == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            });

            data.RefreshInfo = () => Dispatcher.Invoke(() =>
            {
                var sb = new StringBuilder();

                if (data.Author != null)
                {
                    if (data.Author.Contains('|'))
                    {
                        foreach (var s in data.Author.Split('|')[0].Split(';').Where(i => !string.IsNullOrWhiteSpace(i)))
                        {
                            sb.Append(s).Append(", ");
                        }

                        if (sb.Length != 0) sb.Remove(sb.Length - 2, 2);

                        if (data.Author.Split('|')[1].Contains(';'))
                        {
                            if (sb.Length != 0) sb.Append(' ');

                            sb.Append('(');

                            foreach (var s in data.Author.Split('|')[1].Split(';').Where(i => !string.IsNullOrWhiteSpace(i)))
                            {
                                sb.Append(s).Append(", ");
                            }

                            sb.Remove(sb.Length - 2, 2);

                            sb.Append(')');
                        }
                    }
                    else
                    {
                        sb.Append(data.Author);
                    }
                }

                AuthorBlock.Text  = sb.ToString();
                ImgCntBlock.Text = data.ImgCount == -1
                                       ? ""
                                       : $"{data.ImgCount} Imgs{(data.IsCntValid ? "" : "!")}";

                NumBlock.Text     = data.Number;
                SiteBlock.Text    = data.SiteName;
                TitleBlock.Text   = data.Title;
                LblVote.Text      = data.Vote.ToString();
                ViewCntBlock.Text = data.View == -1 ? "" : $"{data.View} Views";

                if (Core.ShowDate && data.Date.Year != 1)
                {
                    DateBlock.Text = data.Date.ToString(CultureInfo.CurrentCulture);
                    Grid.SetColumnSpan(ProgPanel, 1);
                }
                else if (Core.ShowLastDate && data.LastViewDate.Year != 1)
                {
                    DateBlock.Text = data.LastViewDate.ToString(CultureInfo.CurrentCulture);
                    Grid.SetColumnSpan(ProgPanel, 1);
                }
                else
                {
                    DateBlock.Text = "";
                    Grid.SetColumnSpan(ProgPanel, 2);
                }

                ImgCntBlock.Visibility =
                    data.ImgCount == -1
                        ? Visibility.Hidden
                        : Visibility.Visible;

                VoteGrid.Visibility =
                    data.View == -1 || data.IsDownloading
                        ? Visibility.Hidden
                        : Visibility.Visible;

                ViewCntBlock.Visibility =
                    data.View == -1 || data.IsDownloading
                        ? Visibility.Hidden
                        : Visibility.Visible;

                if (data.IsDownloading)
                {
                    ProgBlock.Text = $"{data.Proc.ProgVal}/{data.Proc.ProgMax}";
                    ProgPanel.Visibility =
                        data.View == -1
                            ? Visibility.Hidden
                            : Visibility.Visible;

                    ProgBar.Maximum = data.Proc.ProgMax;
                    ProgBar.Value   = data.Proc.ProgVal;
                }
            });

            data.ProgBarMax = value => Dispatcher.Invoke(() =>
            {
                ProgBlock.Text  = $"{data.Proc.ProgVal}/{value}";
                ProgBar.Maximum = value;
            });

            data.ProgBarVal = (value) => Dispatcher.Invoke(() =>
            {
                ProgBar.Value  = value;
                ProgBlock.Text = $"{value}/{data.Proc.ProgMax}";
            });

            Background = data.IsRead ? Brushes.LightGray : Brushes.White;

            data.RefreshInfo();
        }

        private void UpVote_Click(object sender, RoutedEventArgs e)
        {
            var data = ((IndexItem)DataContext);

            data.Vote++;
            _sender.InfSvc.Save(data);

            LblVote.Text = data.Vote.ToString();

            _isVoteClick = true;
        }

        private void DownVote_Click(object sender, RoutedEventArgs e)
        {
            var data = ((IndexItem)DataContext);

            data.Vote--;
            _sender.InfSvc.Save(data);

            LblVote.Text = data.Vote.ToString();

            _isVoteClick = true;
        }

        private void This_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_isVoteClick) return;
            e.Handled = true;
            _sender.OpenItem();
        }

        private void This_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isVoteClick = false;
        }
    }
}