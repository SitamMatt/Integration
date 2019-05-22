using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.NET.Quadrature
{
    public static class Example
    {
        public static double Polynomial(double x)
        {
            double[] factor = { 4, -2, 4, -4, 1 };
            int n = factor.Length;
            double result = factor[0];
            for (int i = 1; i < n; i++)
            {
                result *= x;
                result += factor[i];
            }
            return result;
        }

        public static double Sinus(double x) => 4 * Math.Sin(2 * x);

        public static double Absolute(double x) => 2 * Math.Pow(Math.Abs(x), 3);

        public static Func<double,double> Exponent(Func<double,double> function)
        {
            double fun(double x)
            {
                var result = Math.Exp(-1*Math.Pow(x, 2)) * function(x);
                return result;
            }
            return fun;
        }
    }
}
