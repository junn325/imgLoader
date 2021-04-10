using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace imgLoader_WPF
{
    public static class StrLoad
    {
        /// <summary>
        /// Value Can be null
        /// </summary>
        public static async Task<string> LoadAsync(string url)
        {
            return await Task.Run(() => Load(url)).ConfigureAwait(false);
        }

        /// <summary>
        ///  Can be null
        /// </summary>
        public static string Load(string url)
        {
            var sb = new StringBuilder();

            var req = WebRequest.Create(url);
            req.Timeout = 3000;

            var resp = req.GetResponse();
            //if (resp == null) return null;

            using var br = resp.GetResponseStream();

            int count;
            var buffer = new byte[1024];
            do
            {
                count = br.Read(buffer, 0, buffer.Length);
                sb.Append(Encoding.UTF8.GetString(buffer, 0, count));
            } while (count > 0);

            return sb.ToString();
        }
    }
}
