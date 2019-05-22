using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.NET
{
    //class Quadrature
    //{

    //}

    public class menu
    {
        //public:
	public int wybor()
        {
            int choice;
            Console.WriteLine("Ktora z funkcji wybierasz : ");
            Console.WriteLine("(1). f(x) = 4x^4 -2x^3 + 4x^2 -4x +1 ");
            Console.WriteLine("(2). f(x) = 4sin(2x) ");
            Console.WriteLine("(3). f(x) =2|x^3|");

            choice = Convert.ToInt32(Console.ReadLine());
            while (choice < 1 && choice > 3)
            {
                Console.WriteLine("Teraz dasz rade : ");
                choice = Convert.ToInt32(Console.ReadLine()) - 48;
            }
            Console.WriteLine();

            return choice;
        }

        public int przedzialy()
        {
            int przedzialy;
            Console.WriteLine("ktory przedzial wybierasz ?(wpisz 1 lub 2 ) ");
            Console.WriteLine("(1). Od -inf do inf ");
            Console.WriteLine("(2). Wlasny ");
            przedzialy = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            return przedzialy;
        }

    };

    public class newton
    {
        public List<ChartValues<ObservablePoint>> values = new List<ChartValues<ObservablePoint>>();
        public ChartValues<ObservablePoint> points = new ChartValues<ObservablePoint>();
        public ChartValues<ObservablePoint> nodesPoints = new ChartValues<ObservablePoint>();
        //public:
        //double exphorner(double x, vector<double> poly)
        public double exphorner(double x, double[] poly)
        {
            double fx;
            fx = poly[0];
            for (int i = 1; i < (poly.Length); i++)
            {
                fx = fx * x + poly[i];
            }

            return fx * Math.Exp(-Math.Pow(x, 2));
        }
        public double expsinusa(double x)
        {
            return (4 * Math.Sin(2 * x)) * Math.Exp(-Math.Pow(x, 2));
        }
        public double expabsolut(double x)
        {
            double y = 2 * Math.Pow(Math.Abs(x), 3) * Math.Exp(-Math.Pow(x, 2));
            return y;
        }


        public double expcalkawielomian(double a, double b, double[] poly, int v)
        {
            var vals = new ChartValues<ObservablePoint>();
            //ofstream plik;
            //string filename("nodes" + std::to_string(v));
            //plik.open(filename);
            double result = 0, x = 0, fx = 0;
            double h = (b - a) / v;
            for (int i = 0; i <= v; i++)
            {
                x = a + i * h;
                fx = exphorner(x, poly);
                vals.Add(new ObservablePoint(x, fx));
                points.Add(new ObservablePoint(x, fx));
                //plik << x << " " << fx << endl;
                if (i == 0 || i == v)
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
            
            
            nodesPoints.AddRange(vals);
            var pointsToAdd = nodesPoints.GroupBy(y => y.X).Where(c=>c.Count()==1).Select(z => z.First()).OrderBy(w => w.X).ToList();


            values.Add(new ChartValues<ObservablePoint>(pointsToAdd));
            //plik.close();
            return result;
            /*double simp = 0, blad = 0, s = 0, x = 0;
            double h = (b - a) / v;
            for (int i = 1; i <= v; i++)
            {
                x = a + i * h;
                blad += exphorner(x - (h / 2), poly);
                if (i < v)
                {
                    s += (exphorner(x, poly));
                }
            }
            return (h / 6)*(exphorner(a, poly) + exphorner(b, poly) + 2 * s + 4 * blad);*/
        }
        public double expcalkasinus(double a, double b, int v)
        {
            var vals = new ChartValues<ObservablePoint>();

            //double simp = 0, blad = 0, s = 0, x = 0;
            //double h = (b - a) / v;
            //for (int i = 1; i <= v; i++)
            //{
            //    x = a + i * h;
            //    blad += expsinusa(x - (h / 2));
            //    if (i < v)
            //    {
            //        s += (expsinusa(x));
            //    }
            //}
            double result = 0, x = 0, fx = 0;
            double h = (b - a) / v;

            for (int i = 0; i <= v; i++)
            {
                x = a + i * h;
                fx = expsinusa(x);
                vals.Add(new ObservablePoint(x, fx));
                //plik << x << " " << fx << endl;
                if (i == 0 || i == v)
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
            values.Add(vals);

            //return (h / 6) * (expsinusa(a) + expsinusa(b) + 2 * s + 4 * blad);
            return result;

        }
        public double expcalkaabsolut(double a, double b, int v)
        {
            //double result = 0, x = 0, fx = 0;
            //double h = (b - a) / v;
            //for (int i = 0; i <= v; i++)
            //{
            //    x = a + i * h;
            //    fx = expabsolut(x);
            //    if (i == 0 || i == v)
            //    {
            //        result += fx;
            //    }
            //    else if (i % 2 != 0)
            //    {
            //        result += 4 * fx;
            //    }
            //    else
            //    {
            //        result += 2 * fx;
            //    }
            //}
            //result *= (h / 3);
            //return result;
            var vals = new ChartValues<ObservablePoint>();
            //ofstream plik;
            //string filename("nodes" + std::to_string(v));
            //plik.open(filename);
            double result = 0, x = 0, fx = 0;
            double h = (b - a) / v;
            for (int i = 0; i <= v; i++)
            {
                x = a + i * h;
                fx = expabsolut(x);
                vals.Add(new ObservablePoint(x, fx));
                //plik << x << " " << fx << endl;
                if (i == 0 || i == v)
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
            values.Add(vals);
            //plik.close();
            return result;
            /*double simp = 0, blad = 0, s = 0, x = 0;
            double h = (b - a) / v;
            for (int i = 1; i <= v; i++)
            {
                x = a + i * h;
                blad += expabsolut(x - (h / 2));
                if (i < v)
                {
                    s += (expabsolut(x));
                }
            }
            return (h / 6)*(expabsolut(a) + expabsolut(b) + 2 * s + 4 * blad);*/
        }


        public double check(double start, double[] poly, int choice)
        {
            double zero = 0;
            switch (choice)
            {
                case 1:
                    {
                        zero = exphorner(start, poly);
                        break;
                    }
                case 2:
                    {
                        zero = expsinusa(start);
                        break;
                    }
                case 3:
                    {
                        zero = expabsolut(start);
                        break;
                    }
            }
            return zero;
        }
    };

    public class kwadratura
    {
        

	public double exphorner(double x, double[] poly, double node)
        {
            double fx;
            fx = poly[0];
            for (int i = 1; i < (poly.Length); i++)
            {
                fx = fx * x + poly[i];
            }

            return fx * node;
        }
        public double expsinusa(double x, double node)
        {
            return (4 * Math.Sin(2 * x)) * node;
        }
        public double expabsolut(double x, double node)
        {
            return 2 * Math.Pow(Math.Abs(x), 3) * node;
        }


        public double calkawielomian(int n, double[] poly, double[] wezel, double[] herm)
        {
            double value = 0;
            for (int i = 0; i <= n; i++)
            {
                value = value + exphorner(wezel[i], poly, herm[i]);
            }

            return value;
        }
        public double calkasinus(int n, double[] x, double[] node)
        {
            double value = 0;
            for (int i = 0; i <= n; i++)
            {
                value = value + expsinusa(x[i], node[i]);
            }
            return value;

        }
        public double calkaabsolut(int n, double[] x, double[] node)
        {
            double value = 0;
            for (int i = 0; i <= n; i++)
            {
                value = value + expabsolut(x[i], node[i]);
            }
            return value;
        }


    };

    public class values : menu
    {

        public static double[] wezel1 = { -0.7071067811865475244008, 0.7071067811865475244008 };
        public static double[] wezel2 = { -1.224744871391589049099, 0, 1.224744871391589049099 };
        public static double[] wezel3 = { -1.650680123885784555883, -0.5246476232752903178841, 0.5246476232752903178841, 1.650680123885784555883 };
        public static double[] wezel4 = { -2.020182870456085632929, -0.9585724646138185071128, 0, 0.9585724646138185071128, 2.020182870456085632929 };
        public static List<double[]> wezel = new List<double[]>() { wezel1, wezel2, wezel3, wezel4 };

        public static double[] hermit1 = { 0.8862269254527580136491, 0.8862269254527580136491 };
        public static double[] hermit2 = { 0.295408975150919337883, 1.181635900603677351532, 0.295408975150919337883 };
        public static double[] hermit3 = { 0.081312835447245177143, 0.8049140900055128365061, 0.8049140900055128365061, 0.081312835447245177143 };
        public static double[] hermit4 = { 0.01995324205904591320774, 0.3936193231522411598285, 0.9453087204829418812257, 0.3936193231522411598285, 0.01995324205904591320774 };
        public List<double[]> hermit = new List<double[]>() { hermit1, hermit2, hermit3, hermit4 };
        public values()
        {
            choice = wybor();
            przedzial = przedzialy();
        }
        public int choice;
        public int przedzial;
        int repeat = 2;
        double step = 0.5, start = 1;
        double[] poly = { 4, -2, 4, -4, 1 };
        public newton cotes = new newton();
        public kwadratura gaussa = new kwadratura();
        double a, b, e, simp, prev_simp, gauss, prev_gauss, zero;
        public void dane()
        {
            if (przedzial == 2)
            {
                Console.WriteLine("Podaj przedzialy : ");
                a = Convert.ToInt32(Console.ReadLine());
                b = Convert.ToInt32(Console.ReadLine());
                while (a > b)
                {
                    Console.WriteLine("Podaj najpierw lewa strone przedzialu potem prawa : ");
                    a = Convert.ToInt32(Console.ReadLine());
                    b = Convert.ToInt32(Console.ReadLine());
                }
            }
            else
            {
                int i = 0;
                while (i < 1)
                {
                    zero = cotes.check(start, poly, choice);

                    if (Math.Abs(zero) < 0.000001)
                    {
                        i++;
                    }
                    start += step;
                }
                b = start;
                a = b * (-1);
            }
            Console.WriteLine("Podaj dokladnosc : ");
            e = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
        }


        public double wartosc_newton()
        {
            
            switch (choice)
            {
                case 1:
                    prev_simp = cotes.expcalkawielomian(a, b, poly, repeat);
                    repeat *= 2;
                    simp = cotes.expcalkawielomian(a, b, poly, repeat);
                    repeat *= 2;
                    while (Math.Abs(prev_simp - simp) > e)
                    {
                        prev_simp = simp;
                        simp = cotes.expcalkawielomian(a, b, poly, repeat);
                        repeat *= 2;
                    }
                    break;
                case 2:
                    prev_simp = cotes.expcalkasinus(a, b, repeat);
                    repeat *= 2;
                    simp = cotes.expcalkasinus(a, b, repeat);
                    repeat *= 2;
                    while (Math.Abs(prev_simp - simp) > e)
                    {
                        prev_simp = simp;
                        simp = cotes.expcalkasinus(a, b, repeat);
                        repeat *= 2;
                    }
                    break;
                case 3:
                    prev_simp = cotes.expcalkaabsolut(a, b, repeat);
                    repeat *= 2;
                    simp = cotes.expcalkaabsolut(a, b, repeat);
                    repeat *= 2;
                    while (Math.Abs(prev_simp - simp) > e)
                    {
                        prev_simp = simp;
                        simp = cotes.expcalkaabsolut(a, b, repeat);
                        repeat *= 2;
                    }
                    break;
            }

            Console.WriteLine(" liczba iteracjii dla newtona : " + repeat);
            return simp;
        }

        public double wartosc_gauss()
        {
            repeat = 3;
            switch (choice)
            {
                case 1:
                    prev_gauss = gaussa.calkawielomian(1, poly, wezel[0], hermit[0]);
                    gauss = gaussa.calkawielomian(2, poly, wezel[1], hermit[1]);
                    while (Math.Abs(gauss - prev_gauss) > e && repeat < 5)
                    {
                        prev_gauss = gauss;
                        gauss = gaussa.calkawielomian(repeat, poly, wezel[repeat - 1], hermit[repeat - 1]);
                        repeat++;
                    }

                    break;
                case 2:
                    prev_gauss = gaussa.calkasinus(1, wezel[0], hermit[0]);
                    gauss = gaussa.calkasinus(2, wezel[1], hermit[1]);
                    while (Math.Abs(gauss - prev_gauss) > e && repeat < 5)
                    {
                        prev_gauss = gauss;
                        gauss = gaussa.calkasinus(repeat, wezel[repeat - 1], hermit[repeat - 1]);
                        repeat++;
                    }

                    break;

                case 3:
                    prev_gauss = gaussa.calkaabsolut(1, wezel[0], hermit[0]);
                    gauss = gaussa.calkaabsolut(2, wezel[1], hermit[1]);
                    while (Math.Abs(gauss - prev_gauss) > e && repeat < 5)
                    {
                        prev_gauss = gauss;
                        gauss = gaussa.calkaabsolut(repeat, wezel[repeat - 1], hermit[repeat - 1]);
                        repeat++;
                    }

                    break;
            }

            Console.WriteLine(" liczba iteracjii dla gaussa : " + repeat);
            return gauss;


        }

    };

}


