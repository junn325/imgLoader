using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;

namespace TestSite
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();

            int temp = 0;
            for (int i = 0; i < 20; i++)
            {
                temp = Directory.EnumerateFiles(@"F:\문서\사진\Saved Pictures\고니\manga", "*", SearchOption.AllDirectories).Count();
            }

            Console.WriteLine(temp);
            Console.WriteLine(sw.Elapsed.Ticks);
        }

    }
    public class StringLoader
    {
        private const int cnt = 1;

        private readonly WebClient wc = new();
        private readonly HttpClient hc = new();

        [Benchmark]
        public void Webclient()
        {
            var temp = new string[cnt];
            for (int i = 0; i < cnt; i++)
            {
                temp[i] = wc.DownloadString("http://www.naver.com");
            }
        }

        [Benchmark]
        public void HttpClient()
        {
            var tasks = new Task<string>[cnt];
            var temp = new string[cnt];
            for (int i = 0; i < cnt; i++)
            {
                tasks[i] = hc.GetStringAsync("http://www.naver.com");
            }

            Task.WaitAll(tasks);

            for (int i = 0; i < cnt; i++)
            {
                temp[i] = tasks[i].Result;
            }
        }

        [Benchmark]
        public void StrLoadA()
        {
            var temp = new string[cnt];
            for (int i = 0; i < cnt; i++)
            {
                temp[i] = StrLoad.Load("http://www.naver.com");
            }
        }

        [Benchmark]
        public void StrLoadU()
        {
            var tasks = new Task<string>[cnt];
            var temp = new string[cnt];
            for (int i = 0; i < cnt; i++)
            {
                tasks[i] = StrLoad.LoadAsync("http://www.naver.com");
            }

            Task.WaitAll(tasks);

            for (int i = 0; i < cnt; i++)
            {
                temp[i] = tasks[i].Result;
            }
        }
    }
}

