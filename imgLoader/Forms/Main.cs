using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imgLoader.Forms
{
    //todo: 작업 표시줄에 프로그래스바 
    //todo: numericupdown 같은것으로 작품별로 순위 매기는 시스템
    //todo: 작가, 태그 등으로 자동으로 폴더로 나눠주는 시스템
    //todo: 항상 위로 상태로 떠 있다가 인터넷 창에서 누르면 자동으로 해당 작품 다운로드 
    //todo: 작가별 트리식 정렬
    //todo: 작가/태그 분포, 주로 보는 작품 등 분석 기능
    //todo: 여러 폴더를 지정해 동시에 관리
    //todo: 특정 이미지 숨기기(삭제x)
    //todo: 아무 값 없는 분류(그냥 빨주노초파남보) 분류 기능
    public partial class Main : Form
    {
        internal const string InitString_Item  = "0개 항목";
        internal const string InitString_NUM   = "*/*";

        internal const ushort FORM_WIDTH  = 825;
        internal const ushort FORM_HEIGHT = 495;

        private readonly Dictionary<string, string> failed = new Dictionary<string, string>();
        private List<Task> tasks = new List<Task>();
        private bool _stop = false;

        private Thread thrDownStart;

        private int _done;

        public Main()
        {
            InitializeComponent();
            Size = new Size(FORM_WIDTH, FORM_HEIGHT);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Text = Core.ProjectName;

            listView1.Columns[0].Width = Core.ColumnWidth;

            if (File.Exists($"{Path.GetTempPath()}{Core.TempRoute}.txt") && Directory.Exists(File.ReadAllText($"{Path.GetTempPath()}{Core.TempRoute}.txt")))
            {
                Core.Route = File.ReadAllText($"{Path.GetTempPath()}{Core.TempRoute}.txt");
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = true;
            Core.ControlLock(new List<Control> { listView1, checkBox1, textBox1, btnStart });

            var itm = new Dictionary<string, string>();
            progAll.Value = 0;

            foreach (ListViewItem item in listView1.CheckedItems)
            {
                itm.Add(item.SubItems[1].Text, item.SubItems[2].Text);
            }

            if (itm.Count == 0)
            {
                MessageBox.Show("다운로드할 항목이 없습니다.");
                //Stopping();
                return;
            }

            if (!Directory.Exists(Core.Route))
            {
                MessageBox.Show("존재하지 않는 경로입니다.");
                //Stopping();
                return;
            }

            toolStrip_lblTotalNum.Text = "0/*";

            //thrDownStart = new Thread(() => Process(itm)) { Name = "스레드" };
            thrDownStart.Start();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = checkBox1.Checked;
            }
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            //Processor.Stopping();
        }

        private void TextBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string address = textBox1.Text, title;
                Core.PrevAddress.Add(address);

                textBox1.Text = "";
                e.SuppressKeyPress = true;

                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.SubItems[2].Text == address) return;
                }

                try
                {
                    var site = Core.LoadSite(address);

                    if (site == null || !site.IsValidated()) { MessageBox.Show("주소에 연결할 수 없음."); return; }

                    title = site.GetTitle();
                }
                catch
                {
                    return;
                }

                var temp = new ListViewItem();
                temp.Text = (listView1.Items.Count + 1).ToString();

                temp.SubItems.Add(title ?? "");
                temp.SubItems.Add(address);
                temp.Name = address;
                listView1.Items.Add(temp);


                toolStrip_lblNum.Text = InitString_Item.Replace("0", listView1.Items.Count.ToString());
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (Core.PrevAddress.Count == 0) return;
                textBox1.Text = Core.PrevAddress[^1];
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var temp = new AddFromBookmark();
            temp.LoadFromBookmark();
            ;
        }

        private void MenuItem_LinkCp_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView1.SelectedItems[0].SubItems[2].Text);
        }

        private void MenuItem_Remove_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.CheckedItems)
            {
                item.Remove();
            }

            foreach (ListViewItem item in listView1.SelectedItems)
            {
                item.Remove();
            }
        }

        private void MenuItem_Add_Click(object sender, EventArgs e)
        {
            var addBook = new AddFromBookmark();
            if (addBook.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (ListViewItem item in addBook.GetResult())
            {
                if (listView1.Items.ContainsKey(item.SubItems[2].Text)) continue;

                var temp = new ListViewItem((listView1.Items.Count + 1) + "*");
                temp.SubItems.Add(item.SubItems[1]);
                temp.SubItems.Add(item.SubItems[2].Text);
                temp.SubItems.Add(item.SubItems[3]);
                temp.SubItems.Add(item.SubItems[4]);
                temp.Checked = Properties.Settings.Default.mainAlways;

                listView1.Items.Add(temp);
            }

            if (listView1.Items.Count > 99)
            {
                listView1.Columns[0].Width = 55;
            }    
            if (listView1.Items.Count > 999)
            {
                listView1.Columns[0].Width = 65;
            }
        }

        private void MenuItem_RemoveAll_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            checkBox1.Checked = false;
        }

        private void MenuItem_Settings_Click(object sender, EventArgs e)
        {
            Settings setting = new Settings();
            setting.ShowDialog();
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            textBox1.Text = "주소 입력 후 엔터로 추가";
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            menuItem_LinkCp.Enabled = listView1.SelectedItems.Count != 0;
            menuItem_Remove.Enabled = listView1.SelectedItems.Count != 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f = new Form1();
            f.Show();
        }
    }
}