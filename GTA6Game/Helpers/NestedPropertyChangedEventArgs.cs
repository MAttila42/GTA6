using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA6Game.Helpers
{
    public class NestedPropertyChangedEventArgs : PropertyChangedEventArgs
    {
        public string NestedPropertyName { get; }

        public NestedPropertyChangedEventArgs(string propertyName,string nestedPropertyName) : base(propertyName)
        {
            NestedPropertyName = nestedPropertyName;
        }

    }
}
