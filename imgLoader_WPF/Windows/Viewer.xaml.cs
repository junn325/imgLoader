using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using imgLoader_WPF.Services;
using Brushes = System.Windows.Media.Brushes;
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
        private BitmapImage[] _imgList;

        private int _index;
        private int _min;
        private int _thres = 0;

        private long _size;

        internal string TTitle = "";
        internal string Author = "";

        private bool _isMouseDown = false;
        private bool _isCtrlDown = false;

        public Viewer()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _imgSvc = new ImgCacheService(this);

            Title = GetTitle(FileList[0]);

            //FileList = FileList.OrderBy(n => Regex.Replace(n, @"\d+", nn => nn.Value.PadLeft(4, '0'))).ToArray();
            FileList = FileList.OrderBy(i => int.TryParse(i.Split('\\')[^1].Split('.')[0], out var result) ? result : int.MaxValue).ToArray();
            _imgList = new BitmapImage[FileList.Length];

            var len = new FileInfo(FileList[0]).Length;
            _size += len;
            Debug.WriteLine($"+{len}");

            _img = new System.Windows.Controls.Image();
            //_img.VerticalAlignment = VerticalAlignment.Center;
            //_img.HorizontalAlignment = HorizontalAlignment.Center;        //활성화시 확대 안됨

            DockPanel.SetDock(_img, Dock.Top);

            RenderOptions.SetBitmapScalingMode(_img, BitmapScalingMode.Fant);
            //GridCanvas.Children.Add(_img);
            Cnvs.Children.Insert(0, _img);

            _imgList[0] = ImageLoad(FileList[0]);

            //var (width, height) = GetFutureSize(FileList[0]);
            //_img.Width = width > Cnvs.ActualWidth ? Cnvs.ActualWidth : width;
            //_img.Height = height > Cnvs.ActualHeight ? Cnvs.ActualHeight : height;

            _img.Source = _imgList[0];
            _img.UpdateLayout();

            PBar.Maximum = FileList.Length;
            PBar.Value = 1;

            //_oriPosition = new Rect(temp.X, temp.Y, _img.ActualWidth, _img.ActualHeight);
            ResetImg();
            //_relRect = new Rect(_oriPosition.X, _oriPosition.Y, _oriPosition.Width, _oriPosition.Height);
        }

        private void SizeChange(bool enlarge)
        {
            if (enlarge)
            {
                _min++;

                _img.Stretch = Stretch.Uniform;
                //MoveImage(_img, (ActualWidth - _img.Width) / 2, (ActualHeight - _img.Height) / 2);
                MoveImage(
                    (ActualWidth - _img.ActualWidth) / 2,
                    (ActualHeight - _img.ActualHeight) / 2,
                    _img.ActualWidth * (Scale + 100) / 100.0,
                    _img.ActualHeight * (Scale + 100) / 100.0
                    );
            }
            else
            {
                if (_min <= 0) return;

                _min--;

                _img.Stretch = Stretch.Uniform;
                //MoveImage(_img, (ActualWidth - _img.Width) / 2, (ActualHeight - _img.Height) / 2);
                MoveImage(
                    (ActualWidth - _img.ActualWidth) / 2,
                    (ActualHeight - _img.ActualHeight) / 2,
                    _img.ActualWidth / (Scale + 100) / 100.0,
                    _img.ActualHeight / (Scale + 100) / 100.0
                );
            }
        }
        private void MoveImage(/*System.Windows.Controls.Image img, */double x, double y)
        {
            _imgRect.X = x;
            _imgRect.Y = y;
            _imgRect.Width = _img.ActualWidth;
            _imgRect.Height = _img.ActualHeight;

            //Debug.WriteLine(_imgRect);

            _img.Arrange(_imgRect);
        }
        private void MoveImage(double x, double y, double width, double height)
        {
            _imgRect.X = x;
            _imgRect.Y = y;
            _imgRect.Width = width;
            _imgRect.Height = height;

            //Debug.WriteLine(_imgRect);

            _img.Arrange(_imgRect);
        }
        private void ChangeImage(string nextPath, bool next)
        {
            var sw = new Stopwatch();
            sw.Start();

            int prevIndex;
            int nextIndex;

            if (next)
            {
                prevIndex =
                    _index == 0
                        ? FileList.Length - 1
                        : _index - 1;
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
                        : _index - 1;
            }

            if (_size >= Properties.Settings.Default.CacheSize)
            {
                for (int i = 0; i < _imgList.Length; i++)
                {
                    if (i == _index) continue;
                    ReleaseImage(i);
                    if (_size < Properties.Settings.Default.CacheSize) break;
                }
            }
            Debug.WriteLine(sw.Elapsed.Ticks);
            sw.Restart();

            if (_imgList[_index] == null)
            {
                LoadImage(_index);
                Debug.WriteLine("LoadImage_now");
            }
            Debug.WriteLine(sw.Elapsed.Ticks);
            sw.Restart();

            if (_imgList[nextIndex] == null)
            {
                CacheImage(nextIndex);
                Debug.WriteLine("cacheimage_next");
            }
            Debug.WriteLine(sw.Elapsed.Ticks);
            sw.Restart();

            //if (_imgList[prevIndex] == null)
            //{
            //    CacheImage(prevIndex);
            //    Debug.WriteLine("cacheimage_prev");
            //}

            //todo:메모리가 지정된 양을 벗어났을 때만 리스트에서 지울 것

            _img.Source = _imgList[_index];
            Debug.WriteLine(sw.Elapsed.Ticks);
            sw.Restart();

            Title = GetTitle(nextPath);
            Debug.WriteLine(sw.Elapsed.Ticks);
            sw.Restart();

            //var imgOffset = _img.TransformToAncestor(this).Transform(new Point(0, 0));
            Debug.WriteLine(sw.Elapsed.Ticks);
            sw.Restart();

            //_img.Measure(new Size(GridCanvas.ActualWidth, GridCanvas.ActualHeight));
            //Debug.WriteLine(sw.Elapsed.Ticks);
            //sw.Restart();

            //_relRect.Width = _img.DesiredSize.Width;
            //_relRect.Height = _img.DesiredSize.Height;
            //_relRect.X = imgOffset.X;
            //_relRect.Y = imgOffset.Y;
            //Debug.WriteLine(sw.Elapsed.Ticks);
            //sw.Restart();

            //_img.Arrange(_relRect);

            //_oriPosition = new Rect(_img.TransformToAncestor(this).Transform(new Point(0, 0)), new Size(_img.ActualWidth, _img.ActualHeight));

            ResetImg();
            Debug.WriteLine(sw.Elapsed.Ticks + "\n====================");

            _min = 0;
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
        private BitmapImage ImageLoad(string path)
        {
            var bitmapImage = new BitmapImage();

            try
            {
                bitmapImage.BeginInit();

                var (newWidth, newHeight) = GetFutureSize(path);
                bitmapImage.DecodePixelWidth = newWidth;
                bitmapImage.DecodePixelHeight = newHeight;

                //TxtTest.Width = newWidth;
                //TxtTest.Height = newHeight;

                bitmapImage.CreateOptions = BitmapCreateOptions.DelayCreation;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.UriSource = new Uri(path);
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
            Dispatcher.Invoke(() => _imgList[index] = ImageLoad(FileList[index]));
        }
        private void ReleaseImage(int index)
        {
            _imgList[index] = null;

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
            //Canvas.SetLeft(_img, 0);
            //Canvas.SetTop(_img, 0);

            var (newWidth, newHeight) = GetFutureSize(FileList[0]);

            //_imgRect = new Rect(
            //    newWidth >= Cnvs.ActualWidth ? 0 : (Cnvs.ActualWidth - newWidth) / 2,
            //    newHeight >= Cnvs.ActualHeight ? 0 : (Cnvs.ActualHeight - newHeight) / 2,
            //    newWidth > Cnvs.ActualWidth ? Cnvs.ActualWidth : newWidth,
            //    newHeight > Cnvs.ActualHeight ? Cnvs.ActualHeight : newHeight);

            _imgRect.X = newWidth >= Cnvs.ActualWidth ? 0 : (Cnvs.ActualWidth - newWidth) / 2;
            _imgRect.Y = newHeight >= Cnvs.ActualHeight ? 0 : (Cnvs.ActualHeight - newHeight) / 2;
            _imgRect.Width = newWidth > Cnvs.ActualWidth ? Cnvs.ActualWidth : newWidth;
            _imgRect.Height = newHeight > Cnvs.ActualHeight ? Cnvs.ActualHeight : newHeight;

            _img.Arrange(_imgRect);

            _imgRect.Width = _img.ActualWidth;
            _imgRect.Height = _img.ActualHeight;


            //_img.Width = width > Cnvs.ActualWidth ? Cnvs.ActualWidth : width;
            //_img.Height = height > Cnvs.ActualHeight ? Cnvs.ActualHeight : height;

            //_img.UpdateLayout();

            //_img.Width = _img.ActualWidth;
            //_img.Height = _img.ActualHeight;

            //Canvas.SetLeft(_img, _img.ActualWidth >= Cnvs.ActualWidth ? 0 : (Cnvs.ActualWidth - _img.ActualWidth) / 2);
            //Canvas.SetTop(_img, _img.ActualHeight >= Cnvs.ActualHeight ? 0 : (Cnvs.ActualHeight - _img.ActualHeight) / 2);

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
                    width = panelLonger;
                }
            }
            else
            {
                if (height > panelLonger)
                {
                    width *= panelLonger / height;
                    height = panelLonger;
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
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

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

                    _min = 1;
                    if (_min != 0)
                    {
                        //_relRect.Y += _movePix;
                        //_img.Arrange(_relRect);
                        //MoveImage(_img.);
                    }
                    break;

                case Key.A:
                    if (_min != 0)
                    {
                        //_relRect.X += _movePix;
                        //_img.Arrange(_relRect);
                    }
                    else
                    {
                        ChangeImagePrev();
                    }
                    break;

                case Key.S:
                    if (_min != 0)
                    {
                        //_relRect.Y -= _movePix;
                        //_img.Arrange(_relRect);
                    }
                    break;

                case Key.D:
                    if (_min != 0)
                    {
                        //_relRect.X -= _movePix;
                        //_img.Arrange(_relRect);
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
                    _min = 0;
                    _thres = 0;
                    break;
            }
        }
        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                _thres = 3;
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
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_img == null) return;

            //_movePix = (int)(_img.ActualHeight / 10);

            //_relRect = new Rect(0, 0, 0, 0);
            //var temp = _img.TransformToAncestor(this).Transform(new Point(0, 0));
            //_oriPosition = new Rect(temp.X, temp.Y, _img.ActualWidth, _img.ActualHeight);

            ResetImg();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseOriPoint = e.GetPosition(this);

            var conPos = _img.TransformToAncestor(this).Transform(new Point(0, 0));
            //_relRect = new Rect(conPos.X, conPos.Y, _img.ActualWidth, _img.ActualHeight);

            _imgRect.X = conPos.X;
            _imgRect.Y = conPos.Y;

            _imgRect.Width = _img.ActualWidth;
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
            if (e.LeftButton != MouseButtonState.Pressed && e.MiddleButton != MouseButtonState.Pressed) _isMouseDown = false;

            var mPos = e.GetPosition(this);
            MoveImage(_imgRect.X + (mPos.X - _mouseOriPoint.X), _imgRect.Y + (mPos.Y - _mouseOriPoint.Y));
            _mouseOriPoint = e.GetPosition(this);
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //_img.Arrange(_oriPosition);
            //_relRect = new Rect(_oriPosition.Size);
            _min = 0;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            for (var i = 0; i < _imgList.Length; i++)
            {
                _imgList[i] = null;
            }
            //GC.Collect();
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
