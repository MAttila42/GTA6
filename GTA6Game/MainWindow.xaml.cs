using GTA6Game.Pages;
using GTA6Game.PlayerData;
using GTA6Game.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GTA6Game
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The router of the application. 
        /// </summary>
        public RoutingHelper Router;

        public MainWindow()
        {
            InitializeComponent();
            Router.StartGame = false;
        }

        private void Windows_Initialized(object sender, EventArgs e)
        {
            Router = new RoutingHelper(PageContainer);
            Router.CurrentPageChanged += OnCurrentPageChanged;
            Router.ChangeCurrentPage(new MinigameSelectionPage());
        }

        private void OnCurrentPageChanged()
        {

        }

        private void Windows_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Windows.Title = $"{Windows.ActualHeight} * {Windows.ActualWidth} sz, m";
        }

        public void BtnSwitchPage_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string pageName = btn.Name.Replace("Btn", "");
            ChangePage(pageName);
        }

        private void ChangePage(string pageName)
        {
            Type pageType = Type.GetType($"{nameof(GTA6Game)}.{pageName}") ?? Type.GetType($"{nameof(GTA6Game)}.{nameof(Pages)}.{pageName}");
            if (pageType != null)
            {
                PageBase page = (PageBase)Activator.CreateInstance(pageType);
                Router.ChangeCurrentPage(page);
            }
        }
    }
}
