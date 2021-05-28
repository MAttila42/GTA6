using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA6Game.Helpers;
using GTA6Game.UserControls.Overlay.Modal;

namespace GTA6Game.UserControls.Overlay
{
    public class OverlaySettings : PropertyChangeNotifier
    {
        private bool moneyBarDisabled;

        public bool MoneyBarDisabled
        {
            get { return moneyBarDisabled; }
            set
            {
                moneyBarDisabled = value;
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

        public ModalCollection OpenedModals { get; }

        public OverlaySettings()
        {
            moneyBarDisabled = false;
            overlayTopOffset = 20;
            OpenedModals = new ModalCollection();
            OpenedModals.PropertyChanged += OnOpenedModalsChanged;
        }

        private void OnOpenedModalsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(OpenedModals), GetNestedPropertyName(nameof(OpenedModals), e));
        }

        public void Reset()
        {
            MoneyBarDisabled = false;
            OverlayTopOffset = 20;
        }


    }
}
