using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace imgL_Sites
{
    public class Pixiv : ISite
    {
        public string Number { get; }
        public string Referer { get; }

        private readonly string _src_gall, _src_this, _title, _artist = "", _date, _imgCnt;

        public Pixiv(string mNumber)
        {
            try
            {
                _src_gall = StrLoad.Load($"https://www.pixiv.net/artworks/{mNumber}");
                if (_src_gall == null)
                    return;

                _src_this = _src_gall.BraceParse("userIllusts").BraceParse(mNumber);

                _title  = _src_gall.GetStringValue("illustTitle");
                _artist = _src_gall.GetStringValue("userName");
                _date   = _src_this.GetStringValue("createDate");

                _imgCnt = _src_this.GetValue("pageCount");

                if (_title == null || _artist == null || _date == null || _imgCnt == null)
                    return;

                Number  = mNumber;
            }
            catch (Exception e)
            {
                Core.Log("pixiv: " + e.Message);
            }
        }

        public string GetArtist()
        {
            return $"{_artist};|";
        }

        public string GetTitle()
        {
            return _title;
        }

        public string[] GetInfo()
        {
            var info = new string[5];

            info[0] = "Pixiv";
            info[1] = _title;
            info[2] = $"{_artist};|";
            info[3] = _imgCnt;

            var sb = new StringBuilder();
            sb.Append("tags:");
            foreach (var item in _src_this.GetValue("tags", '[', ']').Split(','))
            {
                sb.Append(item.Split('\"')[1]).Append(';');
            }

            info[4] = sb.ToString().Trim();

            return info;
        }

        public Dictionary<string, string> GetImgUrls()
        {
            var (date, time) = (_date.Split('T')[0].Split('-'), _date.Split('T')[1].Split(':'));
            var (year, month, day,
                    hour, min, sec) = (date[0], date[1], date[2], time[0], time[1], time[2].Split('+')[0]);

            var imgList = new Dictionary<string, string>();

            if (!int.TryParse(_imgCnt, out var imgCnt)) return null;

            var temp = $"https://i.pximg.net/img-original/img/{year}/{month}/{day}/{hour}/{min}/{sec}/{Number}";
            var ext = Path.GetExtension(_src_gall.BraceParse("urls").GetStringValue("original"));

            for (var i = 0; i < imgCnt; i++)
            {
                imgList.Add($"{Number}_p{i}{ext}", $"{temp}_p{i}{ext}");
            }

            return imgList;
        }

        public bool IsValidated()
        {
            return Number != null;
        }

        //private static string XmlHttpRequest_Data(string username, string password)
        //{
        //    var (temp, header) = StrLoad.LoadRespHeader("https://www.pixiv.net");
        //    var tok = temp.Split("pixiv.context.token = \"")[1].Split('"')[0];

        //    //if (resp == null) return null;

        //    HttpWebRequest rq = null;
        //    WebResponse resp = null;
        //    try
        //    {
        //        string returnVal;

        //        rq             = WebRequest.CreateHttp("https://accounts.pixiv.net/login?return_to=https%3A%2F%2Fwww.pixiv.net%2F&lang=en&source=pc&view_type=page") as HttpWebRequest;
        //        rq.UserAgent   = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36";
        //        rq.Method      = "POST";
        //        rq.Referer     = "https://accounts.pixiv.net/login?return_to=https%3A%2F%2Fwww.pixiv.net%2F&lang=en&source=pc&view_type=page";
        //        rq.ContentType = "application/x-www-form-urlencoded";

        //        var coco = new CookieContainer();
        //        var split = header.Split("Set-Cookie: ")[1].Split('\n')[0].Split(", ");
        //        for (var i = 0; i < split.Length; i += 2)
        //        {
        //            var slice = (split[i] + ", " + split[i + 1]);
        //            coco.Add(new Cookie(
        //                         slice.Split('=')[0],
        //                         /*slice.Split('=')[0] == "PHPSESSID" ? "10531687_Rkxcnxvawwvfou128VINRuJnQPj84fy7" : */slice.Split('=')[1].Split(';')[0],
        //                         slice.Split("path=")[1].Split(';')[0],
        //                         slice.Contains("domain")
        //                             ? slice.Split("domain=")[1].Split(';')[0]
        //                             : "www.pixiv.net"
        //                         )
        //                );
        //        }

        //        rq.CookieContainer  = coco;

        //        var param = Encoding.UTF8.GetBytes("captcha=&g_recaptcha_response=&"           +
        //                                           $"password={password}&"                     +
        //                                           $"pixiv_id={username}&"                     +
        //                                           $"post_key={tok}&"                          +
        //                                           "source=pc&"                                +
        //                                           "ref=&"                                     +
        //                                           "return_to=https%3A%2F%2Fwww.pixiv.net%2F&"
        //                                           );
        //        rq.ContentLength = param.Length;
        //        rq.Headers.Add("origin", "https://accounts.pixiv.net");
        //        rq.Headers.Add("sec-ch-ua", "\" Not A;Brand\";v=\"99\", \"Chromium\";v=\"90\", \"Google Chrome\";v=\"90\"");
        //        rq.Headers.Add("sec-ch-ua-mobile", "?0");
        //        rq.Headers.Add("sec-fetch-mode", "cors");
        //        rq.Headers.Add("sec-fetch-dest", "empty");
        //        rq.Headers.Add("sec-fetch-site", "same-origin");
        //        rq.Headers.Add("pragma", "no-cache");
        //        rq.Headers.Add("accept", "application/json");
        //        rq.Headers.Add("accept-encoding", "deflate, br");
        //        rq.Headers.Add("accept-language", "en-US;q=0.8,ko-KR,ko;q=0.9,en;q=0.7,ja;q=0.6");

        //        using (var s = rq.GetRequestStream())
        //        {
        //            s.Write(param, 0, param.Length);
        //        }

        //        Thread.Sleep(1000);

        //        resp = rq.GetResponse();
        //        using (var s = resp.GetResponseStream())
        //        {
        //            var sb = new StringBuilder();

        //            int count;
        //            var buffer = new byte[1024];
        //            do
        //            {
        //                count = s.Read(buffer, 0, buffer.Length);
        //                sb.Append(Encoding.UTF8.GetString(buffer, 0, count));
        //            } while (count > 0);

        //            returnVal = sb.ToString();
        //        }
        //        resp.Close();

        //        //while (returnVal.Contains("restricted"))
        //        //{
        //        //    param = Encoding.UTF8.GetBytes("captcha=&g_recaptcha_response=&" +
        //        //                                   $"password={password}&"           +
        //        //                                   $"pixiv_id={username}&"           +
        //        //                                   $"post_key={tok}&"                +
        //        //                                   "source=pc&"                      +
        //        //                                   "ref=&"                           +
        //        //                                   "return_to=https%3A%2F%2Fwww.pixiv.net%2F&" +
        //        //                                   "recaptcha_enterprise_score_token="
        //        //    );
        //        //}

        //        return returnVal;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        rq?.Abort();
        //        resp?.Close();
        //    }
        //}
    }

}