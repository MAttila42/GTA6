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
using System.Windows.Threading;
using Point = System.Drawing.Point;

namespace GTA6Game.Pages.HaircutMinigame
{
    public class EditableImage : IDisposable
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        public int Width { get; }

        public int Height { get; }

        private Bitmap Bmp { get; }

        private System.Windows.Controls.Image Canvas;

        private BitmapData BitmapData;

        public EditableImage(Bitmap img)
        {
            Bmp = img;
            Width = img.Width;
            Height = img.Height;
        }

        public void Lock()
        {
            if (BitmapData == null)
            {
                BitmapData = Bmp.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            }
        }

        public void Unlock()
        {
            if (BitmapData != null)
            {
                Bmp.UnlockBits(BitmapData);
                BitmapData = null;
            }
        }

        public int Count(Func<Color, Point, bool> cb)
        {
            int count = 0;
            unsafe
            {
                byte* pointer = (byte*)BitmapData.Scan0;
                for (int i = 0; i < Width * Height * 4; i += 4)
                {
                    byte b = pointer[i];
                    byte g = pointer[i + 1];
                    byte r = pointer[i + 2];
                    byte a = pointer[i + 3];

                    int pixelIndex = i / 4;
                    int x = pixelIndex % Width;
                    int y = pixelIndex / Width;

                    var point = new Point(x, y);

                    Color color = Color.FromArgb(a, r, g, b);
                    if (cb(color,point))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public void ModifyImage(Point point, Color color)
        {
            if (point.X >= Width || point.Y >= Height)
            {
                return;
            }
            int i = point.X * 4 + point.Y * Width * 4;
            unsafe
            {
                byte* pointer = (byte*)BitmapData.Scan0;
                pointer[i] = color.B;
                pointer[i + 1] = color.G;
                pointer[i + 2] = color.R;
                pointer[i + 3] = color.A;
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

        public Color GetPixel(Point point)
        {
            if (BitmapData == null)
            {
                return Bmp.GetPixel(point.X, point.Y);
            }
            else
            {
                byte b;
                byte g;
                byte r;
                byte a;

                int i = point.X * 4 + point.Y * Width * 4;
                unsafe
                {
                    byte* pointer = (byte*)BitmapData.Scan0;
                    b = pointer[i];
                    g = pointer[i + 1];
                    r = pointer[i + 2];
                    a = pointer[i + 3];
                }

                return Color.FromArgb(a, r, g, b);
            }
        }

        public void Dispose()
        {
            Bmp.Dispose();
        }

        public void RenderImage()
        {
            if (Canvas == null)
            {
                return;
            }

            void Render()
            {
                IntPtr hBmp = Bmp.GetHbitmap();
                Canvas.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBmp, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                Canvas.Source.Freeze();
                DeleteObject(hBmp);
            }

            Canvas.Dispatcher.BeginInvoke(new Action(Render)).Wait();

        }
    }

}
