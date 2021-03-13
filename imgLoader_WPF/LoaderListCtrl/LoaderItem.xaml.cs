using System.Windows;
using System.Windows.Media;

namespace imgLoader_WPF.LoaderListCtrl
{
    public partial class LoaderItem
    {
        private int _progMax;
        private int _progVal;

        public static int MHeight { get; } = 53;

        //private readonly Stopwatch sw = new Stopwatch();

        public LoaderItem()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var data = ((IndexItem)DataContext);

            data.ShownChang = () => Background = data.IsRead ? Brushes.LightGray : Brushes.White;
            data.ProgPanelVis = (v) => Dispatcher.Invoke(() => ProgPanel.Visibility = v);

            data.SizeChange = (value) =>
                this.MaxWidth = value;

            data.RefreshInfo = () => Dispatcher.Invoke(() =>
            {
                AuthorBlock.Text = data.Author;
                ViewCntBlock.Text = $"{data.ImgCount}장";
                NumBlock.Text = data.Number;
                SiteBlock.Text = data.SiteName;
                TitleBlock.Text = data.Title;
                LblVote.Text = data.Vote.ToString();
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
        }

        private void UpVote_Click(object sender, RoutedEventArgs e)
        {
            var data = ((IndexItem)DataContext);
            data.Vote++;
            LblVote.Text = data.Vote.ToString();
        }
        private void DownVote_Click(object sender, RoutedEventArgs e)
        {
            var data = ((IndexItem)DataContext);
            data.Vote--;
            LblVote.Text = data.Vote.ToString();
        }
    }
}
