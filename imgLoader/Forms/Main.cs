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
            Text = Core.PROJECT_NAME;

            listView1.Columns[0].Width = Core.COLUMN_WIDTH;

            if (File.Exists($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt") && Directory.Exists(File.ReadAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt")))
            {
                Core.Route = File.ReadAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt");
            }
        }

        private void Process(Dictionary<string,string> url)
        {
            try
            {
                var sw = Stopwatch.StartNew();
                Trace.WriteLine($"Download: start: {sw.ElapsedMilliseconds}ms");
                progAll.Invoke(new Action(() => progAll.Style = ProgressBarStyle.Marquee));

                Thread thStop = new Thread(Stopping);
                long millisec = sw.ElapsedMilliseconds;

                foreach (var link in url)
                {
                    if (url.Count == 0) break;

                    ISite site;
                    Dictionary<string, string> imgList;

                    string artist, title, route;

                    try
                    {
                        //todo: AddUrl에서 이미 로드했으므로 ListViewItem에서 모든 정보를 받아와 진행할 것
                        //todo: column이 없는 SubItem에 추가하여 숨겨진 정보로 처리할 것

                        Trace.WriteLine($"BeforeNewSite: {sw.ElapsedMilliseconds - millisec}ms");
                        millisec = sw.ElapsedMilliseconds;

                        site = Core.LoadSite(link.Value);
                        if (site?.IsValidated() != true) { MessageBox.Show("오류: 로드 실패"); return; }

                        Trace.WriteLine($"BeforeGetList: {sw.ElapsedMilliseconds - millisec}ms"); millisec = sw.ElapsedMilliseconds;

                        toolStrip_lblStatus.Text = "이미지 리스트 추출";
                        progAll.Invoke(new Action(() => progAll.Value = 0));
                        toolStrip_lblTotalNum.Text = $"{ushort.Parse(toolStrip_lblTotalNum.Text.Split('/')[0]) + 1}/{url.Count}";
                        imgList = site.GetImgUrls();
                        Trace.WriteLine($"GetImgUrls: {sw.ElapsedMilliseconds - millisec}ms"); millisec = sw.ElapsedMilliseconds;

                        progAll.Invoke(new Action(() => progAll.Maximum = imgList.Count));
                        toolStrip_lblItemNum.Text = $"0/{imgList.Count}";

                        toolStrip_lblStatus.Text = "작가명 추출";
                        artist = site.GetArtist();
                        Trace.WriteLine($"GetArtist: {sw.ElapsedMilliseconds - millisec}ms"); millisec = sw.ElapsedMilliseconds;

                        if (!Properties.Settings.Default.bookTitle)
                        {
                            toolStrip_lblStatus.Text = "작품명 추출";
                            title = Core.DirFilter(site.GetTitle());
                        }
                        else
                        {
                            title = link.Key;
                        }
                        Trace.WriteLine($"GetTitle: {sw.ElapsedMilliseconds - millisec}ms"); millisec = sw.ElapsedMilliseconds;

                        route =
                            artist == "N/A"
                            ? $@"{Core.Route}\{title}"
                            : Properties.Settings.Default.showAuthor
                                ? $@"{Core.Route}\{title} ({artist})"
                                : $@"{Core.Route}\{title}";
                    }
                    catch
                    {
                        MessageBox.Show($"{link.Value}: \n연결에 실패했습니다.");
                        continue;
                    }

                    try
                    {
                        if (!Directory.Exists(route)) Directory.CreateDirectory(route);
                        Core.CreateInfo(route, Core.GetNumber(link.Value), site);
                    }
                    catch (DirectoryNotFoundException)
                    {
                        MessageBox.Show("오류: 디렉토리를 찾을 수 없음");
                    }
                    catch (FileNotFoundException)
                    {
                        MessageBox.Show("오류: 파일을 찾을 수 없음");
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e);
                        throw;
                    }

                    _done = 0;

                    tasks = new List<Task>();
                    progAll.Invoke(new Action(() => progAll.Style = ProgressBarStyle.Continuous));

                    AllocDown(route, imgList);
                    Trace.WriteLine($"AllocDown: {sw.ElapsedMilliseconds - millisec}ms");

                    while (_done < imgList.Count - failed.Count) Thread.Sleep(Core.WAIT_TIME);

                    _done = 0;

                    HandleFail(route);
                    while (_done < failed.Count) Thread.Sleep(Core.WAIT_TIME);

                    Core.Log($"Item:Complete: {link.Value}");
                    Trace.WriteLine($"Complete: {sw.ElapsedMilliseconds - millisec}ms");
                }

                thStop.Start();
            }
            catch (ThreadInterruptedException) {}
        }

        private void AllocDown(string route, Dictionary<string, string> urlList)
        {
            failed.Clear();

            toolStrip_lblStatus.Text = "배치 중";

            progAll.Invoke(new Action(() => progAll.Value = 0));
            progAll.Invoke(new Action(() => progAll.Maximum = urlList.Count));

            toolStrip_lblStatus.Text = "다운로드 중";

            foreach (var item in urlList)
            {
                tasks.Add(Task.Factory.StartNew(() => ThrDownload(item.Value, route, item.Key)));
            }
        }

        private void ThrDownload(string uri, string route, string fileName)
        {
            if (_stop)
            {
                return;
            }

            var req = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse resp;

            req.Referer = $"https://{new Uri(uri).Host}";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36";

            try
            {
                resp = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException we)
            {
                if (we.Response == null)
                {
                    Core.Log($"실패:응답없음: {uri}");
                    failed.Add(fileName, uri);
                    return;
                }

                Core.Log($"실패:{((HttpWebResponse)we.Response).StatusCode}: {uri}");
                failed.Add(fileName, uri);
                return;
            }

            if (!Properties.Settings.Default.reDownload && File.Exists($"{route}\\{fileName}") && new FileInfo($"{route}\\{fileName}").Length == resp.ContentLength)     //존재하는 파일 다운로드 스킵
            {
                progAll.Invoke(new Action(() => progAll.Value++));
                toolStrip_lblItemNum.Text = $"{progAll.Value}/{toolStrip_lblItemNum.Text.Split('/')[1]}";

                _done++;
                return;
            }

            long fileSize;

            using (var br = resp.GetResponseStream())
            {
                int count;
                byte[] buff = new byte[1024];

                using var fs = new FileStream($"{route}\\{fileName}", FileMode.Create);
                do
                {
                    count = br.Read(buff, 0, buff.Length);
                    fs.Write(buff, 0, count);

                } while (count > 0);

                fileSize = fs.Length;
            }

            if (fileSize == resp.ContentLength)
            {
                progAll.Invoke(new Action(() => progAll.Value++));
                toolStrip_lblItemNum.Text = $"{progAll.Value}/{toolStrip_lblItemNum.Text.Split('/')[1]}";
            }
            else
            {
                failed.Add(fileName, uri);
            }

            _done++;
        }

        private void HandleFail(string route)
        {
            if (failed.Count == 0) return;

            int cnt = Core.FAIL_RETRY;
            var failCopy = new Dictionary<string, string>(failed);

            AllocDown(route, failCopy);

            while (_done < failCopy.Count - failed.Count) Thread.Sleep(Core.WAIT_TIME);

            while (failed.Count != 0 && cnt > 0)
            {
                cnt--;
                HandleFail(route);
            }

            failed.Clear();
        }

        private void Stopping()
        {
            new Thread(() =>
              {
                  if (thrDownStart == null) return;

                  toolStrip_lblStatus.Text = "중지중";

                  while (thrDownStart.ThreadState == System.Threading.ThreadState.Running)
                  {
                      thrDownStart.Interrupt();
                      Thread.Sleep(Core.WAIT_TIME);
                  }

                  _stop = true;

                  foreach (var item in tasks) while (item.Status != TaskStatus.RanToCompletion) Thread.Sleep(Core.WAIT_TIME);

                  _stop = false;

                  failed.Clear();
                  tasks.Clear();

                  btnStop.Invoke(new Action(() => btnStop.Enabled = false));

                  toolStrip_lblNum.Text = InitString_Item.Replace("0", listView1.Items.Count.ToString());
                  toolStrip_lblItemNum.Text = InitString_NUM;
                  toolStrip_lblTotalNum.Text = InitString_NUM;

                  toolStrip_lblStatus.Text = "";

                  progAll.Invoke(new Action(() => progAll.Value = 0));
                  progAll.Invoke(new Action(() => progAll.Style = ProgressBarStyle.Continuous));

                  Core.ControlUnlock(new List<Control> { listView1, checkBox1, textBox1, btnStart });

              }).Start();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = true;
            Core.ControlLock(new List<Control> { listView1, checkBox1, textBox1, btnStart });

            var itm = new Dictionary<string,string>();
            progAll.Value = 0;

            foreach (ListViewItem item in listView1.CheckedItems)
            {
                itm.Add(item.SubItems[1].Text, item.SubItems[2].Text);
            }

            if (itm.Count == 0)
            {
                MessageBox.Show("다운로드할 항목이 없습니다.");
                Stopping();
                return;
            }

            if (!Directory.Exists(Core.Route))
            {
                MessageBox.Show("존재하지 않는 경로입니다.");
                Stopping();
                return;
            }

            toolStrip_lblTotalNum.Text = "0/*";

            thrDownStart = new Thread(() => Process(itm)) { Name = "스레드" };
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
            Stopping();
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
                Core.prevAddress.Add(address);

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
                if (Core.prevAddress.Count == 0) return;
                textBox1.Text = Core.prevAddress[^1];
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
    }
}