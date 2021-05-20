using GTA6Game.PlayerData;
using GTA6Game.UserControls;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GTA6Game.Pages
{
    /// <summary>
    /// Interaction logic for ShootingMission.xaml
    /// </summary>
    public partial class ShootingMission : PageBase
    {

        private int score = SaveLoader.Save.SelectedProfile.Money;

        private int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
                if (Score <= 0)
                {
                    score = 0;
                    GameOver();
                }
            }
        }

        Random R = new Random();
        int LoopNumber = 0;
        int TargetCount = 0;
        DispatcherTimer Timer;
        TimeSpan Time;

        public ShootingMission()
        {
            InitializeComponent();
            LoopNumber = R.Next(10, 100);
        }

        private void GameOver()
        {
            Playground.Children.Clear();
            LoopNumber = 0;
            TargetCount = 0;
            TbMoney.Text = $"{Score} Ft";
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
            if (Score > 0)
            {
                MessageBox.Show("A szint teljesítve. Vissza a kezdőképernyőre", "Kész", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Router.ChangeCurrentPage(new MinigameSelectionPage());
            }
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMoney(0);
            Countdown();
        }

        private void Countdown()
        {
            Time = TimeSpan.FromSeconds(5);

            Timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, async delegate
            {
                TbTime.Text = Time.ToString("ss").TrimStart(new Char[] { '0' });
                if (Time == TimeSpan.Zero)
                {
                    Timer.Stop();
                    await Task.Delay(1000);
                    Start();
                };
                Time = Time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
            Timer.Start();

        }

        private async Task AddTarget()
        {
            UserIcon target = new UserIcon();
            int disappearTime = R.Next(600, 5000);
            bool removed = false;
            target.Content = disappearTime;
            target.Height = R.Next(30, 120);
            target.ImageSource = new BitmapImage(new Uri("/Assets/Target.png", UriKind.Relative));
            target.Style = this.FindResource("Menu") as Style;
            Canvas.SetLeft(target, R.Next(40, 978));
            Canvas.SetTop(target, R.Next(40, 404));
            Playground.Children.Add(target);
            TargetCount++;
            target.Click += (source, e) =>
            {
                UpdateMoney(1000);
                Playground.Children.Remove(target);
                TargetCount--;
                removed = true;
            };
            await Task.Delay(disappearTime);
            if (removed == false)
            {
                if (Score > 0)
                {
                    UpdateMoney(-1500);
                    Playground.Children.Remove(target);
                    TargetCount--;
                }
            }

        }

        private void UpdateMoney(int plusz)
        {
            if (Score > 0)
            {
                SaveLoader.Save.SelectedProfile.Money += plusz;
                Score = SaveLoader.Save.SelectedProfile.Money;
                TbMoney.Text = $"{Score} Ft";
            }
        }
    }
}
