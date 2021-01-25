using imgLoader_CLI.Sites;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace imgLoader_CLI
{
    internal static class Core
    {
        internal const string ProjectName = "imgLoader_CLI";
        internal const string TempRoute   = "ILCTempRout";

        private const string LogDir  = "ILCLOG";
        private const string LogFile = "ILCLG";

        private static readonly string[] DFilter  = { "(", ")", "|", ":", "?", "\"", "<", ">", "/", "*", "..." };
        private static readonly string[] DReplace = { "[", "]", "│", "：", "？", "″", "˂", "˃", "／", "∗", "…" };

        internal static string Route = "";

        internal static void Log(string content)
        {
            new Thread(() => {
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

            if (url.Contains("nhentai.net", StringComparison.OrdinalIgnoreCase))  return new NHentai(mNumber);
            if (url.Contains("pixiv", StringComparison.OrdinalIgnoreCase))        return new Pixiv(mNumber);
            if (url.Contains("hiyobi.me"  , StringComparison.OrdinalIgnoreCase))  return new Hiyobi(mNumber);
            if (url.Contains("hitomi.la"  , StringComparison.OrdinalIgnoreCase))  return new Hitomi(mNumber);
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
            var infoFiles = Directory.EnumerateFiles(route, "*.*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".Hitomi") || s.EndsWith(".Hiyobi") || s.EndsWith(".NHentai") || s.EndsWith("EHentai")).ToArray();

            var infos = new Dictionary<string, string>(infoFiles.Length);
            var tasks = new Task[infoFiles.Length];

            for (var i = 0; i < infoFiles.Length; i++)
            {
                var info = infoFiles[i];
                tasks[i] = Task.Factory.StartNew(() => infos.Add(info, File.ReadAllText(info)));
            }

            foreach (var t in tasks) t.Wait();

            return infos;
        }
        internal static void Search(Dictionary<string, string> index, string search, string route)
        {
            var searchResult = new Dictionary<string, string>(index);
            foreach (var item in index)
            {
                foreach (var srch in search.Split(','))
                {
                    if (!item.Value.Contains(srch, StringComparison.OrdinalIgnoreCase))
                    {
                        searchResult.Remove(item.Key);
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
    }
}