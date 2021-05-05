using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace imgL.Windows
{
    public partial class CategoryViewer : Window
    {
        private readonly ImgL _sender;

        public CategoryViewer()
        {
            InitializeComponent();
        }

        public CategoryViewer(ImgL sender)
        {
            InitializeComponent();
            _sender = sender;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var cat in _sender.Categorizer.CategoryList)
            {
                PanelM.Children.Add(new Button { Margin = new Thickness(5), VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Center, Width = 40, Height = 40, Content = cat.Item1 });
            }

            Height = _sender.Height;

            Top = _sender.Top;
            Left = _sender.Left + _sender.Width + 5;
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
