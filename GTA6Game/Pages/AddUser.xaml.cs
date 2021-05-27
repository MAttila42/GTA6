using GTA6Game.PlayerData;
using GTA6Game.UserControls.Messages;
using GTA6Game.UserControls.Overlay.Modal;
using System;
using System.Linq;
using System.Windows;

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

        string[] ErrorMessages = new string[] { "a jelszavak nem egyeznek!", "ilyen felhasználónév már létezik!", "nem adtál meg minden adatot!", "18 éven aluliak nem használhatják a játékot!" };
        enum ErrorType { password, username, datas, birth, none, check }

        ErrorType CurrentError = ErrorType.datas;

        private void BtnLoginPage_Click(object sender, RoutedEventArgs e)
        {
            Router.ChangeCurrentPage(new LoginPage());
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            ErrorIdentifier();
            if (CurrentError == ErrorType.none)
            {
                if (CbEULA.IsChecked == true)
                {
                    SaveLoader.Save.Profiles.AddProfile(new Profile(TboxUsername.Text, TboxPassword.Password, DpBirth.Text));
                    Router.ChangeCurrentPage(new LoginPage());
                }
                else
                {
                    Modal<object> modal = new Modal<object>(new MessageOk("Sajna-bajna, az ÁSZF elfogadása kötelező!", "Social Club Error"));
                    OverlaySettings.OpenedModals.OpenModal(modal);
                }
                
            }
            else if(CurrentError != ErrorType.check)
            {
                Modal<object> modal = new Modal<object>(new MessageOk($"Sajna-bajna, {ErrorWriter()}", "Social Club Error"));
                OverlaySettings.OpenedModals.OpenModal(modal);
            }
        }

        private async void ErrorIdentifier()
        {
            var limitDate = DateTime.Now.AddYears(-18).Date;

            if (string.IsNullOrEmpty(TboxUsername.Text) || DpBirth.SelectedDate == null || CbLanguage.SelectedItem == null || CbAvatar.SelectedItem == null)
            {
                CurrentError = ErrorType.datas;
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
                                Modal<object> modal = new Modal<object>(new MessageYesNo($"Nincs beállítva jelszó! Beállítasz egyet mégis?", "Alert"));
                                var modalResult = await OverlaySettings.OpenedModals.OpenModal(modal);

                                switch (modalResult.Payload)
                                {
                                    case true:
                                        CurrentError = ErrorType.check;
                                        break;
                                    case false:
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
    }
}
