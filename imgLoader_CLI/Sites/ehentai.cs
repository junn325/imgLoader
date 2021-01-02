using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace imgLoader_CLI.Sites
{
    public class EHentai : ISite
    {
        public static string Supplement = "e-hentai.org/g/\\n\\/\\n\\/";

        private const string _api_url = "https://api.e-hentai.org/api.php";
        private const string _base_url = "https://e-hentai.org/";

        private static readonly string[] FILTER = { " - Read Online", " - hentai doujinshi", "  Hitomi.la", " | Hitomi.la" };
        private static readonly string[] REPLACE = { "", "", "", "" };

        private readonly string _src_gall, _src_item, _src_rtn, _gid, _label, _artist, _title, _showKey;

        public EHentai(string mNumber)
        {
            try
            {
                _src_gall = StrLoad.Load($"https://e-hentai.org/g/{mNumber}/");
                _src_item = StrLoad.Load(_src_gall.Split("\"><img alt")[0].Split('\"').Last());

                var startPage = _src_item.Split("var startpage=")[1].Split(';')[0];
                var startKey  = _src_item.Split("var startkey=\"")[1].Split("\";")[0];
                _showKey      = _src_item.Split("var showkey=\"")[1].Split("\";")[0];

                _gid = mNumber.Split('/')[0];
                _src_rtn = XmlHttpRequest(_api_url, _gid, startPage, startKey, _showKey, "1");

                _title = _src_gall.Split("<title>")[1].Split("</title>")[0];
                if (_title.Contains('[')) _artist = _title.Split('[')[1].Split(']')[0];

                _label = mNumber.Split('/')[1];
            }
            catch
            {
                throw new Exception("failed to initiate");
            }
        }

        public string GetArtist()
        {
            return _artist ?? "N/A";
        }

        public Dictionary<string, string> GetImgUrls()
        {
            var imgList = new Dictionary<string, string>();
            var sb = new StringBuilder(_src_rtn);

            var strTemp = sb.ToString();
            string temp;

            for (int i = 1; i < int.Parse(_src_rtn.Split(" \\/ <span>")[1].Split("<\\/span>")[0]) + 1; i++)        //todo: 5개씩 묶어서 돌려볼 것 페이지를 다운받으면 imgKey가 나옴
            {
                temp = strTemp.Split("return load_image(")[5];
                sb.Clear();
                sb.Append(XmlHttpRequest(_api_url, _gid, temp.Split(')')[0].Split(", ")[0], temp.Split("')")[0].Split(", '")[1], _showKey, i.ToString()));

                temp = strTemp.Split("\\\" src=\\\"")[1].Split("\\\" style=\\\"")[0];
                imgList.Add(temp.Split('/').Last(), temp.Replace("\\/", "/"));

                strTemp = sb.ToString();

            }

            sb.Clear();
            foreach (var i in imgList)
            {
                sb.Append(i.Value).Append('\n');
            }

            File.WriteAllText($"{DateTime.Now.Ticks}.txt", sb.ToString());
            return imgList;
        }

        public string GetTitle()
        {
            return _title;
        }

        public string[] ReturnInfo()
        {
            return new string[5];
        }

        public bool IsValidated()
        {
            return _label != null;
        }

        private string XmlHttpRequest(string url, string gid, string reqPage, string imgKey, string showKey, string pageNum)
        {
            HttpWebRequest rq = null;
            WebResponse resp = null;
            try
            {
                string returnVal;
                var param = Encoding.UTF8.GetBytes($"{{\"method\":\"showpage\",\"gid\":{gid},\"page\":{reqPage},\"imgkey\":\"{imgKey}\",\"showkey\":\"{showKey}\"}}");

                rq = WebRequest.CreateHttp(url);
                rq.Referer = $"https://e-hentai.org/s/{imgKey}/{gid}-{pageNum}";
                rq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36";
                rq.Method = "POST";
                rq.ContentLength = param.Length;
                rq.ContentType = "application/json";

                using (var s = rq.GetRequestStream())
                {
                    s.Write(param, 0, param.Length);
                }

                resp = rq.GetResponse();
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
            catch
            {
                return null;
            }
            finally
            {
                rq?.Abort();
                resp?.Close();
            }
        }
    }
}
