using System;
using System.Collections;
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
    [Serializable]
    public class ProfileCollection : PropertyChangeNotifier,IEnumerable<Profile>, ISerializable
    {
        public int NumberOfProfiles => Profiles.Count;

        public Profile this[int i]
        {
            get
            {
                return Profiles[i];
            }
        }

        private List<Profile> Profiles;

        public ProfileCollection()
        {
            Profiles = new List<Profile>();
        }

        public ProfileCollection(SerializationInfo info, StreamingContext context)
        {
            Profiles = (List<Profile>)info.GetValue(nameof(Profiles), typeof(List<Profile>));
            foreach (var profile in Profiles)
            {
                profile.PropertyChanged += (sender, e) =>
                {
                    OnPropertyChanged(nameof(Profiles));
                };
            }
        }

        public void AddProfile(Profile profile)
        {
            Profiles.Add(profile);
            profile.PropertyChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(Profiles));
            };
            OnPropertyChanged(nameof(Profiles));
        }

        public void RemoveProfile(Profile profile)
        {
            Profiles.Remove(profile);
            profile.Dispose();
            OnPropertyChanged(nameof(Profiles));
        }

        public void RemoveAllProfiles()
        {
            foreach (var profile in Profiles)
            {
                profile.Dispose();
            }
            Profiles.Clear();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Profiles), Profiles, typeof(List<Profile>));
        }

        public IEnumerator<Profile> GetEnumerator()
        {
            return ((IEnumerable<Profile>)Profiles).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Profiles).GetEnumerator();
        }
    }
}
