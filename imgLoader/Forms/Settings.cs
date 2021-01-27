using System;
using System.IO;
using System.Windows.Forms;
//using Microsoft.WindowsAPICodePack.Dialogs;

namespace imgLoader.Forms
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            txtRoute.Text = Core.Route;

            chkExist.Checked = Properties.Settings.Default.reDownload;
            chkAuthor.Checked = Properties.Settings.Default.showAuthor;
            chkMainAlways.Checked = Properties.Settings.Default.mainAlways;
            chkBookTitle.Checked = Properties.Settings.Default.bookTitle;
        }

        private void txtRoute_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //CommonOpenFileDialog dialog = new CommonOpenFileDialog
            //{
            //    DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            //    IsFolderPicker = true,
            //    RestoreDirectory = true
            //};

            //if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            //{
            //    Core.Route = dialog.FileName;
            //    txtRoute.Text = Core.Route;
            //}
        }

        private void txtRoute_Click(object sender, EventArgs e)
        {
            txtRoute.SelectAll();
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {

            Core.Route = txtRoute.Text;

            if (File.Exists($"{Path.GetTempPath()}{Core.RouteFile}.txt"))
            {
                if (File.ReadAllText($"{Path.GetTempPath()}{Core.RouteFile}.txt") != Core.Route)
                {
                    File.WriteAllText($"{Path.GetTempPath()}{Core.RouteFile}.txt", Core.Route);
                }
            }
            else File.WriteAllText($"{Path.GetTempPath()}{Core.RouteFile}.txt", Core.Route);


            Properties.Settings.Default.reDownload = chkExist.Checked;
            Properties.Settings.Default.showAuthor = chkAuthor.Checked;
            Properties.Settings.Default.mainAlways = chkMainAlways.Checked;
            Properties.Settings.Default.bookTitle = chkBookTitle.Checked;

            Properties.Settings.Default.Save();

            DialogResult = DialogResult.OK;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}