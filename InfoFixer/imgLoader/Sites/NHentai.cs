using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace imgL_Fixer.imgLoader.Sites
{
    public class NHentai : ISite
    {
        public const string Supplement = "nhentai.net/g/\\n\\/";

        private static readonly string[] FILTER = { " - Read Online", " - hentai doujinshi", "  Hitomi.la", " | Hitomi.la" };
        private static readonly string[] REPLACE = { "", "", "", "" };

        private readonly string _source;
        public string Number { get; set; }

        private int _imgNum;
        private readonly string _artist;
        private readonly string _title;

        public NHentai(string mNumber)
        {
            var wc = new WebClient();
            wc.Encoding = Encoding.UTF8;

            try
            {
                _source = wc.DownloadString($"https://nhentai.net/api/gallery/{mNumber}");
                Number = StrTools.GetStringValue(_source, "media_id");

                _artist = StrTools.GetStringValue(_source, "artist\",\"name");
                _title = StrTools.GetStringValue(_source, "pretty");
            }
            catch
            {
                throw new Exception("failed to initiate");
            }
        }

        public string GetArtist()
        {
            return _artist;
        }

        public string GetTitle()
        {
            return _title;
        }

        public string[] ReturnInfo()
        {
            string[] info = new string[5];

            info[0] = _title;
            info[1] = _artist ?? "N/A";
            info[2] = _imgNum.ToString();

            var temp = new StringBuilder();
            temp.Append("tags:");
            foreach (var item in StrTools.GetValue(_source,"tags",'[',']').Split("\"type\":\"tag\",\"name\":\""))
            {
                if (!item.Contains("tag")) continue;
                temp.Append(item.Split('\"')[0]).Append(';');
            }
            info[3] = temp.ToString().Trim();

            if (!_source.Contains("datetime")) return info;
            info[4] = _source.Split("datetime=\"")[1].Split('\"')[0];

            return info;
        }

        public Dictionary<string, string> GetImgUrls()
        {
            string ext;
            var temp = new Dictionary<string, string>();
            _imgNum = int.Parse(StrTools.GetValue(_source, "num_pages"));

            for (var i = 1; i <= _imgNum; i++)
            {
                ext = StrTools.GetStringValue(StrTools.GetValue(_source, "pages", '[').Split('{')[i],"t");
                switch (ext)
                {
                    case "j":
                        ext = "jpg";
                        break;
                    case "p":
                        ext = "png";
                        break;
                    case "g":
                        ext = "gif";
                        break;
                }
                temp.Add($"{i}.{ext}", $"https://i.nhentai.net/galleries/{Number}/{i}.{ext}");
            }

            return temp;
        }

        public bool IsValidated()
        {
            return _title != null;
        }
    }
}