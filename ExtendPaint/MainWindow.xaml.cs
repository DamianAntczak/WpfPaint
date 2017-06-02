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

namespace ExtendPaint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point currentPoint = new Point();
        Brush currentBrush = Brushes.Black;
        int currentBrushThickness = 1;


        public MainWindow()
        {
            InitializeComponent();
            CanvasInit();
        }

        public void CanvasInit()
        {
            mainCanvas.Background = Brushes.LightBlue;
            sBrushThickness.Value = currentBrushThickness;
            rColor.Fill = currentBrush;

        }

        private void mainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                currentPoint = e.GetPosition(mainCanvas);
        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                Line line = new Line();

                line.Stroke = currentBrush;
                line.StrokeThickness = currentBrushThickness;
                line.StrokeStartLineCap = PenLineCap.Round;
                line.StrokeStartLineCap = PenLineCap.Round;
                line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = e.GetPosition(mainCanvas).X;
                line.Y2 = e.GetPosition(mainCanvas).Y;

                currentPoint = e.GetPosition(mainCanvas);

                mainCanvas.Children.Add(line);
            }
        }

        private void sBrushThickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            currentBrushThickness = (int)e.NewValue;
        }

        private void bColor_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            currentBrush = button.Background.Clone();
            rColor.Fill = currentBrush;
        }
    }
}
