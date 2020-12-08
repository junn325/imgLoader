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
                Console.WriteLine($"\nCurrent path: {Core.Route}");
            }


            if (args.Length == 0)
            {
                Console.WriteLine("\nCancel command: exit / Reset download path: R / Toggle download first from Hitomi.la : H");
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
                        else {Console.WriteLine("\n No such directory\n"); return;}
                    }
                }
                args = temp.ToArray();
            }

            Console.WriteLine($"\nDownload first from Hitomi.la: {(Core.HitomiAlways ? "On" : "Off")}\n");
            Console.WriteLine("\nInput the address to start the download.");
            while (true)
            {
                if (Core.Route.Length == 0)
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

                    string temp = Console.ReadLine();

                    if (string.Compare(temp, "H", StringComparison.OrdinalIgnoreCase) == 0) {Core.HitomiAlways = !Core.HitomiAlways; Console.WriteLine($"\nDownload first from Hitomi.la:: {(Core.HitomiAlways ? "On" : "Off")}");
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
