using GTA6Game.Languages;
using GTA6Game.Pages.HaircutMinigame;
using GTA6Game.PlayerData;
using GTA6Game.UserControls.Messages;
using GTA6Game.UserControls.Overlay.Modal;
using System.Collections.Generic;
using System.Windows;

namespace GTA6Game.Pages
{
    /// <summary>
    /// Interaction logic for MinigameSelectionPage.xaml
    /// </summary>
    public partial class MinigameSelectionPage : PageBase
    {
        public MinigameSelectionPage()
        {
            InitializeComponent();
        }

        string[] ExitText = new string[] { "Biztosan ki akarsz lépni a GTA VI-ból?", "Are you sure you want to quit GTA VI?" };
        string[] ExitTitle = new string[] { "Kilépés", "Quit" };

        public override void OnAttachedToFrame()
        {
            base.OnAttachedToFrame();
            OverlaySettings.OverlayTopOffset = 79;
        }

        private async void UiExit_Click(object sender, RoutedEventArgs e)
        {
            Modal<object> modal = new Modal<object>(new MessageYesNo($"{(LanguageManager.CurrentCulture.IetfLanguageTag == "hu-HU" ? $"{ExitText[0]}" : $"{ExitText[1]}")}", $"{(LanguageManager.CurrentCulture.IetfLanguageTag == "hu-HU" ? $"{ExitTitle[0]}" : $"{ExitTitle[1]}")}"));
            var modalResult = await OverlaySettings.OpenedModals.OpenModal(modal);

            switch (modalResult.Payload)
            {
                case true:
                    Application.Current.Shutdown();
                    break;
                case false:
                    break;
            }
        }

        private void BtnShootingMission_Click(object sender, RoutedEventArgs e)
        {
            Router.ChangeCurrentPage(new ShootingMission());
        }

        private void BtnBigSmokeOrder_Click(object sender, RoutedEventArgs e)
        {
            Router.ChangeCurrentPage(new BigSmokeOrder());
        }

        private void BtnHaircut_Click(object sender, RoutedEventArgs e)
        {
            Router.ChangeCurrentPage(new HaircutMinigamePage());
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            if (SaveLoader.Save.SelectedProfile.Money <= 0)
            {
                BtnShootingMission.IsEnabled = false;
            }
            else
            {
                BtnShootingMission.IsEnabled = true;
            }
        }
    }
}
