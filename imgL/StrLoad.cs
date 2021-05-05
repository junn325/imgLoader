using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace imgL
{
    public static class StrLoad
    {
        public static async Task<string> LoadAsync(string url)
        {
            return await Task.Run(() => Load(url)).ConfigureAwait(false);
        }

        public static string Load(string url)
        {
            var sb = new StringBuilder();

            var req = WebRequest.Create(url);
            req.Timeout = 5000;

            var resp = req.GetResponse();

            using var br = resp.GetResponseStream();

            int count;
            var buffer = new byte[1024];

            if (br == null) return null;

            do
            {
                count = br.Read(buffer, 0, buffer.Length);
                sb.Append(Encoding.UTF8.GetString(buffer, 0, count));
            } while (count > 0);

            return sb.ToString();
        }
    }
}
