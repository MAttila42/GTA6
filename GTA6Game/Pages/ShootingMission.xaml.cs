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
using System.Windows.Threading;

namespace GTA6Game.Pages
{
    /// <summary>
    /// Interaction logic for ShootingMission.xaml
    /// </summary>
    public partial class ShootingMission : PageBase
    {
        private int score = 500;

        private int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
                if (Score == 0)
                {

                    GameOver();
                }
            }
        }
        Random R = new Random();
        int LoopNumber = 0;
        int WeaponNum = 0;
        int TargetCount = 0;
        DispatcherTimer _timer;
        TimeSpan _time;

        public ShootingMission()
        {
            InitializeComponent();
            LoopNumber = R.Next(1, 100);
            TbLoops.Text = $"{LoopNumber}";
        }

        private void GameOver()
        {
            Playground.Children.Clear();
            LoopNumber = 0;
            TbMoney.Text = "0 Ft";
            MessageBoxResult result = MessageBox.Show("Elvesztetted a játékot!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);
            switch (result)
            {
                case MessageBoxResult.OK:
                    Router.ChangeCurrentPage(new MinigameSelectionPage());
                    break;
            }
        }

        private async void Start()
        {
            int delay = 0;
            for (int i = 0; i < LoopNumber; i++)
            {
                _ = AddTarget();
                delay = R.Next(10, 1000);
                await Task.Delay(delay);
            }
            while (TargetCount != 0)
            {
                await Task.Delay(delay);
            }
            if (TbMoney.Text != "0 Ft")
            {
                MessageBox.Show("A szint teljesítve. Vissza a kezdőképernyőre", "Kész", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Router.ChangeCurrentPage(new MinigameSelectionPage());
            }
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            Router.SelectedUser = SaveLoader.Save.Profiles.First();
            Router.SelectedUser.Money = 500;
            Router.SelectedUser.Weapon = 4;
            Score = Router.SelectedUser.Money;
            WeaponNum = Router.SelectedUser.Weapon;
            TbMoney.Text = $"{Score} Ft";
            Countdown();
        }

        private void Countdown()
        {
            _time = TimeSpan.FromSeconds(5);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, async delegate
            {
                TbTime.Text = _time.ToString("ss").TrimStart(new Char[] { '0' });
                if (_time == TimeSpan.Zero)
                {
                    _timer.Stop();
                    await Task.Delay(1000);
                    Start();
                }; 
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
            _timer.Start();
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
                if (Score > 0)
                {
                    TbMoney.Text = $"{Score} Ft";
                }
                else
                {
                    TbMoney.Text = "0 Ft";
                }
                Playground.Children.Remove(target);
                TargetCount--;
                removed = true;
            };
            await Task.Delay(disappearTime);
            if (removed == false)
            {
                if (Score > 0)
                {
                    Score -= 500;
                    TbMoney.Text = $"{Score} Ft";
                    Playground.Children.Remove(target);
                    TargetCount--;
                }
            }
        }
    }
}
