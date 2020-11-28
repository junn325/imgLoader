using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using imgLoader.Sites;

namespace imgLoader
{
    internal static class Core
    {
        internal const byte COLUMN_WIDTH = 45;
        internal const byte WAIT_TIME    = 25; //밀리세컨드
        internal const byte FAIL_RETRY   = 10;

        internal const string PROJECT_NAME = "imgLoader";
        internal const string TEMP_ROUTE   = "ILTempRout";

        private const string LOG_DIR = "ILLOG";
        private const string LOG_FILE = "ILLG";

        private static readonly string[] DFILTER = { "(", ")", "|", ":", "?", @"""", "<", ">", "/", "*" };
        private static readonly string[] DREPLACE = { "[", "]", ";", "-", "", "''", "[", "]", "", "" };
        internal static List<string> prevAddress = new List<string>(5);

        private static readonly List<ListViewItem> LvItem = new List<ListViewItem>();

        internal static string Route = "";

        internal static void Log(string content)
        {
            new Thread(() => {
                if (!Directory.Exists(Path.GetTempPath() + @$"\{LOG_DIR}"))
                {
                    Directory.CreateDirectory(Path.GetTempPath() + @$"\{LOG_DIR}");
                }

                var temp = false;
                FileStream file = null;

                while (!temp)
                {
                    try
                    {
                        file = new FileStream(Path.GetTempPath() + @$"\{LOG_DIR}\{LOG_FILE}_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", FileMode.Append, FileAccess.Write);
                        temp = true;
                    }
                    catch
                    {
                        temp = false;
                    }
                }

                using StreamWriter sw = new StreamWriter(file);
                sw.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + content);
            }).Start();
        }

        internal static void CreateInfo(string baseRoute, string mNumber, ISite site)
        {
            string fileName = $"{baseRoute}\\{mNumber}.{site.GetType().Name.ToLower()}";
            FileInfo file = new FileInfo(fileName);

            if (file.Exists)                                                                        //총 이미지 장수 넣기
            {
                file.Attributes &= ~FileAttributes.Hidden;
            }
            using StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite), Encoding.UTF8);

            foreach (var item in site.ReturnInfo())
            {
                sw.WriteLine(item);
            }

            File.SetAttributes(fileName, FileAttributes.Hidden);
        }

        internal static string DirFilter(string dirName)
        {
            for (byte i = 0; i < DFILTER.Length; i++)
            {
                if (dirName.Contains(DFILTER[i]))
                {
                    dirName = dirName.Replace(DFILTER[i], DREPLACE[i]);
                }
            }

            return dirName;
        }
        internal static void ControlLock(List<Control> econtrol)
        {
            foreach (var item in econtrol)
            {
                item.Enabled = false;
            }
        }

        internal static void ControlUnlock(List<Control> econtrol)
        {
            foreach (var item in econtrol)
            {
                if (item.InvokeRequired)
                {
                    item.BeginInvoke(new Action(() => item.Enabled = true));
                }
                else
                {
                    item.Enabled = true;
                }
            }
        }

        internal static string GetNumber(string url)
        {
            //  nhentai.net/g/169878/
            //  hitomi.la/reader/1038169.html#5
            //  hiyobi.me/reader/1574526#24-25
            //  www.pixiv.net/artworks/77832611
            try
            {
                //string reg = Regex.Match(url, @"(https:\/\/|).*\/(\d*)\/").Groups[1].Value;
                string val = url.Contains("//") ? url.Split("//")[1] : url;

                if (val.Contains("#")) val = val.Split('#')[0];
                if (val.Split('/').Last().Length == 0) val = val.Split('/')[val.Split('/').Length - 2];    //nhentai
                else val = val.Split('/').Last();                                                          //hitomi/hiyobi/pixiv
                if (val.Contains(".html")) val = val.Split(".html")[0];                                    //hitomi

                return int.TryParse(val, out _) ? val : "";
            }
            catch
            {
                return "";
            }
        }

        internal static void SearchListView(ListView listview, KeyEventArgs e, TextBox text)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;

            foreach (ListViewItem item in from item in listview.Items.Cast<ListViewItem>()
                where item.SubItems[1].Text.IndexOf(text.Text, StringComparison.OrdinalIgnoreCase) < 0
                orderby item.Text
                select item)
            {
                LvItem.Add(item);
                item.Remove();
            }
        }

        internal static void RestoreSearch(ListView listview, TextBox textbox)
        {
            if (LvItem == null) return;
            if (textbox.TextLength > 0) return;

            foreach (ListViewItem item in LvItem)
            {
                listview.Items.Add(item);
            }

            listview.ListViewItemSorter = new ListViewItemComparer();
            listview.Sort();
        }

        internal static ISite LoadSite(string link)
        {
            string mNumber = GetNumber(link);

            if (mNumber.Length == 0) return null;

            if (link.Contains("hiyobi.me")) return new hiyobi(mNumber);
            if (link.Contains("hitomi.la")) return new Hitomi(mNumber);
            if (link.Contains("pixiv")) return new pixiv(mNumber);
            if (link.Contains("nhentai.net")) return new nhentai(mNumber);

            return null;
        }
    }

    internal class ListViewItemComparer : IComparer
    {
        private readonly int _col;
        public ListViewItemComparer()
        {
            _col = 0;
        }
        public ListViewItemComparer(int column)
        {
            _col = column;
        }
        public int Compare(object x, object y)
        {
            //return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            return (int.Parse(((ListViewItem)x).Text) > int.Parse(((ListViewItem)y).Text)) ? 1 : -1;
        }
    }

}