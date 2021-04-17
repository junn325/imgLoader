using System.Collections.Generic;

namespace imgL_Sites
{
    public interface ISite
    {
        public string Number { get; }

        public string GetArtist();

        public Dictionary<string, string> GetImgUrls();

        public string GetTitle();

        public string[] ReturnInfo();        //제목, 태그, 날짜,  파일이름: (품번).ini

        public bool IsValidated();
    }
}