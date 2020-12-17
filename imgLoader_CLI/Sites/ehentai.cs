using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgLoader_CLI.Sites
{
    class ehentai : ISite
    {
        internal static string[] Supplement = {
            "g"
        };

        internal static string Host = "e-hentai.org";

        private static readonly string[] FILTER = { " - Read Online", " - hentai doujinshi", "  Hitomi.la", " | Hitomi.la" };
        private static readonly string[] REPLACE = { "", "", "", "" };

        private readonly string _source;
        private readonly string _number;
        private string _artist;

        public ehentai(string mNumber)
        {

        }

        public string GetArtist()
        {
            return "";
        }

        public Dictionary<string, string> GetImgUrls()
        {

        }

        public string GetTitle();

        public string[] ReturnInfo();

        public bool IsValidated();
    }
}
