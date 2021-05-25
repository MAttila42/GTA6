using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GTA6Game.Helpers;

namespace GTA6Game.UserControls.Overlay.Modal
{
    public class Modal<TResult> : BaseModal, IDisposable
    {
        public delegate void ModalClosedEventHandler<T>(ModalResult<T> result);

        public event ModalClosedEventHandler<TResult> ModalClosed;

        public Modal(ModalContentBase content)
        {
            ModalContent = content;
            ModalContent.InjectModalVM(this);
        }

        public void Dispose()
        {
            ModalClosed = null;
        }

        public void CloseModal(bool isDismissed, TResult result)
        {
            ModalClosed?.Invoke(new ModalResult<TResult>(isDismissed, result));
        }
    }
}
