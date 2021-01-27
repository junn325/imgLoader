using System.Windows.Forms;
using System.Diagnostics;

namespace imgLoader.LoaderList
{
    public partial class LoaderItem : UserControl
    {
        public string Title
        {
            get => title.Text;
            set => title.Text = value;
        }

        public string Author
        {
            get => author.Text;
            set => author.Text = value;
        }

        public string ImgCount
        {
            get => imgCount.Text;
            set => imgCount.Text = (value + "장");
        }

        public string SiteName
        {
            get => siteName.Text;
            set => siteName.Text = value;
        }

        public string Route
        {
            get => route.Text;
            set => route.Text = value;
        }

        public LoaderItem()
        {
            InitializeComponent();
        }

        //private void imgCount_SizeChanged(object sender, System.EventArgs e)
        //{
        //    route.Left = imgCount.Right + 4;
        //}
    }
}
