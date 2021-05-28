using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GTA6Game.PlayerData;
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
            LoopCount = MainR.Next(1, 16);

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

        public override void OnAttachedToFrame()
        {
            base.OnAttachedToFrame();
            OverlaySettings.MoneyBarDisabled = true;
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            SaveLoader.Save.SelectedProfile.Money += 100 * LoopCount;
            Windows.KeyDown += new KeyEventHandler(Windows_KeyDown);
            Img.Move(ImgBackground, -1200, -1210, -2467, LoopCount, BackPicturesName, Router);
            Img2.Move(ImgScprite, -1200, -1500, -2467, LoopCount, SpritePicturesName, Router);
        }

        private void Windows_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1 && Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.H))
            {
                SaveLoader.Save.SelectedProfile.Money -= 100 * LoopCount;
                Router.ChangeCurrentPage(new MinigameSelectionPage());
            }
        }
    }
}
