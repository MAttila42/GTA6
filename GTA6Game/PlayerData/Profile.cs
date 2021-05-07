using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GTA6Game.Helpers;

namespace GTA6Game.PlayerData
{
    [Serializable]
    public class Profile : PropertyChangeNotifier, IDisposable
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

        public Profile(string name)
        {
            Name = name;
        }

        public void Dispose()
        {
            DisposePropertyChangedEvent();
        }
    }
}
