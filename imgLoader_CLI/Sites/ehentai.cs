using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgLoader_CLI.Sites
{
    //content(Sources tab) : Ln22 in (index)
    //base_url = "https://e-hentai.org/"
    //api_url = "https://api.e-hentai.org/api.php"
    //gid = first number
    //token = second number
    //aipuid = 4855480 <<< ?
    //popbase = base_url + "gallerypopups.php?gid=1806482&t=287828bb60&act="
    public class ehentai : ISite
    {
        public static string Supplement = "e-hentai.org/g/\\n\\/\\n\\/";

        private static readonly string[] FILTER = { " - Read Online", " - hentai doujinshi", "  Hitomi.la", " | Hitomi.la" };
        private static readonly string[] REPLACE = { "", "", "", "" };

        private readonly string _source;
        public string Gid { get; set; }

        private readonly string _label;
        private string _artist;

        public ehentai(string mNumber)
        {

            Gid = mNumber.Split('/')[0];
            _label = mNumber.Split('/')[1];
        }

        public string GetArtist()
        {
            return "";
        }

        public Dictionary<string, string> GetImgUrls()
        {
            return new Dictionary<string, string>();
        }

        public string GetTitle()
        {
            return "";
        }

        public string[] ReturnInfo()
        {
            return new string[5];
        }

        public bool IsValidated()
        {
            return false;
        }
    }
}
