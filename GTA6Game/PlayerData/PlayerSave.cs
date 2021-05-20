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

        public Profile SelectedProfile { get; set; }

        public PlayerSave()
        {
            Profiles = new ProfileCollection();
            Profiles.PropertyChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(Profiles));
            };
        }

        public PlayerSave(SerializationInfo info, StreamingContext context)
        {
            Profiles = (ProfileCollection)info.GetValue(nameof(Profiles), typeof(ProfileCollection));
            Profiles.PropertyChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(Profiles));
            };
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Profiles), Profiles, typeof(ProfileCollection));
        }
    }
}
