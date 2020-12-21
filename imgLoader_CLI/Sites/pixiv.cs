using System.Collections.Generic;

namespace imgLoader_CLI.Sites
{
    public class pixiv : ISite
    {
        public static string[] Supplement = { "artworks" };
        public static string Host = "pixiv.net";

        private static readonly string[] FILTER = { " - Read Online", " - hentai doujinshi", "  Hitomi.la", " | Hitomi.la" };
        private static readonly string[] REPLACE = { "", "", "", "" };

        private readonly string _source;
        private readonly string _number;

        public pixiv(string mNumber)
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

        public static string Filter(string dirName)
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