using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GTA6Game.Routing
{
    public class PageBase : Page
    {
        public RoutingHelper Router { get; set; }

        public virtual void OnAttachedToFrame()
        {

        }
    }
}
