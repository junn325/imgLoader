using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;

namespace TestSite
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<StringLoader>();
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

