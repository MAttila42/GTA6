using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TeslaCarConfigurator.Data;

namespace TeslaCarConfigurator.Helpers
{
    public class RoutingHelper
    {
        public PageBase CurrentPage { get; private set; }

        public bool HasConfig => carConfiguration != null;

        public event Action CurrentPageChanged;

        private Frame container;
        private CarConfiguration carConfiguration;
        private MessageBarController messageBarController;


        public RoutingHelper(Frame frame, MessageBarController messageBarController)
        {
            container = frame;
            container.Navigated += OnFrameNavigation;
            this.messageBarController = messageBarController;
        }

        private void OnFrameNavigation(object sender, NavigationEventArgs e)
        {
            container.NavigationService.RemoveBackEntry();
        }

        public void ChangeCurrentPage(PageBase page)
        {
            page.Router = this;
            page.Config = carConfiguration;
            page.MessageBarController = messageBarController;
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
            CurrentPage.Config = carConfiguration;
            CurrentPage.OnAttachedToFrame();
        }

        public void SetConfig(CarConfiguration carConfiguration)
        {
            this.carConfiguration = carConfiguration;

        }

    }
}
