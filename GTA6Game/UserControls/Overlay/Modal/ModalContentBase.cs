using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GTA6Game.UserControls.Overlay.Modal
{
    public abstract class ModalContentBase : UserControl
    {
        public abstract void InjectModalVM(BaseModal modal);
    }
}
