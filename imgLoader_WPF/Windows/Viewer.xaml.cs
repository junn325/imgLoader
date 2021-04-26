using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using imgLoader_WPF.Services;

using Point = System.Windows.Point;

namespace imgLoader_WPF.Windows
{
    /// <summary>
    /// Interaction logic for Viewer.xaml
    /// </summary>
    public partial class Viewer
    {
        private const byte Scale = 15; //percent
        private int _movePix = 50;

        private ImgCacheService _imgSvc;
        private System.Windows.Controls.Image _img;

        //private Rect _oriPosition;
        private Rect _imgRect;
        private Point _mouseOriPoint;

        internal string[] FileList;
        //private BitmapImage[] _imgList;

        private int _index;
        private int _min;
        private int _thres;

        private long _size;

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

            _imgSvc = new ImgCacheService(this);
            Title   = GetTitle(FileList[0]);

            FileList = FileList.OrderBy(n => Regex.Replace(n, @"\d+", nn => nn.Value.PadLeft(4, '0'))).ToArray();
            //_imgList = new BitmapImage[FileList.Length];

            var len = new FileInfo(FileList[0]).Length;
            _size += len;
            Debug.WriteLine($"+{len}");

            _img = new System.Windows.Controls.Image();
            //_img.VerticalAlignment = VerticalAlignment.Center;
            //_img.HorizontalAlignment = HorizontalAlignment.Center;        //활성화시 확대 안됨

            DockPanel.SetDock(_img, Dock.Top);
            RenderOptions.SetBitmapScalingMode(_img, BitmapScalingMode.Fant);

            Cnvs.Children.Insert(0, _img);

            //_imgList[0] = ImageLoad(FileList[0]);
            _img.Source = ImageLoad(FileList[0]);
            //_img.Source = _imgList[0];
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

        private void ChangeImage(string nextPath, bool next)
        {
            int prevIndex;
            int nextIndex;

            if (next)
            {
                prevIndex =
                    _index == 0
                        ? FileList.Length - 1
                        : _index          - 1;
                nextIndex =
                    _index == FileList.Length - 1
                        ? 0
                        : _index + 1;
            }
            else
            {
                prevIndex =
                    _index == FileList.Length - 1
                        ? 0
                        : _index + 1;
                nextIndex =
                    _index == 0
                        ? FileList.Length - 1
                        : _index          - 1;
            }

            //if (_size >= Properties.Settings.Default.CacheSize)
            //{
            //    for (int i = 0; i < _imgList.Length; i++)
            //    {
            //        if (i == _index) continue;
            //        ReleaseImage(i);
            //        if (_size < Properties.Settings.Default.CacheSize) break;
            //    }
            //}

            //if (_imgList[_index] == null)
            //{
            //    LoadImage(_index);
            //    Debug.WriteLine("LoadImage_now");
            //}

            //if (_imgList[nextIndex] == null)
            //{
            //    CacheImage(nextIndex);
            //    Debug.WriteLine("cacheimage_next");
            //}

            //todo:메모리가 지정된 양을 벗어났을 때만 리스트에서 지울 것

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
            ChangeImage(GetNextPath(true), false);
            PBar.Value--;
        }

        private void ChangeImageNext()
        {
            ChangeImage(GetNextPath(false), true);
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
            _imgSvc.Add(index);
        }

        internal void LoadImage(int index)
        {
            var length = new FileInfo(FileList[index]).Length;
            _size += length;
            //Debug.WriteLine($"+{length}");

            //_imgList[index] = ImageLoad(FileList[index]);
            //Dispatcher.Invoke(() => _imgList[index] = ImageLoad(FileList[index]));
        }

        private void ReleaseImage(int index)
        {
            //_imgList[index] = null;

            var temp = new FileInfo(FileList[index]).Length;
            _size -= temp;
            Debug.WriteLine($"-{temp}");

            //_size -= new FileInfo(FileList[index]).Length;
        }

        private static (double, double) GetImgSize(string path)
        {
            var decoder = BitmapDecoder.Create(new Uri(path), BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default);

            return (decoder.Frames[0].PixelWidth, decoder.Frames[0].PixelHeight);
        }

        private void ResetImg()
        {
            var sw = new Stopwatch();

            var (newWidth, newHeight) = GetFutureSize(FileList[_index]);

            _imgRect.Width  = newWidth;
            _imgRect.Height = newHeight;

            do
            {
                Debug.WriteLine("Size adjust");

                if (_imgRect.Height >= Cnvs.ActualHeight)
                {
                    if (newHeight >= Cnvs.ActualHeight)
                    {
                        _imgRect.Height = Cnvs.ActualHeight;
                        _imgRect.Width = (Cnvs.ActualHeight / newHeight) * newWidth;
                    }
                    else
                    {
                        _imgRect.Height = newHeight;
                        _imgRect.Width = newWidth;
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
                        _imgRect.Width = Cnvs.ActualWidth;
                        _imgRect.Height = (Cnvs.ActualWidth / newWidth) * newHeight;
                    }
                    else
                    {
                        _imgRect.Width = newWidth;
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
            }
            while (_imgRect.Width > Cnvs.ActualWidth || _imgRect.Height > Cnvs.ActualHeight);

            _img.Width = _imgRect.Width;
            _img.Height = _imgRect.Height;
            _img.Arrange(_imgRect);

            //_img.UpdateLayout();

            _imgRect.Width  = _img.ActualWidth;
            _imgRect.Height = _img.ActualHeight;
            _imgRect.X      = newWidth  >= Cnvs.ActualWidth ? 0 : (Cnvs.ActualWidth   - _imgRect.Width)  / 2;
            _imgRect.Y      = newHeight >= Cnvs.ActualHeight ? 0 : (Cnvs.ActualHeight - _imgRect.Height) / 2;

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

        public static Bitmap ResizeImageWithOptions(System.Drawing.Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode    = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode  = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode      = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode    = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        //

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
                        this.Close();
                    }

                    if (_min != 0)
                    {
                        MoveImage(_imgRect.X, _imgRect.Y + _movePix);
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
            //for (var i = 0; i < _imgList.Length; i++)
            //{
            //    _imgList[i] = null;
            //}
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