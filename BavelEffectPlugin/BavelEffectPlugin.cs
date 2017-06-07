using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;

namespace BavelEffectPlugin
{
    public class BavelEffectPlugin
    {
        public Effect getEffect()
        {
            BevelBitmapEffect myBevelEffect = new BevelBitmapEffect();
            myBevelEffect.BevelWidth = 15;
            myBevelEffect.EdgeProfile = EdgeProfile.CurvedIn;
            myBevelEffect.LightAngle = 320;
            myBevelEffect.Relief = 0.4;
            myBevelEffect.Smoothness = 0.4;
            return myBevelEffect;
        }

        public Button getPluginButton()
        {
            Button button = new Button();
            button.Content = "Blur";
            return button;
        }
    }
}
