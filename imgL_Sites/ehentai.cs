using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace imgL_Sites
{
    public class EHentai : ISite
    {
        public string Number { get; }
        public string Referer { get; }

        private const string ApiUrl = "https://api.e-hentai.org/api.php";
        private const string BaseUrl = "https://e-hentai.org/";

        private Stopwatch sw = new();
        private readonly string _src_gall, _src_data, _gall_id, _artist, _group, _title, _showKey;

        public EHentai(string mNumber)
        {
            try
            {
                sw.Start();
                _src_gall = StrLoad.Load($"{BaseUrl}g/{mNumber}/");
                Debug.WriteLine(sw.ElapsedMilliseconds);
                sw.Restart();

                var sb = new StringBuilder();
                var temp = StrLoad.LoadAsync(_src_gall.Split("\"><img alt")[0].Split('\"').Last());        //1번째 항목 불러옴
                Debug.WriteLine(sw.ElapsedMilliseconds);
                sw.Restart();

                _gall_id = mNumber.Split('/')[0];
                _src_data = XmlHttpRequest_Data(ApiUrl, _gall_id, mNumber.Split('/')[1]);
                _title = _src_data.GetStringValue("title");
                Debug.WriteLine(sw.ElapsedMilliseconds);
                sw.Restart();

                for (var i = 1; i < _src_data.StrLen("group:") + 1; i++)
                {
                    sb.Append(_src_data.Split("group:")[i].Split('"')[0]).Append(';');
                }
                _group = sb.ToString();

                sb.Clear();

                for (var i = 1; i < _src_data.StrLen("artist:") + 1; i++)
                {
                    sb.Append(_src_data.Split("artist:")[i].Split('"')[0]).Append(';');
                }
                _artist = sb.ToString();

                temp.Wait();
                Debug.WriteLine(sw.ElapsedMilliseconds);
                sw.Restart();

                var srcItem = temp.Result;
                _showKey = srcItem.Split("var showkey=\"")[1].Split("\";")[0];

                Number = mNumber;
                sw.Restart();
            }
            catch (Exception ex)
            {
                Core.Log("ehentai: " + ex.Message);
            }
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
            Debug.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();

            var sb = new StringBuilder(pages);
            for (var i = 1; i < (pageCount / 40) + 1; i++)      //이미지가 40장 이상이어서 여러 페이지일 때 수행 (한 페이지에 40장씩 보임)
            {
                sb.Append(StrLoad.Load($"{BaseUrl}g/{Number}?p={i}").Split("<div id=\"gdt\">")[1].Split("<div class=\"gtb\">")[0]);
            }
            Debug.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();

            var temp = sb.ToString();
            for (var i = 0; i < pageCount; i++)
            {
                var url = temp.Split("<a href=\"")[i + 1].Split("\">")[0];
                tasks[i] = XmlHttpRequest_ItemAsync(_gall_id, (i + 1).ToString(), url.Split('/')[4], _showKey, (i + 1).ToString());
            }
            Debug.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();

            for (var i = 0; i < pageCount; i++)
            {
                rtnVal[i] = tasks[i].Result;
                var url = tasks[i].Result.Split("\"img\\\" src=\\\"")[1].Split("\\\"")[0].Replace("\\/", "/");
                imgList.Add(url.Split("/").Last(), url);
            }
            Debug.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();

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
            info[3] = _src_data.GetStringValue("filecount");

            var sb = new StringBuilder();
            sb.Append("tags:");
            foreach (var item in _src_data.GetValue("tags", '[', ']').Split("\","))
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
            return await Task.Run(() =>
            {
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
                        var sb = new StringBuilder();

                        int count;
                        var buffer = new byte[1024];
                        do
                        {
                            count = s.Read(buffer, 0, buffer.Length);
                            sb.Append(Encoding.UTF8.GetString(buffer, 0, count));
                        } while (count > 0);

                        returnVal = sb.ToString();
                    }
                    //resp.Close();

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
                    var sb = new StringBuilder();

                    int count;
                    var buffer = new byte[1024];
                    do
                    {
                        count = s.Read(buffer, 0, buffer.Length);
                        sb.Append(Encoding.UTF8.GetString(buffer, 0, count));
                    } while (count > 0);

                    returnVal = sb.ToString();
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
