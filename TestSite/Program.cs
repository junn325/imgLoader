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
            Console.WriteLine(Calculator.div(17.0, 2.0));
        }
    }

    static class Calculator
    {
        public static int Sum(int a, int b)
        {
            return a + b;
        }

        public static int Minus(int a, int b)
        {
            return a - b;
        }
        public static double div(double a, double b)
        {
            return a / b;
        }

    }
}
