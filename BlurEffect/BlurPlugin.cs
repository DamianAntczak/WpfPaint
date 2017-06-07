using PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace BlurEffectPlugin
{
    public class BlurPlugin : IPlugin
    {
        public Effect getEffect()
        {
            BlurEffect blur = new BlurEffect();
            blur.Radius = 15;
            return blur;
        } 

        public Button getPluginButton()
        {
            Button button = new Button();
            button.Content = "Blur";
            return button;
        }
    }
}
