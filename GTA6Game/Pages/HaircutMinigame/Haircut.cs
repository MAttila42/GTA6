using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GTA6Game.Helpers;
using GTA6Game.Properties;

namespace GTA6Game.Pages.HaircutMinigame
{
    public class Haircut : PropertyChangeNotifier, IDisposable
    {
        public EditableImage Hair { get; }

        public BitmapImage HeadImage { get; }

        public Haircut(Orientation side)
        {
            Stream hairImgStream = App.ResourceAssembly.GetManifestResourceStream($"GTA6Game.Assets.HaircutMinigame.Hair{side}.png");
            Stream headImgStream = App.ResourceAssembly.GetManifestResourceStream($"GTA6Game.Assets.HaircutMinigame.Head{side}.png");

            Hair = new EditableImage(new Bitmap(hairImgStream));
            HeadImage = new BitmapImage();
            HeadImage.BeginInit();
            HeadImage.StreamSource = headImgStream;
            HeadImage.EndInit();
        }

        public void Dispose()
        {
            DisposePropertyChangedEvent();
            Hair.Dispose();
        }


    }
}
