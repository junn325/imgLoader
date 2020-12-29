﻿using System;
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
    //aipuid = 4855480 <<< ?
    //popbase = base_url + "gallerypopups.php?gid=1806482&t=287828bb60&act="
    public class ehentai : ISite
    {
        public static string Supplement = "e-hentai.org/g/\\n\\/\\n\\/";

        private static readonly string[] FILTER = { " - Read Online", " - hentai doujinshi", "  Hitomi.la", " | Hitomi.la" };
        private static readonly string[] REPLACE = { "", "", "", "" };

        private const string _api_url = "https://api.e-hentai.org/api.php";
        private const string _base_url = "https://e-hentai.org/";

        private readonly string _src_gall;
        private readonly string _src_item;
        private readonly string _src_rtn;

        public string Gid { get; set; }

        private readonly string _label;
        private string _artist;
        private string showKey;

        public ehentai(string mNumber)
        {
            var sr = new StringLoader();
            _src_gall = sr.Load($"https://e-hentai.org/g/{mNumber}/");

            var itemLink = _src_gall.Split("\"><img alt")[0].Split('\"').Last();
            _src_item = sr.Load(itemLink);

            var startPage = _src_item.Split("var startpage=")[1].Split(';')[0];
            var startKey = _src_item.Split("var startkey=\"")[1].Split("\";")[0];
            showKey = _src_item.Split("var showkey=\"")[1].Split("\";")[0];

            Gid = mNumber.Split('/')[0];
            _label = mNumber.Split('/')[1];

            _src_rtn = XmlHttpRequest(_api_url, Gid, startPage, startKey, showKey);
        }

        public string GetArtist()
        {
            return "";
        }

        public Dictionary<string, string> GetImgUrls()
        {
            var imgList = new Dictionary<string, string>();
            var sb = new StringBuilder(_src_rtn);

            string nextPage;
            string nextKey;

            while (sb.ToString().Split("\"p\":")[1].Split(',')[0] != sb.ToString().Split("return load_image(")[5].Split(',')[0])
            {
                var temp = sb.ToString().Split("\\\" src=\\")[1].Split("\\\" style=\\\"")[0];
                imgList.Add(temp.Split('/').Last(), temp);

                nextPage = sb.ToString().Split("return load_image(")[5].Split(')')[0].Split(", ")[0];
                nextKey = sb.ToString().Split("return load_image(")[5].Split("')")[0].Split(", '")[1];
                sb.Clear();
                sb.Append(XmlHttpRequest(_api_url, Gid, nextPage, nextKey, showKey));
            }

            var tempp = new StringBuilder();

            foreach (var i in imgList)
            {
                tempp.Append(i.Value).Append('\n');
            }

            File.WriteAllText($"{DateTime.Now.Ticks}.txt", tempp.ToString());
            return imgList;
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

        private string XmlHttpRequest(string url, string gid, string page, string imgKey, string showKey)
        {
            //gid = "1806482";
            //page = "3";
            //imgKey = "527e2155ce";
            //showKey = "ie9t3z99kvk";

            //url = "https://api.e-hentai.org/api.php";


            string returnVal;

            var param = Encoding.UTF8.GetBytes($"{{\"method\":\"showpage\",\"gid\":{gid},\"page\":{page},\"imgkey\":\"{imgKey}\",\"showkey\":\"{showKey}\"}}");

            var rq = WebRequest.Create(url) as HttpWebRequest;
            if (rq == null) return null;

            rq.Referer = "https://e-hentai.org/s/527e2155ce/1806482-3";
            rq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36";
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
