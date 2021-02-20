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
        public BitmapImage Image;
        public string[] FileList;

        private int _index;
        //private BitmapImage _imgHandler;
        private Rect _oriPosition;
        private Image _img;
        public CanvasWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _img = new Image();
            _img.Source = Image;
            RenderOptions.SetBitmapScalingMode(_img, BitmapScalingMode.Fant);
            Grid.Children.Add(_img);

            //_imgHandler = Image;
            var temp = _img.TransformToAncestor(this).Transform(new Point(0, 0));
            _oriPosition = new Rect(temp.X, temp.Y, _img.ActualWidth, _img.ActualHeight);
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
                _img.Source = new BitmapImage(new Uri(temp));
                Title = temp.Split('\\')[^1];
            }
            else if (e.Key == Key.Right)
            {
                var temp = GetNextPath(false);
                _img.Source = new BitmapImage(new Uri(temp));
                Title = temp.Split('\\')[^1];
            }

        }

        private const byte Scale = 50;
        private Rect _relRect;

        private bool _isMouseDown = false;
        private Point _oriPoint;
        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.MiddleButton != MouseButtonState.Pressed) return;

            var mousePos = Mouse.GetPosition(_img);
            var conPos = _img.TransformToAncestor(this).Transform(new Point(0, 0));

            if (_relRect.Width == 0 || _relRect.Height == 0) _relRect = new Rect(conPos.X, conPos.Y, _img.ActualWidth, _img.ActualHeight);

            var xRightRatio = mousePos.X / _img.ActualWidth;
            var xLeftRatio = (_img.ActualWidth - mousePos.X) / _img.ActualWidth;
            var yTopRatio = mousePos.Y / _img.ActualHeight;
            var yBotRatio = (_img.ActualHeight - mousePos.Y) / _img.ActualHeight;

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

                _img.Stretch = Stretch.Uniform;
                _img.Arrange(_relRect);
            }
            else
            {
                _relRect.X += Scale * xRightRatio;
                _relRect.Y += Scale * yTopRatio;
                _relRect.Width -= Scale * xLeftRatio;
                _relRect.Height -= Scale * yBotRatio;

                _img.RenderTransform = new ScaleTransform(1, 1, Scale, Scale);
                //Canvas.SetLeft(Container, relRect.X);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _img.Width = _img.ActualWidth + 50;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_img == null) return;

            _relRect = new Rect(0, 0, 0, 0);
            var temp = _img.TransformToAncestor(this).Transform(new Point(0, 0));
            _oriPosition = new Rect(temp.X, temp.Y, _img.ActualWidth, _img.ActualHeight);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _oriPoint = Mouse.GetPosition(this);

            var conPos = _img.TransformToAncestor(this).Transform(new Point(0, 0));
            _relRect = new Rect(conPos.X, conPos.Y, _img.ActualWidth, _img.ActualHeight);

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
            _img.Arrange(_relRect);
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _img.Arrange(_oriPosition);
        }
    }
}
