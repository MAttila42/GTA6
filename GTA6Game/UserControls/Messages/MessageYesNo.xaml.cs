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

namespace GTA6Game.UserControls.Messages
{
    /// <summary>
    /// Interaction logic for MessageYesNo.xaml
    /// </summary>
    public partial class MessageYesNo : ModalContentBase
    {
        private Modal<bool> Modal;

        public MessageYesNo(string description, string title)
        {
            InitializeComponent();
            TbDescription.Text = description;
            TbTitle.Text = title;
        }

        public override void InjectModalVM(BaseModal modal)
        {
            Modal = (Modal<bool>)modal;
        }

        private void TbYes_Click(object sender, RoutedEventArgs e)
        {
            Modal.CloseModal(false, true);
        }

        private void TbNo_Click(object sender, RoutedEventArgs e)
        {
            Modal.CloseModal(false, false);
        }
    }
}
