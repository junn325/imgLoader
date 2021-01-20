using System;
using System.IO;
using System.Linq;
using System.Text;
using imgL_Fixer.imgLoader;

namespace imgL_Fixer
{
    class Program
    {
        //페이지수 0인것도 수정할것
        //todo: 작가명 제대로 안써진것들 수정
        private static void Main(string[] args)
        {
            Console.Write("Route: ");
            var route = Console.ReadLine();

            ScanforNoInfo(route);
            FixInfo(route);
        }

        private static void FixInfo(string route)
        {
            foreach (var item in Directory.EnumerateFiles(route, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".Hiyobi") || s.EndsWith(".Hitomi") || s.EndsWith(".EHentai") || s.EndsWith(".NHentai")))
            {
                var text = File.ReadAllText(item);
                if (text.Length == 0)
                {
                    Console.WriteLine($"Empty info: {item}");
                    continue;
                }
                if (text.Contains(';')) continue;

                var temp = text.Split('\n');
                var sb = new StringBuilder();
                for (var i = 0; i < 3; i++)
                {
                    sb.Append(temp[i]).Append('\n');
                }

                sb.Append("tags:");

                for (var i = 3; i < temp.Length; i++)
                {
                    if (temp[i].Contains(';')) continue;
                    if (!temp[i].Contains(':')) continue;
                    if (temp[i].Contains("tags: ")) temp[i] = temp[i].Replace("tags: ", "tags:");
                    if (!temp[i].Contains('"') && !temp[i].Contains("female") && !temp[i].Contains("male") && !temp[i].Contains("artist") && !temp[i].Contains("tag"))
                    {
                        sb.Append('\n').Append(temp[i]);
                        break;
                    }

                    try
                    {
                        switch (item.Split('.').Last())
                        {
                            case "Hiyobi":
                                if (!temp[i].Contains("value")) goto hitomi;
                                sb.Append(StrTools.GetStringValue(temp[i], "value")).Append(';');
                                break;

                            case "Hitomi":
                                hitomi: sb.Append(
                                         temp[i].Contains("female")
                                            ? (StrTools.GetValue(temp[i], "female") == "1" || StrTools.GetValue(temp[i], "female") == "\"1\"")
                                                ? "female"
                                                : "male"
                                            : "tag"
                                    )
                                    .Append(':')
                                    .Append(StrTools.GetStringValue(temp[i], "tag")).Append(';');
                                break;

                            case "EHentai":
                                sb.Append(temp[i]).Append(';');
                                break;

                            case "NHentai":
                                foreach (var s in temp[i].Split(", "))
                                {
                                    sb.Append(s).Append(';');
                                }
                                break;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }

                if (sb.ToString()[^1] == '\n' || sb.ToString()[^1] == '\r')
                    sb.Remove(sb.Length - 1, 1);

                File.Delete(item);
                Console.WriteLine($"Deleted: {item}");
                File.WriteAllText(item, sb.ToString());
            }

        }

        private static void FixAllFromNumber(string infoFile)
        {
            if (!int.TryParse(infoFile.Split('\\').Last().Split('.')[0], out var number)) return;
            if (number < 5000) return;

            Console.WriteLine($"\ntrying to fix {number}.");
            //if (!string.Equals(Console.ReadLine(), "Y", StringComparison.OrdinalIgnoreCase)) return;

            string link = null;
            switch (infoFile.Split('\\').Last().Split('.')[1])
            {
                case "Hitomi":
                    link = $"https://hitomi.la/reader/{number}.html";
                    break;

                case "Hiyobi":
                    link = $"https://hiyobi.me/reader/{number}";
                    break;
            }

            if (string.IsNullOrEmpty(link)) return;

            Core.Route = infoFile.Replace("\\" + infoFile.Split('\\').Last(), "");

            foreach (var file in Directory.GetFiles(Core.Route))
            {
                File.Delete(file);
                Console.WriteLine($"delete: {file}");
            }

            var psr = new Processor();
            psr.Initialize(new string[] { link });
        }

        private static void ScanforNoInfo(string route)
        {
            foreach (var item in Directory.GetDirectories(route))
            {
                if (!Directory.EnumerateFiles(item, "*.*", SearchOption.AllDirectories).Any(s => s.EndsWith(".Hiyobi") || s.EndsWith(".Hitomi") || s.EndsWith(".EHentai") || s.EndsWith(".NHentai")))
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
