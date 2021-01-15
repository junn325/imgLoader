using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace imgLoader_CLI.Sites
{
    public class Hiyobi : ISite
    {
        public const string Supplement = "hiyobi.me/reader/\\n\\";

        private static readonly string[] FILTER = { " - Hiyobi.me", " - hiyobi.me" };
        private static readonly string[] REPLACE = { "", "" };

        private readonly string _src_cdn;
        private readonly string _src_api;

        public string Number { get; set; }

        private readonly string _title;
        private readonly string _artist;

        public Hiyobi(string mNumber)
        {
            var wc = new WebClient();
            wc.Encoding = Encoding.UTF8;

            try
            {
                _src_api = wc.DownloadString($"https://api.hiyobi.me/gallery/{mNumber}");
                _src_cdn = wc.DownloadString($"https://cdn.hiyobi.me/json/{mNumber}_list.json");

                Number = mNumber;

                _title = StrTools.GetStringValue(_src_api, "title");
                _artist = _src_api.Contains("artists") && StrTools.GetValue(_src_api, "artists", '[').Contains("value") ? StrTools.GetStringValue(StrTools.GetValue(_src_api, "artists", '['), "value") : "N/A";
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
            string[] js = _src_cdn.Split('{');
            var imgList = new Dictionary<string, string>();

            for (int i = 1; i < js.Length; i++)
            {
                if (!js[i].Contains("name")) continue;

                string name = StrTools.GetStringValue(js[i], "name");
                imgList.Add(name, $"http://cdn.hiyobi.me/data/{Number}/{name}");
            }

            return imgList;
        }

        public string GetTitle()
        {
            return _title ?? throw new Exception("_title was Null");
        }

        public string[] ReturnInfo()
        {
            string[] info = new string[5];

            info[0] = _title ?? throw new Exception("_title was Null");
            info[1] = _artist ?? "N/A";
            info[2] = _src_cdn.StrLen("name").ToString();

            var sb = new StringBuilder();
            sb.Append("tags:");
            foreach (var item in StrTools.GetValue(_src_api, "tags", '[', ']').Split('{'))
            {
                if (item.Length == 0) continue;
                sb.Append(StrTools.GetStringValue(item.Split('}')[0],"value")).Append(';');
            }

            info[3] = sb.ToString().Trim();
            if (!_src_api.Contains("date")) return info;
            info[4] = StrTools.GetStringValue(_src_api, "date");

            return info;
        }

        public bool IsValidated()
        {
            return _artist != null;
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