using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace ExtendPaint
{
    interface ICommand
    {
        void Execute();
        void UnExecute();
    }

    public class DrawCommand : ICommand
    {
        private Shape shape;
        private Canvas canvas;

        public DrawCommand(Shape shape, Canvas canvas)
        {
            this.shape = shape;
            this.canvas = canvas;
        }

        public void Execute()
        {
            canvas.Children.Add(this.shape);
        }

        public void UnExecute()
        {
            canvas.Children.Remove(this.shape);
        }
    }

    public class EffectCommand : ICommand
    {
        private Effect effect;
        private Canvas canvas;

        public EffectCommand(Effect effect, Canvas canvas)
        {
            this.canvas = canvas;
            this.effect = effect;
        }

        public void Execute()
        {
            foreach (Shape children in canvas.Children)
            {
                children.Effect = effect;
            }
        }

        public void UnExecute()
        {
            foreach (Shape children in canvas.Children)
            {
                children.Effect = null;
            }
        }
    }

    public class FillCommand : ICommand
    {
        private Brush previousBrush;
        private Brush currentBrush;
        private Canvas canvas;

        public FillCommand(Brush brush, Canvas canvas)
        {
            this.currentBrush = brush;
            this.canvas = canvas;
        }

        public void Execute()
        {

            previousBrush = canvas.Background;
            canvas.Background = currentBrush;
        }

        public void UnExecute()
        {
            canvas.Background = previousBrush;
        }
    }
}
