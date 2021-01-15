using imgLoader_CLI.Sites;

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace imgLoader_CLI
{
    internal static class Core
    {
        internal const byte WAIT_TIME    = 25;
        internal const byte FAIL_RETRY   = 10;

        internal const string PROJECT_NAME = "imgLoader_CLI";
        internal const string TEMP_ROUTE   = "ILCTempRout";

        private const string LOG_DIR  = "ILLOG";
        private const string LOG_FILE = "ILLG";

        private static readonly string[] DFILTER  = { "(", ")", "|", ":", "?", "\"", "<", ">", "/", "*", "..." };
        private static readonly string[] DREPLACE = { "[", "]", "│", "：", "？", "″", "˂", "˃", "／", "∗", "…" };

        internal static string Route = "";

        internal static void Log(string content)
        {
            new Thread(() => {
                var sb = new StringBuilder(Path.GetTempPath());
                sb.Append('\\').Append(LOG_DIR);

                if (!Directory.Exists(sb.ToString()))
                {
                    Directory.CreateDirectory(sb.ToString());
                }

                var temp = false;
                FileStream file = null;
                sb.Append('\\').Append(LOG_FILE).Append('_').Append(DateTime.Now.ToString("yyyy-MM-dd")).Append(".txt");

                while (!temp)
                {
                    try
                    {
                        file= new FileStream(sb.ToString(), FileMode.Append, FileAccess.Write);
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
            foreach (var item in site.ReturnInfo())
            {
                sw.WriteLine(item);
            }

            File.SetAttributes(infoRoute, FileAttributes.Hidden);
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
            if (val.Contains("e-hentai")) return val.Split("/g/")[1].Remove(val.Split("/g/")[1].Length - 1);

            return "";
        }

        internal static ISite LoadSite(string link)
        {
            var mNumber = GetNumber(link);
            if (mNumber.Length == 0) return null;

            if (link.Contains("nhentai.net", StringComparison.OrdinalIgnoreCase))  return new NHentai(mNumber);
            if (link.Contains("pixiv", StringComparison.OrdinalIgnoreCase))        return new Pixiv(mNumber);
            if (link.Contains("hiyobi.me"  , StringComparison.OrdinalIgnoreCase))  return new Hiyobi(mNumber);
            if (link.Contains("hitomi.la"  , StringComparison.OrdinalIgnoreCase))  return new Hitomi(mNumber);
            if (link.Contains("e-hentai.org", StringComparison.OrdinalIgnoreCase)) return new EHentai(mNumber);

            return null;
        }
    }
}