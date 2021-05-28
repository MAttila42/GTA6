using GTA6Game.PlayerData;
using GTA6Game.UserControls;
using GTA6Game.UserControls.Messages;
using GTA6Game.UserControls.Overlay.Modal;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                    _ = GameOver();
                }
            }
        }

        Random R = new Random();
        int LoopNumber = 0;
        int TargetCount = 0;
        int ClickedCount = 0;
        DispatcherTimer Timer;
        TimeSpan Time;

        public ShootingMission()
        {
            InitializeComponent();
            LoopNumber = R.Next(10, 100);
            UpdateClicks();
        }

        private async Task GameOver()
        {
            Playground.Children.Clear();
            LoopNumber = 0;
            TargetCount = 0;
            Modal<object> modal = new Modal<object>(new MessageOk("Elvesztetted a játékot!", "Mission Failed"));
            await OverlaySettings.OpenedModals.OpenModal(modal);
            Router.ChangeCurrentPage(new MinigameSelectionPage());
            
        }

        private async Task Start()
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
                if (ClickedCount > LoopNumber / 2)
                {
                    Modal<object> modal = new Modal<object>(new MessageOk("A szint teljesítve. Vissza a kezdőképernyőre!", "Mission Passed"));
                    await OverlaySettings.OpenedModals.OpenModal(modal);
                    Router.ChangeCurrentPage(new MinigameSelectionPage());
                }
                else
                {
                    await GameOver();
                }
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
                    _ = Start();
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
                ClickedCount++;
                UpdateClicks();
                removed = true;
            };
            await Task.Delay(disappearTime);
            if (removed == false && Score > 0)
            {
                UpdateMoney(-1500);
                Playground.Children.Remove(target);
                TargetCount--;
            }
        }

        private void UpdateMoney(int plusz)
        {
            SaveLoader.Save.SelectedProfile.Money += plusz;
            Score = SaveLoader.Save.SelectedProfile.Money;
        }

        private void UpdateClicks()
        {
            TbCount.Text = $"{ClickedCount}/{LoopNumber}";
        }
    }
}
