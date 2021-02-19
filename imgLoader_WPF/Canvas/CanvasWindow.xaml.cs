using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace imgLoader_WPF.Canvas
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

        private const byte Delta = 200;
        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                var temp = Mouse.GetPosition(Container);
                var thisPos = Container.TransformToAncestor(this).Transform(new Point(0, 0));

                if (e.Delta > 0)
                {
                    var xDelta = Delta * (Math.Abs(temp.X - Container.ActualWidth / 2) / (Container.ActualWidth / 2));
                    if (temp.X - Container.ActualWidth / 2 < 0) xDelta *= -1;

                    var yDelta = Delta * (Math.Abs(temp.Y - Container.ActualHeight / 2) / (Container.ActualHeight / 2));
                    if (temp.Y - Container.ActualHeight / 2 / 2 < 0) yDelta *= -1;

                    
                    Container.Arrange(
                        new Rect(
                            thisPos.X - xDelta,
                            thisPos.Y - yDelta,
                            Container.ActualWidth + Delta,
                            Container.ActualHeight + Delta
                            ));
                }
                else
                { 
                    Container.Arrange(
                        new Rect(
                            thisPos.X + (Delta * (Math.Abs(temp.X - (Container.ActualWidth / 2)) / (Container.ActualWidth / 2))),
                            thisPos.Y + (Delta * (Math.Abs(temp.Y - (Container.ActualHeight / 2)) / (Container.ActualHeight / 2))),
                            Container.ActualWidth - Delta,
                            Container.ActualHeight - Delta
                        ));
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _imgHandler = Image;
        }
    }
}
