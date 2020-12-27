using System;
using System.IO;
using System.Text.Json;
using System.Net;
using System.Text;

namespace TestSite
{
    class Program
    {
        static void Main(string[] args)
        {
            //var a = new StringLoader();
            //a.Load("https://e-hentai.org/g/1806482/287828bb60/");

            var temp = XmlHttpRequest("https://api.e-hentai.org/api.php");
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
