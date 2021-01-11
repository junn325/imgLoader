using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace imgLoader_CLI.Sites
{
    public class EHentai : ISite
    {
        public const string Supplement = "e-hentai.org/g/\\n\\/\\n\\/";

        private const string _api_url = "https://api.e-hentai.org/api.php";
        private const string _base_url = "https://e-hentai.org/";

        private static readonly string[] FILTER = { " - Read Online", " - hentai doujinshi", "  Hitomi.la", " | Hitomi.la" };
        private static readonly string[] REPLACE = { "", "", "", "" };

        private readonly string _src_gall, _src_item, _src_rtn, _src_data, _gall_id, _gall_token, _artist, _title, _showKey;

        public string Number { get; }

        public EHentai(string mNumber)
        {
            try
            {
                Number = mNumber;

                _src_gall = StrLoad.Load($"https://e-hentai.org/g/{mNumber}/");
                _src_item = StrLoad.Load(_src_gall.Split("\"><img alt")[0].Split('\"').Last());

                if (_src_gall == null || _src_item == null) throw new Exception();

                var startPage = _src_item.Split("var startpage=")[1].Split(';')[0];
                var startKey  = _src_item.Split("var startkey=\"")[1].Split("\";")[0];
                _showKey      = _src_item.Split("var showkey=\"")[1].Split("\";")[0];
                                                                                                                                                       
                _gall_id = mNumber.Split('/')[0];
                _gall_token = mNumber.Split('/')[1];

                _src_rtn = XmlHttpRequest_Item(_api_url, _gall_id, startPage, startKey, _showKey, "1");
                _src_data = XmlHttpRequest_Data(_api_url, _gall_id, _gall_token);

                _title = StrTools.GetStringValue(_src_data,"title");
                _artist = _src_data.Contains("artist") ? _src_data.Split("artist:")[1].Split('\"')[0] : "N/A";

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

            var pageCount = int.Parse(_src_gall.Split(" of ")[1].Split(" images")[0]);
            var pages = _src_gall.Split("<div id=\"gdt\">")[1].Split("<div class=\"gtb\">")[0];
            var arrPage = new string[pageCount];

            return imgList;
        }

        public string GetTitle()
        {
            return _title ?? throw new Exception("_title was Null");
        }

        public string[] ReturnInfo()
        {
            var info = new string[5];
            info[0] = _title ?? throw new Exception("_title was Null");
            info[1] = _artist ?? "N/A";
            info[2] = StrTools.GetStringValue(_src_data, "filecount");

            var temp = new StringBuilder();
            foreach (string item in StrTools.GetValue(_src_data, "tags", '[', ']').Split("\","))
            {
                if (item.Length == 0) continue;

                temp.Append(item.Split('\"')[1]).Append('\n');
            }
            info[3] = temp.ToString().Trim();
            info[4] = _src_gall.Split("<td class=\"gdt2\">")[1].Split("</td>")[0];

            return info;
        }

        public bool IsValidated()
        {
            return _artist != null;
        }

        private string XmlHttpRequest_Item(string url, string gid, string reqPage, string imgKey, string showKey, string pageNum)
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
                    using var reader = new StreamReader(s);
                    returnVal = reader.ReadToEnd();
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
        private async Task<string> XmlHttpRequest_ItemAsync(string url, string gid, string reqPage, string imgKey, string showKey, string pageNum)
        {
            return await Task.Run(() => {

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
                        using var reader = new StreamReader(s);
                        returnVal = reader.ReadToEnd();
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

            }).ConfigureAwait(false);
        }
        private string XmlHttpRequest_Data(string url, string gall_id, string gall_token)
        {
            HttpWebRequest rq = null;
            WebResponse resp = null;
            try
            {
                string returnVal;
                var param = Encoding.UTF8.GetBytes($"{{\"method\": \"gdata\",\"gidlist\": [[{gall_id},\"{gall_token}\"]],\"namespace\": 1}}");

                rq = WebRequest.CreateHttp(url);
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
                    using var reader = new StreamReader(s);
                    returnVal = reader.ReadToEnd();
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
