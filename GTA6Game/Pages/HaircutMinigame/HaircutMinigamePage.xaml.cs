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

namespace GTA6Game.Pages.HaircutMinigame
{
    /// <summary>
    /// Interaction logic for HaircutMinigamePage.xaml
    /// </summary>
    public partial class HaircutMinigamePage : PageBase
    {
        private HaircutMinigameVM ViewModel;

        public HaircutMinigamePage()
        {
            DesiredShape.LoadShapes();
            ViewModel = new HaircutMinigameVM();
            ViewModel.NewRoundStarted += OnNewRoundStarted;
            DataContext = ViewModel;
            InitializeComponent();
            
        }

        public override void OnAttachedToFrame()
        {
            base.OnAttachedToFrame();
            ViewModel.InjectOverlaySettings(OverlaySettings);
            ViewModel.InjectRouter(Router);

        }

        private void PageBase_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Dispose();
        }

        private void OnNewRoundStarted()
        {
            ViewModel.Dispose();
            ViewModel = new HaircutMinigameVM();
            ViewModel = new HaircutMinigameVM();
            ViewModel.InjectOverlaySettings(OverlaySettings);
            ViewModel.InjectRouter(Router);
            ViewModel.NewRoundStarted += OnNewRoundStarted;
            DataContext = ViewModel;
        }
    }
}
