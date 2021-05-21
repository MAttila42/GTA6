using GTA6Game.PlayerData;
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

namespace GTA6Game.UserControls.Overlay
{
    /// <summary>
    /// Interaction logic for MoneyOverlay.xaml
    /// </summary>
    public partial class Overlay : UserControl
    {
        public Overlay()
        {
        }

        public void Init(OverlaySettings settings)
        {
            DataContext = new OverlayVM(settings);
            InitializeComponent();

        }
    }
}
