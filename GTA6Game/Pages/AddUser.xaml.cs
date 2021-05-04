using GTA6Game.PlayerData;
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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : PageBase
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void BtnLoginPage_Click(object sender, RoutedEventArgs e)
        {
            Router.ChangeCurrentPage(new LoginPage());
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (TboxUsername.Text != "")
            {
                SaveLoader.Save.Profiles.AddProfile(new Profile(TboxUsername.Text));
            }
        }
    }
}
