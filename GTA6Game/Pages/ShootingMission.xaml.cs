using GTA6Game.PlayerData;
using GTA6Game.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ShootingMission.xaml
    /// </summary>
    public partial class ShootingMission : PageBase
    {
        int Score = 0;
        Random R = new Random();
        int LoopNumber = 0;
        int WeaponNum = 0;
        int TargetCount = 0;

        public ShootingMission()
        {
            InitializeComponent();
            LoopNumber = R.Next(1, 100);
            TbLoops.Text = $"{LoopNumber}";
        }

        private async void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            int delay = 0;
            for (int i = 0; i < LoopNumber; i++)
            {
                _ = AddTarget();
                delay = R.Next(10, 1000);
                BtnStart.Content = delay;
                await Task.Delay(delay);
            }
            while (TargetCount != 0)
            {
                await Task.Delay(delay);
            }
            MessageBox.Show("A szint teljesítve. Vissza a kezdőképernyőre", "Kész", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            Router.ChangeCurrentPage(new MinigameSelectionPage());
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            SaveLoader.Save.SelectedProfile = SaveLoader.Save.Profiles.First();
            SaveLoader.Save.SelectedProfile.Money = 500;
            SaveLoader.Save.SelectedProfile.Weapon = 4;
            Score = SaveLoader.Save.SelectedProfile.Money;
            WeaponNum = SaveLoader.Save.SelectedProfile.Weapon;
            TbMoney.Text = $"{Score} Ft";
        }

        private async Task AddTarget()
        {
            UserIcon target = new UserIcon();
            int disappearTime = R.Next(600, 5000);
            bool removed = false;
            target.Content = disappearTime;
            target.Height = R.Next(WeaponNum * 10, WeaponNum * 30);
            target.ImageSource = new BitmapImage(new Uri("/Assets/Target.png", UriKind.Relative));
            target.Style = this.FindResource("Menu") as Style;
            Canvas.SetLeft(target, R.Next(40, 978));
            Canvas.SetTop(target, R.Next(40, 404));
            Playground.Children.Add(target);
            TargetCount++;
            target.Click += (source, e) =>
            {
                Score += 1000;
                TbMoney.Text = $"{Score} Ft";
                Playground.Children.Remove(target);
                TargetCount--;
                removed = true;
            };
            await Task.Delay(disappearTime);
            if (removed == false)
            {
                Score -= 500;
                TbMoney.Text = $"{Score} Ft";
                Playground.Children.Remove(target);
                TargetCount--;
            }
        }
    }
}
