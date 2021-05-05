using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Point = System.Windows.Point;

namespace imgL.Windows
{
    public partial class Viewer
    {
        private const byte Scale = 15;
        private int _movePix = 50;

        internal ImgL Sender;
        private Image _img;
        private Rect _imgRect;
        private Point _mouseOriPoint;

        internal string[] FileList;

        private int _index;
        private int _min;
        private int _thres;

        internal string TTitle = "";
        internal string Author = "";

        private bool _isMouseDown;
        private bool _isCtrlDown;

        public Viewer()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InputMethod.SetIsInputMethodEnabled(this, false);
            Title = GetTitle(FileList[0]);

            FileList =
                FileList.Where(file => file.Contains(".jpg") || file.Contains(".png") || file.Contains(".webp") || file.Contains(".gif"))
                        .OrderBy(n => Regex.Replace(n, @"\d+", nn => nn.Value.PadLeft(4, '0')))
                        .ToArray();

            var len = new FileInfo(FileList[0]).Length;
            Debug.WriteLine($"+{len}");

            _img = new Image();

            DockPanel.SetDock(_img, Dock.Top);
            RenderOptions.SetBitmapScalingMode(_img, BitmapScalingMode.Fant);

            Cnvs.Children.Insert(0, _img);

            _img.Source = ImageLoad(FileList[0]);
            _img.UpdateLayout();

            PBar.Maximum = FileList.Length;
            PBar.Value   = 1;

            ResetImg();
        }

        private void SizeChange(bool enlarge, double xRatio, double yRatio)
        {
            if (enlarge)
            {
                _min++;

                MoveImage(
                    _imgRect.X - (_imgRect.Width * (Scale / 100.0) * xRatio),
                    _imgRect.Y - (_imgRect.Height * (Scale / 100.0) * yRatio),
                    _imgRect.Width  * ((Scale + 100) / 100.0),
                    _imgRect.Height * ((Scale + 100) / 100.0)
                );
            }
            else
            {
                if (_min <= 0) return;

                _min--;

                MoveImage(
                    _imgRect.X + (_imgRect.Width * (Scale / (100.0 + Scale)) * xRatio),
                    _imgRect.Y + (_imgRect.Height * (Scale / (100.0 + Scale)) * yRatio),
                    _imgRect.Width  / ((Scale + 100) / 100.0),
                    _imgRect.Height / ((Scale + 100) / 100.0)
                );
            }
        }

        private void SizeChange(bool enlarge)
        {
            if (enlarge)
            {
                _min++;

                MoveImage(
                    _imgRect.X - (_imgRect.Width  * (Scale / 100.0) / 2),
                    _imgRect.Y - (_imgRect.Height * (Scale / 100.0) / 2),
                    _imgRect.Width  * ((Scale + 100) / 100.0),
                    _imgRect.Height * ((Scale + 100) / 100.0)
                );
            }
            else
            {
                if (_min <= 0) return;

                _min--;

                MoveImage(
                    _imgRect.X + (_imgRect.Width  * (Scale / (100.0 + Scale)) / 2),
                    _imgRect.Y + (_imgRect.Height * (Scale / (100.0 + Scale)) / 2),
                    _imgRect.Width  / ((Scale + 100) / 100.0),
                    _imgRect.Height / ((Scale + 100) / 100.0)
                );

            }
        }

        private void MoveImage(double x, double y)
        {
            _imgRect.X      = x;
            _imgRect.Y      = y;
            _imgRect.Width  = _img.ActualWidth;
            _imgRect.Height = _img.ActualHeight;

            _img.Arrange(_imgRect);
        }

        private void MoveImage(double x, double y, double width, double height)
        {
            _imgRect.X      = x;
            _imgRect.Y      = y;
            _imgRect.Width  = width;
            _imgRect.Height = height;

            _img.Width  = width;
            _img.Height = height;

            _img.Arrange(_imgRect);
        }

        private void ChangeImage(string nextPath)
        {
            _img.Source = ImageLoad(FileList[_index]);

            Title = GetTitle(nextPath);

            ResetImg();
        }

