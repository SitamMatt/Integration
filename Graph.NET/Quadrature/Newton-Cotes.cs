using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.NET.Quadrature
{
    public class Newton_Cotes : IntegralResult
    {
        private readonly double _IterationsCount;
        private readonly List<ChartValues<ObservablePoint>> _Nodes;
        private List<ObservablePoint> _Values;
        public Newton_Cotes(Func<double,double> function, double a, double b, double epsilon)
        {
            _Nodes = new List<ChartValues<ObservablePoint>>();
            _Values = new List<ObservablePoint>();
            int n = 2;
            var previousValue = Quadrature(function, a, b, n);
            n *= 2;
            var value = Quadrature(function, a, b, n);
            n *= 2;
            while(Math.Abs(previousValue - value) > epsilon)
            {
                previousValue = value;
                value = Quadrature(function, a, b, n);
                n *= 2;
            }
            _Result = value;
            _IterationsCount = n/2 + 1;
        }
        private double Quadrature(Func<double, double> function, double a, double b, int n)
        {
            List<ObservablePoint> points = new List<ObservablePoint>();
            double result = 0, x = 0, fx = 0;
            double h = (b - a) / n;
            for (int i = 0; i <= n; i++)
            {
                x = a + i * h;
                fx = function(x);
                var point = new ObservablePoint(x, fx);
                if (!_Values.Any(val => val.X == x))
                {
                    points.Add(point);
                }
                if (i == 0 || i == n)
                {
                    result += fx;
                }
                else if (i % 2 != 0)
                {
                    result += 4 * fx;
                }
                else
                {
                    result += 2 * fx;
                }
            }
            result *= (h / 3);
            _Values.AddRange(points);
            _Nodes.Add(new ChartValues<ObservablePoint>(points));
            return result;
        }
        public double GetIterationsCount()
        {
            return _IterationsCount;
        }
        public List<ChartValues<ObservablePoint>> GetNodes()
        {
            return _Nodes;
        }
        public ChartValues<ObservablePoint> GetValues()
        {
            var ordered = _Values.OrderBy(point => point.X);
            return new ChartValues<ObservablePoint>(ordered);
        }
    }
}
