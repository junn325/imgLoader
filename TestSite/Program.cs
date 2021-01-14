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
        private static void Main(string[] args)
        {
            var source = "</script>\r\n<div id=\"nb\" class=\"nosel\"><div><a class=\"nbw\" href=\"https://e-hentai.org/\">Front<span class=\"nbw1\"> Page</span></a></div><div><a href=\"https://e-hentai.org/watched\">Watched</a></div><div><a href=\"https://e-hentai.org/popular\">Popular</a></div><div><a href=\"https://e-hentai.org/torrents.php\">Torrents</a></div><div><a href=\"https://e-hentai.org/favorites.php\">Fav<span class=\"nbw1\">orite</span>s</a></div><div><a href=\"https://e-hentai.org/home.php\"><span class=\"nbw2\">My </span>Home</a></div><div><a href=\"https://upload.e-hentai.org/manage.php\"><span class=\"nbw2\">My </span>Uploads</a></div><div><a href=\"https://e-hentai.org/toplist.php\">Toplists</a></div><div><a href=\"https://e-hentai.org/bounty.php\">Bounties</a></div><div><a href=\"https://e-hentai.org/news.php\">News</a></div><div><a href=\"https://forums.e-hentai.org/\">Forums</a></div><div><a href=\"https://ehwiki.org/\">Wiki</a></div><div><a href=\"https://hentaiverse.org/\" onclick=\"popUp('https://hentaiverse.org/',1250,720); return false\">H<span class=\"nbw1\">entai</span>V<span class=\"nbw1\">erse</span></a></div></div><script async src=\"//adserver.juicyads.com/js/jads.js\"></script><ins id=\"265910\" data-width=\"728\" data-height=\"90\"></ins><script>(adsbyjuicy = window.adsbyjuicy || []).push({'adzone':265910});</script>\r\n<script type=\"text/javascript\">";

            var temp = source.GetBetween("</script>", "<div>");
            for (int i = 0; i < 100; i++)
            {
                  
            }
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
