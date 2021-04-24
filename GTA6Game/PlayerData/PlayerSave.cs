using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GTA6Game.PlayerData
{
    /// <summary>
    /// The player's saved data
    /// </summary>
    [Serializable]
    public class PlayerSave : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Creates a new save with default values
        /// </summary>
        /// <returns>The new save</returns>
        public static PlayerSave CreateInitialSave()
        {
            return new PlayerSave() { Money = 1000 };
        }

        /// <summary>
        /// Event that gets invoked whenever a property of this object changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private int money;

        /// <summary>
        /// The player's money
        /// </summary>
        public int Money
        {
            get { return money; }
            set
            {
                money = value;
                OnPropertyChanged();
            }
        }

        public PlayerSave()
        {

        }

        public void Dispose()
        {
            PropertyChanged = null;
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
