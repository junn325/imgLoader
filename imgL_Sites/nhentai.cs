using System;
using System.Collections.Generic;
using System.Text;

namespace imgL_Sites
{
    public class NHentai : ISite
    {
        private readonly string _source, _artist, _group, _title;
        public string Number { get; }
        public string Referer { get; }

        private int _imgNum;
        private readonly string _hitomiNumber;

        public NHentai(string mNumber)
        {
            try
            {
                _source = StrLoad.Load($"https://nhentai.net/api/gallery/{mNumber}");

                var sb = new StringBuilder();
                for (var i = 1; i < _source.Split("group\",\"name\":\"").Length; i++) sb.Append(_source.Split("group\",\"name\":\"")[i].Split('"')[0]).Append(';');
                _group = sb.ToString();
                sb.Clear();

                for (var i = 1; i < _source.Split("artist\",\"name\":\"").Length; i++) sb.Append(_source.Split("artist\",\"name\":\"")[i].Split('"')[0]).Append(';');
                _artist = sb.ToString();

                _title = _source.GetStringValue("pretty");

                _hitomiNumber = _source.GetStringValue("media_id");
                Number       = mNumber;
            }
            catch (Exception ex)
            {
                Core.Log("nhentai: " + ex.Message);
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
            var info = new string[5];

            info[0] = "NHentai";
            info[1] = _title;
            info[2] = $"{_artist}|{_group}";
            info[3] = _imgNum.ToString();

            var temp = new StringBuilder();
            temp.Append("tags:");
            foreach (var item in _source.GetValue("tags", '[', ']').Split("\"type\":\"tag\",\"name\":\""))
            {
                if (!item.Contains("tag")) continue;
                temp.Append(item.Split('\"')[0]).Append(';');
            }
            info[4] = temp.ToString().Trim();

            return info;
        }

        public Dictionary<string, string> GetImgUrls()
        {
            string ext;
            var temp = new Dictionary<string, string>();
            _imgNum = int.Parse(_source.GetValue("num_pages"));

            for (var i = 1; i <= _imgNum; i++)
            {
                ext = _source.Split("\"pages\":[")[1].Split(']')[0].Split('{')[i].GetStringValue("t");
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
                temp.Add($"{i}.{ext}", $"https://i.nhentai.net/galleries/{_hitomiNumber}/{i}.{ext}");
            }

            return temp;
        }

        public bool IsValidated()
        {
            return Number != null;
        }
    }
}