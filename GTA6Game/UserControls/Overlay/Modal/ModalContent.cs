using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GTA6Game.UserControls.Overlay.Modal
{
    public abstract class ModalContent<T> : UserControl
    {
        public void BindModalData(Modal<T> modal)
        {
            
        }
    }
}
