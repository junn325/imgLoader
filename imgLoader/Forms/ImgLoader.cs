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
