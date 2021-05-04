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

namespace GTA6Game.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : PageBase
    {

        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            Router.ChangeCurrentPage(new AddUser());
        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
