using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;

namespace imgLoader_WPF.Canvas
{
    /// <summary>
    /// Interaction logic for Canvas.xaml
    /// </summary>
    public partial class Canvas : Window
    {
        public ImageSource Image
        {
            get => ImgContainer.Source;
            set => ImgContainer.Source = value;
        }

        public string[] FileList;
        private int _index = 0;
        private BitmapImage _imgHandler;

        public Canvas()
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

        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Left)
            {
                var temp = GetNextPath(true);
                _imgHandler = new BitmapImage(new Uri(temp));
                ImgContainer.Source = _imgHandler;
            }
            else if (e.Key == System.Windows.Input.Key.Right)
            {
                var temp = GetNextPath(false);
                _imgHandler = new BitmapImage(new Uri(temp));
                ImgContainer.Source = _imgHandler;
            }
        }

        private void Window_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ;
        }
    }
}
