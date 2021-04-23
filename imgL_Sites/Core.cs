using System;
using System.IO;
using System.Text;
using System.Threading;

namespace imgL_Sites
{
    public static class Core
    {
        private static readonly string FilesRoute = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.imgL\\";

        private const string LogDir = "ILLOG";
        private const string LogFile = "ILLG";

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
    }
}
