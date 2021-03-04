using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace imgLoader_WPF.LoaderListCtrl
{
    public partial class LoaderItem
    {
        public string[] Tags;

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
            data.TagPanelVis = (v) => Dispatcher.Invoke(() => TagPanel.Visibility = v);

            data.SizeChange = (value) =>
                //this.Grid.ColumnDefinitions[2].MaxWidth = this.ActualWidth - VoteGrid.ActualWidth;
                this.MaxWidth = value;

            data.RefreshInfo = () => Dispatcher.Invoke(() =>
            {
                AuthorBlock.Text = data.Author;
                CountBlock.Text = $"{data.ImgCount}장";
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

            if (Tags == null) return;
            foreach (var tag in Tags)
            {
                if (string.IsNullOrEmpty(tag)) return;
                TagPanel.Children.Add(new TextBlock
                {
                    Text = tag.Contains(':') ? tag.Split(':')[1] : tag,

                    Background =
                        tag.Contains(':')
                            ? string.Equals(tag.Split(':')[0], "female", StringComparison.OrdinalIgnoreCase)
                                ? (Brush)new BrushConverter().ConvertFrom("#E86441")
                                : (Brush)new BrushConverter().ConvertFrom("#00A2FF")
                            : (Brush)new BrushConverter().ConvertFrom("#838587")
                });
            }
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
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender == null) return;
            var temp = ((ScrollViewer)sender);
            if (Properties.Settings.Default.NoScrollTag && temp.IsEnabled)
            {
                temp.IsEnabled = false;
                return;
            }

            if(!temp.IsEnabled) temp.IsEnabled = true;

            if (e.Delta > 0)
                temp.LineLeft();
            else
                temp.LineRight();

            e.Handled = true;
        }
    }
}
