using GTA6Game.Pages.HaircutMinigame;
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

namespace GTA6Game.UserControls
{
    /// <summary>
    /// Interaction logic for CameraMovementPanel.xaml
    /// </summary>
    public partial class CameraMovementPanel : UserControl
    {
        public CameraMovementPanel()
        {
            InitializeComponent();
        }

        private void MoveCameraUp(object sender, RoutedEventArgs e)
        {
            var vm = (HaircutMinigameVM)DataContext;
            vm.CameraOrientation.MoveCamera(Direction.Up);
        }

        private void MoveCameraDown(object sender, RoutedEventArgs e)
        {
            var vm = (HaircutMinigameVM)DataContext;
            vm.CameraOrientation.MoveCamera(Direction.Down);
        }

        private void MoveCameraLeft(object sender, RoutedEventArgs e)
        {
            var vm = (HaircutMinigameVM)DataContext;
            vm.CameraOrientation.MoveCamera(Direction.Left);
        }

        private void MoveCameraRight(object sender, RoutedEventArgs e)
        {
            var vm = (HaircutMinigameVM)DataContext;
            vm.CameraOrientation.MoveCamera(Direction.Right);
        }
    }
}
