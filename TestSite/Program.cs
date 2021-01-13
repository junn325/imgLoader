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
            var wc = new WebClient();
            wc.Headers.Add("referer", "https://forums.e-hentai.org/");
            wc.Headers.Add("user-agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Mobile Safari/537.36");
            //wc.Headers.Add(":authority", "e-hentai.org");
            //wc.Headers.Add(":method", "GET");
            //wc.Headers.Add(":path", "r/1656fe0a7158c67e944db3d3cd8c7431fd091bf4-439514-1280-1816-jpg/forumtoken/1806482-1/cover.jpg");
            wc.Headers.Add("accept", "image/avif,image/webp,image/apng,image/*,*/*;q=0.8");
            wc.Headers.Add("accept-encoding", "gzip, deflate, br");
            wc.Headers.Add("accept-language", "ko-KR,ko;q=0.9,en-US;q=0.8,en;q=0.7,ja;q=0.6");
            wc.Headers.Add("cache-control", "no-cache");
            wc.Headers.Add("cookie", "ipb_member_id=4855480; ipb_pass_hash=48c32f739d1a014ee5dfea321403a23a; sk=444ejl1m0w0bko8vxg3c0264gcfn; __cfduid=d5de58acf6df8562c39d22e0f745ac9331610549659; event=1610549977; ipb_session_id=32642861a3f1df9b0639bf3c92319757");
            wc.Headers.Add("pragma", "no-cache");
            wc.Headers.Add("scheme", "https");


            wc.DownloadFile("https://e-hentai.org/r/1656fe0a7158c67e944db3d3cd8c7431fd091bf4-439514-1280-1816-jpg/forumtoken/1806482-1/cover.jpg", "cover.jpg");
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
