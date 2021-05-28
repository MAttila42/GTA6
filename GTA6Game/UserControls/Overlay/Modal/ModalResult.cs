using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA6Game.UserControls.Overlay.Modal
{
    public class ModalResult<T>
    {
        public bool IsDismissed { get; }

        public T Payload { get; }

        public ModalResult(bool isDismissed, T payload)
        {
            IsDismissed = isDismissed;
            Payload = payload;
        }

    }
}
