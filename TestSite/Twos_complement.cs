using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSite
{
    internal static class TwosComplement
    {
        internal static void Calc(sbyte number)
        {
            Console.WriteLine($"\n{PutUnderBar(Convert.ToString(number, 2).PadLeft(8, '0'))}");

            Console.WriteLine(PutUnderBar(Convert.ToString((sbyte)(unchecked(~number)), toBase: 2).PadLeft(8, '0')) + "  << 1의 보수\n");
            Console.WriteLine(PutUnderBar(Convert.ToString((sbyte)(unchecked(~number) + 0b1), toBase: 2).PadLeft(8, '0')) + "  << 2의 보수\n");
        }
        internal static void Calc(byte number)
        {
            Console.WriteLine($"\n{PutUnderBar(Convert.ToString(number, 2).PadLeft(8, '0'))}");

            Console.WriteLine(PutUnderBar(Convert.ToString((byte)(unchecked(~number)), toBase: 2).PadLeft(8, '0')) + "  << 1의 보수\n");
            Console.WriteLine(PutUnderBar(Convert.ToString((byte)(unchecked(~number) + 0b1), toBase: 2).PadLeft(8, '0')) + "  << 2의 보수\n");
        }

        private static string PutUnderBar(string number)
        {
            var temp = number;

            for (var i = 4; i < temp.Length; i += 4)
            {
                temp = temp.Insert(i, "_");
                i++;
            }

            return temp;
        }
    }
}
