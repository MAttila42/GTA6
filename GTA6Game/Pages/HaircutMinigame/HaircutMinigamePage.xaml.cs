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

        private byte x = 0;

        public HaircutMinigamePage()
        {
            DesiredShape.LoadShapes();
            ViewModel = new HaircutMinigameVM();
            DataContext = ViewModel;
            InitializeComponent();
            
        }

      
    }
}
