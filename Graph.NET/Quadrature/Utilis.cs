using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.NET.Quadrature
{
    static class Utilis
    {
        public static double CalculateLimit(double from, double step, Func<double, double> function)
        {
            double part = from;
            part += step;
            double value = function(part);
            while (Math.Abs(value) > 0.000001)
            {
                part += step;
                value = function(part);
            }
            return part;
        }
    }
}
