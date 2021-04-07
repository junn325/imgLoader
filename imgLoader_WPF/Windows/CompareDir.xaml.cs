using System.Windows;
using System.Windows.Input;

namespace imgLoader_WPF.Windows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CompareDir : Window
    {
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
    }
}
