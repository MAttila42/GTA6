using GTA6Game.PlayerData;
using System;
using System.Linq;
using System.Windows;

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

        string[] ErrorMessages = new string[] { "a jelszavak nem egyeznek!", "ilyen felhasználónév már létezik!", "nem adtál meg minden adatot!", "18 éven aluliak nem használhatják a játékot!" };
        enum ErrorType { password, username, datas, birth, none, check }

        ErrorType CurrentError = ErrorType.datas;

        string OriginalName;

        private void BtnLoginPage_Click(object sender, RoutedEventArgs e)
        {
            Router.ChangeCurrentPage(new LoginPage());
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Biztosan törölni akarod a felhasználót? Ha törlöd, az összes pénzed elveszik!", "Törlés megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    SaveLoader.Save.Profiles.RemoveProfile(SaveLoader.Save.SelectedProfile);
                    Router.ChangeCurrentPage(new LoginPage());
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void BtnModify_Click(object sender, RoutedEventArgs e)
        {
            ErrorIdentifier();
            if (CurrentError == ErrorType.none)
            {
                if (CbEULA.IsChecked == true)
                {
                    SaveLoader.Save.SelectedProfile.Name = TboxUsername.Text;
                    SaveLoader.Save.SelectedProfile.Password = TboxPassword.Password;
                    SaveLoader.Save.SelectedProfile.DateOfBirth = DpBirth.Text;
                    Router.ChangeCurrentPage(new LoginPage());
                }
                else
                {
                    MessageBox.Show($"Sajna-bajna, az ÁSZF elfogadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }

            }
            else if (CurrentError != ErrorType.check)
            {
                MessageBox.Show($"Sajna-bajna, {ErrorWriter()}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            TboxUsername.Text = SaveLoader.Save.SelectedProfile.Name;
            TboxPassword.Password = SaveLoader.Save.SelectedProfile.Password;
            TboxPasswordCheck.Password = SaveLoader.Save.SelectedProfile.Password;
            TboxPasswordCheck.Password = SaveLoader.Save.SelectedProfile.Password;
            DpBirth.Text = SaveLoader.Save.SelectedProfile.DateOfBirth;
            CbLanguage.SelectedItem = CbLanguage.Items[0];
            CbAvatar.SelectedItem = CbAvatar.Items[0];
            OriginalName = SaveLoader.Save.SelectedProfile.Name;
        }

        private void ErrorIdentifier()
        {
            

            if (string.IsNullOrEmpty(TboxUsername.Text) || DpBirth.SelectedDate == null || CbLanguage.SelectedItem == null || CbAvatar.SelectedItem == null)
            {
                CurrentError = ErrorType.datas;
            }
            else
            {
                if (TboxUsername.Text == OriginalName)
                {
                    ErrorIdentifierTwo();
                }
                else
                {
                    var ifContains = SaveLoader.Save.Profiles.Where(x => x.Name == TboxUsername.Text);

                    if (ifContains.Count() != 0)
                    {
                        CurrentError = ErrorType.username;
                    }
                    else
                    {
                        ErrorIdentifierTwo();
                    }
                }
                
            }
        }

        private string ErrorWriter()
        {
            string errorResult = "";

            switch (CurrentError)
            {
                case ErrorType.password:
                    errorResult = ErrorMessages[0];
                    break;
                case ErrorType.username:
                    errorResult = ErrorMessages[1];
                    break;
                case ErrorType.datas:
                    errorResult = ErrorMessages[2];
                    break;
                case ErrorType.birth:
                    errorResult = ErrorMessages[3];
                    break;
            }
            return errorResult;
        }

        private void ErrorIdentifierTwo()
        {
            var limitDate = DateTime.Now.AddYears(-18).Date;
            if (DpBirth.SelectedDate > limitDate)
            {
                CurrentError = ErrorType.birth;
            }
            else
            {
                if (TboxPassword.Password == TboxPasswordCheck.Password)
                {
                    if (string.IsNullOrEmpty(TboxPassword.Password))
                    {
                        MessageBoxResult result = MessageBox.Show($"Nincs beállítva jelszó! Beállítasz egyet mégis?", "Jelszó kérdés", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                CurrentError = ErrorType.check;
                                break;
                            case MessageBoxResult.No:
                                CurrentError = ErrorType.none;
                                break;
                        }
                    }
                    else
                    {
                        CurrentError = ErrorType.none;
                    }
                }
                else
                {
                    CurrentError = ErrorType.password;
                }
            }
        }
    }
}
