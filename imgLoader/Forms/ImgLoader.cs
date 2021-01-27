using imgLoader.LoaderList;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace imgLoader.Forms
{
    public partial class ImgLoader : Form
    {
        private int i;
        public ImgLoader()
        {
            InitializeComponent();
        }

        private void ImgLoader_Load(object sender, EventArgs e)
        {
            const string route = "D:\\문서\\사진\\Saved Pictures\\고니\\manga";

            var index = Core.Index(route);
            foreach (var (path, info) in index)
            {
                var file = info.Split("\n");
                var lItem = new LoaderItem
                {
                    Title = file[0],
                    Author = file[1],
                    SiteName = path.Split('.').Last(),
                    ImgCount = file[2],
                    Route = path
                };
                loaderList1.Controls.Add(lItem);
            }

            label1.Text = $"item: {loaderList1.Controls.Count}개";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var sites = new string[] {"hiyobi", "Hitomi", "nhentai", "e-hentai"};
            var lItem = new LoaderItem { Title = $"{i++}번째 항목", Author = ((char)(i * 10)).ToString(), SiteName = sites[i % 4], ImgCount = i.ToString() };
            loaderList1.Controls.Add(lItem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var temp = new Form1();
            temp.Show();
        }
    }
}
