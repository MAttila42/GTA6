using System;
using System.Windows;
using System.Windows.Threading;

namespace GTA6Game.Pages
{
    /// <summary>
    /// Interaction logic for LoadingPage.xaml
    /// </summary>
    public partial class LoadingPage : PageBase
    {
        public LoadingPage()
        {
            InitializeComponent();
        }

        public static void ExecuteWithDelay(Action action, TimeSpan delay)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = delay;
            timer.Tag = action;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        static void timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            Action action = (Action)timer.Tag;

            action.Invoke();
            timer.Stop();
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            if (Router.StartGame == true)
            {
                ExecuteWithDelay(new Action(delegate { Router.ChangeCurrentPage(new LoadingMission()); }), TimeSpan.FromSeconds(3));
            }
            else
            {
                ExecuteWithDelay(new Action(delegate { Router.ChangeCurrentPage(new LoginPage()); }), TimeSpan.FromSeconds(3));
            }
        }
    }
}
