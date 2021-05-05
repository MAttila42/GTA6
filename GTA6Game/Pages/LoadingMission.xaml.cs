using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using GTA6Game.Routing;
using GTA6Game.UserControls;

namespace GTA6Game.Pages
{
    /// <summary>
    /// Interaction logic for LoadingMission.xaml
    /// </summary>
    public partial class LoadingMission : PageBase
    {
        ImageMovement img = new ImageMovement();
        ImageMovement img2 = new ImageMovement();
        List<string> BackPicturesName = new List<string>();
        HashSet<int> BackPicturesNameHash = new HashSet<int>();

        List<string> SpritePicturesName = new List<string>();
        HashSet<int> SpritePicturesNameHash = new HashSet<int>();
        Random MainR = new Random();
        int loopCount = -1;

        public LoadingMission()
        {
            InitializeComponent();

            loopCount = MainR.Next(1, 11);

            Btn.Content = loopCount;

            FeltöltKépekNeveHashSet();
            FeltöltKépekNeveLista();
            ImgBackground.Source = new BitmapImage(new Uri(BackPicturesName.First(), UriKind.Relative));
            ImgScprite.Source = new BitmapImage(new Uri(SpritePicturesName.First(), UriKind.Relative));

            //ExecuteWithDelay(new Action(delegate {  }), TimeSpan.FromSeconds(1));
            //ExecuteWithDelay(new Action(delegate {  }), TimeSpan.FromSeconds(1));

        }

        public void FeltöltKépekNeveHashSet()
        {
            Random r = new Random();
            while (BackPicturesNameHash.Count != loopCount)
            {
                int aktRandom = r.Next(1, 11);
                BackPicturesNameHash.Add(aktRandom);
            }
            while (SpritePicturesNameHash.Count != loopCount)
            {
                int aktRandom = r.Next(1, 11);
                SpritePicturesNameHash.Add(aktRandom);
            }
        }

        public void FeltöltKépekNeveLista()
        {
            foreach (var i in BackPicturesNameHash)
            {
                BackPicturesName.Add($"/Assets/Backgrounds/Bg{i}.png");
            }
            foreach (var i in SpritePicturesNameHash)
            {
                SpritePicturesName.Add($"/Assets/Backgrounds/Sp{i}.png");
            }
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

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            img.Move(ImgBackground, -1200, -1210, -2467, loopCount, BackPicturesName, Router);
            img2.Move(ImgScprite, -1200, -1500, -2467, loopCount, SpritePicturesName, Router);
        }
    }
}
