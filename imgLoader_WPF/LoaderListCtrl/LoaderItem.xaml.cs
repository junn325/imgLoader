using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace imgLoader_WPF.LoaderListCtrl
{
    public partial class LoaderItem
    {
        private Windows.ImgLoader _sender;

        public static int MHeight { get; } = 53;

        //private readonly Stopwatch sw = new Stopwatch();

        public LoaderItem()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var data = (IndexItem)DataContext;

            if (data.IsSeparator)
            {
                this.Height = 2;
                MainGrid.Visibility = Visibility.Collapsed;
                return;
                //this.Background = Brushes.LightGray;
            }

            _sender = (Windows.ImgLoader)Window.GetWindow(this);

            if (data.IsDownloading) VoteGrid.Visibility = Visibility.Hidden;

            data.ShownChang = () =>
            {
                Background = data.IsRead ? Brushes.LightGray : Brushes.White;
                ViewCntBlock.Text = data.View == -1 ? "" : $"{data.View} Views";    //'열기' 시 조회수 새로고침
            };

            data.ProgPanelVis = (v) => Dispatcher.Invoke(() =>
            {
                ProgPanel.Visibility = v;
                ViewCntBlock.Visibility = v == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
                VoteGrid.Visibility = v == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            });

            //data.SizeChange = (value) => this.MaxWidth = value;

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

                AuthorBlock.Text = sb.ToString();
                ImgCntBlock.Text = data.ImgCount == -1 ? "" : $"{data.ImgCount} Imgs";
                NumBlock.Text = data.Number;
                SiteBlock.Text = data.SiteName;
                TitleBlock.Text = data.Title;
                LblVote.Text = data.Vote.ToString();
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
                    ProgBar.Value = data.Proc.ProgVal;
                }
            });

            data.ProgBarMax = value => Dispatcher.Invoke(() =>
            {
                ProgBlock.Text = $"{data.Proc.ProgVal}/{value}";
                ProgBar.Maximum = value;
            });

            data.ProgBarVal = (value) => Dispatcher.Invoke(() =>
            {
                ProgBar.Value = value;
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
        }
        private void DownVote_Click(object sender, RoutedEventArgs e)
        {
            var data = ((IndexItem)DataContext);

            data.Vote--;
            _sender.InfSvc.Save(data);

            LblVote.Text = data.Vote.ToString();
        }
    }
}
