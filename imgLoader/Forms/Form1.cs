using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            var p = new PictureBox();
            p.Size = new Size(50, 20);
            p.Location = new Point(5, 5);
            p.BackColor = Color.Red;

            loaderList1.Controls.Add(p);
        }
    }
}
