using System.Collections.Generic;

namespace imgLoader_CLI
{
    interface ISite
    {
        string GetArtist();

        Dictionary<string, string> GetImgUrls();

        string GetTitle();

        string[] ReturnInfo();

        bool IsValidated();
    }
}