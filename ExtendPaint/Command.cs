using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
}
