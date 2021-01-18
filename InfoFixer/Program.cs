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
            var route =  Console.ReadLine();

            var a = 0;
            foreach (var item in Directory.EnumerateFiles(route, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".Hiyobi") || s.EndsWith(".Hitomi")))
            {
                var temp = Directory.EnumerateFiles(item.Replace("\\" + item.Split('\\').Last(), ""), "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".Hiyobi") || s.EndsWith(".Hitomi"));
                if (temp.Count() == 1) continue;
                FixAllFromNumber(item);
                a++;

                if (a == 319) 
                    ;
            }
        }

        private static void FixInfo(string route)
        {
            foreach (var item in Directory.EnumerateFiles(route, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".Hiyobi") || s.EndsWith(".Hitomi") || s.EndsWith(".EHentai") || s.EndsWith(".NHentai")))
            {
                var temp = File.ReadAllText(item).Split('\n');
                var sb = new StringBuilder();
                for (var i = 3; i < temp.Length; i++)
                {
                    if (!temp[i].Contains(':')) continue;
                    if (temp[i].Contains("tags: ")) temp[i] = temp[i].Replace("tags: ", "tags:");

                    try
                    {
                        switch (item.Split('.')[1])
                        {
                            case "Hiyobi":
                                sb.Append(StrTools.GetStringValue(item.Split('}')[0], "value")).Append(';');
                                break;

                            case "Hitomi":
                                sb.Append(
                                        temp.Contains("female")
                                            ? (StrTools.GetValue(temp[i], "female") == "1")
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

                        temp[i] = sb.ToString();
                        sb.Clear();
                    }
                    catch
                    {
                        continue;
                    }
                }
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
    }
}
