using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GTA6Game.Helpers;
using GTA6Game.Properties;

namespace GTA6Game.Pages.HaircutMinigame
{
    public class Haircut : PropertyChangeNotifier, IDisposable
    {
        public EditableImage Hair { get; }

        public EditableImage AreaToCut { get; }

        public EditableImage FailedCuts { get; }

        public BitmapImage HeadImage { get; }

        public Haircut(Orientation side, Bitmap areaToCut)
        {
            Stream hairImgStream = App.ResourceAssembly.GetManifestResourceStream($"GTA6Game.Assets.HaircutMinigame.Hair.{side}.png");
            Stream headImgStream = App.ResourceAssembly.GetManifestResourceStream($"GTA6Game.Assets.HaircutMinigame.Head.{side}.png");

            Hair = new EditableImage(new Bitmap(hairImgStream));

            HeadImage = new BitmapImage();
            HeadImage.BeginInit();
            HeadImage.StreamSource = headImgStream;
            HeadImage.EndInit();

            AreaToCut = new EditableImage(areaToCut);

            FailedCuts = new EditableImage(new Bitmap(Hair.Bmp.Width, Hair.Bmp.Height));
        }

        public void Dispose()
        {
            DisposePropertyChangedEvent();
            Hair.Dispose();
            AreaToCut.Dispose();
            FailedCuts.Dispose();
        }

        public void Cut(IEnumerable<Point> points)
        {
            points = points.Where(point => Hair.GetPixel(point).A != 0);
            if (points.Count()==0)
            {
                return;
            }
            IEnumerable<Point> pointsOutsideArea = points.Where(point => AreaToCut.GetPixel(point).A == 0);

            Hair.ModifyImage((cb)=>
            {
                foreach (var point in points)
                {
                    cb(point, Color.FromArgb(0, 0, 0, 0));
                }
            });

            AreaToCut.ModifyImage((cb)=> 
            {
                foreach (var point in points)
                {
                    cb(point, Color.FromArgb(0, 0, 0, 0));
                }
            });

            if (pointsOutsideArea.Count() == 0)
            {
                return;
            }

            FailedCuts.ModifyImage((cb) => 
            {
                foreach (var point in pointsOutsideArea)
                {
                    cb(point, Color.FromArgb(127, 255, 0, 0));
                }
            });
        }
    }
}
