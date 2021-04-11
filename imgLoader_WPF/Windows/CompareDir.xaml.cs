using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace imgLoader_WPF.Windows
{
    public partial class CompareDir : Window
    {
        private ImgLoader _sender;
        private readonly ObservableCollection<CompareData> _itemsData = new();
        public CompareDir(ImgLoader sender)
        {
            InitializeComponent();
            _sender = sender;
        }

        private void TitleGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private class CompareData
        {
            public string Item { get; set; }
            //public string Second { get; set; }
        }

        private void Compare_Click(object sender, RoutedEventArgs e)
        {
            var (_, second) = Core.Dir.CompareWorkspace(BlockPath1.Text, TxtPath2.Text);

            _itemsData.Clear();

            foreach (var item in second)
            {
                _itemsData.Add(new CompareData { Item = !ChkNumOnly.IsChecked.Value ? item.Replace(TxtPath2.Text + "\\", "").Split('\\')[0] : Path.GetFileNameWithoutExtension(item) });
            }
            //List<string> longer;
            //List<string> shorter;
            //string lPath;
            //string sPath;

            //if (first.Count > second.Count)
            //{
            //    longer = first;
            //    shorter = second;

            //    lPath = BlockPath1.Text;
            //    sPath = TxtPath2.Text;
            //}
            //else
            //{
            //    longer = second;
            //    shorter = first;

            //    lPath = TxtPath2.Text;
            //    sPath = BlockPath1.Text;
            //}

            //var count = shorter.Count;
            //for (var i = 0; i < longer.Count; i++)
            //{
            //    var data = new CompareData();

            //    data.First = !ChkNumOnly.IsChecked.Value ? longer[i].Replace(lPath + "\\", "").Split('\\')[0] : Path.GetFileNameWithoutExtension(longer[i]);
            //    if (count > i) data.Second = !ChkNumOnly.IsChecked.Value ? shorter[i].Replace(sPath + "\\", "").Split('\\')[0] : Path.GetFileNameWithoutExtension(shorter[i]);

            //    _itemsData.Add(data);
            //}
        }

        private void LView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _itemsData.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListV.ItemsSource = _itemsData;

            //var gView = ListV.View as GridView;
            //if (gView == null) return;

            //var workingWidth = ListV.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar

            //gView.Columns[0].Width = workingWidth * 0.5;
            //gView.Columns[1].Width = workingWidth * 0.5;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            foreach (string item in ListV.Items)
            {
                if (item == null) break;

                _sender.AddItem((BlockPath1.Text + item).Split('\\')[^1].Split('.')[0]);
            }
        }

        private void Swap_Click(object sender, RoutedEventArgs e)
        {
            var temp = BlockPath1.Text;
            BlockPath1.Text = TxtPath2.Text;
            TxtPath2.Text = temp;
        }
    }
}
