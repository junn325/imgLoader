using System.Collections.Generic;
using System.Net;
using System.Text;

namespace imgLoader_CLI.Sites
{
    public class nhentai : ISite
    {
        public static string[] Supplement = { "g", "http://nhentai.net/g/" };
        public static string Host = "nhentai.net";

        private static readonly string[] FILTER = { " - Read Online", " - hentai doujinshi", "  Hitomi.la", " | Hitomi.la" };
        private static readonly string[] REPLACE = { "", "", "", "" };

        private readonly string _source;
        private readonly string _number;

        private int _imgNum;
        private string _artist;
        private string _title;

        public nhentai(string mNumber)
        {
            var wc = new WebClient();
            wc.Encoding = Encoding.UTF8;

            try
            {
                _source = wc.DownloadString($"https://nhentai.net/api/gallery/{mNumber}");
            }
            catch
            {
                return;
            }

            _number = StrTools.GetStringValue(_source,"media_id");
        }

        public string GetArtist()
        {
            _artist = StrTools.GetStringValue(_source, "artist\",\"name");
            return _artist;
        }

        public string GetTitle()
        {
            _title = StrTools.GetStringValue(_source, "pretty");
            return _title;
        }

        public string[] ReturnInfo()
        {
            string[] info = new string[5];

            info[0] = _title;
            info[1] = _artist ?? "N/A";
            info[2] = _imgNum.ToString();

            StringBuilder temp = new StringBuilder();
            temp.Append("tags: ");
            foreach (string item in StrTools.GetValue(_source,"tags",'[',']').Split("\"type\":\"tag\",\"name\":\""))
            {
                if (!item.Contains("tag")) continue;
                temp.Append(item.Split('\"')[0]).Append(", ");
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
            _imgNum = int.Parse(StrTools.GetValue(_source,"num_pages"));

            for (int i = 1; i <= _imgNum; i++)
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
                temp.Add($"{i}.{ext}", $"https://i.nhentai.net/galleries/{_number}/{i}.{ext}");
            }

            return temp;
        }

        public static string Filter(string dirName)
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