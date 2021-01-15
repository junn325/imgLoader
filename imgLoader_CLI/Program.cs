using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace imgLoader_CLI
{
    //todo: 저장된 정보 읽어오는 함수 만들것
    //todo: numericupdown 같은것으로 작품별로 순위 매기는 시스템
    //todo: 작가, 태그 등으로 자동으로 폴더로 나눠주는 시스템
    //todo: 여러 작품이 하나로 나오는 것 처리 (예시: Gakuen Rankou (jairou))
    //todo: 항상 위로 상태로 떠 있다가 인터넷 창에서 누르면 자동으로 해당 작품 다운로드 
    //todo: 작가별 트리식 정렬
    //todo: 중복 작품 체크: 번호              
    //todo: 작가/태그 분포, 주로 보는 작품 등 분석 기능

    internal static class Program
    {
        private static void Main(string[] args)
        {
            //var test = new Hitomi("1806482");

            //var ttest = test.GetImgUrls();
            //var test1 = new ehentai("1810150/8c545adb44");

            Console.WriteLine($"\n\n{Core.PROJECT_NAME} {Assembly.GetExecutingAssembly().GetName().Version}\n");

            if (args.Length != 0)
            {
                string[] temp = new string[args.Length];
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

            if (Core.Route.Length == 0 && File.Exists($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt") && Directory.Exists(File.ReadAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt")))
            {
                Core.Route = File.ReadAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt");
            }

            Console.WriteLine($"Current path: {Core.Route}\n");

            if (args.Length == 0)
            {
                Console.WriteLine("\nType \'help\' for help, Input address to start download.");
            }

            while (true)
            {
                if (Core.Route?.Length == 0)
                {
                    Console.Write("\nNew path: ");

                    var path = Console.ReadLine();
                    if (string.Equals(path, "exit", StringComparison.OrdinalIgnoreCase))
                    {
                        Core.Route = File.ReadAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt");
                        continue;
                    }
                    if (Directory.Exists(path))
                    {
                        Core.Route = path;
                        File.WriteAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt", path);
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
                        if (string.Equals(read, "r", StringComparison.OrdinalIgnoreCase))
                        {
                            Core.Route = "";
                            continue;
                        }

                        if (string.Equals(read, "help", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("\nReset download path: r\nCancel command: exit");
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
