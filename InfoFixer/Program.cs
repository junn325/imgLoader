using System;
using System.IO;
using System.Linq;
using System.Text;
using imgL_Fixer.imgLoader;

namespace imgL_Fixer
{
    class Program
    {
        //todo: 작가명 제대로 안써진것들 수정
        private static void Main(string[] args)
        {
            const string temp = "D:\\문서\\사진\\Saved Pictures\\고니\\manga";

            Console.Write("File(s): " + temp);
            var key = Console.ReadKey();

            string route;
            if (key.Key == ConsoleKey.Enter)
            {
                route = temp;
            }
            else
            {
                Console.Clear();
                Console.Write("File(s): ");
                route = Console.ReadLine();
            }

            Console.Write("Route: ");
            //var route = Console.ReadLine();

            //FixInfo_LineBreak(route);
            FixInfo_Ext_N_Content(route);
        }

        private static void FixInfo_LineBreak(string route)
        {
            var tp = Directory.EnumerateFiles(route, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".Hiyobi") || s.EndsWith(".Hitomi") || s.EndsWith(".EHentai") || s.EndsWith(".NHentai"));
            foreach (var item in tp)
            {
                var text = File.ReadAllText(item);
                if (text.Length == 0)
                {
                    Console.WriteLine($"Empty info: {item}");
                    continue;
                }
                //if (text.Contains(';')) continue;

                var sb = new StringBuilder(text);
                if (text.StrLen("\n") == 3) sb.Append('\n');
                sb.Replace("\r\n", "\n");


                if ((sb.ToString()[^1] == '\n' || sb.ToString()[^1] == '\r') && (sb.ToString()[^2] == '\n' || sb.ToString()[^2] == '\r'))
                    sb.Remove(sb.Length - 1, 1);

                if (sb.ToString() == text) continue;

                File.Delete(item);
                Console.WriteLine($"Deleted: {item}");
                File.WriteAllText(item, sb.ToString());
                File.SetAttributes(item, FileAttributes.Hidden);
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

        private static void FixInfo_Tag(string route)
        {
            var tp = Directory.EnumerateFiles(route, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".Hiyobi") || s.EndsWith(".Hitomi") || s.EndsWith(".EHentai") || s.EndsWith(".NHentai"));
            foreach (var item in tp)
            {
                var text = File.ReadAllText(item);
                if (text.Length == 0)
                {
                    Console.WriteLine($"Empty info: {item}");
                    continue;
                }
                //if (text.Contains(';')) continue;

                var temp = text.Split('\n');
                var sb = new StringBuilder();
                for (var i = 0; i < 3; i++)
                {
                    sb.Append(temp[i]).Append('\n');
                }

                sb.Append("tags:");

                for (var i = 3; i < temp.Length; i++)
                {
                    //if (temp[i].Contains(';')) continue;
                    if (!temp[i].Contains(':')) continue;
                    if (temp[i].Contains("tags: ")) temp[i] = temp[i].Replace("tags: ", "tags:");
                    if (!temp[i].Contains('"') && !temp[i].Contains("female") && !temp[i].Contains("male") && !temp[i].Contains("artist") && !temp[i].Contains("tag"))
                    {
                        sb.Append('\n').Append(temp[i]);
                        break;
                    }

                    if (temp[i].Contains('\r'))
                        temp[i] = temp[i].Remove(temp[i].IndexOf('\r'));

                    try
                    {
                        switch (item.Split('.').Last())
                        {
                            case "Hiyobi":
                                if (!temp[i].Contains("value")) goto hitomi;
                                sb.Append(StrTools.GetStringValue(temp[i], "value")).Append(';');
                                break;

                            case "Hitomi":
                            hitomi: if (!temp[i].Contains("\"tag\"")) continue;
                                sb.Append(
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

                if (sb.ToString() != text)
                {
                    File.Delete(item);
                    Console.WriteLine($"Deleted: {item}");
                    File.WriteAllText(item, sb.ToString());
                    File.SetAttributes(item, FileAttributes.Hidden);
                }
                else
                {
                    Console.WriteLine($"{item}: no changes");
                }
            }


        }

        private static void FixInfo_Ext_N_Content(string route)
        {
            var tp = Directory.EnumerateFiles(route, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".Hiyobi") || s.EndsWith(".Hitomi") || s.EndsWith(".EHentai") || s.EndsWith(".NHentai"));
            var sb = new StringBuilder();
            var sb1 = new StringBuilder();

            string[] infos;
            foreach (var info in tp)
            {
                infos = new string[6];
                var temp = File.ReadAllText(info).Split('\n');

                infos[0] = info.Split('.').Last();
                for (int i = 1; i < 5; i++) infos[i] = temp[i - 1];

                Console.WriteLine($"Deleted: {info}");
                sb.Append("Deleted: ").Append(info).Append('\n');
                File.Delete(info);

                var infoRoute = info.Replace(info.Split('.').Last(), "ilif");
                using var sw = new StreamWriter(new FileStream(infoRoute, FileMode.Create, FileAccess.ReadWrite), Encoding.UTF8);

                for (var i = 0; i < infos.Length; i++)
                {
                    sw.Write(
                        i != infos.Length - 1
                            ? infos[i] + '\n'
                            : infos[i]
                    );
                }

                File.SetAttributes(infoRoute, FileAttributes.Hidden);
            }
            File.WriteAllText($"{DateTime.Now.Ticks}.txt", sb.ToString());
        }

        private static void FixInfo_Ext_Only(string route)
        {
            var tp = Directory.EnumerateFiles(route, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".Hiyobi") || s.EndsWith(".Hitomi") || s.EndsWith(".EHentai") || s.EndsWith(".NHentai"));

            foreach (var path in tp)
            {
                File.Move(path, $"{route}\\{path.Split('\\')[^2]}\\{path.Split('\\').Last().Split('.')[0]}.ilif");
            }
        }
    }
}
