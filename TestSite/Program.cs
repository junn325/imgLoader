using System;
using System.IO;
using System.Text.Json;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestSite
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var sw = new Stopwatch();
            var route = "D:\\문서\\사진\\Saved Pictures\\고니\\manga\\Lingua Franca!! (akatsuki myuuto)\\958567.Hiyobi";
            string test;
            sw.Start();

            for (int j = 0; j < 20; j++)
            {
                for (var i = 0; i < 1000; i++)
                {
                    //using var sr = File.OpenText(route);
                    //test = sr.ReadToEnd();

                    test = File.ReadAllText(route);
                }
                Console.WriteLine(sw.Elapsed.Ticks);
                sw.Restart();
            }

        }

    }
}
