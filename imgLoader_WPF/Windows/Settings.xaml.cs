using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Path = System.Windows.Shapes.Path;

namespace imgLoader_WPF.Windows
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TxtPath.Text = Core.Route.Length == 0 ? TxtPath.Text : Core.Route;

            CheckAuthorName.IsChecked = Properties.Settings.Default.ShowAuthor_Folder;
            CheckFolder.IsChecked = Properties.Settings.Default.BookMark_Name;
            CheckDupl.IsChecked = Properties.Settings.Default.NoAsk_Dupl;
            CheckImmid.IsChecked = Properties.Settings.Default.Down_Immid;
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var a = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (a.ShowDialog() == true) TxtPath.Text = a.SelectedPath;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtPath.Text == "더블클릭하여 다운로드 경로를 선택하거나 직접 입력" || Core.Route == TxtPath.Text) return;

            Core.Route = TxtPath.Text;

            if (File.Exists($"{System.IO.Path.GetTempPath()}{Core.RouteFile}.txt"))
            {
                if (File.ReadAllText($"{System.IO.Path.GetTempPath()}{Core.RouteFile}.txt") != Core.Route)
                {
                    File.WriteAllText($"{System.IO.Path.GetTempPath()}{Core.RouteFile}.txt", Core.Route);
                }
            }
            else
            {
                File.WriteAllText($"{System.IO.Path.GetTempPath()}{Core.RouteFile}.txt", Core.Route);
            }
        }

        private void CheckFolder_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.BookMark_Name = ((CheckBox)sender).IsChecked.GetValueOrDefault();
            Properties.Settings.Default.Save();
        }

        private void CheckAuthorName_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ShowAuthor_Folder = ((CheckBox)sender).IsChecked.GetValueOrDefault();
            Properties.Settings.Default.Save();
        }

        private void CheckImmid_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Down_Immid = ((CheckBox)sender).IsChecked.GetValueOrDefault();
            Properties.Settings.Default.Save();
        }

        private void CheckDupl_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.NoAsk_Dupl = ((CheckBox)sender).IsChecked.GetValueOrDefault(); 
            Properties.Settings.Default.Save();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
