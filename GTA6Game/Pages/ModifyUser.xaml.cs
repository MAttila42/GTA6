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
using GTA6Game.Pages;

namespace GTA6Game.Pages
{
    /// <summary>
    /// Interaction logic for ModifyUser.xaml
    /// </summary>
    public partial class ModifyUser : PageBase
    {
        public ModifyUser()
        {
            InitializeComponent();
        }

        private void BtnLoginPage_Click(object sender, RoutedEventArgs e)
        {
            Router.ChangeCurrentPage(new LoginPage());
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            SaveLoader.Save.Profiles.RemoveProfile(Router.SelectedUser);
            Router.ChangeCurrentPage(new LoginPage());
        }

        private void BtnModify_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
