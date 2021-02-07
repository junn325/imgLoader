using imgLoader_WPF.Sites;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using imgLoader_WPF.LoaderListCtrl;

namespace imgLoader_WPF
{
    internal static class Core
    {
        internal const string ProjectName = "imgLoader";
        internal const string RouteFile = "ILTempRout";
        internal const string IndexFile = "ILIdx";

        internal const string LogDir = "ILLOG";
        internal const string LogFile = "ILLG";

        internal const string InfoExt = "ilif";

        private static readonly string[] DFilter =  { "(",  ")",  "|", ":",  "?", "\"", "<", ">", "/", "*", "..."};
        private static readonly string[] DReplace = { "（", "）", "│", "：", "？", "″", "˂", "˃", "／", "＊", "…"};

        //internal const byte ColumnWidth = 45;
        //internal static List<string> PrevAddress = new List<string>(5);

        //private static readonly List<ListViewItem> LvItem = new List<ListViewItem>();

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

        private static void InfoEncrypt(string path, string[] info)
        {
            var stream = new FileStream(path, FileMode.Create);
            var writer = new BinaryWriter(stream);

            try
            {
                foreach (var s in info)
                {
                    if (s == null)
                    {
                        writer.Write('\0');
                        continue;
                    }

                    writer.Write(StringCipher.Encrypt(s + "\n"));
                }
            }
            finally
            {
                writer.Close();
                stream.Close();
            }
        }

        private static void InfoDecrypt(string path)
        {
            var stream = new FileStream(path, FileMode.Open);
            var reader = new BinaryReader(stream);

            try
            {
                var sb = new StringBuilder();

                do
                {
                    sb.Append(StringCipher.Decrypt(reader.ReadString()));
                } while (reader.PeekChar() != -1);

                MessageBox.Show(sb.ToString());
            }
            finally
            {
                reader.Close();
                stream.Close();
            }
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

        /// <summary>
        ///  Can be null
        /// </summary>
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

        internal static T[] AppendArray<T>(T[] a, T[] b)
        {
            var temp = new T[a.Length + b.Length];

            for (var i = 0; i < a.Length; i++)
            {
                temp[i] = a[i];
            }

            for (var i = 0; i < b.Length; i++)
            {
                temp[i + a.Length] = b[i];
            }

            return temp;
        }

        internal static string GetDirectoryFromFile(string path)
        {
            return path.Substring(0, path.IndexOf(path.Split('\\').Last(), StringComparison.Ordinal));
        }

        internal static void OpenDir(string path)
        {
            Process.Start("explorer.exe", path);
        }
    }

    internal class ItemRefreshService
    {
        private bool _stop;

        private readonly Dictionary<string, string> _index;
        private int _indexCnt;

        private readonly Label _label;
        private readonly LoaderList _list;

        public ItemRefreshService(Dictionary<string, string> index, LoaderList list, Label label)
        {
            _label = label;
            _list = list;

            _indexCnt = index.Count;
            _index = index;
        }

        internal void Start()
        {
            var service = new Thread(() =>
            {
                int count = 0;
                while (!_stop)
                {
                    _list.Dispatcher.Invoke(() => count = _list.Children.Count);

                    if (count == _index.Count && _indexCnt == _index.Count)
                    {
                        Thread.Sleep(2000);
                        continue;
                    }

                    Dictionary<string, string> temp = null;

                    _list.Dispatcher.Invoke(() =>
                    {
                        using (_list.Dispatcher.DisableProcessing())
                        {
                            _list.Children.Clear();

                            temp = new Dictionary<string, string>(_index);
                            foreach (var (path, info) in temp)
                            {
                                if (string.IsNullOrEmpty(info)) continue;
                                var file = info.Split("\n");

                                var lItem = new LoaderItem(file[1], file[2], file[3], file[0], path, path.Split('\\').Last().Split('.')[0], file[6].Length != 0 ? int.Parse(file[6]) : 0);
                                lItem.Tags = file[4].Split("tags:")[1].Split('\n')[0].Split(';');
                                _list.Children.Add(lItem);
                            }
                        }
                    });

                    _indexCnt = temp.Count;
                    _label.Dispatcher.Invoke(() => _label.Content = $"{temp.Count}개 항목");

                    Thread.Sleep(2000);
                }
            });

            service.Name = "Rfshsvc";
            service.Start();
        }
        internal void Stop()
        {
            _stop = true;
        }
    }
    internal class IndexingService
    {
        private bool _stop;
        private readonly string _route;
        private readonly Dictionary<string, string> _index;

