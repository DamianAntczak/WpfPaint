using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace PluginInterface
{
    public interface IPlugin
    {
        Effect getEffect();

        Button getPluginButton();
    }
}
