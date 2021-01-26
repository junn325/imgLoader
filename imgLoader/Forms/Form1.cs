using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using imgLoader.LoaderList;

namespace imgLoader.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var item = new LoaderItem();

            loaderList1.Controls.Add(item);
            loaderList1.AutoScrollOffset = new Point(item.Left, item.Bottom);
        }
    }
}
