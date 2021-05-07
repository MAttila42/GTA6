using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA6Game.Pages.HaircutMinigame
{
    public class Haircut : INotifyPropertyChanged, IDisposable
    {
        public EditableImage Hair { get; }

        public Image HeadImage { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Haircut(Orientation side)
        {
            Bitmap hairImg = (Bitmap)Properties.Resources.ResourceManager.GetObject("Hair" + side);
            Hair = new EditableImage(hairImg);
            HeadImage = (Image)Properties.Resources.ResourceManager.GetObject("Head" + side);
        }

        public void Dispose()
        {
            PropertyChanged = null;
            Hair.Dispose();
        }


    }
}
