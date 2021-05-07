using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace GTA6Game.Pages.HaircutMinigame
{
    public class HaircutMinigameVM : INotifyPropertyChanged, IDisposable
    {
        public CameraOrientation CameraOrientation { get; }

        public HaircutState HaircutState { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public HaircutMinigameVM()
        {
            CameraOrientation = new CameraOrientation(Orientation.Top);
            CameraOrientation.PropertyChanged += OnCameraOrientationChanged;

            HaircutState = new HaircutState();
        }

        private void OnCameraOrientationChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CameraOrientation) + "." + e.PropertyName);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            PropertyChanged = null;
            CameraOrientation?.Dispose();
        }
    }
}
