using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace imgLoader_CLI.Sites
{
    internal class nhentai : ISite
    {
        public static string[] Supplement = { "g", "-5" };
        public static string Host = "nhentai.net";

        private static readonly string[] FILTER = { " - Read Online", " - hentai doujinshi", "  Hitomi.la", " | Hitomi.la" };
        private static readonly string[] REPLACE = { "", "", "", "" };

        private readonly string _source;
        private readonly string _number;
        private string _artist;
        private string _title;

        public nhentai(string mNumber)
        {
            var wc = new WebClient();
            wc.Encoding = Encoding.UTF8;

            try
            {
                _source = wc.DownloadString($"https://nhentai.net/g/{mNumber}/");
            }
            catch
            {
                return;
            }

            _number = mNumber;
        }

        public string GetArtist()
        {
            _artist = _source.Split("artist\\u002F")[1].Split("\\u002F")[0]; 
            return _artist;
        }

        public string GetTitle()
        { 
            _title = _source.Split("\\u0022pretty\\u0022:\\u0022")[1].Split("\\u0022")[0];
            return _title;
        }

        public string[] ReturnInfo()
        {
            string[] temp = { };
            return temp;
        }

        public Dictionary<string, string> GetImgUrls()
        {
            return new Dictionary<string, string>();
        }

        internal static string Filter(string dirName)
        {
            //for (byte i = 0; i < FILTER.Length; i++)
            //{
            //    if (dirName.Contains(FILTER[i]))
            //    {
            //        dirName = dirName.Replace(FILTER[i], REPLACE[i]);
            //    }
            //}

            return dirName;
        }
        public bool IsValidated()
        {
            if (_number == null)
            {
                return false;
            }

            return true;

        }

    }
}