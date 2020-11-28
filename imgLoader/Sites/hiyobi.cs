using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace imgLoader.Sites
{
    internal class hiyobi : ISite
    {
        internal static string[] Supplement = { "reader" };
        internal static string Host = "hiyobi.me";

        private static readonly string[] FILTER = { " - Hiyobi.me", " - hiyobi.me" };
        private static readonly string[] REPLACE = { "", "" };

        private readonly string _source;
        private readonly string _number;
        private string _artist;

        public hiyobi(string mNumber)
        {
            var wc = new WebClient();
            wc.Encoding = Encoding.UTF8;

            try
            {
                _source = wc.DownloadString($"https://ltn.hitomi.la/galleries/{mNumber}.js");

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
            string[] js = _source.Split('{');
            var imgList = new Dictionary<string, string>();

            for (int i = 2; i < js.Length - 1; i++)
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
            return Filter(_source.Split("title\":\"")[1].Split('\"')[0]);
        }

        public string[] ReturnInfo()
        {
            string[] info = new string[5];

            info[0] = StrTools.GetStringValue(_source, "title");
            info[1] = _artist ?? "N/A";
            info[2] = _source.StrLen("hash").ToString();

            StringBuilder temp = new StringBuilder();
            foreach (string item in StrTools.GetStringValue(_source, "tags", '[', ']').Split('{'))
            {
                if (item.Length == 0) continue;

                temp.Append(item.Split('}')[0] + '\n');
            }

            info[3] = temp.ToString().Trim();
            info[4] = StrTools.GetStringValue(_source, "date");

            return info;
        }

        public bool IsValidated()
        {
            return _number != null;
        }

        internal static string Filter(string dirName)
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

        private string Subdomain_from_url(string url, string Base)
        {
            var retval = "a";

            if (Base.Length != 0)
            {
                retval = Base;
            }

            var frontendNum = 3;
            const int parseBase = 16;                          //몇진수인지 표시

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