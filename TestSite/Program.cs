
using System;

namespace TestSite
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            int a = 5;
            int b = 12;

            Console.WriteLine($"{a}+{b}의 값은: {Sum(a, b)} 입니다.");
            Console.WriteLine($"{a}-{b}의 값은: {Minus(a, b)} 입니다.");
            Console.WriteLine($"{a}*{b}의 값은: {Multiple(a, b)} 입니다.");
            Console.WriteLine($"{a}/{b}의 값은: {Divide(a, b)} 입니다.");
        }

        private static int Sum(int firstNumber, int secondNumber)
        {
            int result;
            result = firstNumber + secondNumber;

            return result;
        }

        private static int Minus(int firstNumber, int secondNumber)
        {
            int result;
            result = firstNumber - secondNumber;

            return result;
        }

        private static double Divide(int firstNumber, int secondNumber)
        {
            double result;
            result = (double)firstNumber / secondNumber;

            return result;
        }

        private static int Multiple(int firstNumber, int secondNumber)
        {
            int result;
            result = firstNumber * secondNumber;

            return result;
        }


    }
}

