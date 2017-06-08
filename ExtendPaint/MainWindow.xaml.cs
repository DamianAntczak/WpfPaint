using PluginInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
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
        enum ToolType { Line, Rect, Ellipse }

        ToolType currentTool = ToolType.Line;
        Point startPoint = new Point();
        //Point currentPoint = new Point();
        Shape currentShape = null;
        Brush currentBrush = Brushes.Black;
        MouseButtonState previousMouseEvent = new MouseButtonState();
        UndoRedoProvider undoRedo = new UndoRedoProvider();
        int currentBrushThickness = 1;


        public MainWindow()
        {
            InitializeComponent();
            CanvasInit();
            loadPlugins();
        }

        public void CanvasInit()
        {
            mainCanvas.Background = Brushes.White;
            sBrushThickness.Value = currentBrushThickness;
            rColor.Fill = currentBrush;
            //getCanvasBitmap();

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

                switch (currentTool)
                {
                    case ToolType.Line:
                        drawLine(e);
                        break;
                    case ToolType.Rect:
                        drawRect(e);
                        break;
                    case ToolType.Ellipse:
                        drawEllipse(e);
                        break;
                }
            }
            else if(e.LeftButton == MouseButtonState.Released && previousMouseEvent == MouseButtonState.Pressed)
            {
                DrawCommand command = new DrawCommand(currentShape, mainCanvas);
                undoRedo.InsertComand(command);
            }
            previousMouseEvent = e.LeftButton;
        }

        private void drawLine(MouseEventArgs e)
        {
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

        private void drawRect(MouseEventArgs e)
        {
            Rectangle rect = new Rectangle();
            rect.Width = Math.Abs(startPoint.X - e.GetPosition(mainCanvas).X);
            rect.Height = Math.Abs(startPoint.Y - e.GetPosition(mainCanvas).Y);
            rect.Stroke = currentBrush;
            rect.StrokeThickness = currentBrushThickness;

            if (cbFillShape.IsChecked == true)
            {
                rect.Fill = currentBrush;
            }

            if (startPoint.X - e.GetPosition(mainCanvas).X > 0)
            {
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

        private void drawEllipse(MouseEventArgs e)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = Math.Abs(startPoint.X - e.GetPosition(mainCanvas).X);
            ellipse.Height = Math.Abs(startPoint.Y - e.GetPosition(mainCanvas).Y);
            ellipse.Stroke = currentBrush;
            ellipse.StrokeThickness = currentBrushThickness;

            if (cbFillShape.IsChecked == true)
            {
                ellipse.Fill = currentBrush;
            }

            if (startPoint.X - e.GetPosition(mainCanvas).X > 0)
            {
                Canvas.SetLeft(ellipse, startPoint.X - ellipse.Width);
            }
            else
            {
                Canvas.SetLeft(ellipse, startPoint.X);
            }

            if (startPoint.Y - e.GetPosition(mainCanvas).Y > 0)
            {
                Canvas.SetTop(ellipse, startPoint.Y - ellipse.Height);
            }
            else
            {
                Canvas.SetTop(ellipse, startPoint.Y);
            }

            mainCanvas.Children.Add(ellipse);
            currentShape = ellipse;
        }

        private void mainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {

            DrawCommand command = new DrawCommand(currentShape, mainCanvas);
            undoRedo.InsertComand(command);
            
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
            SolidColorBrush colorBrush = (SolidColorBrush)currentBrush;
            
            sRed.Value = colorBrush.Color.R;
            sGreen.Value = colorBrush.Color.G;
            sBlue.Value = colorBrush.Color.B;
        }

        private void bUndo_Click(object sender, RoutedEventArgs e)
        {
            undoRedo.Undo(1);
        }

        private void bRedo_Click(object sender, RoutedEventArgs e)
        {
            undoRedo.Redo(1);
        }


        private void bLine_Click(object sender, RoutedEventArgs e)
        {
            currentTool = ToolType.Line;
        }

        private void bRect_Click(object sender, RoutedEventArgs e)
        {
            currentTool = ToolType.Rect;
        }

        private void getCanvasBitmap()
        {
            
        }

        private void SaveBitmap(Canvas canvas)
        {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
             (int)canvas.ActualWidth, (int)canvas.ActualHeight,
             96d, 96d, PixelFormats.Pbgra32);
            // needed otherwise the image output is black
            canvas.Measure(new Size((int)canvas.ActualWidth, (int)canvas.ActualHeight));
            canvas.Arrange(new Rect(new Size((int)canvas.ActualWidth, (int)canvas.ActualHeight)));

            renderBitmap.Render(canvas);



            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = "PNG (*.png)|*.png|JPEG (*.jpg)|*.jpg|All(*.*)|*",
                FileName = "picture",
                DefaultExt = "png"
            };
            var DialougeResult = dialog.ShowDialog();

            if (DialougeResult == true)
            {
                var extension = System.IO.Path.GetExtension(dialog.FileName);
                using (FileStream file = File.Create(dialog.FileName))
                {
                    BitmapEncoder encoder = null;
                    switch (extension.ToLower())
                    {
                        case ".jpg":
                            encoder = new JpegBitmapEncoder();
                            break;
                        case ".png":
                            encoder = new PngBitmapEncoder();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(extension);
                    }
                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                    encoder.Save(file);
                    InvalidateVisual();
                }
            }
          
        }
    

        private void testEffects_Click(object sender, RoutedEventArgs e)
        {
            //this.CreateBitmap(mainCanvas);
            DropShadowEffect effect = new DropShadowEffect();
            effect.Direction = 320;
            effect.ShadowDepth = 25;
            effect.Color = Color.FromRgb(122, 122, 122);
            effect.Opacity = 1;

            BlurEffect blur = new BlurEffect();
            blur.Radius = 15;


            EffectCommand command = new EffectCommand(blur, mainCanvas);
            undoRedo.InsertComand(command);
            command.Execute();
        }

        private void loadPlugins()
        {
            try
            {
                foreach (string dll in Directory.GetFiles("./plugins", "*.dll"))
                {
                    Assembly assembly = Assembly.LoadFrom(dll);
                    foreach (Type type in assembly.GetTypes())
                    {

                        toolBar.Items.Add(new Separator());
                        if (type.IsClass && type.IsPublic && typeof(IPlugin).IsAssignableFrom(type))
                        {
                            Object obj = Activator.CreateInstance(type);
                            IPlugin plugin = (IPlugin)obj;

                            Effect effect = plugin.getEffect();
                            EffectCommand command = new EffectCommand(effect, mainCanvas);

                            Button pluginButton = plugin.getPluginButton();
                            pluginButton.DataContext = command;
                            pluginButton.Click += pluginButton_Click;

                            toolBar.Items.Add(pluginButton);
                        }

                    }
                }
            }
            catch(DirectoryNotFoundException e)
            {
                MessageBox.Show("No plugins");
            }
        }

        private void pluginButton_Click(object sender, RoutedEventArgs e)
        {
            Button senderButton = sender as Button;
            EffectCommand command = senderButton.DataContext as EffectCommand;
            undoRedo.InsertComand(command);
            command.Execute();
        }

        private void saveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveBitmap(mainCanvas);
        }

        private void bElipse_Click(object sender, RoutedEventArgs e)
        {
            currentTool = ToolType.Ellipse;
        }

        private void bFill_Click(object sender, RoutedEventArgs e)
        {
            FillCommand command = new FillCommand(this.currentBrush, this.mainCanvas);
            undoRedo.InsertComand(command);
            command.Execute();
        }

        private void sColorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromRgb((byte)sRed.Value, (byte)sGreen.Value, (byte)sBlue.Value);
            currentBrush = brush;
            rColor.Fill = currentBrush;
        }
    }
}
