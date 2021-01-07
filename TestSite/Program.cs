using System;
using System.IO;
using System.Text.Json;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TestSite
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            var sb = new StringBuilder();
            var wc = new WebClient();
            var temp = new Task<string>[50];

            const string url = "https://blog.naver.com/designpress2016/222179251416";
            sw.Start();

            for (int i = 0; i < 5; i++)
            {
                for (var j = 0; j < 50; j++)
                {
                    //sb.Append(StrLoad.Load(url));
                    //sb.Append(wc.DownloadString(url));
                    temp[j] = StrLoad.LoadAsync(url);
                    Console.WriteLine(j);
                }

                foreach (var item in temp)
                {
                    sb.Append(item.Result);
                }

                File.AppendAllText("time.txt", sw.Elapsed.Ticks + "\n" + sb.ToString().Length + "\n\n");
                Console.WriteLine(sb.ToString().Length);

                Thread.Sleep(1000);
                sb.Clear();
                sw.Restart();
            }

            File.AppendAllText("time.txt", "\n");

            sw.Stop();
            ;
        }

        static string XmlHttpRequest(string url)
        {
            var gid = "1806482";
            var page = "4";
            var imgkey = "5e915772c9";
            var showkey = "29fdjvx9kva";

            string returnVal = null;

            var param = Encoding.UTF8.GetBytes($"{{\"method\":\"showpage\",\"gid\":{gid},\"page\":{page},\"imgkey\":\"{imgkey}\",\"showkey\":\"{showkey}\"}}");

            var rq = WebRequest.Create(url) as HttpWebRequest;
            rq.Referer = url;
            rq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36";
            rq.Method = "POST";
            rq.KeepAlive = true;
            rq.ContentLength = param.Length;
            rq.ContentType = "application/json";

            using (var s = rq.GetRequestStream())
            {
                s.Write(param, 0, param.Length);
            }

            var resp = rq.GetResponse() as HttpWebResponse;
            using (var s = resp.GetResponseStream())
            {
                using (var reader = new StreamReader(s))
                {
                    returnVal = reader.ReadToEnd();
                }
            }
            resp.Close();

            return returnVal;
        }
    }
}
