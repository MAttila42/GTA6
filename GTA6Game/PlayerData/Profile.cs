using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GTA6Game.PlayerData
{
    [Serializable]
    public class Profile : INotifyPropertyChanged, IDisposable
    {
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private int money;

        public int Money
        {
            get { return money; }
            set
            {
                money = value;
                OnPropertyChanged();
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        private int weapon;

        public int Weapon
        {
            get { return weapon; }
            set
            {
                weapon = value;
                OnPropertyChanged();
            }
        }

        public Profile(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void Dispose()
        {
            PropertyChanged = null;
        }
    }
}
