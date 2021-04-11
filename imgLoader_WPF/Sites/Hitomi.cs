using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace imgLoader_WPF.Sites
{
    public class Hitomi : ISite
    {
        public string Number { get; }

        private readonly string _src_info, _src_gall, _artist, _group, _title;
        public Hitomi(string mNumber)
        {
            var sb = new StringBuilder();

            try
            {
                var temp = StrLoad.LoadAsync($"https://ltn.hitomi.la/galleries/{mNumber}.js");
                var gallAddress = StrLoad.Load($"https://hitomi.la/galleries/{mNumber}.html").Split("window.location.href = \"")[1].Split('\"')[0];
                _src_gall = StrLoad.Load(gallAddress);

                _src_info = temp.Result;

                if (_src_info == null) return;

                if (_src_info.Contains("\\")) _src_info = _src_info.Replace("\\", "");
                _title = _src_info.Split("title\":\"")[1].Split('\"')[0];

                for (var i = 1; i < _src_gall.StrLen("/group/") + 1; i++) sb.Append(_src_gall.Split("/group/")[i].Split("</a>")[0].Split(">")[1]).Append(';');
                _group = sb.ToString();
                sb.Clear();

                for (var i = 1; i < _src_gall.StrLen("/artist/") + 1; i++) sb.Append(_src_gall.Split("/artist/")[i].Split("</a>")[0].Split(">")[1]).Append(';');
                _artist = sb.ToString();

                Number = mNumber;
            }
            catch (Exception ex)
            {
                Core.Log("hitomi: " + ex.Message);
            }
        }

        public string GetArtist()
        {
            return $"{_artist}|{_group}";
        }
        public Dictionary<string, string> GetImgUrls()
        {
            var js = _src_info.Split('{');
            var imgList = new Dictionary<string, string>();

            for (var i = 2; i < js.Length; i++)
            {
                var @base = "";
                if (!js[i].Contains("hash")) continue;

                var hash = StrTools.GetStringValue(js[i], "hash");
                var name = StrTools.GetStringValue(js[i], "name");

                string type;
                if (js[i].Contains("haswebp\":1")) type = "webp";
                else if (js[i].Contains("hasavif\":1")) type = "avif";
                else
                {
                    type = "images";
                    @base = "b";
                }

                string ext;
                switch (type)
                {
                    case "webp":
                        ext = "webp";
                        name = $"{name.Split('.')[0]}.webp";
                        break;

                    case "avif":
                        ext = "avif";
                        name = $"{name.Split('.')[0]}.avif";
                        break;

                    default:
                        ext = name.Split('.')[1];
                        break;
                }

                var last = Regex.Replace(hash, "^.*(..)(.)$", "$2/$1/" + hash);

                imgList.Add(name, $"https://{Subdomain_from_url(last, @base)}.hitomi.la/{type}/{last}.{ext}");
            }

            return imgList;
        }

        public string GetTitle()
        {
            return _title;
        }

        public string[] ReturnInfo()
        {
            var info = new string[5];

            info[0] = "Hitomi";
            info[1] = _title;
            info[2] = $"{_artist}|{_group}";
            info[3] = _src_info.StrLen("hash").ToString();

            var sb = new StringBuilder();
            sb.Append("tags:");
            foreach (var item in StrTools.GetValue(_src_info, "tags", '[', ']').Split('{'))
            {
                if (item.Length == 0) continue;
                var temp = item.Split('}')[0];

                sb.Append(
                        temp.Contains("female")
                            ? (StrTools.GetValue(temp, "female") == "\"1\"")
                                ? "female"
                                : "male"
                            : "tag"
                        )
                    .Append(':')
                    .Append(StrTools.GetStringValue(item.Split('}')[0], "tag")).Append(';');
            }
            foreach (var item in _src_gall.Split("Characters</td><td>")[1].Split("</ul>")[0].Split("<li>"))
            {
                if (!item.Contains("li")) continue;

                sb.Append("character").Append(':').Append(item.Split("</a></li>")[0].Split('>')[1].Trim()).Append(';');
            }

            info[4] = sb.ToString().Trim();

            return info;
        }

        public bool IsValidated()
        {
            return Number != null;
        }

        private static string Subdomain_from_url(string url, string @base)
        {
            var retval = "a";
            var frontendNum = 3;
            const int parseBase = 16;
            var regex = new Regex("[0-9a-f]\\/([0-9a-f]{2})\\/");
            var matches = regex.Match(url).Groups[1];

            if (@base.Length != 0) retval = @base;
            if (matches.Length == 0) return "a";

            if (!int.TryParse(matches.ToString(), NumberStyles.HexNumber, null, out _)) return retval;

            var g = Convert.ToInt32(matches.ToString(), parseBase);
            if (g < 0x30) frontendNum = 2;
            if (g < 0x09) g = 1;

            return (char)(97 + (g % frontendNum)) + retval;
        }
    }
}