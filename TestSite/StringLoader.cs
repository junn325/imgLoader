using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace TestSite
{
    public class StringLoader
    {
        public void Load(string url)
        {
            var sb = new StringBuilder();

            var req = WebRequest.Create(url) as HttpWebRequest;
            if (req == null) return;

            var resp = req.GetResponse() as HttpWebResponse;
            if (resp == null) return;

            using var br = resp.GetResponseStream();
            if (br == null) return;

            int count;
            byte[] buffer = new byte[1024];

            using var fs = new FileStream((url.Contains('?')) ? url.Split('/').Last().Split('?')[0] : url.Split('/').Last().Length == 0 ? url.Split('/')[url.Split('/').Length - 2] : url.Split('/').Last(), FileMode.Create, FileAccess.ReadWrite);
            do
            {
                count = br.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, count);
            } while (count > 0);
        }
    }
}
