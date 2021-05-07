using GTA6Game.Languages;
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
            var ifContains = SaveLoader.Save.Profiles.Where(x => x.Name.Contains(TboxUsername.Text));

            if (TboxUsername.Text != "" && TboxPassword.Text == TboxPasswordCheck.Text && ifContains.Count() == 0)
            {
                SaveLoader.Save.Profiles.AddProfile(new Profile(TboxUsername.Text, TboxPassword.Text));
                Router.ChangeCurrentPage(new LoginPage());
            }
            else
            {
                string msgboxTitle;
                string msgboxText;

                if (LanguageManager.CurrentCulture.IetfLanguageTag == "hu-HU" )// angolnál en-US
                {
                    msgboxTitle = "Hiba";
                    msgboxText = "Sajna-bajna, hibás a felhasználónév vagy a jelszavak nem egyeznek!";
                }
                else
                {
                    msgboxTitle = "english";
                    msgboxText = "english";
                }

                MessageBox.Show(msgboxText, msgboxTitle, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }
    }
}
