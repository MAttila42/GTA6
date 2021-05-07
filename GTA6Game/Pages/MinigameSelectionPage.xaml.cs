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
    /// Interaction logic for MinigameSelectionPage.xaml
    /// </summary>
    public partial class MinigameSelectionPage : PageBase
    {
        public MinigameSelectionPage()
        {
            InitializeComponent();
        }

        private void UiShop_Click(object sender, RoutedEventArgs e)
        {
            Router.ChangeCurrentPage(new ShopPage());
        }

        private void BtnShootingMission_Click(object sender, RoutedEventArgs e)
        {
            Router.ChangeCurrentPage(new ShootingMission());
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

        private void BtnBigSmokeOrder_Click(object sender, RoutedEventArgs e)
        {
            Router.ChangeCurrentPage(new BigSmokeOrder());
        }
    }
}
