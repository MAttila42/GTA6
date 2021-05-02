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
    public class PlayerSave : INotifyPropertyChanged
    {
        /// <summary>
        /// Event that gets invoked whenever a property of this object changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;



        public PlayerSave()
        {

        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
