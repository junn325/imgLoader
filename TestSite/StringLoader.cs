using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace TestSite
{
    class StringLoader
    {
        public void Load(string url)
        {
            var sb = new StringBuilder();

            var req = WebRequest.Create(url) as HttpWebRequest;
            var resp = req.GetResponse() as HttpWebResponse;

            using (var br = resp.GetResponseStream())
            {
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
}
