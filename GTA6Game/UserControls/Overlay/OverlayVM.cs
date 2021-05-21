using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GTA6Game.Helpers;
using GTA6Game.PlayerData;

namespace GTA6Game.UserControls.Overlay
{
    public class OverlayVM : PropertyChangeNotifier
    {
        public PlayerSave Save { get; }

        public string MoneyBarText => $"{Save.SelectedProfile?.Money} Ft";

        public Visibility MoneyBarVisibility => Save.SelectedProfile == null || Settings.OverlayDisabled ? Visibility.Hidden : Visibility.Visible;

        public Thickness MoneyBarMargin => new Thickness(715, Settings.OverlayTopOffset, 10, 560);

        private Profile PreviousSelectedProfile = null;

        private OverlaySettings Settings;

        public OverlayVM(OverlaySettings settings)
        {
            Save = SaveLoader.Save;
            Save.PropertyChanged += OnSaveChanged;
            Settings = settings;
            Settings.PropertyChanged += OnSettingsChanged;
        }

        private void OnSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OverlaySettings.OverlayDisabled))
            {
                OnPropertyChanged(nameof(MoneyBarVisibility));
            }

            if (e.PropertyName == nameof(OverlaySettings.OverlayTopOffset))
            {
                OnPropertyChanged(nameof(MoneyBarMargin));
            }

        }

        private void OnSaveChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PlayerSave.SelectedProfile))
            {
                if (PreviousSelectedProfile != null)
                {
                    PreviousSelectedProfile.PropertyChanged -= OnSelectedProfileChanged;
                }
                PreviousSelectedProfile = Save.SelectedProfile;
                PreviousSelectedProfile.PropertyChanged += OnSelectedProfileChanged;
                OnPropertyChanged(nameof(MoneyBarText));
                OnPropertyChanged(nameof(MoneyBarVisibility));
            }
        }

        private void OnSelectedProfileChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Profile.Money))
            {
                OnPropertyChanged(nameof(MoneyBarText));
            }
        }
    }
}
