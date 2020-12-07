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

        private const string LOG_DIR = "ILLOG";
        private const string LOG_FILE = "ILLG";

        private static readonly string[] DFILTER = { "(", ")", "|", ":", "?", @"""", "<", ">", "/", "*" };
        private static readonly string[] DREPLACE = { "[", "]", ";", "-", "", "''", "[", "]", "", "" };

        internal static string Route = "";
        internal static bool HitomiAlways = true;

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
                        file= new FileStream(Path.GetTempPath() + @$"\{LOG_DIR}\{LOG_FILE}_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", FileMode.Append, FileAccess.Write);
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

            if (file.Exists)
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

        internal static string GetNumber(string url)
        {
            try
            {
                string val = url.Contains("//") ? url.Split("//")[1] : url;

                if (val.Contains("#")) val = val.Split('#')[0];
                if (val.Split('/').Last().Length == 0) val = val.Split('/')[val.Split('/').Length - 3];    //nhentai
                else val = val.Split('/').Last();                                                          //hitomi/hiyobi/pixiv
                if (val.Contains(".html")) val = val.Split(".html")[0];                                    //hitomi

                return val;
            }
            catch
            {
                return "";
            }
        }

        internal static ISite LoadSite(string link)
        {
            string mNumber = GetNumber(link);

            if (mNumber.Length == 0) return null;

            if (HitomiAlways)
            {
                var temp = new Hitomi(mNumber);
                if (temp.IsValidated()) return temp;
            }

            if (link.Contains("hiyobi.me"  , StringComparison.OrdinalIgnoreCase)) return new hiyobi(mNumber);
            if (link.Contains("hitomi.la"  , StringComparison.OrdinalIgnoreCase)) return new Hitomi(mNumber);
            if (link.Contains("pixiv"      , StringComparison.OrdinalIgnoreCase)) return new pixiv(mNumber);
            if (link.Contains("nhentai.net", StringComparison.OrdinalIgnoreCase)) return new nhentai(mNumber);

            return null;
        }
    }
}