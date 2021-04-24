using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace GTA6Game.Routing
{
    public class RoutingHelper
    {
        public PageBase CurrentPage { get; private set; }

        public event Action CurrentPageChanged;

        private Frame container;


        public RoutingHelper(Frame frame)
        {
            container = frame;
            container.Navigated += OnFrameNavigation;
        }

        private void OnFrameNavigation(object sender, NavigationEventArgs e)
        {
            container.NavigationService.RemoveBackEntry();
        }

        public void ChangeCurrentPage(PageBase page)
        {
            page.Router = this;
            CurrentPage = page;
            container.Content = page;
            page.OnAttachedToFrame();
            CurrentPageChanged?.Invoke();
        }

        public void ReloadPage()
        {
            if (CurrentPage ==null)
            {
                return;
            }
            CurrentPage.OnAttachedToFrame();
        }


    }
}
