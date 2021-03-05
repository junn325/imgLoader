using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace imgLoader_WPF.Sites
{
    public class EHentai : ISite
    {
        public string Number { get; }

        private const string ApiUrl = "https://api.e-hentai.org/api.php";
        private const string BaseUrl = "https://e-hentai.org/";

        private readonly string _src_gall, _src_data, _gall_id, _artist, _group, _title, _showKey;

        public EHentai(string mNumber)
        {
            _src_gall = StrLoad.Load($"{BaseUrl}g/{mNumber}/");

            var sb = new StringBuilder();
            var temp = StrLoad.LoadAsync(_src_gall.Split("\"><img alt")[0].Split('\"').Last());        //1번째 항목 불러옴

            if (_src_gall == null) throw new Exception();

            _gall_id = mNumber.Split('/')[0];
            var gallToken = mNumber.Split('/')[1];

            _src_data = XmlHttpRequest_Data(ApiUrl, _gall_id, gallToken);

            _title = StrTools.GetStringValue(_src_data, "title");

            for (var i = 1; i < _src_data.StrLen("group") + 1; i++) sb.Append(_src_data.Split("group:")[i].Split('"')[0]).Append(';');
            _group = sb.ToString();
            sb.Clear();

            for (var i = 1; i < _src_data.StrLen("artist") + 1; i++) sb.Append(_src_data.Split("artist:")[i].Split('"')[0]).Append(';');
            _artist = sb.ToString();

            temp.Wait();
            var srcItem = temp.Result;
            _showKey = srcItem.Split("var showkey=\"")[1].Split("\";")[0];

            Number = mNumber;
        }

        public string GetArtist()
        {
            return $"{_artist}|{_group}";
        }

        public Dictionary<string, string> GetImgUrls()
        {
            var imgList = new Dictionary<string, string>();

            var pageCount = int.Parse(_src_gall.Split(" of ")[1].Split(" images")[0]);
            var pages = _src_gall.Split("<div id=\"gdt\">")[1].Split("<div class=\"gtb\">")[0];
            var tasks = new Task<string>[pageCount];
            var rtnVal = new string[pageCount];

            var sb = new StringBuilder(pages);
            for (var i = 1; i < (pageCount / 40) + 1; i++) sb.Append(StrLoad.Load($"{BaseUrl}g/{Number}?p={i}").Split("<div id=\"gdt\">")[1].Split("<div class=\"gtb\">")[0]);

            var temp = sb.ToString();
            for (var i = 0; i < pageCount; i++)
            {
                var url = temp.Split("<a href=\"")[i + 1].Split("\">")[0];
                tasks[i] = XmlHttpRequest_ItemAsync(_gall_id, (i + 1).ToString(), url.Split('/')[4], _showKey, (i + 1).ToString());
            }

            for (var i = 0; i < pageCount; i++)
            {
                rtnVal[i] = tasks[i].Result; var url = tasks[i].Result.Split("\"img\\\" src=\\\"")[1].Split("\\\"")[0].Replace("\\/", "/");
                imgList.Add(url.Split("/").Last(), url);
            }

            return imgList;
        }

        public string GetTitle()
        {
            return _title ?? throw new Exception("_title was Null");
        }

        public string[] ReturnInfo()
        {
            var info = new string[5];
            info[0] = "EHentai";
            info[1] = _title ?? throw new Exception("_title was Null");
            info[2] = $"{_artist}|{_group}";
            info[3] = StrTools.GetStringValue(_src_data, "filecount");

            var sb = new StringBuilder();
            sb.Append("tags:");
            foreach (var item in StrTools.GetValue(_src_data, "tags", '[', ']').Split("\","))
            {
                if (item.Length == 0) continue;

                sb.Append(item.Split('\"')[1]).Append(';');
            }
            info[4] = sb.ToString().Trim();

            return info;
        }

        public bool IsValidated()
        {
            return Number != null;
        }

        /// <summary>
        ///  Can be null
        /// </summary>
        private static async Task<string> XmlHttpRequest_ItemAsync(string gid, string reqPage, string imgKey, string showKey, string pageNum)
        {
            return await Task.Run(() => {

                HttpWebRequest rq = null;
                WebResponse resp = null;
                try
                {
                    string returnVal;
                    var param = Encoding.UTF8.GetBytes($"{{\"method\":\"showpage\",\"gid\":{gid},\"page\":{reqPage},\"imgkey\":\"{imgKey}\",\"showkey\":\"{showKey}\"}}");

                    rq = WebRequest.CreateHttp(ApiUrl);
                    rq.Referer = $"{BaseUrl}s/{imgKey}/{gid}-{pageNum}";
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

        /// <summary>
        ///  Can be null
        /// </summary>
        private static string XmlHttpRequest_Data(string url, string gall_id, string gall_token)
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
