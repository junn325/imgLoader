using System.Windows;
using System.Windows.Media;

namespace imgLoader_WPF.Canvas
{
    /// <summary>
    /// Interaction logic for Canvas.xaml
    /// </summary>
    public partial class Canvas : Window
    {
        public ImageSource Image
        {
            get => ImgContainer.Source;
            set => ImgContainer.Source = value;
        }

        public Canvas()
        {
            InitializeComponent();
        }
    }
}
