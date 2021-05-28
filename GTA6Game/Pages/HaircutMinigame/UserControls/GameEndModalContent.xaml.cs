using GTA6Game.UserControls.Overlay.Modal;
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

namespace GTA6Game.Pages.HaircutMinigame.UserControls
{
    /// <summary>
    /// Interaction logic for GameEndModalContent.xaml
    /// </summary>
    public partial class GameEndModalContent : ModalContentBase
    {
        private Modal<GameEndUserDecision> ModalVM;

        public GameEndModalContent(GameEndPayload payload)
        {
            DataContext = payload;
            InitializeComponent();
        }

        public override void InjectModalVM(BaseModal modal)
        {
            ModalVM = (Modal<GameEndUserDecision>)modal;
        }

    
        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            ModalVM.CloseModal(false, GameEndUserDecision.BackToMenu);
        }

        private void NewRoundButton_Click(object sender, RoutedEventArgs e)
        {
            ModalVM.CloseModal(false, GameEndUserDecision.NewRound);
        }
    }

    enum GameEndUserDecision
    {
        BackToMenu,
        NewRound
    }
}
