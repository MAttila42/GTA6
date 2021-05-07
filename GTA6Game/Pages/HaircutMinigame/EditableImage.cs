using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GTA6Game.Pages.HaircutMinigame
{
    public class EditableImage : IDisposable
    {
        public delegate void ModifyImageCallback(int x, int y, Color color);

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        public System.Windows.Controls.Image Canvas;

        private Bitmap Bmp;

        public EditableImage(Bitmap img)
        {
            Bmp = img;
        }

        public void ModifyImage(Action<ModifyImageCallback> action)
        {
            unsafe
            {
                Rectangle lockRegion = new Rectangle(0, 0, 640, 480);
                BitmapData data = Bmp.LockBits(lockRegion, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                ModifyImageCallback cb = (x, y, color) =>
                {
                    int i = x * 4 + y * Bmp.Width * 4;
                    byte* pointer = (byte*)data.Scan0;
                    pointer[i] = color.B;
                    pointer[i + 1] = color.G;
                    pointer[i + 2] = color.R;
                    pointer[i + 3] = color.A;

                };

                action(cb);

                Bmp.UnlockBits(data);
                IntPtr hBmp;
                hBmp = Bmp.GetHbitmap();
                if (Canvas != null)
                {
                    Canvas.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBmp, IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                    Canvas.Source.Freeze();
                }
                DeleteObject(hBmp);
            }
        }

        public void Dispose()
        {
            Bmp.Dispose();
        }
    }

}
