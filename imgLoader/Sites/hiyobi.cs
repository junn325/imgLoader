using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace imgLoader.Sites
{
    public class hiyobi : ISite
    {
        public static string[] Supplement = { "reader" };
        public static string Host = "hiyobi.me";

        private static readonly string[] FILTER = { " - Hiyobi.me", " - hiyobi.me" };
        private static readonly string[] REPLACE = { "", "" };

        private readonly string _src_cdn;
        private readonly string _src_api;

        private readonly string _number;
        private string _artist;

        //갤러리 정보: https://api.hiyobi.me/gallery/{mNumber}
        //이미지 리스트: https://cdn.hiyobi.me/json/{mNumber}_list.json

        public hiyobi(string mNumber)
        {
            var wc = new WebClient();
            wc.Encoding = Encoding.UTF8;

            try
            {
                _src_api = wc.DownloadString($"https://api.hiyobi.me/gallery/{mNumber}");
                _src_cdn = wc.DownloadString($"https://cdn.hiyobi.me/json/{mNumber}_list.json");

            }
            catch
            {
                return;
            }
            _number = mNumber;
        }

        public string GetArtist()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;

                string temp = wc.DownloadString($"https://hitomi.la/galleries/{_number}.html");
                string source = wc.DownloadString(temp.Split("window.location.href = \"")[1].Split('\"')[0]);

                _artist = source.Split("/artist/")[1].Split("</a>")[0].Split(">")[1];
                return _artist;
            }
            catch
            {
                return "N/A";
            }
        }

        public Dictionary<string, string> GetImgUrls()
        {
            string[] js = _src_cdn.Split('{');
            var imgList = new Dictionary<string, string>();

            for (int i = 1; i < js.Length - 1; i++)
            {
                if (!js[i].Contains("name")) continue;

                string name = StrTools.GetStringValue(js[i], "name");
                imgList.Add(name, $"http://cdn.hiyobi.me/data/{_number}/{name}");
            }

            return imgList;
        }

        public string GetTitle()
        {
            return Filter(_src_api.Split("title\":\"")[1].Split('\"')[0]);
        }

        public string[] ReturnInfo()
        {
            string[] info = new string[5];

            info[0] = StrTools.GetStringValue(_src_api, "title");
            info[1] = _artist ?? "N/A";
            info[2] = _src_api.StrLen("hash").ToString();

            StringBuilder temp = new StringBuilder();
            foreach (string item in StrTools.GetStringValue(_src_api, "tags", '[', ']').Split('{'))
            {
                if (item.Length == 0) continue;

                temp.Append(item.Split('}')[0] + '\n');
            }

            info[3] = temp.ToString().Trim();

            if (!_src_api.Contains("date")) return info;

            info[4] = StrTools.GetStringValue(_src_api, "date");
            return info;
        }

        public bool IsValidated()
        {
            return _number != null;
        }

        public static string Filter(string dirName)
        {
            for (byte i = 0; i < FILTER.Length; i++)
            {
                if (dirName.Contains(FILTER[i]))
                {
                    dirName = dirName.Replace(FILTER[i], REPLACE[i]);
                }
            }

            return dirName;
        }
    }
}