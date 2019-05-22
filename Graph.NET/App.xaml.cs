using ExtensionMethods;
using Graph.NET.Quadrature;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Graph.NET
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            Func<double, double> function = null;
            int funSelection = 0;
            bool isParsable = false;
            bool isBetween = false;
            do
            {
                Console.WriteLine("Wybierz wariant funkcji do całkowania:");
                Console.WriteLine("(1). f(x) = 4x^4 -2x^3 + 4x^2 -4x +1 ");
                Console.WriteLine("(2). f(x) = 4sin(2x) ");
                Console.WriteLine("(3). f(x) =2|x^3|");
                isParsable = Int32.TryParse(Console.ReadLine(), out funSelection);
                isBetween = funSelection.IsBetween(1, 3);

            } while( !(isParsable && isBetween) );

            switch (funSelection)
            {
                case 1:
                    function = Example.Polynomial;
                    break;
                case 2:
                    function = Example.Sinus;
                    break;
                case 3:
                    function = Example.Absolute;
                    break;
            }

            Console.Write("Nieskończone przedziały? (t/*)");
            bool isInifniteRange = Console.ReadKey().IsKey('t');
            Console.WriteLine();

            double a, b;

            if (isInifniteRange)
            {
                a = double.NegativeInfinity;
                b = double.PositiveInfinity;
            }
            else
            {
                bool isNumber = false;
                bool isCorrect = false;
                do
                {
                    Console.WriteLine("Określ przedziały całkowania:");
                    Console.Write("a:");
                    isNumber = double.TryParse(Console.ReadLine(), NumberStyles.Any ,CultureInfo.InvariantCulture, out a);
                    Console.Write("b:");
                    isNumber = double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out b);
                    isCorrect = a < b;
                } while (!isNumber || !isCorrect);
            }

            double epsilon = 0;
            bool isEpsilonCorrect = false;

            do
            {
                Console.Write("Określ dokładność obliczeń: ");
                isEpsilonCorrect = double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out epsilon);
            } while (!isEpsilonCorrect);
            
            if (double.IsInfinity(a) || double.IsInfinity(b))
            {
                a = Utilis.CalculateLimit(0, -2, Example.Exponent(function));
                b = Utilis.CalculateLimit(0, 2, Example.Exponent(function));
            }

            Newton_Cotes NewtonCotes;
            Gauss_Hermite GaussHermite;

            if (isInifniteRange)
            {
                NewtonCotes = new Newton_Cotes(Example.Exponent(function), a, b, epsilon);
                GaussHermite = new Gauss_Hermite(function, epsilon);
            }
            else
            {
                NewtonCotes = new Newton_Cotes(Example.Exponent(function), a, b, epsilon);
                GaussHermite = null;
            }

            Console.WriteLine("Całka obliczona przy użyciu metody Newtona-Cotesa wynosi: " + NewtonCotes.GetResult());
            Console.WriteLine("Liczba węzłów: " + NewtonCotes.GetIterationsCount());
            if (!(GaussHermite is null))
            {
                Console.WriteLine("Całka obliczona przy użyciu kwadratury Gaussa wynosi: " + GaussHermite.GetResult());
                Console.WriteLine("Liczba węzłów: " + GaussHermite.GetIterationsCount());
            }


            MainWindow mainWindow = new MainWindow();
            mainWindow.Plot.NewtonCotesValues = NewtonCotes.GetNodes();
            mainWindow.Plot.Values = NewtonCotes.GetValues();
            mainWindow.Show();
        }
    }
}
