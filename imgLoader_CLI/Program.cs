using System;
using System.IO;
using System.Linq;
using System.Reflection;
using imgLoader_CLI.Sites;

namespace imgLoader_CLI
{
    //todo: 저장된 정보 읽어오는 함수 만들것
    //todo: hitomi.la 토글을 프로그램 settings에 추가
    //todo: -a 같은 옵션 추가
    //todo: numericupdown 같은것으로 작품별로 순위 매기는 시스템
    //todo: 50(100?) 장마다 작은 선분으로 표시
    //todo: 작가, 태그 등으로 자동으로 폴더로 나눠주는 시스템
    //todo: 여러 작품이 하나로 나오는 것 처리 (예시: Gakuen Rankou (jairou))
    //todo: e-hentai
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var test = new ehentai("1806482/287828bb60");
            //var test1 = new ehentai("1810150/8c545adb44");

            Console.WriteLine($"\n\n{Core.PROJECT_NAME} {Assembly.GetExecutingAssembly().GetName().Version}\n");

            if (args.Length != 0)
            {
                string[] temp = new string[args.Length];
                for (var i = 0; i < args.Length; i++)
                {
                    temp[i] = "";
                    if (!args[i].Contains('-')) temp[i] = args[i];
                    //if (string.Equals(args[i], "-nh", StringComparison.OrdinalIgnoreCase)) Core.HitomiAlways = false;
                    if (string.Equals(args[i], "-r", StringComparison.OrdinalIgnoreCase))
                    {
                        if (Directory.Exists(args[i + 1])) { Core.Route = args[i + 1]; i++; }
                        else {Console.WriteLine("\n No such directory\n"); return;}
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
                //Console.WriteLine($"(hiyobi)Download first from Hitomi.la: {(Core.HitomiAlways ? "on" : "off")}\n");
                Console.WriteLine("\nType \'help\' for help, Input address to start download.");
            }

            while (true)
            {
                if (Core.Route?.Length == 0)
                {
                    Console.Write("\nNew path: ");

                    string path = Console.ReadLine();

                    if (string.Equals(path, "exit", StringComparison.OrdinalIgnoreCase)) { Core.Route = File.ReadAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt"); continue; }

                    if (Directory.Exists(path)) { Core.Route = path; File.WriteAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt", path); }
                    else { Console.WriteLine("\n No such directory\n"); continue; }
                }

                if (args.Length == 0)
                {
                    Console.Write("\nInput: ");

                    var read = Console.ReadLine();
                    string[] temp;

                    if (read.Contains(" "))                      
                    {
                        temp = read.Split(" ");
                        var psr = new Processor();
                        psr.Initialize(temp);
                    }
                    else
                    {
                        //if (string.Compare(read, "h", StringComparison.OrdinalIgnoreCase) == 0)
                        //{
                        //    Core.HitomiAlways = !Core.HitomiAlways; Console.WriteLine($"\n(hiyobi)Download first from Hitomi.la: {(Core.HitomiAlways ? "on" : "off")}");
                        //    continue;
                        //}
                        if (string.Compare(read, "exit", StringComparison.OrdinalIgnoreCase) == 0) break;
                        if (string.Compare(read, "r", StringComparison.OrdinalIgnoreCase) == 0) { Core.Route = ""; continue; }
                        if (string.Compare(read, "help", StringComparison.OrdinalIgnoreCase) == 0) { Console.WriteLine("\nReset download path: r\nCancel command: exit"); continue; }
                        
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
