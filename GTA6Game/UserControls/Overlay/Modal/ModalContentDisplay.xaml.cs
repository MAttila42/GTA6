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

namespace GTA6Game.UserControls.Overlay.Modal
{
    /// <summary>
    /// Interaction logic for ModalDisplay.xaml
    /// </summary>
    public partial class ModalContentDisplay : UserControl
    {
        public ModalContentDisplay()
        {
            InitializeComponent();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            BaseModal modal = (BaseModal)DataContext;
            ContentContainer.Children.Clear();
            ContentContainer.Children.Add(modal.ModalContent);

        }
    }
}
