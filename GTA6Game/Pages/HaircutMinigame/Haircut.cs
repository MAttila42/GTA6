using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA6Game.Helpers;
using GTA6Game.Properties;

namespace GTA6Game.Pages.HaircutMinigame
{
    public class Haircut : PropertyChangeNotifier, IDisposable
    {
        public EditableImage Hair { get; }

        public Image HeadImage { get; }

        public Haircut(Orientation side)
        {
            Bitmap hairImg = (Bitmap)Resources.ResourceManager.GetObject("Hair" + side);
            Hair = new EditableImage(hairImg);
            HeadImage = (Image)Resources.ResourceManager.GetObject("Head" + side);
        }

        public void Dispose()
        {
            DisposePropertyChangedEvent();
            Hair.Dispose();
        }


    }
}
