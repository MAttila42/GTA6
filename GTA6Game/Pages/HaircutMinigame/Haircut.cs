using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GTA6Game.Helpers;
using GTA6Game.Properties;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Windows;
using Point = System.Drawing.Point;

namespace GTA6Game.Pages.HaircutMinigame
{
    public class Haircut : PropertyChangeNotifier, IDisposable
    {
        public EditableImage Hair { get; }

        public EditableImage AreaToCut { get; }

        public EditableImage FailedCuts { get; }

        public BitmapImage HeadImage { get; }

        private int completePercent;

        /// <summary>
        /// Relative to the area that needs to be cut
        /// </summary>
        public int CompletePercent
        {
            get { return completePercent; }
            set
            {
                completePercent = value;
                OnPropertyChanged();
            }
        }

        private int failPercent;

        /// <summary>
        /// Relative to the area of the hair which mustn't be cut
        /// </summary>
        public int FailPercent
        {
            get { return failPercent; }
            set
            {
                failPercent = value;
                OnPropertyChanged();
            }
        }

        private Thread EditorThread;

        private BlockingCollection<Action> EditActions;

        private bool HairNeedsRender = false;

        private bool AreaToCutNeedsRender = false;

        private bool FailedCutsNeedsRender = false;

        private int PixelsToCut;

        private int PixelsToLeave;

        private bool IsDisposed = false;

        public Haircut(Orientation side, Bitmap areaToCut)
        {
            EditActions = new BlockingCollection<Action>();

            Stream hairImgStream = App.ResourceAssembly.GetManifestResourceStream($"GTA6Game.Assets.HaircutMinigame.Hair.{side}.png");
            Stream headImgStream = App.ResourceAssembly.GetManifestResourceStream($"GTA6Game.Assets.HaircutMinigame.Head.{side}.png");

            Hair = new EditableImage(new Bitmap(hairImgStream));

            HeadImage = new BitmapImage();
            HeadImage.BeginInit();
            HeadImage.StreamSource = headImgStream;
            HeadImage.EndInit();

            AreaToCut = new EditableImage(areaToCut);

            AreaToCut.Lock();

            PixelsToCut = AreaToCut.Count((color, point) => color.A != 0);

            Hair.Lock();

            PixelsToLeave = Hair.Count((color, point) => color.A != 0 && AreaToCut.GetPixel(point).A == 0);

            Hair.Unlock();
            AreaToCut.Unlock();

            FailedCuts = new EditableImage(new Bitmap(Hair.Width, Hair.Height));

            EditorThread = new Thread(EditorThreadLoop)
            {
                IsBackground = true,
                Name = "EditorThread" + side
            };
            EditorThread.Start(this);
        }

        public void Dispose()
        {
            IsDisposed = true;
            DisposePropertyChangedEvent();
            EditorThread.Abort();
            EditActions.Dispose();
            Hair.Dispose();
            AreaToCut.Dispose();
            FailedCuts.Dispose();
        }

        public void Cut(IEnumerable<Point> points)
        {
            EditActions.Add(() =>
            {
                points = points.Where(point =>
                {
                    bool isOutOfBounds = point.X < 0
                                         || point.X >= Hair.Width
                                         || point.Y < 0
                                         || point.Y >= Hair.Height;
                    return !isOutOfBounds && Hair.GetPixel(point).A != 0;
                }).ToList();
                if (points.Count() == 0)
                {
                    return;
                }

                IEnumerable<Point> pointsOutsideArea = points.Where(point => AreaToCut.GetPixel(point).A == 0).ToList();

                HairNeedsRender = true;
                AreaToCutNeedsRender = true;
                Hair.Lock();
                AreaToCut.Lock();
                foreach (var point in points)
                {
                    Hair.ModifyImage(point, Color.FromArgb(0, 0, 0, 0));
                    AreaToCut.ModifyImage(point, Color.FromArgb(0, 0, 0, 0));
                }
                Hair.Unlock();
                AreaToCut.Unlock();

                if (pointsOutsideArea.Count() == 0)
                {
                    return;
                }

                FailedCutsNeedsRender = true;
                FailedCuts.Lock();
                foreach (var point in pointsOutsideArea)
                {
                    FailedCuts.ModifyImage(point, Color.FromArgb(255, 255, 0, 0));
                }
                FailedCuts.Unlock();
            });
        }

        private void EditorThreadLoop(object state)
        {
            Haircut haircut = (Haircut)state;

            void Rerender()
            {

                if (haircut.HairNeedsRender)
                {
                    haircut.Hair.RenderImage();
                    haircut.HairNeedsRender = false;
                }
                if (haircut.AreaToCutNeedsRender)
                {
                    haircut.AreaToCut.RenderImage();
                    haircut.AreaToCutNeedsRender = false;
                }
                if (haircut.FailedCutsNeedsRender)
                {
                    haircut.FailedCuts.RenderImage();
                    haircut.FailedCutsNeedsRender = false;
                }
            }

            int cycle = 0;

            while (true)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                while (haircut.EditActions.Count > 0)
                {
                    if (stopWatch.ElapsedMilliseconds > 1000)
                    {
                        Rerender();
                    }
                    var job = haircut.EditActions.Take();
                    job();
                }
                stopWatch.Stop();
                Rerender();
                cycle++;
                if (cycle >= 20)
                {
                    cycle = 0;

                    haircut.AreaToCut.Lock();
                    int completePercent = haircut.CalculateCompletePercent();
                    haircut.AreaToCut.Unlock();

                    haircut.FailedCuts.Lock();
                    int failPercent = haircut.CalculateFailPercent();
                    haircut.FailedCuts.Unlock();

                    haircut.SetPercents(completePercent, failPercent);
                }
                Thread.Sleep(100);
            }
        }

        private int CalculateCompletePercent()
        {
            int remainingPixels = AreaToCut.Count((color, point) => color.A != 0);
            double cutPixels = PixelsToCut - remainingPixels;
            double percent = (cutPixels / PixelsToCut) * 100;
            return (int)Math.Round(percent, 0);
        }

        private int CalculateFailPercent()
        {
            double failedPixels = FailedCuts.Count((color, point) => color.A != 0);
            double percent = (failedPixels / PixelsToLeave) * 100;
            return (int)Math.Floor(percent);
        }

        private void SetPercents(int completePercent, int failPercent)
        {
            if (IsDisposed)
            {
                return;
            }
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {

                CompletePercent = completePercent;
                FailPercent = failPercent;

            })).Wait();
        }
    }
}
