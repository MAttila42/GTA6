using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA6Game.Helpers;

namespace GTA6Game.UserControls.Overlay
{
    public class OverlaySettings : PropertyChangeNotifier
    {
        private bool overlayDisabled;

        public bool OverlayDisabled
        {
            get { return overlayDisabled; }
            set
            {
                overlayDisabled = value;
                OnPropertyChanged();
            }
        }

        private int overlayTopOffset;

        public int OverlayTopOffset
        {
            get { return overlayTopOffset; }
            set
            {
                overlayTopOffset = value;
                OnPropertyChanged();
            }
        }

        public OverlaySettings()
        {
            overlayDisabled = false;
            overlayTopOffset = 20;
        }

        public void Reset()
        {
            OverlayDisabled = false;
            OverlayTopOffset = 20;
        }
    }
}
