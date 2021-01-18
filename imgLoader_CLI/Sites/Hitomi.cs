using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace imgLoader_CLI.Sites
{
    public class Hitomi : ISite
    {
        public const string Supplement = "hitomi.la/reader/\\n\\.html";
        public string Number { get; }

        private static readonly string[] Filter = { " - Read Online", " - hentai doujinshi", "  Hitomi.la", " | Hitomi.la" };
        private static readonly string[] Replace = { "", "", "", "" };

        private readonly string _src_info;
        private readonly string _artist;
        private readonly string _title;

        public Hitomi(string mNumber)
        {
            var wc = new WebClient();
            wc.Encoding = Encoding.UTF8;

            try
            {
                _src_info = wc.DownloadString($"https://ltn.hitomi.la/galleries/{mNumber}.js");
                var temp = wc.DownloadString($"https://hitomi.la/galleries/{mNumber}.html");
                var srcGall = wc.DownloadString(temp.Split("window.location.href = \"")[1].Split('\"')[0]);

                _title = Core.TitleFilter(_src_info.Split("title\":\"")[1].Split('\"')[0], Filter, Replace);
                _artist = srcGall.Contains("/artist/") ? srcGall.Split("/artist/")[1].Split("</a>")[0].Split(">")[1] : "N/A";

                Number = mNumber;
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
        public Dictionary<string, string> GetImgUrls()            //키: 이미지이름/값: 주소
        {
            string[] js = _src_info.Split('{');
            var imgList = new Dictionary<string, string>();

            for (int i = 2; i < js.Length; i++)
            {
                string Base = "";
                if (!js[i].Contains("hash")) continue;
                string hash = StrTools.GetStringValue(js[i], "hash");
                string name = StrTools.GetStringValue(js[i], "name");
                string type;

                if (js[i].Contains("haswebp\":1")) type = "webp";
                else if (js[i].Contains("hasavif\":1")) type = "avif";
                else
                {
                    type = "images";
                    Base = "b";
                }

                string ext;
                if (type == "webp")
                {
                    ext = "webp";
                    name = $"{name.Split('.')[0]}.webp";
                }
                else if (type == "avif")
                {
                    ext = "avif";
                    name = $"{name.Split('.')[0]}.avif";
                }
                else ext = name.Split('.')[1];

                string last = Regex.Replace(hash, "^.*(..)(.)$", "$2/$1/" + hash);

                imgList.Add(name, $"https://{Subdomain_from_url(last, Base)}.hitomi.la/{type}/{last}.{ext}");
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

            info[0] = _title ?? throw new Exception("_title was Null");
            info[1] = _artist ?? "N/A";
            info[2] = _src_info.StrLen("hash").ToString();

            var sb = new StringBuilder();
            sb.Append("tags:");
            foreach (var item in StrTools.GetValue(_src_info, "tags", '[', ']').Split('{'))
            {
                if (item.Length == 0) continue;
                var temp = item.Split('}')[0];

                sb.Append(
                        temp.Contains("female")
                            ? (StrTools.GetValue(temp, "female") == "1")
                                ? "female"
                                : "male"
                            : "tag"
                        )
                    .Append(':')
                    .Append(StrTools.GetStringValue(item.Split('}')[0], "tag")).Append(';');
            }

            info[3] = sb.ToString().Trim();
            info[4] = StrTools.GetStringValue(_src_info, "date");

            return info;
        }

        public bool IsValidated()
        {
            return Number != null;
        }

        private string Subdomain_from_url(string url, string Base)
        {
            var retval = "a";

            if (Base.Length != 0)
            {
                retval = Base;
            }

            var frontendNum = 3;
            const int parseBase = 16;           //진수

            var regex = new Regex("[0-9a-f]\\/([0-9a-f]{2})\\/");
            var matches = regex.Match(url).Groups[1];

            if (matches.Length == 0)
            {
                return "a";
            }

            if (!int.TryParse(matches.ToString(), NumberStyles.HexNumber, null, out _))
            {
                return retval;
            }

            int g = Convert.ToInt32(matches.ToString(), parseBase);

            if (g < 0x30)
            {
                frontendNum = 2;
            }

            if (g < 0x09)
            {
                g = 1;
            }

            return (char)(97 + (g % frontendNum)) + retval;
        }
    }
}