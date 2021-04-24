using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TeslaCarConfigurator.Data;

namespace TeslaCarConfigurator.Helpers
{
    public class PageBase : Page
    {
        public RoutingHelper Router { get; set; }

        public CarConfiguration Config { get; set; }

        public MessageBarController MessageBarController { get; set; }

        public virtual void OnAttachedToFrame()
        {

        }
    }
}
