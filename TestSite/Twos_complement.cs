using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSite
{
    internal static class TwosComplement
    {
        internal static void Calc(byte number)
        {
            var b = (byte)(unchecked(~number) + 0b1);
            Console.WriteLine($"\n{Convert.ToString(number, 2).PadLeft(8, '0').Insert(4, "_")}");

            Console.WriteLine(Convert.ToString(b, toBase: 2).PadLeft(8, '0').Insert(4, "_") + "  <<\n");
            Console.WriteLine(Convert.ToString((byte)(unchecked(~number) + 0b1), toBase: 2).PadLeft(8, '0').Insert(4, "_") + "  <<\n");
        }
    }
}
