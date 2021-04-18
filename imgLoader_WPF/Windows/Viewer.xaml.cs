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
        //private Rect _relRect;          //얘네들 존나겹치는거같음 _relRect는 계산이 너무 무거워서 제거 필요
        //private Point _oriPoint;

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
            Cnvs.Children.Add(_img);

            _imgList[0] = ImageLoad(FileList[0]);

            var (width, height) = GetFutureSize(FileList[0]);
            _img.Width = width > Cnvs.ActualWidth ? Cnvs.ActualWidth : width;
            _img.Height = height > Cnvs.ActualHeight ? Cnvs.ActualHeight : height;

            _img.Source = _imgList[0];
            _img.UpdateLayout();

            PBar.Maximum = FileList.Length;
            PBar.Value = 1;

            //_oriPosition = new Rect(temp.X, temp.Y, _img.ActualWidth, _img.ActualHeight);
            ResetImgLocation();
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

        //private static void MoveImage(Image img, double x, double y)
        //{
        //    img.Arrange(new Rect(x, y, img.Width, img.Height));
        //}

        private void MoveImage(double x, double y, double width, double height)
        {
            _img.Arrange(new Rect(x, y, width, height));

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

            ResetImgLocation();
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

        private void ResetImgLocation()
        {
            Canvas.SetLeft(_img, _imgList[0].DecodePixelWidth >= Cnvs.ActualWidth ? 0 : (Cnvs.ActualWidth - _imgList[0].DecodePixelWidth) / 2);
            Canvas.SetTop(_img, _imgList[0].DecodePixelHeight >= Cnvs.ActualHeight ? 0 : (Cnvs.ActualHeight - _imgList[0].DecodePixelHeight) / 2);
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
                        ;
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
                    _min = 1;
                    Canvas.SetLeft(_img, Canvas.GetLeft(_img) + 20);

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
                    //_img.Arrange(_oriPosition);
                    //_relRect = new Rect(_oriPosition.Size);
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

            ResetImgLocation();

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //_oriPoint = Mouse.GetPosition(this);

            var conPos = _img.TransformToAncestor(this).Transform(new Point(0, 0));
            //_relRect = new Rect(conPos.X, conPos.Y, _img.ActualWidth, _img.ActualHeight);

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
            //_relRect.X += mPos.X - _oriPoint.X;
            //_relRect.Y += mPos.Y - _oriPoint.Y;

            //_oriPoint = Mouse.GetPosition(this);
            //_img.Arrange(_relRect);
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
