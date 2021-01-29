using System.Collections.Generic;

namespace imgLoader_WPF
{
    interface ISite
    {
        string GetArtist();

        Dictionary<string, string> GetImgUrls();

        string GetTitle();

        string[] ReturnInfo();        //제목, 태그, 날짜,  파일이름: (품번).ini

        bool IsValidated();
    }
}