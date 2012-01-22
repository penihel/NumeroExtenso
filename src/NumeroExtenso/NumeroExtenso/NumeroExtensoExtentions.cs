using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumeroExtenso
{
    public static class NumeroExtensoExtentions
    {
        public static string ToExtenso(this decimal v)
        {
            Numero numero = new Numero(v);

            return numero.ToString();
        }

        public static string ToExtenso(this double v)
        {
            Numero numero = new Numero( (decimal) v);

            return numero.ToString();
        }

        public static string ToExtenso(this float v)
        {
            Numero numero = new Numero( (decimal) v);

            return numero.ToString();
        }
    }
}
