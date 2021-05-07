<<<<<<< Sziszi
﻿using GTA6Game.Pages;
using GTA6Game.PlayerData;
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
        /// <summary>
        /// The currently shown page
        /// </summary>
        public PageBase CurrentPage { get; private set; }

        /// <summary>
        /// Gets invoked when we navigate to another page
        /// </summary>
        public event Action CurrentPageChanged;

        /// <summary>
        /// The page container. This frame's child element will be the current page
        /// </summary>
        private Frame container;

        public bool StartGame { get; set; }
        public Profile SelectedUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame">The frame that will show the pages</param>
        public RoutingHelper(Frame frame)
        {
            container = frame;
            container.Navigated += OnFrameNavigation;
            StartGame = false;
        }

        private void OnFrameNavigation(object sender, NavigationEventArgs e)
        {
            container.NavigationService.RemoveBackEntry();
        }

        /// <summary>
        /// Navigates to the given page
        /// </summary>
        /// <param name="page">The page that we want to navigate to</param>
        public void ChangeCurrentPage(PageBase page)
        {
            page.Router = this;
            CurrentPage = page;
            container.Content = page;
            page.OnAttachedToFrame();
            CurrentPageChanged?.Invoke();
        }

        /// <summary>
        /// Reloads the current page by calling its Reload method
        /// </summary>
        public void ReloadPage()
        {
            if (CurrentPage == null)
            {
                return;
            }
            CurrentPage.Reload();
        }


    }
}
=======
﻿using GTA6Game.Pages;
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
        /// <summary>
        /// The currently shown page
        /// </summary>
        public PageBase CurrentPage { get; private set; }

        /// <summary>
        /// Gets invoked when we navigate to another page
        /// </summary>
        public event Action CurrentPageChanged;

        /// <summary>
        /// The page container. This frame's child element will be the current page
        /// </summary>
        private Frame container;

        public bool StartGame { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame">The frame that will show the pages</param>
        public RoutingHelper(Frame frame)
        {
            container = frame;
            container.Navigated += OnFrameNavigation;
            StartGame = false;
        }

        private void OnFrameNavigation(object sender, NavigationEventArgs e)
        {
            container.NavigationService.RemoveBackEntry();
        }

        /// <summary>
        /// Navigates to the given page
        /// </summary>
        /// <param name="page">The page that we want to navigate to</param>
        public void ChangeCurrentPage(PageBase page)
        {
            page.Router = this;
            CurrentPage = page;
            container.Content = page;
            page.OnAttachedToFrame();
            CurrentPageChanged?.Invoke();
        }

        /// <summary>
        /// Reloads the current page by calling its Reload method
        /// </summary>
        public void ReloadPage()
        {
            if (CurrentPage == null)
            {
                return;
            }
            CurrentPage.Reload();
        }


    }
}
>>>>>>> master
