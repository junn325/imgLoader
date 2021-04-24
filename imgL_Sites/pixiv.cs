using System;
using System.Collections.Generic;
using System.Text;

namespace imgL_Sites
{
    public class Pixiv : ISite
    {
        public string Number { get; }

        private readonly string _src_gall, _src_this, _title, _artist = "", _date, _imgCnt;

        public Pixiv(string mNumber)
        {
            try
            {
                _src_gall = StrLoad.Load($"https://www.pixiv.net/artworks/{mNumber}");
                if (_src_gall == null)
                    return;

                _src_this = _src_gall.BraceParse("userIllusts").BraceParse("88966501");

                _title  = _src_gall.GetStringValue("illustTitle");
                _artist = _src_gall.GetStringValue("userName");
                _date   = _src_this.GetStringValue("createDate");

                _imgCnt = _src_this.GetValue("pageCount");

                if (_title == null || _artist == null || _date == null || _imgCnt == null)
                    return;

                Number  = mNumber;
            }
            catch (Exception e)
            {
                Core.Log("pixiv: " + e.Message);
            }
        }

        public string GetArtist()
        {
            return $"{_artist};|";
        }

        public string GetTitle()
        {
            return _title;
        }

        public string[] ReturnInfo()
        {
            var info = new string[5];

            info[0] = "Hiyobi";
            info[1] = _title;
            info[2] = $"{_artist};|";
            info[3] = _imgCnt;

            var sb = new StringBuilder();
            sb.Append("tags:");
            foreach (var item in _src_this.GetValue("tags", '[', ']').Split(','))
            {
                sb.Append(item.Split('\"')[1]).Append(';');
            }

            info[4] = sb.ToString().Trim();

            return info;
        }

        public Dictionary<string, string> GetImgUrls()
        {
            var (date, time) = (_date.Split('T')[0].Split('-'), _date.Split('T')[1].Split(':'));
            var (year, month, day,
                    hour, min, sec) = (date[0], date[1], date[2], time[0], time[1], time[2].Split('+')[0]);

            var imgList = new Dictionary<string, string>();

            if (!int.TryParse(_imgCnt, out var imgCnt)) return null;

            var temp = $"https://i.pximg.net/img-original/img/{year}/{month}/{day}/{hour}/{min}/{sec}/{Number}";
            for (var i = 0; i < imgCnt; i++)
            {
                imgList.Add($"{Number}_p{i}", $"{temp}_p{i}.png");
            }

            return imgList;
        }

        public bool IsValidated()
        {
            return Number != null;
        }
    }
}