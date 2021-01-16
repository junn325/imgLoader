using System.IO;
using System.Linq;

namespace imgLoader_CLI.Searching
{
    public class Indexer
    {
        private string _route;

        public Indexer(string route)
        {
            _route = route;
        }

        public void Index()
        {
            var fs = Directory.EnumerateFiles(_route, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".Hitomi") || s.EndsWith(".Hiyobi") || s.EndsWith(".NHentai") || s.EndsWith("EHentai"));
            ;
            //Seeker(_route);
        }

        private void Seeker(string route)
        {
            foreach (var item in new DirectoryInfo(route).GetDirectories())
            {

                if(item.Exists) Seeker(route);
            }
        }
    }
}
