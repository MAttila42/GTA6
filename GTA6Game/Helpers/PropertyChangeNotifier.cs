using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GTA6Game.Helpers
{
    [Serializable]
    public class PropertyChangeNotifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null, string nestedPropertyName = null)
        {
            if (nestedPropertyName != null)
            {
                PropertyChanged?.Invoke(this, new NestedPropertyChangedEventArgs(propertyName, nestedPropertyName));
            }
            else
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void DisposePropertyChangedEvent()
        {
            PropertyChanged = null;
        }

        protected string GetNestedPropertyName(string propName, PropertyChangedEventArgs e)
        {
            string nestedPropertyName;
            if (e is NestedPropertyChangedEventArgs nested)
            {
                nestedPropertyName = propName + "." + nested.NestedPropertyName;
            }
            else
            {
                nestedPropertyName = propName + "." + e.PropertyName;
            }

            return nestedPropertyName;
        }
    }
}
