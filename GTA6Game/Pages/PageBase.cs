using GTA6Game.Routing;
using GTA6Game.UserControls.Overlay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GTA6Game.Pages
{
    /// <summary>
    /// The base class for all pages. Can use the router (RoutingHelper) to navigate to another page.
    /// </summary>
    public class PageBase : Page
    {
        /// <summary>
        /// The Router object that will get injected to the page by the Router when the page is navigated to
        /// </summary>
        public RoutingHelper Router { get; set; }

        public OverlaySettings OverlaySettings { get; set; }

        /// <summary>
        /// Method that will get called when the page is shown
        /// </summary>
        public virtual void OnAttachedToFrame()
        {

        }

        /// <summary>
        /// Reload functionality
        /// </summary>
        public virtual void Reload()
        {

        }
    }
}
