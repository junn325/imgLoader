using System;
using System.IO;
using System.Linq;
using System.Text;
using imgLoader_CLI;

namespace InfoFixer
{
    class Program
    {
        //페이지수 0인것도 수정할것
        private static void Main(string[] args)
        {
            Console.Write("Route: ");
            var route =  Console.ReadLine();

        }

        private void FixInfo(string route)
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

                    ;
                }
            }

        }
    }
}
