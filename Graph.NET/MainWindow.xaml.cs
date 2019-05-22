using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Graph.NET
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.PrintScreen)
            {
                TakeScreenShot();
            }
        }

        public void TakeScreenShot()
        {
            var w = Application.Current.MainWindow.Width * 2;
            var h = Application.Current.MainWindow.Height * 2;

            var screen = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            var visual = new DrawingVisual();
            using (var context = visual.RenderOpen())
            {
                context.DrawRectangle(new VisualBrush(screen),
                                      null,
                                      new Rect(new System.Windows.Point(), new System.Windows.Size((int)screen.Width, (int)screen.Height)));
            }

            visual.Transform = new ScaleTransform(w / screen.ActualWidth, h / screen.ActualHeight);

            var rtb = new RenderTargetBitmap((int)w, (int)h, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(visual);

            var enc = new PngBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(rtb));

            var stm = new FileStream("ScreenShot.png", FileMode.OpenOrCreate);

            enc.Save(stm);
            stm.Close();
        }
    }
}
