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
        private const byte Scale = 15; //percent

        private Rect _oriPosition;
        private Rect _relRect;
        private Point _oriPoint;

        private Image _img;
        public BitmapImage Image;

        public string[] FileList;

        private int _index;
        private int _min;
        private int _thres = 5;

        private bool _isMouseDown = false;

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
            switch (e.Key)
            {
                case Key.A:
                case Key.Left:
                    MoveImage(true);
                    break;

                case Key.D:
                case Key.Right:
                    MoveImage(false);
                    break;
            }
        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                _thres = 5;
                SizeChange(e.Delta > 0);
            }
            else
            {
                _thres--;
                if (_thres > 0) return;
                MoveImage(e.Delta > 0);
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
            _min = 0;
        }

        private void SizeChange(bool enlarge)
        {
            var conPos = _img.TransformToAncestor(this).Transform(new Point(0, 0));
            if (_relRect.Width == 0 || _relRect.Height == 0) _relRect = new Rect(conPos.X, conPos.Y, _img.ActualWidth, _img.ActualHeight);

            if (enlarge)
            {
                _relRect.Width *= (Scale + 100) / 100d;
                _relRect.Height *= (Scale + 100) / 100d;
                _relRect.X = (ActualWidth - _relRect.Width) / 2;
                _relRect.Y = (ActualHeight - _relRect.Height) / 2;

                _min++;

                _img.Stretch = Stretch.Uniform;
                _img.Arrange(_relRect);
            }
            else
            {
                if (_min <= 0) return;
                _relRect.Width /= (Scale + 100) / 100d;
                _relRect.Height /= (Scale + 100) / 100d;
                _relRect.X = (ActualWidth - _relRect.Width) / 2;
                _relRect.Y = (ActualHeight - _relRect.Height) / 2;

                _min--;

                _img.Arrange(_relRect);
                _img.Stretch = Stretch.Uniform;
            }
        }

        private void MoveImage(bool next)
        {
            var temp = GetNextPath(next);
            _img.Source = new BitmapImage(new Uri(temp));
            Title = temp.Split('\\')[^1];

            _img.Arrange(_oriPosition);
            _min = 0;
        }
    }
}
