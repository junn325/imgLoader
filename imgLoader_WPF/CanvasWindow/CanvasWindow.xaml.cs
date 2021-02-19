using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace imgLoader_WPF.CanvasWindow
{
    /// <summary>
    /// Interaction logic for Canvas.xaml
    /// </summary>
    public partial class CanvasWindow
    {
        public BitmapImage Image
        {
            get => (BitmapImage)Container.Source;
            set => Container.Source = value;
        }

        public string[] FileList;
        private int _index;
        private BitmapImage _imgHandler;

        public CanvasWindow()
        {
            InitializeComponent();
        }

        private string GetNextPath(bool left)
        {
            if (left)
            {
                if (_index == 0)
                {
                    _index = FileList.Length - 1;
                }
                else
                {
                    _index--;
                }

                return FileList[_index];
            }

            if (_index == FileList.Length - 1)
            {
                _index = 0;
            }
            else
            {
                _index++;
            }

            return FileList[_index];
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                var temp = GetNextPath(true);
                _imgHandler = new BitmapImage(new Uri(temp));
                Container.Source = _imgHandler;
            }
            else if (e.Key == Key.Right)
            {
                var temp = GetNextPath(false);
                _imgHandler = new BitmapImage(new Uri(temp));
                Container.Source = _imgHandler;
            }
        }

        private const byte Scale = 15; //percent
        private Rect _relRect;
        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.MiddleButton != MouseButtonState.Pressed) return;

            var temp = Mouse.GetPosition(Container);
            var thisPos = Container.TransformToAncestor(this).Transform(new Point(0, 0));

            if (_relRect.Width == 0 || _relRect.Height == 0) _relRect = new Rect(thisPos.X, thisPos.Y, Container.ActualWidth, Container.ActualHeight);

            var xRightPct = temp.X / Container.ActualWidth;
            var xLeftPct = (Container.ActualWidth - temp.X) / Container.ActualWidth;
            var yTopPct = temp.Y / Container.ActualHeight;
            var yBottPct = (Container.ActualHeight - temp.Y) / Container.ActualHeight;

            //var rect = new Rectangle
            //{
            //    Width = relRect.Width,
            //    Height = relRect.Height,
            //    Fill = Brushes.Green,
            //    Stroke = Brushes.Red,
            //    StrokeThickness = 2
            //};

            //Canvas1.Children.Add(rect);

            if (e.Delta > 0)
            {
                _relRect.X -= Delta * xRightPct;
                _relRect.Y -= Delta * yTopPct;
                _relRect.Width += Delta * xLeftPct;
                _relRect.Height += Delta * yBottPct;

                Container.Stretch = Stretch.Uniform;
                Container.Arrange(_relRect);
            }
            else
            {
                _relRect.X += Delta * xRightPct;
                _relRect.Y += Delta * yTopPct;
                _relRect.Width -= Delta * xLeftPct;
                _relRect.Height -= Delta * yBottPct;

                Container.RenderTransform = new ScaleTransform(1, 1, 50, 50);
                //Canvas.SetLeft(Container, relRect.X);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _imgHandler = Image;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Container.Width = Container.ActualWidth + 50;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _relRect = new Rect(0, 0, 0, 0);
        }
    }
}
