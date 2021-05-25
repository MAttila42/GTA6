using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using GTA6Game.Helpers;
using GTA6Game.UserControls.Overlay;
using GTA6Game.UserControls.Overlay.Modal;
using GTA6Game.Pages.HaircutMinigame.UserControls;

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

        public DesiredShape Shape { get; }

        private bool IsRoundEnded = false;

        private OverlaySettings OverlaySettings;

        public HaircutMinigameVM()
        {
            Shape = GetRandomShape();

            CameraOrientation = new CameraOrientation(Orientation.Top);
            CameraOrientation.PropertyChanged += OnCameraOrientationChanged;

            HaircutState = new HaircutState(Shape);
            HaircutState.PropertyChanged += OnHaircutStateChanged;

            CurrentSide.PropertyChanged += OnCurrentSideChanged;
            LastSide = CurrentSide;
        }

        public void InjectOverlaySettings(OverlaySettings overlaySettings)
        {
            OverlaySettings = overlaySettings;
        }

        public void Dispose()
        {
            DisposePropertyChangedEvent();
            CameraOrientation.Dispose();
            HaircutState.Dispose();
        }

        public async void EndRound()
        {
            if (!IsRoundEnded)
            {
                bool isFailed = HaircutState.FailPercent >= 40;
                GameEndPayload payload = new GameEndPayload(CalculateReward(), HaircutState.FailPercent, "", isFailed);
                GameEndModalContent modalContent = new GameEndModalContent(payload);
                await OverlaySettings.OpenedModals.OpenModal(new Modal<object>(modalContent));
            }
            IsRoundEnded = true;
        }

        private void OnCurrentSideChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentSide), GetNestedPropertyName(nameof(CurrentSide), e));
        }

        private void OnHaircutStateChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HaircutState), GetNestedPropertyName(nameof(HaircutState), e));
            if (e.PropertyName == nameof(HaircutState.CompletePercent))
            {
                OnCompletePercentChanged();
            }
            if (e.PropertyName == nameof(HaircutState.FailPercent))
            {
                OnFailPercentChanged();
            }
        }

        private void OnCompletePercentChanged()
        {
            if (HaircutState.CompletePercent >= 98)
            {
                EndRound();
            }
        }

        private void OnFailPercentChanged()
        {
            if (HaircutState.FailPercent >= 40)
            {
                EndRound();
            }
        }

        private void OnCameraOrientationChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CameraOrientation), GetNestedPropertyName(nameof(CameraOrientation), e));

            if (e.PropertyName == nameof(CameraOrientation.CurrentOrientation))
            {
                OnPropertyChanged(nameof(CurrentSide));

                LastSide.PropertyChanged -= OnCurrentSideChanged;
                LastSide.Hair.DetachCanvas();

                CurrentSide.PropertyChanged += OnCurrentSideChanged;
                LastSide = CurrentSide;
            }
        }

        private DesiredShape GetRandomShape()
        {
            int rnd = new Random().Next(0, DesiredShape.Shapes.Count);
            return DesiredShape.Shapes.ToList()[rnd].Value;
        }

        private int CalculateReward()
        {
            return 0;
        }
    }
}
