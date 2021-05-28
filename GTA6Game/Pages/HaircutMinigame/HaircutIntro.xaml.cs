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
    /// Interaction logic for HaircutIntro.xaml
    /// </summary>
    public partial class HaircutIntro : PageBase
    {
        public HaircutIntro()
        {
            InitializeComponent();
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            Router.ChangeCurrentPage(new HaircutMinigamePage());
        }

        public override void OnAttachedToFrame()
        {
            base.OnAttachedToFrame();
            OverlaySettings.MoneyBarDisabled = true;
        }
    }
}
