using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private long _size;
        //public BitmapImage Image;

        public string[] FileList;
        private BitmapImage[] _imgList;
        //private long[] _imgSizeList;

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
            _imgList = new BitmapImage[FileList.Length];

            _imgList[0] = ImageLoad(FileList[0]);

            for (var i = 1; i < FileList.Length; i++)
            {
                CacheImage(i);
            }

            var len = new FileInfo(FileList[0]).Length;
            _size += len;
            Debug.WriteLine($"+{len}");

            _img = new Image();
            _img.Source = _imgList[0];
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
        private void ChangeImage(string nextPath, int index)
        {
            //todo: 현재 인덱스가 아닌 다음 인덱스를 받음 :: 1번에서 마지막번 갈때 오류
            ReleaseImage(index == 0 ? FileList.Length - 1 : index - 1);

            if (_imgList[index] == null)
            {
                LoadImage(index);
            }

            //todo:이미지 추가 삭제를 함수로 묶어서 제대로 빼고 덧셈이 되게 할 것
            //todo:메모리가 지정된 양을 벗어났을 때만 리스트에서 지울 것

            _img.Source = _imgList[index];

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
            ChangeImage(GetNextPath(true), _index);
            PBar.Value--;
        }
        private void ChangeImageNext()
        {
            ChangeImage(GetNextPath(false), _index);
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
        private static BitmapImage ImageLoad(string path)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnDemand;
            bitmapImage.UriSource = new Uri(path);
            bitmapImage.EndInit();

            return bitmapImage;
        }
        private void CacheImage(int index)
        {
            var service = new Thread(() =>
            {
                if (_imgList[index] != null) return;

                while (_size >= Properties.Settings.Default.CacheSize)
                {
                    Task.Delay(200).Wait();
                }

                LoadImage(index);
            });

            service.Start();
        }
        private void LoadImage(int index)
        {
            var temp = new FileInfo(FileList[index]).Length;
            _size += temp;
            Debug.WriteLine($"+{temp}");

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
        //
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.G:
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
        private void Window_Closed(object sender, EventArgs e)
        {
            for (var i = 0; i < _imgList.Length; i++)
            {
                _imgList[i] = null;
            }
            GC.Collect();
        }
    }
}
