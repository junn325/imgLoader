using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;

using imgL_Sites;

using imgLoader_WPF.Services;
using imgLoader_WPF.Windows;

namespace imgLoader_WPF
{
    internal static class Core
    {
        internal const string ProjectName = "imgLoader";
        internal static readonly string FilesRoute = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.imgL\\";

        internal const string RouteFile = "ILRout";
        internal const string OpenFile = "ILOpen";

        internal const string LogDir = "ILLOG";
        internal const string LogFile = "ILLG";

        internal const string InfoExt = "ilif";

        internal const int InfoCount = 10;

        internal static bool ShowDate = true;
        internal static bool ShowLastDate;

        private static readonly string[] DFilter = { "|", ":", "?", "\"", "<", ">", "/", "*", "..." };
        private static readonly string[] DReplace = { "│", "：", "？", "″", "˂", "˃", "／", "＊", "…" };

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

        internal static bool CreateInfo(string infoPath, ISite site)
        {
            if (!Directory.Exists(Dir.GetDirFromFile(infoPath)))
                return false;
            if (site == null)
                return false;

            using var fs = Dir.DelayStream(infoPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using var sw = new StreamWriter(fs, Encoding.UTF8);

            var info = site.GetInfo();

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

            sw.Flush();                //┐
            fs.SetLength(fs.Position); //오류 등으로 원래 담겨야할 줄 수 이상의 줄이 있을 때 지움

            sw.Close();

            File.SetAttributes(infoPath, FileAttributes.Hidden);

            return true;
        }

        internal static string GetNumber(string url)
        {
            if (url == null) return null;

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
            var mNumber = GetNumber(url);
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

            if (rawArtist == null) return null;
            if (rawArtist == "|") return "";

            if (!rawArtist.Contains('|') && !rawArtist.Contains(';')) return rawArtist;
            if (rawArtist.Split('|')[0].Length != 0)
            {
                foreach (var s in rawArtist.Split('|')[0].Split(';'))
                {
                    if (s.Length == 0) continue;
                    sb.Append(s).Append(", ");
                }
                var temp = sb.ToString()[..(sb.Length - 2)];

                sb.Clear();

                foreach (var s in rawArtist.Split('|')[1].Split(';'))
                {
                    if (s.Length == 0) continue;
                    sb.Append(s).Append(", ");
                }

                return sb.Length != 0
                        ? $"{temp} ({sb.ToString()[..(sb.Length - 2)]})"
                        : temp;
            }

            foreach (var s in rawArtist.Split('|')[1].Split(';'))
            {
                if (s.Length == 0) continue;
                sb.Append(s).Append(", ");
            }

            return sb.ToString()[..(sb.Length - 2)];
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

        internal static int GetFutureIndexOnList(IEnumerable<IndexItem> index, IndexItem item, Sorter.SortOption option)
        {
            var items = index.ToList();
            items.Add(item);
            //var sorted = items.OrderBy(i => i.Title, StringComparer.OrdinalIgnoreCase).ToList();
            var sorted = Sorter.GetSortedList(items, option);

            return sorted.IndexOf(item);
        }

        internal static class Dir
        {
            /// <summary>
            /// No last backslash
            /// </summary>
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
                if (!File.Exists(OpenWith)) return;
                if (!File.Exists(path)) return;

                Process.Start(OpenWith, path);
            }

            internal static void OpenOnCanvas(string imgSetPath, string title, string author, ImgLoader sender)
            {
                if (!Directory.Exists(imgSetPath)) return;

                var temp = Directory.EnumerateFiles(imgSetPath, "*.*").Where(f => !f.Contains($".{InfoExt}")).ToArray();

                if (temp.Length == 0) return;

                var canvas = new Viewer { TTitle = title, Author = author, FileList = temp, Sender = sender };
                canvas.Show();
            }

            internal static (List<string>, List<string>) CompareWorkspace(string firstPath, string secondPath)
            {
                var first = Directory.GetFiles(firstPath, $"*.{InfoExt}", SearchOption.AllDirectories);
                var second = Directory.GetFiles(secondPath, $"*.{InfoExt}", SearchOption.AllDirectories);

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

            internal static List<string> GetFiles(string path, string contains)
            {
                var result = new List<string>();

                foreach (var fileName in Directory.EnumerateFiles(path))
                {
                    if (!fileName.Contains(contains) || fileName.Contains(".lnk"))
                    {
                        continue;
                    }

                    result.Add(fileName);
                }

                foreach (var dir in Directory.EnumerateDirectories(path))
                {
                    if (dir.Contains("$RECYCLE.BIN") || dir.Contains(@":\Windows\") || dir.Contains(@":\Program Files"))
                    {
                        continue;
                    }

                    try
                    {
                        result.AddRange(GetFiles(dir, contains));
                    }
                    catch
                    {
                        // 오류 발생 시 무시
                    }
                }

                return result;
            }
        }
    }
}
