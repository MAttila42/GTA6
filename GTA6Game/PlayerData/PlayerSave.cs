using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTA6Game.PlayerData
{
    /// <summary>
    /// The player's saved data
    /// </summary>
    [Serializable]
    public class PlayerSave : INotifyPropertyChanged, ISerializable
    {
        /// <summary>
        /// Event that gets invoked whenever a property of this object changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
