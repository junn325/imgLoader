using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace imgL.Windows
{
    public partial class CompareDir : Window
    {
        private readonly ImgLoader _sender;
        private readonly ObservableCollection<CompareData> _itemsData = new();
        public CompareDir(ImgLoader sender)
        {
            InitializeComponent();

            _sender = sender;
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
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
        }

        private void Compare_Click(object sender, RoutedEventArgs e)
        {
            var (_, second) = Core.Dir.CompareWorkspace(BlockPath1.Text, TxtPath2.Text);

            _itemsData.Clear();

            foreach (var item in second)
            {
                _itemsData.Add(new CompareData { Item = !ChkNumOnly.IsChecked.Value ? item.Replace(TxtPath2.Text + "\\", "").Split('\\')[0] : Path.GetFileNameWithoutExtension(item) });
            }
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
