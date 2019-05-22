using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.NET.Quadrature
{
    class Gauss_Hermite : IntegralResult
    {
        private readonly double _IterationsCount;
        public Gauss_Hermite(Func<double, double> function, double epsilon)
        {
            int n = 2;
            var previousValue = Quadrature(function, n++);
            var value = Quadrature(function, n++);
            while (Math.Abs(value - previousValue) > epsilon && n <= 5)
            {
                previousValue = value;
                value = Quadrature(function, n++);
            }
            _Result = value;
            _IterationsCount = n-1;
        }
        private double Quadrature(Func<double, double> function, int n)
        {
            double result = 0;
            for (int i = 0; i < n; i++)
            {
                double A = HermiteFactors[n - 2][i];
                double x = Nodes[n - 2][i];
                double fx = function(x);
                result +=  A*fx;
            }
            return result;
        }
        private readonly List<double[]> HermiteFactors = new List<double[]>()
        {
            new double[]{ 0.8862269254527580136491, 0.8862269254527580136491 },
            new double[]{ 0.295408975150919337883, 1.181635900603677351532, 0.295408975150919337883 },
            new double[]{ 0.081312835447245177143, 0.8049140900055128365061, 0.8049140900055128365061, 0.081312835447245177143 },
            new double[]{ 0.01995324205904591320774, 0.3936193231522411598285, 0.9453087204829418812257, 0.3936193231522411598285, 0.01995324205904591320774 }
        };
        private readonly List<double[]> Nodes = new List<double[]>()
        {
            new double[]{ -0.7071067811865475244008, 0.7071067811865475244008 },
            new double[]{ -1.224744871391589049099, 0, 1.224744871391589049099 },
            new double[]{ -1.650680123885784555883, -0.5246476232752903178841, 0.5246476232752903178841, 1.650680123885784555883 },
            new double[]{ -2.020182870456085632929, -0.9585724646138185071128, 0, 0.9585724646138185071128, 2.020182870456085632929 }
        };

        public double GetIterationsCount()
        {
            return _IterationsCount;
        }

       
    }
}
