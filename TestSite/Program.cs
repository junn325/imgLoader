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
            var a = new StringLoader();
            a.Load("https://e-hentai.org/g/1806482/287828bb60/");

            //var temp = XmlHttpRequest("https://api.e-hentai.org/api.php");
            //;
        }

        static string XmlHttpRequest(string url)
        {
            var gid = "1806482";
            var page = "3";
            var imgkey = "527e2155ce";
            var showkey = "u4bplqx9ktj";

            string returnVal = null;

            var param = Encoding.UTF8.GetBytes($"{{\"method\":\"showpage\",\"gid\":{gid},\"page\":{page},\"imgkey\":\"{imgkey}\",\"showkey\":\"{showkey}\"}}");

            var rq = WebRequest.Create(url) as HttpWebRequest;
            rq.Referer = url;
            rq.UserAgent = "Mozilla/5.0 (iPad; CPU OS 11_0 like Mac OS X) AppleWebKit/604.1.34 (KHTML, like Gecko) Version/11.0 Mobile/15A5341f Safari/604.1";
            rq.Method = "POST";
            //rq.KeepAlive = true;
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
