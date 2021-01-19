using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace imgLoader_CLI.Sites
{
    public class NHentai : ISite
    {
        public const string Supplement = "nhentai.net/g/\\n\\/";

        private static readonly string[] Filter = { " - Read Online", " - hentai doujinshi", "  Hitomi.la", " | Hitomi.la" };
        private static readonly string[] Replace = { "", "", "", "" };

        private readonly string _source, _artist, _group, _title;
        public string Number { get; }
        public string HitomiNumber { get; }

        private int _imgNum;

        public NHentai(string mNumber)
        {
            var wc = new WebClient();
            wc.Encoding = Encoding.UTF8;

            try
            {
                _source = wc.DownloadString($"https://nhentai.net/api/gallery/{mNumber}");

                var sb = new StringBuilder();
                for (var i = 1; i < _source.Split("group\",\"name\":\"").Length; i++) sb.Append(_source.Split("group\",\"name\":\"")[i].Split('"')[0]).Append(';');
                _group = sb.ToString();
                sb.Clear();

                for (var i = 1; i < _source.Split("artist\",\"name\":\"").Length; i++) sb.Append(_source.Split("artist\",\"name\":\"")[i].Split('"')[0]).Append(';');
                _artist = sb.ToString();

                _title = StrTools.GetStringValue(_source, "pretty");

                HitomiNumber = StrTools.GetStringValue(_source, "media_id");
                Number = mNumber;
            }
            catch
            {
                throw new Exception("failed to initiate");
            }
        }

        public string GetArtist()
        {
            return $"{_artist}|{_group}";
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
            foreach (var item in StrTools.GetValue(_source, "tags", '[', ']').Split("\"type\":\"tag\",\"name\":\""))
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
                ext = StrTools.GetStringValue(StrTools.GetValue(_source, "pages").Split('{')[i],"t");
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
            return Number != null;
        }
    }
}