        public IndexingService(string route, Dictionary<string, string> index)
        {
            _route = route;

            _index = index;
            DoIndex(_route, _index);
        }

        private static void DoIndex(string route, Dictionary<string, string> index)
        {
            const string countSeparator = "/**/";
            const string itemSeparator = "-**-";

            var tempPath = Path.GetTempPath();
            var infoFiles = Directory.GetFiles(route, $"*.{Core.InfoExt}", SearchOption.AllDirectories);

            //if (File.Exists($"{tempPath}{Core.IndexFile}.txt"))
            //{
            //    var file = File.ReadAllText($"{tempPath}{Core.IndexFile}.txt");
            //    if (file.Length != 0 && file.Contains("/**/") && int.Parse(file.Split(countSeparator)[0]) == infoFiles.Length)
            //    {
            //        return file.Split(countSeparator)[1].Split(itemSeparator).Where(s => s.Length != 0).ToDictionary(s => s.Split('`')[0], s => s.Split('`')[1]);
            //    }
            //}

            var sb = new StringBuilder();
            //var infos = new Dictionary<string, string>(infoFiles.Length);
            index.Clear();
            var tasks = new Task[infoFiles.Length];

            for (var i = 0; i < infoFiles.Length; i++)
            {
                var infoRoute = infoFiles[i];
                tasks[i] = Task.Factory.StartNew(() =>
                {
                    lock (sb)
                    {
                        using var sr = new StreamReader(new FileStream(infoRoute, FileMode.OpenOrCreate, FileAccess.ReadWrite), Encoding.UTF8);
                        var info = sr.ReadToEnd();
                        index.Add(infoRoute, info);
                        sb.Append(infoRoute).Append('`').Append(info).Append(itemSeparator);
                    }
                });
            }

            Task.WaitAll(tasks);

            File.WriteAllText($"{tempPath}{Core.IndexFile}.txt", $"{index.Count}{countSeparator}{sb}", Encoding.UTF8);
        }

        internal void Start()
        {
            var service = new Thread(() =>
            {
                while (!_stop)
                {
                    //if (temp.Count == Directory.GetFiles(_route, $"*.{Core.InfoExt}", SearchOption.AllDirectories).Length) continue;

                    DoIndex(_route, _index);
                    Thread.Sleep(2000);
                }
            });

            service.Name = "IdxSvc";
            service.Start();
        }

        internal void Stop()
        {
            _stop = true;
        }
    }
    internal class VoteSavingService
    {
        private bool _stop;

        internal void Start(LoaderList list)
        {
            var service = new Thread(() =>
            {
                while (!_stop)
                {
                    list.Dispatcher.Invoke(() =>
                    {
                        foreach (LoaderItem item in list.Children)
                        {
                            var file = new FileInfo(item.Route);
                            if (!file.Exists) continue;

                            using var sr = new StreamReader(new FileStream(item.Route, FileMode.OpenOrCreate, FileAccess.ReadWrite), Encoding.UTF8);
                            var temp = sr.ReadToEnd().Split('\n');

                            if (temp.Length == 7 && item.Vote.ToString() == temp[6]) continue;

                            //if ((file.Attributes & FileAttributes.Hidden) != 0) File.SetAttributes(item.Route, FileAttributes.Normal);

                            var info = new string[7];

                            temp.CopyTo(info, 0);
                            info[6] = item.Vote.ToString();

                            using var sw = new StreamWriter(new FileStream(item.Route, FileMode.OpenOrCreate, FileAccess.ReadWrite), Encoding.UTF8);
                            for (var i = 0; i < info.Length; i++)
                            {
                                sw.Write(
                                    i != info.Length - 1
                                        ? info[i] + '\n'
                                        : info[i]
                                );
                            }

                            //File.SetAttributes(item.Route, FileAttributes.Hidden);
                        }
                    });

                    Thread.Sleep(2000);
                }
            });

            service.Name = "VtSvc";
            service.Start();
        }

        internal void Stop()
        {
            _stop = true;
        }
    }
}
