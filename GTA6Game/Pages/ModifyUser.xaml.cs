using GTA6Game.Languages;
using GTA6Game.PlayerData;
using GTA6Game.UserControls.Messages;
using GTA6Game.UserControls.Overlay.Modal;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GTA6Game.Pages
{
    /// <summary>
    /// Interaction logic for ModifyUser.xaml
    /// </summary>
    public partial class ModifyUser : PageBase
    {
        private Profile ProfileToModify;

        public ModifyUser(Profile profileToModify)
        {
            InitializeComponent();
            ProfileToModify = profileToModify;
        }

        string[] ErrorMessagesHun = new string[] { "a jelszavak nem egyeznek!", "ilyen felhasználónév már létezik!", "nem adtál meg minden adatot!", "18 éven aluliak nem használhatják a játékot!" };
        string[] ErrorMessagesEng = new string[] { "passwords don't match!", "this username already exists!", "you haven't entered all the data!", "children under the age of 18 are not allowed to use the game!" };
        enum ErrorType { password, username, datas, birth, none, check }

        ErrorType CurrentError = ErrorType.datas;

        string OriginalName;

        private void BtnLoginPage_Click(object sender, RoutedEventArgs e)
        {
            Router.ChangeCurrentPage(new LoginPage());
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Modal<object> modal = new Modal<object>(new MessageYesNo($"{(LanguageManager.CurrentCulture.IetfLanguageTag == "hu-HU" ? "Biztosan törölni akarod a felhasználót? Ha törlöd, az összes pénzed elveszik!" : "Are you sure to want to delete the user? If you delete it, all your money will be lost!")}", "Alert"));
            var modalResult = await OverlaySettings.OpenedModals.OpenModal(modal);

            switch (modalResult.Payload)
            {
                case true:
                    SaveLoader.Save.Profiles.RemoveProfile(ProfileToModify);
                    Router.ChangeCurrentPage(new LoginPage());
                    break;
                case false:
                    break;
            }
        }

        private async void BtnModify_Click(object sender, RoutedEventArgs e)
        {
            await ErrorIdentifier();
            if (CurrentError == ErrorType.none)
            {
                if (CbEULA.IsChecked == true)
                {
                    ProfileToModify.Name = TboxUsername.Text;
                    ProfileToModify.Password = TboxPassword.Password;
                    ProfileToModify.DateOfBirth = DpBirth.Text;
                    Router.ChangeCurrentPage(new LoginPage());
                }
                else
                {
                    Modal<object> modal = new Modal<object>(new MessageOk($"{(LanguageManager.CurrentCulture.IetfLanguageTag == "hu-HU" ? "Sajna-bajna, az ÁSZF elfogadása kötelező!" : "Unfortunately, you have to agree the EULA!")}", "Social Club Error"));
                    await OverlaySettings.OpenedModals.OpenModal(modal);
                }

            }
            else if (CurrentError != ErrorType.check)
            {
                Modal<object> modal = new Modal<object>(new MessageOk($"{(LanguageManager.CurrentCulture.IetfLanguageTag == "hu-HU" ? "Sajna-bajna, " : "Unfortunately, ")} {ErrorWriter()}", "Social Club Error"));
                await OverlaySettings.OpenedModals.OpenModal(modal);
            }
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            TboxUsername.Text = ProfileToModify.Name;
            TboxPassword.Password = ProfileToModify.Password;
            TboxPasswordCheck.Password = ProfileToModify.Password;
            TboxPasswordCheck.Password = ProfileToModify.Password;
            DpBirth.Text = ProfileToModify.DateOfBirth;
            CbLanguage.SelectedItem = CbLanguage.Items[0];
            CbAvatar.SelectedItem = CbAvatar.Items[0];
            OriginalName = ProfileToModify.Name;
        }

        private async Task ErrorIdentifier()
        {
            

            if (string.IsNullOrEmpty(TboxUsername.Text) || DpBirth.SelectedDate == null || CbLanguage.SelectedItem == null || CbAvatar.SelectedItem == null)
            {
                CurrentError = ErrorType.datas;
            }
            else
            {
                if (TboxUsername.Text == OriginalName)
                {
                    await ErrorIdentifierTwo();
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
                        await ErrorIdentifierTwo();
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
                    errorResult = (LanguageManager.CurrentCulture.IetfLanguageTag == "hu-HU" ? $"{ErrorMessagesHun[0]}" : $"{ErrorMessagesEng[0]}");
                    break;
                case ErrorType.username:
                    errorResult = (LanguageManager.CurrentCulture.IetfLanguageTag == "hu-HU" ? $"{ErrorMessagesHun[1]}" : $"{ErrorMessagesEng[1]}");
                    break;
                case ErrorType.datas:
                    errorResult = (LanguageManager.CurrentCulture.IetfLanguageTag == "hu-HU" ? $"{ErrorMessagesHun[2]}" : $"{ErrorMessagesEng[2]}");
                    break;
                case ErrorType.birth:
                    errorResult = (LanguageManager.CurrentCulture.IetfLanguageTag == "hu-HU" ? $"{ErrorMessagesHun[3]}" : $"{ErrorMessagesEng[3]}");
                    break;
            }
            return errorResult;
        }

        private async Task ErrorIdentifierTwo()
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
                        Modal<object> modal = new Modal<object>(new MessageYesNo($"{(LanguageManager.CurrentCulture.IetfLanguageTag == "hu-HU" ? "Nincs beállítva jelszó! Beállítasz egyet mégis?" : "A password isn't defined! Would you like to set one?")}", "Alert"));
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
