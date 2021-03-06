using imgLoader_WPF.Sites;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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

        private static readonly string[] DFilter = {"|", ":", "?", "\"", "<", ">", "/", "*", "..." };
        private static readonly string[] DReplace = { "│", "：", "？", "″", "˂", "˃", "／", "＊", "…" };

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
                sw.Close();
            }).Start();
        }

        internal static void CreateInfo(string infoFileName, ISite site)
        {
            if (!Directory.Exists(GetDirectoryFromFile(infoFileName)))
                throw new DirectoryNotFoundException();
            if (site == null)
                throw new NullReferenceException("\"site\" is null.");

            var file = new FileInfo(infoFileName);
            if (file.Exists && (file.Attributes & FileAttributes.Hidden) != 0) file.Attributes &= ~FileAttributes.Hidden;

            using var sw = new StreamWriter(new FileStream(infoFileName, FileMode.Create, FileAccess.ReadWrite), Encoding.UTF8);
            var info = site.ReturnInfo();

            for (var i = 0; i < info.Length; i++)
            {
                sw.Write(
                    i != info.Length - 1
                        ? info[i] + '\n'
                        : info[i]
                );
            }
            sw.Write($"\n{DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture)}\n0\n1"); //0 = Vote, 1 = Show

            sw.Close();

            File.SetAttributes(infoFileName, FileAttributes.Hidden);
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
            if (url.Contains("exhentai.org", StringComparison.OrdinalIgnoreCase)) return null;

            return null;
        }

        internal static FileStream DelayStream(string route, FileMode mode, FileAccess access)
        {
            FileStream file = null;

            var temp = false;
            var thres = 0;

            while (!temp)
            {
                try
                {
                    file = new FileStream(route, mode, access);
                    temp = true;
                }
                catch (Exception ex)
                {
                    if (thres++ > 100) break;

                    temp = false;
                    Log($"{route} wait: {ex.Message}");
                    Thread.Sleep(20);
                }
            }

            //if (file == null) throw new Exception("stream is null");

            return file;
        }

        internal static async void Wait(int msec)
        {
            await Task.Delay(msec).ConfigureAwait(false);
        }

        internal static int CountIndexOf(this string target, char find, int count)
        {
            for (int i = 0; i < target.Length; i++)
            {
                if (target[i] != find) continue;
                if (count == 0) return i;
                count--;
            }

            return -1;
        }

        internal static void SearchFromSpecif(ObservableCollection<IndexItem> index, SearchOption option)
        {

        }
        internal static void SearchFromAll(List<IndexItem> searchIndex, string search, List<IndexItem> destIndex)
        {
            var sb = new StringBuilder();

            var temp = new string[searchIndex.Count];
            var searchResult = new List<IndexItem>(searchIndex);

            for (var i = 0; i < searchIndex.Count; i++)
            {
                var item = searchIndex[i];

                sb.Append(item.Author).Append(item.Number).Append(item.SiteName).Append(item.Title);
                foreach (var tag in item.Tags) sb.Append(tag);

                temp[i] = sb.ToString();
                sb.Clear();
            }

            for (var i = 0; i < searchIndex.Count; i++)
            {
                foreach (var srch in search.Split(','))             //검색어 나열
                {
                    if (!temp[i].Contains(srch, StringComparison.OrdinalIgnoreCase))
                    {
                        searchResult.Remove(searchIndex[i]);
                    }
                }
            }

            destIndex.Clear();
            foreach (var item in searchResult)
            {
                destIndex.Add(item);
            }
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
            return path.Substring(0, path.IndexOf(path.Split('\\')[^1], StringComparison.Ordinal) - 1);
        }

        internal static void OpenDir(string path)
        {
            Process.Start("explorer.exe", path);
        }

        internal static string EHNumForDir(string number)
        {
            return number.Contains('/') ? number.Replace('/', '!') : number;
        }

        internal static string EHNumForInternal(string number)
        {
            return number.Contains('!') ? number.Replace('!', '/') : number;
        }

        internal static string CompareCollections(IEnumerable<IndexItem> collect1, IEnumerable<IndexItem> collect2)
        {
            var sb = new StringBuilder();
            foreach (var item in collect1.Where(item => collect2.All(i => i.Title != item.Title)))
            {
                sb.Append(item.Title).Append(", ");
                Debug.WriteLine($"{item.Title}");
            }
            return sb.ToString();
        }

        internal enum SearchOption
        {
            
        }
    }
}
