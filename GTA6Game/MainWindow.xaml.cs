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
        private RoutingHelper Router;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Router = new RoutingHelper(PageContainer);
            Router.CurrentPageChanged += OnCurrentPageChanged;
            Router.ChangeCurrentPage(new StartingPage());
        }

        private void OnCurrentPageChanged()
        {

        }
    }
}
