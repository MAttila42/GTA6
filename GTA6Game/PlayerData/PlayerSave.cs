using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GTA6Game.Helpers;

namespace GTA6Game.PlayerData
{
    /// <summary>
    /// The player's saved data
    /// </summary>
    [Serializable]
    public class PlayerSave : PropertyChangeNotifier, ISerializable
    {
        /// <summary>
        /// The user's profiles
        /// </summary>
        public ProfileCollection Profiles { get; }

        private Profile selectedProfile;

        public Profile SelectedProfile
        {
            get => selectedProfile; set
            {
                if (value == selectedProfile)
                {
                    return;
                }
                selectedProfile = value;
                OnPropertyChanged();
            }
        }

        public PlayerSave()
        {
            Profiles = new ProfileCollection();
            BindEventHandler();
        }

        public PlayerSave(SerializationInfo info, StreamingContext context)
        {
            Profiles = (ProfileCollection) info.GetValue(nameof(Profiles), typeof(ProfileCollection));
            BindEventHandler();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Profiles), Profiles, typeof(ProfileCollection));
        }

        private void BindEventHandler()
        {
            Profiles.PropertyChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(Profiles));
            };
        }
    }
}
