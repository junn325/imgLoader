using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using imgLoader.Sites;

namespace imgLoader
{
    internal static class Core
    {
        internal const string ProjectName = "imgLoader";
        internal const string RouteFile = "ILTempRout";
        internal const string IndexFile = "ILIdx";

        private const string LogDir = "ILLOG";
        private const string LogFile = "ILLG";

        private static readonly string[] DFilter = {"(", ")", "|", ":", "?", "\"", "<", ">", "/", "*", "..."};
        private static readonly string[] DReplace = { "（", "）", "│", "：", "？", "″", "˂", "˃", "／", "＊", "…"};

        internal const byte ColumnWidth = 45;
        internal static List<string> PrevAddress = new List<string>(5);

        private static readonly List<ListViewItem> LvItem = new List<ListViewItem>();

        internal static string Route = "";

        internal static void Log(string content)
        {
            new Thread(() =>
            {
                var sb = new StringBuilder(Path.GetTempPath());
                sb.Append('\\').Append(LogDir);

                if (!Directory.Exists(sb.ToString())) Directory.CreateDirectory(sb.ToString());

                var temp = false;
                FileStream file = null;
                sb.Append('\\').Append(LogFile).Append('_').Append(DateTime.Now.ToString("yyyy-MM-dd")).Append(".txt");

                while (!temp)
                {
                    try
                    {
                        file = new FileStream(sb.ToString(), FileMode.Append, FileAccess.Write);
                        temp = true;
                    }
                    catch
                    {
                        temp = false;
                    }
                }

                using var sw = new StreamWriter(file);
                sb.Clear();
                sb.Append('[').Append(DateTime.Now.ToString("HH:mm:ss")).Append(']').Append(content);
                sw.WriteLine(sb.ToString());
            }).Start();
        }

        internal static void CreateInfo(string infoRoute, ISite site)
        {
            if (!Directory.Exists(Path.GetDirectoryName(infoRoute))) throw new DirectoryNotFoundException();
            if (site == null) throw new NullReferenceException("\"site\" is null.");

            var file = new FileInfo(infoRoute);
            if (file.Exists && (file.Attributes & FileAttributes.Hidden) != 0) file.Attributes &= ~FileAttributes.Hidden;

            using var sw = new StreamWriter(new FileStream(infoRoute, FileMode.Create, FileAccess.ReadWrite), Encoding.UTF8);
            var info = site.ReturnInfo();
            for (var i = 0; i < info.Length; i++)
            {
                sw.Write(
                    i != info.Length - 1
                        ? info[i] + '\n'
                        : info[i]
                );
            }

            File.SetAttributes(infoRoute, FileAttributes.Hidden);
        }

        internal static string DirFilter(string dirName)
        {
            for (byte i = 0; i < DFilter.Length; i++)
                if (dirName.Contains(DFilter[i]))
                    dirName = dirName.Replace(DFilter[i], DReplace[i]);

            return dirName;
        }

        internal static string GetNumber(string url)
        {
            var val = url.Contains("//") ? url.Split("//")[1] : url;

            if (val.Contains("hitomi")) return val.Contains("-") ? val.Split('-').Last().Split('.')[0] : val.Split('/')[2].Split(".html")[0];
            if (val.Contains("hiyobi")) return val.Contains("#") ? val.Split('/')[2].Split('#')[0] : val.Split('/')[2];
            if (val.Contains("nhentai")) return val.Split('/')[2];
            if (val.Contains("pixiv"))
            {
                if (val.Contains("artworks")) return val.Split('/')[2];
                if (val.Contains("id=")) return val.Split("id=")[1];
            }

            if (val.Contains("e-hentai") || val.Contains("exhentai")) return val.Split("/g/")[1].Remove(val.Split("/g/")[1].Length - 1);

            return "";
        }

