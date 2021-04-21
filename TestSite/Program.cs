using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;

namespace TestSite
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("2진수 입력시 0b 접두사 필요; 자리수 구분자 _ 자동으로 무시");
            //while (true)
            //{
            //    var temp = Console.ReadLine().Replace("_", "");

            //    try
            //    {
            //        temp = Convert.ToString(Convert.ToSByte(temp), 2);
            //    }
            //    catch
            //    {
            //        Console.WriteLine("숫자가 아니거나 너무 큽니다. 다시 시도하세요.");
            //        continue;
            //    }

            //    TwosComplement.Calc(Convert.ToSByte(temp.Length >= 8 ? temp[8..] : temp.PadLeft(8, '0'), 2));
            //}

            var temp = Warshall.DoWarshall(new bool[,] {
                { false, false, true, true, false },
                { true, false, true, false, false },
                { false, false, false, false, false },
                { false, false, false, false, true },
                { false, false, false, false, false }

            });

            Warshall.Print2DArr(temp);
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

