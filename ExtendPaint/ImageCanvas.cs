using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ExtendPaint
{
    class ImageCanvas : Canvas
    {

        protected override void OnRender(DrawingContext dc)
        {
            BitmapImage img = new BitmapImage(new Uri("c:\\avatar-man.png"));
            dc.DrawImage(img, new Rect(0, 0, img.PixelWidth, img.PixelHeight));
        }
    }
}
