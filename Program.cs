using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace imgLoader_CLI
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine($"\n\n{Core.PROJECT_NAME} {Assembly.GetExecutingAssembly().GetName().Version}\n");

            if (File.Exists($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt") && Directory.Exists(File.ReadAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt")))
            {
                Core.Route = File.ReadAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt");
                Console.WriteLine($"\n현재 경로:{Core.Route}");
            }


            if (args.Length == 0)
            {
                Console.WriteLine("\n명령 취소: exit / 경로 재설정: R / Hitomi.la 우선 다운로드 토글: H");
            }
            else
            {
                string[] temp = new string[args.Length];
                for (var i = 0; i < args.Length; i++)
                {
                    temp[i] = "";
                    if (!args[i].Contains('-')) temp[i] = args[i];
                    if (string.Equals(args[i], "-nh", StringComparison.OrdinalIgnoreCase)) Core.HitomiAlways = false;
                    if (string.Equals(args[i], "-r", StringComparison.OrdinalIgnoreCase))
                    {
                        if (Directory.Exists(args[i + 1])) { Core.Route = args[i + 1]; i++; }
                        else {Console.WriteLine("\n 존재하지 않는 경로\n"); return;}
                    }
                }
                args = temp.ToArray();
            }

            Console.WriteLine($"\nHitomi.la에서 우선적으로 다운로드: {(Core.HitomiAlways ? "켜짐" : "꺼짐")}\n");
            Console.WriteLine("\n주소를 입력해 다운로드를 시작합니다.");
            while (true)
            {
                if (Core.Route.Length == 0)
                {
                    Console.Write("\n경로: ");

                    string path = Console.ReadLine();

                    if (string.Equals(path, "exit", StringComparison.OrdinalIgnoreCase)) { Core.Route = File.ReadAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt"); continue; }

                    if (Directory.Exists(path)) { Core.Route = path; File.WriteAllText($"{Path.GetTempPath()}{Core.TEMP_ROUTE}.txt", path); }
                    else { Console.WriteLine("\n 존재하지 않는 경로\n"); continue; }
                }

                if (args.Length == 0)
                {
                    Console.Write("\n입력: ");

                    string temp = Console.ReadLine();

                    if (string.Compare(temp, "H", StringComparison.OrdinalIgnoreCase) == 0) {Core.HitomiAlways = !Core.HitomiAlways; Console.WriteLine($"\nHitomi.la에서 우선적으로 다운로드: {(Core.HitomiAlways ? "켜짐" : "꺼짐")}");
                        continue; }
                    if (string.Compare(temp, "exit", StringComparison.OrdinalIgnoreCase) == 0) break;
                    if (string.Compare(temp, "R", StringComparison.OrdinalIgnoreCase) == 0) { Core.Route = ""; continue; }

                    Processor psr = new Processor();
                    psr.Initialize(new string[] { temp });
                }
                else
                {
                    Processor psr = new Processor();
                    psr.Initialize(args);
                    break;
                }
            }
        }
    }
}
