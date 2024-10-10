using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi07_TinhToan3
{
    public class decimalNumber
    {
        public int Mantissa { get; }
        public int Exponent { get; }

        public decimalNumber(int mantissa, int exponent)
        {
            Mantissa = mantissa;
            Exponent = exponent;
        }

        public static decimalNumber operator *(decimalNumber a, decimalNumber b)
        {
            return new decimalNumber(a.Mantissa * b.Mantissa, a.Exponent + b.Exponent);
        }

        public override string ToString()
        {
            return $"{Mantissa}e{Exponent}";
        }
    }
}
