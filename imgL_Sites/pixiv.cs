using System.Collections.Generic;

namespace imgL_Sites
{
    public class Pixiv : ISite
    {

        private readonly string _source;
        private readonly string _number;
        public string Number { get; }

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