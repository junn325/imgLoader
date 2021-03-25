using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace imgLoader_WPF.Windows
{
    /// <summary>
    /// Interaction logic for Canvas.xaml
    /// </summary>
    public partial class Canvas
    {
        private const byte Scale = 15; //percent
        private int _movePix = 50;

        private Rect _oriPosition;
        private Rect _relRect;
        private Point _oriPoint;

        private Image _img;
        public BitmapImage Image;

        public string[] FileList;
        public BitmapImage[] ImgList;

        private int _index;
        private int _min;
        private int _thres = 0;

        private bool _isMouseDown = false;

        public Canvas()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //FileList = FileList.OrderBy(n => Regex.Replace(n, @"\d+", nn => nn.Value.PadLeft(4, '0'))).ToArray();
            FileList = FileList.OrderBy(i => int.TryParse(i.Split('\\')[^1].Split('.')[0], out var result) ? result : int.MaxValue).ToArray();
            ImgList = new BitmapImage[FileList.Length];

            for (var i = 1; i < FileList.Length; i++)
            {
                LoadImage(i);
            }

            ImgList[0] = Image;

            _img = new Image();
            _img.Source = ImgList[0];
            //_img.VerticalAlignment = VerticalAlignment.Center;
            //_img.HorizontalAlignment = HorizontalAlignment.Center;        //활성화시 확대 안됨 넣지말것

            DockPanel.SetDock(_img, Dock.Top);

            RenderOptions.SetBitmapScalingMode(_img, BitmapScalingMode.Fant);
            MPanel.Children.Add(_img);
            //MPanel.dock

            PBar.Maximum = FileList.Length;
            PBar.Value = 1;

            var temp = _img.TransformToAncestor(this).Transform(new Point(0, 0));
            _oriPosition = new Rect(temp.X, temp.Y, _img.ActualWidth, _img.ActualHeight);
        }

        private void SizeChange(bool enlarge)
        {
            if (_min == 0)
            {
                _oriPosition = new Rect(_img.TransformToAncestor(this).Transform(new Point(0, 0)), new Size(_img.ActualWidth, _img.ActualHeight));
            }

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
        private void ChangeImage(string nextPath)
        {
            _img.Source = ImgList[_index];

            Title = nextPath.Split('\\')[^1];

            var imgOffset = _img.TransformToAncestor(this).Transform(new Point(0, 0));

            _img.Measure(new Size(MPanel.ActualWidth, MPanel.ActualHeight));

            _relRect.Width = _img.DesiredSize.Width;
            _relRect.Height = _img.DesiredSize.Height;
            _relRect.X = imgOffset.X;
            _relRect.Y = imgOffset.Y;

            //_img.Arrange(_relRect);

            _img.UpdateLayout();
            _oriPosition = new Rect(_img.TransformToAncestor(this).Transform(new Point(0, 0)), new Size(_img.ActualWidth, _img.ActualHeight));

            _min = 0;
        }
        private void ChangeImagePrev()
        {
            ChangeImage(GetNextPath(true));
            PBar.Value--;
        }
        private void ChangeImageNext()
        {
            ChangeImage(GetNextPath(false));
            PBar.Value++;

        }
        private string GetNextPath(bool left)
        {
            if (left)
            {
                if (_index == 0)
                {
                    _index = FileList.Length - 1;
                    PBar.Value = FileList.Length - 1;
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
                PBar.Value = 0;
            }
            else
            {
                _index++;
            }

            return FileList[_index];
        }
        private void LoadImage(int index)
        {
            var service = new Thread(() =>
            {
                if (ImgList[index] != null) return;
                Dispatcher.Invoke(() => ImgList[index] = new BitmapImage(new Uri(FileList[index])));
            });

            service.IsBackground = true;
            service.Name = "Load_Img";

            service.Start();
        }

        //
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.G:
                    ;
                    break;

                case Key.W:
                    if (_min != 0)
                    {
                        _relRect.Y += _movePix;
                        _img.Arrange(_relRect);
                    }
                    break;
                case Key.A:
                    if (_min != 0)
                    {
                        _relRect.X += _movePix;
                        _img.Arrange(_relRect);
                    }
                    else
                    {
                        ChangeImagePrev();
                    }
                    break;
                case Key.S:
                    if (_min != 0)
                    {
                        _relRect.Y -= _movePix;
                        _img.Arrange(_relRect);
                    }
                    break;
                case Key.D:
                    if (_min != 0)
                    {
                        _relRect.X -= _movePix;
                        _img.Arrange(_relRect);
                    }
                    else
                    {
                        ChangeImageNext();
                    }
                    break;

                case Key.Left:
                    ChangeImagePrev();
                    break;
                case Key.Right:
                    ChangeImageNext();
                    break;

                case Key.Q:
                    SizeChange(false);
                    break;
                case Key.E:
                    SizeChange(true);
                    break;
                case Key.R:
                    _img.Arrange(_oriPosition);
                    _relRect = new Rect(_oriPosition.Size);
                    _min = 0;
                    _thres = 0;
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
                if (e.Delta > 0)
                {
                    ChangeImagePrev();
                }
                else
                {
                    ChangeImageNext();
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _img.Width = _img.ActualWidth + 50;
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //return;
            if (_img == null) return;

            _movePix = (int)(_img.ActualHeight / 10);

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
            _relRect = new Rect(_oriPosition.Size);
            _min = 0;
        }
    }
}