        private string GetTitle(string nextPath)
        {
            return $"{TTitle}{(Author != "|" ? " :by " + Core.GetArtistFromRaw(Author) : "")} || {nextPath.Split('\\')[^1]}";
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

        private string GetNextPath(bool prev)
        {
            if (prev)
            {
                if (_index == 0)
                {
                    _index     = FileList.Length - 1;
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
                _index     = 0;
                PBar.Value = 0;
            }
            else
            {
                _index++;
            }

            return FileList[_index];
        }

        private BitmapImage ImageLoad(string path)
        {
            var bitmapImage = new BitmapImage();

            try
            {
                bitmapImage.BeginInit();

                var (newWidth, newHeight)     = GetFutureSize(path);
                bitmapImage.DecodePixelWidth  = newWidth;
                bitmapImage.DecodePixelHeight = newHeight;

                bitmapImage.CreateOptions = BitmapCreateOptions.DelayCreation;
                bitmapImage.CacheOption   = BitmapCacheOption.OnLoad;
                bitmapImage.UriSource     = new Uri(path);
                bitmapImage.EndInit();
            }
            catch
            {
                return null;
            }

            return bitmapImage;
        }

        private void CacheImage(int index)
        {
        }

        internal void LoadImage(int index)
        {
        }

        private void ReleaseImage(int index)
        {
        }

        private static (double, double) GetImgSize(string path)
        {
            var decoder = BitmapDecoder.Create(new Uri(path), BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default);

            return (decoder.Frames[0].PixelWidth, decoder.Frames[0].PixelHeight);
        }

        private void ResetImg()
        {
            var (newWidth, newHeight) = GetFutureSize(FileList[_index]);

            _imgRect.Width  = newWidth;
            _imgRect.Height = newHeight;

            for (var i = 0; i < 5; i++)
            {
                Debug.WriteLine("Size adjust");

                if (_imgRect.Height >= Cnvs.ActualHeight)
                {
                    if (newHeight >= Cnvs.ActualHeight)
                    {
                        _imgRect.Height = Cnvs.ActualHeight;
                        _imgRect.Width  = (Cnvs.ActualHeight / newHeight) * newWidth;
                    }
                    else
                    {
                        _imgRect.Height = newHeight;
                        _imgRect.Width  = newWidth;
                    }

                    _imgRect.X =
                        _imgRect.Width >= Cnvs.ActualWidth
                            ? 0
                            : (Cnvs.ActualWidth - _imgRect.Width) / 2;
                    _imgRect.Y =
                        _imgRect.Height >= Cnvs.ActualHeight
                            ? 0
                            : (Cnvs.ActualHeight - _imgRect.Height) / 2;
                }

                if (_imgRect.Width >= Cnvs.ActualWidth)
                {
                    if (newWidth >= Cnvs.ActualWidth)
                    {
                        _imgRect.Width  = Cnvs.ActualWidth;
                        _imgRect.Height = (Cnvs.ActualWidth / newWidth) * newHeight;
                    }
                    else
                    {
                        _imgRect.Width  = newWidth;
                        _imgRect.Height = newHeight;
                    }

                    _imgRect.X =
                        _imgRect.Width >= Cnvs.ActualWidth
                            ? 0
                            : (Cnvs.ActualWidth - _imgRect.Width) / 2;
                    _imgRect.Y =
                        _imgRect.Height >= Cnvs.ActualHeight
                            ? 0
                            : (Cnvs.ActualHeight - _imgRect.Height) / 2;
                }

                if (Cnvs.ActualWidth >= _imgRect.Width && Cnvs.ActualHeight >= _imgRect.Height) break;
            }

            _img.Width  = _imgRect.Width;
            _img.Height = _imgRect.Height;
            _imgRect.X  = newWidth  >= Cnvs.ActualWidth ? _imgRect.X : (Cnvs.ActualWidth   - _imgRect.Width)  / 2;
            _imgRect.Y  = newHeight >= Cnvs.ActualHeight ? _imgRect.Y : (Cnvs.ActualHeight - _imgRect.Height) / 2;

            _img.Arrange(_imgRect);

            _imgRect.Width  = _img.ActualWidth;
            _imgRect.Height = _img.ActualHeight;
            _imgRect.X      = newWidth  >= Cnvs.ActualWidth ? _imgRect.X : (Cnvs.ActualWidth   - _imgRect.Width)  / 2;
            _imgRect.Y      = newHeight >= Cnvs.ActualHeight ? _imgRect.Y : (Cnvs.ActualHeight - _imgRect.Height) / 2;

            _min = 0;
            _thres = 0;
        }

        private (int, int) GetFutureSize(string imgPath)
        {
            var (width, height) = GetImgSize(imgPath);

            var panelLonger = GridCanvas.ActualWidth > GridCanvas.ActualHeight ? GridCanvas.ActualWidth : GridCanvas.ActualHeight;

            if (width >= height)
            {
                if (width > panelLonger)
                {
                    height *= panelLonger / width;
                    width  =  panelLonger;
                }
            }
            else
            {
                if (height > panelLonger)
                {
                    width  *= panelLonger / height;
                    height =  panelLonger;
                }
            }

            return ((int)width, (int)height);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.LeftCtrl:
                    if (_isCtrlDown) return;
                    _isCtrlDown = true;
                    break;

                case Key.G:
                    break;

                case Key.Y:
                    break;

                case Key.W:
                    if (_isCtrlDown)
                    {
                        Close();
                        return;
                    }

                    if (_min != 0)
                    {
                        if (_imgRect.Y < _imgRect.Height - Cnvs.Height)
                        {
                            MoveImage(_imgRect.X, _imgRect.Height - Cnvs.Height);
                        }
                        else
                        {
                            MoveImage(_imgRect.X, _imgRect.Y + _movePix);
                        }
                    }

                    break;

                case Key.A:
                    if (_min != 0)
                    {
                        MoveImage(_imgRect.X + _movePix, _imgRect.Y);
                    }
                    else
                    {
                        ChangeImagePrev();
                    }

                    break;

                case Key.S:
                    if (_min != 0)
                    {
                        MoveImage(_imgRect.X, _imgRect.Y - _movePix);
                    }

                    break;

                case Key.D:
                    if (_min != 0)
                    {
                        MoveImage(_imgRect.X - _movePix, _imgRect.Y);
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
                    ResetImg();
                    break;
            }
        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (_isCtrlDown || e.MiddleButton == MouseButtonState.Pressed)
            {
                var mPos = e.GetPosition(Cnvs);

                var xOnImg = mPos.X - _imgRect.X;
                var yOnImg = mPos.Y - _imgRect.Y;

                _thres = 3;

                SizeChange(e.Delta > 0, xOnImg / _imgRect.Width, yOnImg / _imgRect.Height);
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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_img == null) return;

            ResetImg();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseOriPoint = e.GetPosition(this);

            var conPos = _img.TransformToAncestor(this).Transform(new Point(0, 0));

            _imgRect.X = conPos.X;
            _imgRect.Y = conPos.Y;

            _imgRect.Width  = _img.ActualWidth;
            _imgRect.Height = _img.ActualHeight;

            _isMouseDown = true;
        }

        private void Window_MouseUp(object sender, MouseEventArgs e)
        {
            _isMouseDown = false;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMouseDown) return;
            if (e.LeftButton != MouseButtonState.Pressed && e.MiddleButton != MouseButtonState.Pressed)
            {
                _isMouseDown = false;
                return;
            }

            var mPos = e.GetPosition(Cnvs);
            MoveImage(_imgRect.X + (mPos.X - _mouseOriPoint.X), _imgRect.Y + (mPos.Y - _mouseOriPoint.Y));
            _mouseOriPoint = e.GetPosition(Cnvs);
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ResetImg();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GC.Collect();
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                _isCtrlDown = false;
            }
        }
    }
}