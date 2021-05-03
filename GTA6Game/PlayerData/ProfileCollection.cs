using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTA6Game.PlayerData
{
    [Serializable]
    public class ProfileCollection : IEnumerable<Profile>, INotifyPropertyChanged, ISerializable
    {
        public int NumberOfProfiles => profiles.Count;

        public Profile this[int i]
        {
            get
            {
                return profiles[i];
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private List<Profile> profiles;

        public ProfileCollection()
        {
            profiles = new List<Profile>();
        }

        public ProfileCollection(SerializationInfo info, StreamingContext context)
        {
            profiles = (List<Profile>)info.GetValue(nameof(profiles), typeof(List<Profile>));
            foreach (var profile in profiles)
            {
                profile.PropertyChanged += (sender, e) =>
                {
                    OnPropertyChanged(nameof(profiles));
                };
            }
        }

        public IEnumerator<Profile> GetEnumerator()
        {
            return ((IEnumerable<Profile>)profiles).GetEnumerator();
        }

        public void AddProfile(Profile profile)
        {
            profiles.Add(profile);
            profile.PropertyChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(profiles));
            };
            OnPropertyChanged(nameof(profiles));
        }

        public void RemoveProfile(Profile profile)
        {
            profiles.Remove(profile);
            profile.Dispose();
            OnPropertyChanged(nameof(profiles));
        }

        public void RemoveAllProfiles()
        {
            foreach (var profile in profiles)
            {
                profile.Dispose();
            }
            profiles.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)profiles).GetEnumerator();
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(profiles), profiles, typeof(List<Profile>));
        }
    }
}
