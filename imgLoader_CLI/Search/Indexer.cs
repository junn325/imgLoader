using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace imgLoader_CLI.Search
{
    public class Indexer
    {
        private readonly string _route;

        public Indexer(string route)
        {
            _route = route;
        }

        public void Index()
        {
            var infoFiles = Directory.EnumerateFiles(_route, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".Hitomi") || s.EndsWith(".Hiyobi") || s.EndsWith(".NHentai") || s.EndsWith("EHentai"));
            var icnt = infoFiles.Count();
            var infos = new string[icnt];

            var tasks = new Task[icnt];

            var i = 0;
            foreach (var info in infoFiles)
            {
                var i1 = i;
                tasks[i] = Task.Factory.StartNew(() => infos[i1] = File.ReadAllText(info));
                i++;
            }

            foreach (var t in tasks)
            {
                t.Wait();
            }

            ;
        }
    }
}
