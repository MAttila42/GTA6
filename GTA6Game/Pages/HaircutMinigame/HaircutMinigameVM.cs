using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using GTA6Game.Helpers;

namespace GTA6Game.Pages.HaircutMinigame
{
    public class HaircutMinigameVM : PropertyChangeNotifier, IDisposable
    {
        public CameraOrientation CameraOrientation { get; }

        public HaircutState HaircutState { get; }

        public Haircut CurrentSide
        {
            get
            {
                switch (CameraOrientation.CurrentOrientation)
                {
                    case Orientation.Top:
                        return HaircutState.Top;

                    case Orientation.Left:
                        return HaircutState.Left;

                    case Orientation.Right:
                        return HaircutState.Right;

                    case Orientation.Front:
                        return HaircutState.Front;

                    case Orientation.Rear:
                        return HaircutState.Rear;

                    default:
                        return null;
                }
            }
        }

        private Haircut LastSide;

        public HaircutMinigameVM()
        {
            CameraOrientation = new CameraOrientation(Orientation.Top);
            CameraOrientation.PropertyChanged += OnCameraOrientationChanged;

            HaircutState = new HaircutState();
            HaircutState.PropertyChanged += OnHaircutStateChanged;

            CurrentSide.PropertyChanged += OnCurrentSideChanged;
            LastSide = CurrentSide;
        }

        public void Dispose()
        {
            DisposePropertyChangedEvent();
            CameraOrientation.Dispose();
            HaircutState.Dispose();
        }

        private void OnCurrentSideChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentSide), GetNestedPropertyName(nameof(CurrentSide), e));
        }

        private void OnHaircutStateChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HaircutState), GetNestedPropertyName(nameof(HaircutState), e));
        }

        private void OnCameraOrientationChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CameraOrientation), GetNestedPropertyName(nameof(CameraOrientation), e));

            if (e.PropertyName == nameof(CameraOrientation.CurrentOrientation))
            {
                OnPropertyChanged(nameof(CurrentSide));
                LastSide.PropertyChanged -= OnCurrentSideChanged;
                CurrentSide.PropertyChanged += OnCurrentSideChanged;
                LastSide = CurrentSide;
            }
        }

    }
}
