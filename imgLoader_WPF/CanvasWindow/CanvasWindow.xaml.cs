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
        private Rect _oriPosition;
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

        private const byte Scale = 50;
        private Rect _relRect;

        private bool _isMouseDown = false;
        private Point _oriPoint;
        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.MiddleButton != MouseButtonState.Pressed) return;

            var mousePos = Mouse.GetPosition(Container);
            var conPos = Container.TransformToAncestor(this).Transform(new Point(0, 0));

            if (_relRect.Width == 0 || _relRect.Height == 0) _relRect = new Rect(conPos.X, conPos.Y, Container.ActualWidth, Container.ActualHeight);

            var xRightRatio = mousePos.X / Container.ActualWidth;
            var xLeftRatio = (Container.ActualWidth - mousePos.X) / Container.ActualWidth;
            var yTopRatio = mousePos.Y / Container.ActualHeight;
            var yBotRatio = (Container.ActualHeight - mousePos.Y) / Container.ActualHeight;

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
                _relRect.X -= Scale * xRightRatio;
                _relRect.Y -= Scale * yTopRatio;
                _relRect.Width += Scale * xLeftRatio;
                _relRect.Height += Scale * yBotRatio;

                Container.Stretch = Stretch.Uniform;
                Container.Arrange(_relRect);
            }
            else
            {
                _relRect.X += Scale * xRightRatio;
                _relRect.Y += Scale * yTopRatio;
                _relRect.Width -= Scale * xLeftRatio;
                _relRect.Height -= Scale * yBotRatio;

                Container.RenderTransform = new ScaleTransform(1, 1, Scale, Scale);
                //Canvas.SetLeft(Container, relRect.X);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _imgHandler = Image;
            var temp = Container.TransformToAncestor(this).Transform(new Point(0, 0));
            _oriPosition = new Rect(temp.X, temp.Y, Container.ActualWidth, Container.ActualHeight);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Container.Width = Container.ActualWidth + 50;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _relRect = new Rect(0, 0, 0, 0);
            var temp = Container.TransformToAncestor(this).Transform(new Point(0, 0));
            _oriPosition = new Rect(temp.X, temp.Y, Container.ActualWidth, Container.ActualHeight);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _oriPoint = Mouse.GetPosition(this);

            var conPos = Container.TransformToAncestor(this).Transform(new Point(0, 0));
            _relRect = new Rect(conPos.X, conPos.Y, Container.ActualWidth, Container.ActualHeight);

            _isMouseDown = true;
        }

        private void Window_MouseUp(object sender, MouseEventArgs e)
        {
            _isMouseDown = false;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMouseDown) return;
            if (e.LeftButton != MouseButtonState.Pressed && e.MiddleButton != MouseButtonState.Pressed) _isMouseDown = false;

            var mPos = Mouse.GetPosition(this);
            _relRect.X += mPos.X - _oriPoint.X;
            _relRect.Y += mPos.Y - _oriPoint.Y;

            _oriPoint = Mouse.GetPosition(this);
            Container.Arrange(_relRect);
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Container.Arrange(_oriPosition);
        }
    }
}
