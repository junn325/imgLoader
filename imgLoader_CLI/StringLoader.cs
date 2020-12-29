using System;
using System.Net;
using System.Text;

namespace imgLoader_CLI
{
    public class StringLoader
    {
        public string Load(string url)
        {
            var sb = new StringBuilder();

            var req = WebRequest.Create(url);
            var resp = req.GetResponse();
            using var br = resp.GetResponseStream();

            int count;
            byte[] buffer = new byte[1024];
            do
            {
                count = br.Read(buffer, 0, buffer.Length);
                sb.Append(Encoding.UTF8.GetString(buffer, 0, count));
            } while (count > 0);

            return sb.ToString();
        }
    }
}
