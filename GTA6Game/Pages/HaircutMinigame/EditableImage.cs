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
        public delegate void ModifyImageCallback(System.Drawing.Point point, Color color);

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        private System.Windows.Controls.Image Canvas;

        public Bitmap Bmp { get; }

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

                ModifyImageCallback cb = (point, color) =>
                {
                    if (point.X >= Bmp.Width || point.Y >= Bmp.Height)
                    {
                        return;
                    }
                    int i = point.X * 4 + point.Y * Bmp.Width * 4;
                    byte* pointer = (byte*)data.Scan0;
                    pointer[i] = color.B;
                    pointer[i + 1] = color.G;
                    pointer[i + 2] = color.R;
                    pointer[i + 3] = color.A;

                };

                action(cb);

                Bmp.UnlockBits(data);
                RenderImage();
            }
        }

        public void SetCanvas(System.Windows.Controls.Image canvas)
        {
            Canvas = canvas;
            RenderImage();
        }

        public void DetachCanvas()
        {
            Canvas = null;
        }

        public Color GetPixel(System.Drawing.Point point)
        {
            return Bmp.GetPixel(point.X,point.Y);
        }

        public void Dispose()
        {
            Bmp.Dispose();
        }

        private void RenderImage()
        {
            if (Canvas == null)
            {
                return;
            }

            IntPtr hBmp = Bmp.GetHbitmap();

            Canvas.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBmp, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            Canvas.Source.Freeze();

            DeleteObject(hBmp);
        }
    }

}
