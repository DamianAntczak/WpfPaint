using PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ShadowEffectPlugin
{
    public class ShadowEffectPlugin : IPlugin
    {
        public Effect getEffect()
        {
            DropShadowEffect effect = new DropShadowEffect();
            effect.Direction = 5;
            effect.ShadowDepth = 25;
            effect.Color = Color.FromRgb(122, 122, 122);
            effect.Opacity = 1;
            return effect;
        }

        public Button getPluginButton()
        {
            Button button = new Button();
            button.Content = "Shadow";
            return button;
        }
    }
}
