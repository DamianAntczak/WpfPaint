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
        Point startPoint = new Point();
        //Point currentPoint = new Point();
        Shape currentShape = null;
        Brush currentBrush = Brushes.Black;
        int currentBrushThickness = 1;
        bool isLine = false;


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
                currentShape = new Line();
                startPoint = e.GetPosition(mainCanvas);
        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                mainCanvas.Children.Remove(currentShape);

                if (isLine) {
                    Line line = new Line();
                    line.Stroke = currentBrush;
                    line.StrokeThickness = currentBrushThickness;
                    line.StrokeStartLineCap = PenLineCap.Round;
                    line.StrokeStartLineCap = PenLineCap.Round;
                    line.X1 = startPoint.X;
                    line.Y1 = startPoint.Y;
                    line.X2 = e.GetPosition(mainCanvas).X;
                    line.Y2 = e.GetPosition(mainCanvas).Y;

                    mainCanvas.Children.Add(line);
                    currentShape = line;
                }
                else
                {
                    Rectangle rect = new Rectangle();
                    rect.Width = Math.Abs(startPoint.X - e.GetPosition(mainCanvas).X);
                    rect.Height = Math.Abs(startPoint.Y - e.GetPosition(mainCanvas).Y);
                    rect.Stroke = currentBrush;
                    rect.StrokeThickness = currentBrushThickness;

                    if(startPoint.X - e.GetPosition(mainCanvas).X > 0) {
                        Canvas.SetLeft(rect, startPoint.X - rect.Width);
                    }
                    else
                    {
                        Canvas.SetLeft(rect, startPoint.X);
                    }

                    if (startPoint.Y - e.GetPosition(mainCanvas).Y > 0)
                    {
                        Canvas.SetTop(rect, startPoint.Y - rect.Height);
                    }
                    else
                    {
                        Canvas.SetTop(rect, startPoint.Y);
                    }

                    mainCanvas.Children.Add(rect);
                    currentShape = rect;
                }
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
