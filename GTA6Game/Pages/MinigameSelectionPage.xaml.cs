using GTA6Game.Pages.HaircutMinigame;
using GTA6Game.PlayerData;
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

        private void UiExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan ki akar lépni?", "Bezárás", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No:
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
                SaveLoader.Save.SelectedProfile.Money = 0;
                BtnShootingMission.IsEnabled = false;
            }
            else
            {
                BtnShootingMission.IsEnabled = true;
            }
            TbMoney.Text = $"{SaveLoader.Save.SelectedProfile.Money} Ft";
        }
    }
}