        internal static ISite LoadSite(string url)
        {
            var mNumber = GetNumber(url);
            if (mNumber.Length == 0) return null;

            if (url.Contains("nhentai.net", StringComparison.OrdinalIgnoreCase)) return new NHentai(mNumber);
            if (url.Contains("pixiv", StringComparison.OrdinalIgnoreCase)) return new Pixiv(mNumber);
            if (url.Contains("hiyobi.me", StringComparison.OrdinalIgnoreCase)) return new Hiyobi(mNumber);
            if (url.Contains("hitomi.la", StringComparison.OrdinalIgnoreCase)) return new Hitomi(mNumber);
            if (url.Contains("e-hentai.org", StringComparison.OrdinalIgnoreCase)) return new EHentai(mNumber);
            if (url.Contains("exhentai.org", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\nThis program may not possible to download from exhentai.org. Trying to download from Hitomi.la...");
                var temp = new Hitomi(mNumber.Contains('/') ? mNumber.Split('/')[0] : mNumber);
                //var tempp = 
                if (temp.IsValidated()) return temp;
            }

            return null;
        }

        internal static Dictionary<string, string> Index(string route)
        {
            const string countSeparator = "/**/";
            const string itemSeparator = "-**-";

            var count = 0;
            var sb = new StringBuilder();
            string file = null;
            Dictionary<string, string> infos;

            if (File.Exists($"{Path.GetTempPath()}{IndexFile}.txt"))
            {
                file = File.ReadAllText($"{Path.GetTempPath()}{IndexFile}.txt");
                if (file.Length != 0 || file.Contains("/**/"))
                {
                    count = int.Parse(file.Split(countSeparator)[0]);
                }
            }

            var infoFiles = Directory.EnumerateFiles(route, "*.*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".Hitomi") || s.EndsWith(".Hiyobi") || s.EndsWith(".NHentai") || s.EndsWith("EHentai")).ToArray();

            if (file != null && infoFiles.Length == count)
            {
                var content = file.Split(countSeparator)[1];
                infos = new Dictionary<string, string>(count);
                foreach (var s in content.Split(itemSeparator))
                {
                    if (s.Length == 0) continue;
                    infos.Add(s.Split('`')[0], s.Split('`')[1]);
                }

                return infos;
            }

            infos = new Dictionary<string, string>(infoFiles.Length);
            var tasks = new Task[infoFiles.Length];

            for (var i = 0; i < infoFiles.Length; i++)
            {
                var infoRoute = infoFiles[i];
                tasks[i] = Task.Factory.StartNew(() =>
                {
                    lock (sb)
                    {
                        var info = File.ReadAllText(infoRoute);
                        infos.Add(infoRoute, info);
                        sb.Append(infoRoute).Append('`').Append(info).Append(itemSeparator);
                        if (sb.Length != sb.Replace("\0", string.Empty).Length)
                            Debug.Write($"*/*/*/*/*{infoRoute}, {info}");
                    }
                });
            }

            Task.WaitAll(tasks);

            File.WriteAllText($"{Path.GetTempPath()}{IndexFile}.txt", $"{infos.Count}{countSeparator}{sb}", Encoding.UTF8);
            Debug.Write(sb);

            return infos;
        }

        internal static void Search(Dictionary<string, string> index, string search, string route)
        {
            var searchResult = new Dictionary<string, string>(index);
            foreach (var (key, value) in index)
            {
                foreach (var srch in search.Split(','))
                {
                    if (!value.Contains(srch, StringComparison.OrdinalIgnoreCase))
                    {
                        searchResult.Remove(key);
                    }
                }
            }

            foreach (var item in searchResult)
            {
                Console.WriteLine(item.Key.Replace(route, "%BaseDir%").Replace($"\\{item.Key.Split('\\').Last()}", "").Insert(10, " "));
            }

            Console.WriteLine($"{searchResult.Count} results");
            Console.WriteLine(new string('=', 100));
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
            if (x == null || y == null) throw new NullReferenceException("x or y was Null.");
            return (int.Parse(((ListViewItem)x).Text) > int.Parse(((ListViewItem)y).Text)) ? 1 : -1;
        }
    }

}
