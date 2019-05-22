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
using Wolfram.Alpha;
using Wolfram.Alpha.Models;

namespace Graph.NET
{
    /// <summary>
    /// Logika interakcji dla klasy WolframResults.xaml
    /// </summary>
    public partial class WolframResults : UserControl
    {
        public WolframResults()
        {
            InitializeComponent();
            Ask();
        }

        public async void Ask()
        {
            WolframAlphaService service = new WolframAlphaService("5YAGW3-LXEQ4H8YQJ");
            WolframAlphaRequest request = new WolframAlphaRequest("Integrate e^(-x^2)2|x^3| from 2 to 5");
            WolframAlphaResult result = await service.Compute(request);
            StringBuilder strb = new StringBuilder();

            if (result.QueryResult.Pods != null)
            {
                var WolframImage = result.QueryResult.Pods
                    .Where(x => x.Title == "Visual representation of the integral").First()
                    .SubPods.First()
                    .Image;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(WolframImage.Src, UriKind.Absolute);
                bitmap.EndInit();
                
                VisualRepresentation.Width = WolframImage.Width;
                VisualRepresentation.Source = bitmap;
            }
        }
    }
}
