using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace imgLoader_WPF.Windows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CompareDir : Window
    {
        private readonly ObservableCollection<CompareData> _itemsData = new();
        public CompareDir()
        {
            InitializeComponent();
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

        class CompareData
        {
            public string First { get; set; }
            public string Second { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = Core.Dir.CompareWorkspace(TxtPath1.Text, TxtPath2.Text);

            _itemsData.Clear();

            List<string> longer;
            List<string> shorter;
            string lPath;
            string sPath;

            if (result[0].Count > result[1].Count)
            {
                longer = result[0];
                shorter = result[1];

                lPath = TxtPath1.Text;
                sPath = TxtPath2.Text;
            }
            else
            {
                longer = result[1];
                shorter = result[0];
                lPath = TxtPath2.Text;
                sPath = TxtPath1.Text;
            }

            var count = shorter.Count;
            for (var i = 0; i < longer.Count; i++)
            {
                var data = new CompareData();

                data.First = !ChkNumOnly.IsChecked.Value ? longer[i].Replace(lPath, "") : Path.GetFileNameWithoutExtension(longer[i]);
                if (count > i) data.Second = !ChkNumOnly.IsChecked.Value ? shorter[i].Replace(sPath, "") : Path.GetFileNameWithoutExtension(shorter[i]);

                _itemsData.Add(data);
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
            LView.ItemsSource = _itemsData;

            var gView = LView.View as GridView;
            if (gView == null) return;

            var workingWidth = LView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar

            gView.Columns[0].Width = workingWidth * 0.5;
            gView.Columns[1].Width = workingWidth * 0.5;
        }
    }
}
