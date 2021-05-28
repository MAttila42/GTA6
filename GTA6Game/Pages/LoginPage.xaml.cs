using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GTA6Game.Languages;
using GTA6Game.PlayerData;
using GTA6Game.UserControls;
using GTA6Game.UserControls.Messages;
using GTA6Game.UserControls.Overlay.Modal;

namespace GTA6Game.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : PageBase
    {
        public int SelectedUserIndex = -1;

        private Profile SelectedProfile = null;

        public LoginPage()
        {
            InitializeComponent();
            RefreshUsersList();
            BtnLogIn.IsEnabled = false;
            BtnModifyUser.IsEnabled = false;
        }

        private void RefreshUsersList()
        {
            WpUserIconContainer.Children.Clear();
            foreach (var i in SaveLoader.Save.Profiles)
            {
                UserIcon newUser = new UserIcon();
                newUser.Content = i.Name;
                newUser.Style = FindResource("UserIconStyle") as Style;
                newUser.ImageSource = new BitmapImage(new Uri("/Assets/User.png", UriKind.Relative));
                newUser.Click += new RoutedEventHandler(UserIcon_Click);
                WpUserIconContainer.Children.Add(newUser);
            }
        }

        private void UserIcon_Click(object sender, RoutedEventArgs e)
        {
            BtnLogIn.IsEnabled = true;
            BtnModifyUser.IsEnabled = true;
            Button selectedUser = (Button)sender;

            foreach (var i in SaveLoader.Save.Profiles.Where(x => x.Name == (string)selectedUser.Content))
            {
                SelectedProfile = i;
            }

            SelectedUserIndex = WpUserIconContainer.Children.IndexOf(selectedUser);
            for (int i = 0; i < WpUserIconContainer.Children.Count; i++)
            {
                Button userIcon = (Button)WpUserIconContainer.Children[i];
                if (i != SelectedUserIndex)
                {
                    userIcon.IsEnabled = true;
                }
                else
                {
                    userIcon.IsEnabled = false;
                }
            }
        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (TboxPassword.Password == SelectedProfile?.Password && CbRobot.IsChecked == true)
            {
                SaveLoader.Save.SelectedProfile = SelectedProfile;
                Router.StartGame = true;
                Router.ChangeCurrentPage(new LoadingPage());
            }
            else
            {
                Modal<object> modal = new Modal<object>(new MessageOk($"{(LanguageManager.CurrentCulture.IetfLanguageTag == "hu-HU" ? "Helytelen jelszót adtál meg, vagy lehet, hogy robot vagy!" : "Password is incorrect or you are a robot!")}", "Social Club Error"));
                OverlaySettings.OpenedModals.OpenModal(modal);
            }           
        }

        private void BtnRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            if (TboxPassword.Password == SelectedProfile?.Password)
            {
                Router.ChangeCurrentPage(new ModifyUser(SelectedProfile));
            }
            else
            {
                Modal<object> modal = new Modal<object>(new MessageOk($"{(LanguageManager.CurrentCulture.IetfLanguageTag == "hu-HU" ? "Helytelen jelszót adtál meg!" : "Password is incorrect!")}", "Social Club Error"));
                OverlaySettings.OpenedModals.OpenModal(modal);
            }
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            Router.ChangeCurrentPage(new AddUser());
        }
    }
}