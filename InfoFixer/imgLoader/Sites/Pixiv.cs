using System.Collections.Generic;

namespace imgL_Fixer.imgLoader.Sites
{
    public class Pixiv : ISite
    {
        public const string Supplement = "www.pixiv.net/artworks/\\n\\";

        private static readonly string[] FILTER = { " - Read Online", " - hentai doujinshi", "  Hitomi.la", " | Hitomi.la" };
        private static readonly string[] REPLACE = { "", "", "", "" };

        private readonly string _source;
        private readonly string _number;

        public Pixiv(string mNumber)
        {
        }

        public string GetArtist()
        {
            return "";
        }

        public string GetTitle()
        {
            return "";
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

        public bool IsValidated()
        {
            return _number != null;
        }
    }
}