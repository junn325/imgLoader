using System.Text;
using System.Windows;
using System.Windows.Media;

namespace imgLoader_WPF.LoaderListCtrl
{
    public partial class LoaderItem
    {
        private int _progMax;
        private int _progVal;
        private Windows.ImgLoader _sender;

        public static int MHeight { get; } = 53;

        //private readonly Stopwatch sw = new Stopwatch();

        public LoaderItem()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _sender = (Windows.ImgLoader)Window.GetWindow(this);

            var data = ((IndexItem)DataContext);

            data.ShownChang = () => Background = data.IsRead ? Brushes.LightGray : Brushes.White;
            data.ProgPanelVis = (v) => Dispatcher.Invoke(() => ProgPanel.Visibility = v);

            data.SizeChange = (value) => this.MaxWidth = value;

            data.RefreshInfo = () => Dispatcher.Invoke(() =>
            {
                var sb = new StringBuilder();

                if (data.Author.Contains('|'))
                {
                    foreach (var s in data.Author.Split('|')[0].Split(';'))
                    {
                        if (string.IsNullOrWhiteSpace(s)) continue;
                        sb.Append(s).Append(", ");
                    }

                    if (sb.Length != 0) sb.Remove(sb.Length - 2, 2);

                    if (data.Author.Split('|')[1].Contains(';'))
                    {
                        sb.Append(" (");
                        foreach (var s in data.Author.Split('|')[1].Split(';'))
                        {
                            if (string.IsNullOrWhiteSpace(s)) continue;
                            sb.Append(s).Append(", ");
                        }
                        sb.Remove(sb.Length - 2, 2);
                        sb.Append(')');
                    }
                }
                else
                {
                    sb.Append(data.Author);

                    //foreach (var s in data.Author.Split(';'))
                    //{
                    //    if (string.IsNullOrWhiteSpace(s)) continue;
                    //    sb.Append(s).Append(", ");
                    //}
                }

                AuthorBlock.Text = sb.ToString();
                ImgCntBlock.Text = $"{data.ImgCount} Imgs";
                NumBlock.Text = data.Number;
                SiteBlock.Text = data.SiteName;
                TitleBlock.Text = data.Title;
                LblVote.Text = data.Vote.ToString();
                ViewCntBlock.Text = $"{data.View} Views";
            });

            data.ProgBarMax = value => Dispatcher.Invoke(() =>
            {
                _progMax = value;
                ProgBlock.Text = $"0/{value}";
                ProgBar.Maximum = value;
            });
            data.ProgBarVal = () => Dispatcher.Invoke(() =>
            {
                _progVal++;
                ProgBar.Value++;
                ProgBlock.Text = $"{_progVal}/{_progMax}";
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
