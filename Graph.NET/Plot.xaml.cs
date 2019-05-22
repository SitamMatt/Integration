using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graph.NET
{
    /// <summary>
    /// Logika interakcji dla klasy Plot.xaml
    /// </summary>
    public partial class Plot : UserControl
    {
        public Plot()
        {
            //Plotter = new CartesianChart();
            Values = new ChartValues<ObservablePoint>();
            //for (double i = 2; i <= 8; i+=0.5)
            //{
            //    var y = Fun(i);
            //    var point = new ObservablePoint(i, y);
            //    Values.Add(point);
            //}
            InitializeComponent();
            DataContext = this;
            ////Plotter.AxisY[0].MinValue = 0;
            //Plotter.AxisY.Clear();
            //Plotter.AxisY.Add(
            //new Axis
            //{
            //    Separator = new LiveCharts.Wpf.Separator
            //    {
            //        StrokeThickness = 1,
            //    }
            //});
            //Plotter.Update();
        }

        public ChartValues<ObservablePoint> Values { get; set; }

        private List<ChartValues<ObservablePoint>> _NewtonCotesValues;
        public List<ChartValues<ObservablePoint>> NewtonCotesValues
        {
            get { return _NewtonCotesValues; }
            set
            {
                _NewtonCotesValues = value;
                int counter = 1;
                string tit = "Iteracja: ";
                foreach (var item in NewtonCotesValues)
                {
                    var series = new StackedColumnSeries
                    {
                        MaxColumnWidth = 3,
                        Fill = Brushes.Transparent,
                        Stroke = Brushes.Black,
                        Title = tit + counter++,
                        StrokeThickness = 1,
                        Values = item
                    };
                    Plotter.Series.Add(series);
                }
                //Plotter.Update();
            }
        }
        public double Fun(double x)
        {
            return Math.Pow(Math.E, x);
        }
    }
}
