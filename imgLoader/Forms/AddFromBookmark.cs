using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using imgLoader.Sites;

namespace imgLoader.Forms
{

    public partial class AddFromBookmark : Form
    {
       internal ListViewItem[] result;

        private delegate string FilterDele(string dirName);

        private ListViewItem[] lvItem;

        public AddFromBookmark()
        {
            InitializeComponent();
            cbxFilter.SelectedIndex = 0;
            cbxSite.SelectedIndex = 0;

        }

        private void AddFromBookmark_Load(object sender, EventArgs e)
        {
            listView1.Columns[0].Width = Core.COLUMN_WIDTH;

            LoadFromBookmark();
        }

        public T CastObject<T>(object input)
        {
            return (T)input;
        }

        public void LoadFromBookmark()
        {
            string bookMark;

            listView1.Items.Clear();
            checkBox1.Checked = false;

            string route = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            switch (cbxFilter.SelectedIndex)
            {
                case 0: //Chrome
                    route = $@"{route.Split('\\')[0]}\{route.Split('\\')[1]}\{route.Split('\\')[2]}\{route.Split('\\')[3]}\Local\Google\Chrome\User Data\Default\Bookmarks";
                    using (StreamReader sr = new StreamReader(route))
                    {
                        bookMark = sr.ReadToEnd();
                    }

                    break;

                case 1: //Edge
                    route = $@"{route.Split('\\')[0]}\{route.Split('\\')[1]}\{route.Split('\\')[2]}\{route.Split('\\')[3]}\Local\Microsoft\Edge\User Data\Default\Bookmarks";
                    using (StreamReader sr = new StreamReader(route))
                    {
                        bookMark = sr.ReadToEnd();
                    }

                    break;

                default:
                    MessageBox.Show("필터 설정 오류");
                    return;
            }

            string host;
            string[] supplement;
            FilterDele filt;

            var tempi = new []{ "imgLoader.Sites.Hitomi", "imgLoader.Sites.hiyobi", "imgLoader.Sites.pixiv", "imgLoader.Sites.nhentai" };

            var tempobj = new object();
            var ttempi = Type.GetType(tempi[cbxSite.SelectedIndex])?.GetField("Supplement")?.GetValue(tempobj);
            var ttempii = Type.GetType(tempi[cbxSite.SelectedIndex]).GetField("Host").GetValue(tempobj);
            var ttempiii = Type.GetType(tempi[cbxSite.SelectedIndex]).GetMethod("Filter");

            supplement = ttempi as string[];
            host = ttempii as string;
            filt = Delegate.CreateDelegate(typeof(FilterDele), ttempiii) as FilterDele;

            short imgNum = 1;

            foreach (string item in bookMark.Split(@"""name"": """).Where(item => item.Contains(host) && item.Contains($"/{supplement[0]}/")).ToArray())
            {
                var listitem = new ListViewItem(imgNum.ToString());
                listitem.SubItems.Add(filt(item.Split('"')[0]));          //다시돌려놓을것

                string bUrl = item.Split(@"""url"": """)[1].Split('"')[0];
                if (bUrl.Contains("#")) bUrl = bUrl.Split('#')[0];

                if (supplement.Length > 1) //supplement 추가시
                {
                    bUrl = $"{supplement[1]}{Core.GetNumber(bUrl)}";
                }

                listitem.SubItems.Add(bUrl);

                listView1.Items.Add(listitem);
                imgNum++;
            }

            lvItem = new ListViewItem[listView1.Items.Count];

            listView1.Items.CopyTo(lvItem, 0);

            if (listView1.Items.Count > 999)
            {
                listView1.Columns[0].Width += 10;
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (listView1.CheckedItems.Count == 0)
            {
                MessageBox.Show("선택된 항목이 없습니다.");
                return;
            }

            int numChecked = 0;

            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Checked) numChecked++;
            }
            result = new ListViewItem[numChecked];
            List<ListViewItem> temp = new List<ListViewItem>(numChecked);

            for (var i = 0; i < listView1.Items.Count; i++)
            {
                if (!listView1.Items[i].Checked)
                {
                    continue;
                }

                var item = (ListViewItem)listView1.Items[i].Clone();
                item.Name = item.SubItems[2].Text;
                item.SubItems.Add("N/A");
                item.SubItems.Add("N/A");
                temp.Add(item);
            }

            result = temp.ToArray();

            this.DialogResult = DialogResult.OK;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = checkBox1.Checked;
            }
        }

        private void CbxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSite.SelectedIndex == -1) return;
            LoadFromBookmark();
        }

        private void CbxSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFromBookmark();
        }

        private void TextBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Core.RestoreSearch(listView1,textBox1);
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Core.SearchListView(listView1, e, textBox1);
        }

        private void ContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (listView1.SelectedItems.Count != 0) contextMenuStrip1.Items[0].Enabled = true;
        }

        private void MenuItem_LinkCp_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView1.SelectedItems[0].SubItems[2].Text);
        }
    }
}