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
        ImageMovement Img = new ImageMovement();
        ImageMovement Img2 = new ImageMovement();
        List<string> BackPicturesName = new List<string>();
        HashSet<int> BackPicturesNameHash = new HashSet<int>();

        List<string> SpritePicturesName = new List<string>();
        HashSet<int> SpritePicturesNameHash = new HashSet<int>();
        Random MainR = new Random();
        int LoopCount = -1;

        public LoadingMission()
        {
            InitializeComponent();

            LoopCount = MainR.Next(1, 15);

            AddPicturesNameHashSet();
            AddPicturesNameList();
            ImgBackground.Source = new BitmapImage(new Uri(BackPicturesName.First(), UriKind.Relative));
            ImgScprite.Source = new BitmapImage(new Uri(SpritePicturesName.First(), UriKind.Relative));
        }

        public void AddPicturesNameHashSet()
        {
            Random r = new Random();
            while (BackPicturesNameHash.Count != LoopCount)
            {
                int aktRandom = r.Next(1, 16);
                BackPicturesNameHash.Add(aktRandom);
            }
            while (SpritePicturesNameHash.Count != LoopCount)
            {
                int aktRandom = r.Next(1, 16);
                SpritePicturesNameHash.Add(aktRandom);
            }
        }

        public void AddPicturesNameList()
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

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            Img.Move(ImgBackground, -1200, -1210, -2467, LoopCount, BackPicturesName, Router);
            Img2.Move(ImgScprite, -1200, -1500, -2467, LoopCount, SpritePicturesName, Router);
        }
    }
}
