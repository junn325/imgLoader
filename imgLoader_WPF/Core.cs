using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

using imgL_Sites;
using imgLoader_WPF.Windows;

namespace imgLoader_WPF
{
    internal static class Core
    {
        internal const string ProjectName = "imgLoader";
        internal static readonly string FilesRoute = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.imgL\\";

        internal const string RouteFile = "ILRout";
        internal const string OpenFile = "ILOpen";
        internal const string IndexFile = "ILIdx";

        internal const string LogDir = "ILLOG";
        internal const string LogFile = "ILLG";

        internal const string InfoExt = "ilif";

        internal const int InfoCount = 10;

        internal static bool ShowDate;
        internal static bool ShowLastDate;

        private static readonly string[] DFilter = { "|", ":", "?", "\"", "<", ">", "/", "*", "..." };
        private static readonly string[] DReplace = { "│", "：", "？", "″", "˂", "˃", "／", "＊", "…" };

        //internal const string PreparationText = "준비 중...";
        //internal static List<string> PrevAddress = new List<string>(5);

        //private static readonly List<ListViewItem> LvItem = new List<ListViewItem>();

        internal static string Route = "";
        internal static string OpenWith = "";

        internal static void Log(string content)
        {
            new Thread(() =>
            {
                var sb = new StringBuilder(FilesRoute);
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

        internal static void CreateInfo(string infoPath,  imgL_Sites.ISite site)
        {
            if (!Directory.Exists(Dir.GetDirFromFile(infoPath)))
                throw new DirectoryNotFoundException();
            if (site == null)
                throw new NullReferenceException("\"site\" is null.");

            using var fs = Dir.DelayStream(infoPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using var sw = new StreamWriter(fs, Encoding.UTF8);

            var info = site.ReturnInfo();

            Debug.Assert(info[2].Contains('|'));

            for (var i = 0; i < info.Length; i++)
            {
                sw.Write(
                    i != info.Length - 1
                        ? info[i] + '\n'
                        : info[i]
                );
            }
            sw.Write($"\n{DateTime.Now.ToString(CultureInfo.InvariantCulture)}\n0\n1\n0"); //0=Vote, 1=Show, 0=View

            sw.Flush();                 //┐
            fs.SetLength(fs.Position);  //오류 등으로 원래 담겨야할 줄 수 이상의 줄이 있을 때 지움

            sw.Close();

            File.SetAttributes(infoPath, FileAttributes.Hidden);
        }

        internal static string GetNum(string url)
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

            return url;
        }

        /// <summary>
        ///  Can be null
        /// </summary>
        internal static ISite LoadSite(string url)
        {
            var mNumber = GetNum(url);
            if (mNumber.Length == 0) return null;

            if (url == mNumber)     //입력값이 번호
            {
                ISite temp = new Hitomi(mNumber);
                if (temp.IsValidated()) return temp;

                temp = new Hiyobi(mNumber);
                if (temp.IsValidated()) return temp;

                temp = new NHentai(mNumber);
                if (temp.IsValidated()) return temp;

                temp = new EHentai(mNumber);
                if (temp.IsValidated()) return temp;

                return null;
            }

            if (url.Contains("nhentai.net", StringComparison.OrdinalIgnoreCase)) return new NHentai(mNumber);
            if (url.Contains("pixiv", StringComparison.OrdinalIgnoreCase)) return new Pixiv(mNumber);
            if (url.Contains("hiyobi.me", StringComparison.OrdinalIgnoreCase)) return new Hiyobi(mNumber);
            if (url.Contains("hitomi.la", StringComparison.OrdinalIgnoreCase)) return new Hitomi(mNumber);
            if (url.Contains("e-hentai.org", StringComparison.OrdinalIgnoreCase)) return new EHentai(mNumber);
            if (url.Contains("exhentai.org", StringComparison.OrdinalIgnoreCase)) return null;

            return null;
        }
        
        internal static string GetArtistFromRaw(string rawArtist)
        {
            var sb = new StringBuilder();
            string temp;

            if (rawArtist == "|") return "";
            if (!rawArtist.Contains('|') && !rawArtist.Contains(';')) return rawArtist;
            if (rawArtist.Split('|')[0].Length != 0)
            {
                foreach (var s in rawArtist.Split('|')[0].Split(';'))
                {
                    if (s.Length == 0) continue;
                    sb.Append(s).Append(", ");
                }
                temp = sb.ToString()[..(sb.Length - 2)];

                sb.Clear();

                foreach (var s in rawArtist.Split('|')[1].Split(';'))
                {
                    if (s.Length == 0) continue;
                    sb.Append(s).Append(", ");
                }

                temp =
                    sb.Length != 0
                        ? $"{temp} ({sb.ToString()[..(sb.Length - 2)]})"
                        : temp;
            }
            else
            {
                foreach (var s in rawArtist.Split('|')[1].Split(';'))
                {
                    if (s.Length == 0) continue;
                    sb.Append(s).Append(", ");
                }

                temp = sb.ToString()[..(sb.Length - 2)];
            }

            return temp;
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

        internal static T[] InitArray<T>(int count)
        {
            var temp = new T[count];

            for (var i = 0; i < temp.Length; i++) temp[i] = default;

            return temp;
        }
        internal static T[] InitArray<T>(int count, T value)
        {
            var temp = new T[count];

            for (var i = 0; i < temp.Length; i++) temp[i] = value;

            return temp;
        }

        internal static T[] InitArray<T>(int count, T[] array)
        {
            var temp = new T[count];

            for (var i = 0; i < temp.Length; i++)
            {
                temp[i] = array.Length > i ? array[i] : default;
            }

            return temp;
        }

        internal static IEnumerable<IndexItem> CompareCollections(IEnumerable<IndexItem> collect1, IEnumerable<IndexItem> collect2)
        {
            return collect1.Where(item => collect2.All(i => i.Title != item.Title));
        }

        internal static void RefreshList(List<IndexItem> list, IEnumerable<IndexItem> newContent)
        {
            list.Clear();

            foreach (var item in newContent)
            {
                list.Add(item);
            }
        }

        internal static class Dir
        {
            internal static string GetDirFromFile(string path)
            {
                return path[..(path.IndexOf(path.Split('\\')[^1], StringComparison.Ordinal) - 1)];
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

            internal static void OpenDir(string path)
            {
                if (!Directory.Exists(path)) return;
                Process.Start("Explorer.exe", @"/open,""" + path);
            }

            internal static string EHNumFromRaw(string number)
            {
                return number.Contains('/') ? number.Replace('/', '!') : number;
            }

            internal static string EHNumFromInternal(string number)
            {
                return number.Contains('!') ? number.Replace('!', '/') : number;
            }

            internal static void OpenOn(string path)
            {
                if (!File.Exists(Core.OpenWith)) return;
                if (!File.Exists(path)) return;

                Process.Start(Core.OpenWith, path);
            }

            internal static void OpenOnCanvas(string imgSetPath, string title, string author)
            {
                if (!Directory.Exists(imgSetPath)) return;

                //var img = new BitmapImage();
                var temp = Directory.GetFiles(imgSetPath, "*.*").Where(f => !f.Contains(".ilif")).ToArray();

                //img.BeginInit();
                //img.UriSource = new Uri(temp[0]);
                //img.EndInit();

                var canvas = new Viewer { TTitle = title, Author = author, FileList = temp};
                canvas.Show();
            }

            internal static (List<string>, List<string>) CompareWorkspace(string firstPath, string secondPath)
            {
                var first = Directory.GetFiles(firstPath, $"*.{Core.InfoExt}", SearchOption.AllDirectories);
                var second = Directory.GetFiles(secondPath, $"*.{Core.InfoExt}", SearchOption.AllDirectories);

                var result1 = new List<string>();
                var result2 = new List<string>();

                foreach (var s in first)
                {
                    if (second.Any(i => Path.GetFileNameWithoutExtension(i) == Path.GetFileNameWithoutExtension(s))) continue;

                    result1.Add(s);
                }

                foreach (var s in second)
                {
                    if (first.Any(i => Path.GetFileNameWithoutExtension(i) == Path.GetFileNameWithoutExtension(s))) continue;

                    result2.Add(s);
                }

                return (result1, result2);
            }

            internal static int[] TestRead(string route)
            {
                using var sr = new StreamReader(Dir.DelayStream(route, FileMode.OpenOrCreate, FileAccess.ReadWrite), Encoding.UTF8);
                var temp = new int[1000];
                var count = 0;
                var itr = 0;
                do
                {
                    count = sr.Read();
                    temp[itr++] = count;
                } while (count > 0);

                return temp;
            }
        }
    }
}
