using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace imgLoader_CLI
{
    //todo: 저장된 정보 읽어오는 함수 만들것
    //todo: 여러 작품이 하나로 나오는 것 처리 (예시: Gakuen Rankou (jairou))
    //todo: 중복 작품 체크: 번호              
    internal static class Program
    {
        private static void Main(string[] args)
        {
            //var test = new Hitomi("1806482");

            //var ttest = test.GetImgUrls();
            //var test1 = new ehentai("1810150/8c545adb44");
            //new Indexer("D:\\test\\1").Index();

            Console.WriteLine($"\n\n{Core.ProjectName} {Assembly.GetExecutingAssembly().GetName().Version}\n");

            if (args.Length != 0)
            {
                var temp = new string[args.Length];
                for (var i = 0; i < args.Length; i++)
                {
                    temp[i] = "";
                    if (!args[i].Contains('-')) temp[i] = args[i];
                    if (string.Equals(args[i], "-r", StringComparison.OrdinalIgnoreCase))
                    {
                        if (Directory.Exists(args[i + 1]))
                        {
                            Core.Route = args[i + 1]; i++;
                        }
                        else
                        {
                            Console.WriteLine("\n No such directory\n");
                            return;
                        }
                    }
                }
                args = temp.ToArray();
            }

            if (Core.Route.Length == 0 && File.Exists($"{Path.GetTempPath()}{Core.TempRoute}.txt") && Directory.Exists(File.ReadAllText($"{Path.GetTempPath()}{Core.TempRoute}.txt")))
            {
                Core.Route = File.ReadAllText($"{Path.GetTempPath()}{Core.TempRoute}.txt");
            }

#if DEBUG
            Core.Route = "D:\\문서\\사진\\Saved Pictures\\고니\\i\\새 폴더 (5)";
#endif

            Console.WriteLine($"Current path: {Core.Route}\n");

            if (args.Length == 0) Console.WriteLine("\nType \'help\' for help, Input gallery address to start download.");

            while (true)
            {
                if (Core.Route?.Length == 0)
                {
                    Console.Write("\nNew path: ");

                    var path = Console.ReadLine();
                    if (string.Equals(path, "exit", StringComparison.OrdinalIgnoreCase))
                    {
                        Core.Route = File.ReadAllText($"{Path.GetTempPath()}{Core.TempRoute}.txt");
                        continue;
                    }
                    if (Directory.Exists(path))
                    {
                        Core.Route = path;
                        File.WriteAllText($"{Path.GetTempPath()}{Core.TempRoute}.txt", path);
                    }
                    else
                    {
                        Console.WriteLine("\n No such directory\n");
                        continue;
                    }
                }

                if (args.Length == 0)
                {
                    Console.Write("\nInput: ");

                    var read = Console.ReadLine();
                    if (read == null) return;

                    if (read.Contains(" "))
                    {
                        var psr = new Processor();
                        psr.Initialize(read.Split(" "));
                    }
                    else
                    {
                        if (string.Equals(read, "exit", StringComparison.OrdinalIgnoreCase)) break;
                        if (string.Equals(read, "path", StringComparison.OrdinalIgnoreCase))
                        {
                            Core.Route = "";
                            continue;
                        }

                        if (string.Equals(read, "search", StringComparison.OrdinalIgnoreCase))
                        {
                            var index = Core.Index(Core.Route);
                            while (true)
                            {
                                Console.Write("\nSearch: ");

                                var temp = Console.ReadLine();
                                if (string.Equals(temp, "exit", StringComparison.OrdinalIgnoreCase)) break;

                                Core.Search(index, temp, Core.Route);
                            }
                            continue;
                        }

                        if (string.Equals(read, "help", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("\nReset download path: \t\tPath\nSearch from infos: \tSearch\nCancel command: \t\tExit"); //\nSearch from indexed infos: \tSearch
                            continue;
                        }

                        var psr = new Processor();
                        psr.Initialize(new string[] { read });
                    }
                }
                else
                {
                    var psr = new Processor();
                    psr.Initialize(args);
                    break;
                }
            }
        }
    }
}
