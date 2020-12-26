using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace imgLoader_CLI.Sites
{
    //content(Sources tab) : Ln22 in (index)
    //base_url = "https://e-hentai.org/"
    //api_url = "https://api.e-hentai.org/api.php"
    //gid = first number
    //token = second number
    //aipuid = 4855480 <<< ?
    //popbase = base_url + "gallerypopups.php?gid=1806482&t=287828bb60&act="
    public class ehentai : ISite
    {
        public static string Supplement = "e-hentai.org/g/\\n\\/\\n\\/";

        private static readonly string[] FILTER = { " - Read Online", " - hentai doujinshi", "  Hitomi.la", " | Hitomi.la" };
        private static readonly string[] REPLACE = { "", "", "", "" };

        private readonly string _source;                                                                                                              
        public string Gid { get; set; }

        private readonly string _label;
        private string _artist;

        public ehentai(string mNumber)
        {
            var sr = new StringLoader();
            var temp = sr.Load($"https://e-hentai.org/g/{mNumber}/");

            var ttemp = new WebClient().DownloadString($"https://e-hentai.org/g/{mNumber}/");

            Console.Write(temp);

            //todo: 받은 내용으로 1페이지를 찾아 아래 변수를 받아와야함

            var startPage = temp.Split("var startpage = ")[1].Split(';')[0];
            var startKey = temp.Split("var startkey = \"")[1].Split("\";")[0];
            var showKey = temp.Split("var showkey = \"")[1].Split("\";")[0];
            var bUrl = temp.Split("var base_url = \"")[1].Split("\";")[0];
            var apiUrl = temp.Split("var api_url = \"")[1].Split("\";")[0];

            Gid = mNumber.Split('/')[0];
            _label = mNumber.Split('/')[1];

            var reqReturn = XmlHttpRequest(Gid, startPage, startKey, showKey);
            ;
        }

        public string GetArtist()
        {
            return "";
        }

        public Dictionary<string, string> GetImgUrls()
        {
            return new Dictionary<string, string>();
        }

        public string GetTitle()
        {
            return "";
        }

        public string[] ReturnInfo()
        {
            return new string[5];
        }

        public bool IsValidated()
        {
            return false;
        }

        private static string XmlHttpRequest(string gid, string page, string imgKey, string showKey)
        {
            var url = "https://api.e-hentai.org/api.php";
            //var gid = "1806482";
            //var page = "3";
            //var imgkey = "527e2155ce";
            //var showkey = "u4bplqx9ktj";

            string returnVal;

            var param = Encoding.UTF8.GetBytes($"{{\"method\":\"showpage\",\"gid\":{gid},\"page\":{page},\"imgkey\":\"{imgKey}\",\"showkey\":\"{showKey}\"}}");

            var rq = WebRequest.Create(url) as HttpWebRequest;
            if (rq == null) return null;

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
            if (resp == null) return null;

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
