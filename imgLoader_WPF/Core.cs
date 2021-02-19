using imgLoader_WPF.Sites;

using System;
using System.Collections.Generic;
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

        private static readonly string[] DFilter = { "(", ")", "|", ":", "?", "\"", "<", ">", "/", "*", "..." };
        private static readonly string[] DReplace = { "（", "）", "│", "：", "？", "″", "˂", "˃", "／", "＊", "…" };

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

        internal static void CreateInfo(string infoRoute, ISite site)
        {
            if (!Directory.Exists(Path.GetDirectoryName(infoRoute))) throw new DirectoryNotFoundException();
            if (site == null) throw new NullReferenceException("\"site\" is null.");

            var file = new FileInfo(infoRoute);
            if (file.Exists && (file.Attributes & FileAttributes.Hidden) != 0) file.Attributes &= ~FileAttributes.Hidden;

            using var sw = new StreamWriter(new FileStream(infoRoute, FileMode.Create, FileAccess.ReadWrite), Encoding.UTF8);
            var info = site.ReturnInfo(); //todo: 각 사이트 객체가 아닌 여기에서 7번째 칸 뚫어서 vote 작성

            for (var i = 0; i < info.Length; i++)
            {
                sw.Write(
                    i != info.Length - 1
                        ? info[i] + '\n'
                        : info[i]
                );
            }
            sw.Close();

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
                //Console.WriteLine("\nThis program may not possible to download from exhentai.org. Trying to download from Hitomi.la...");
                //var temp = new Hitomi(mNumber.Contains('/') ? mNumber.Split('/')[0] : mNumber);
                ////var tempp = 
                //if (temp.IsValidated()) return temp;
            }

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
                catch
                {
                    if (thres++ > 100) break;

                    temp = false;
                    Debug.WriteLine($"{route} wait");
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
            return path.Substring(0, path.IndexOf(path.Split('\\')[^1], StringComparison.Ordinal));
        }

        internal static void OpenDir(string path)
        {
            Process.Start("explorer.exe", path);
        }
    }
}
