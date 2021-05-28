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
using System.Windows;
using GTA6Game.Languages;
using GTA6Game.PlayerData;
using GTA6Game.Routing;

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

        public DesiredShape Shape { get; }

        private Visibility hudVisibility;

        public Visibility HUDVisibility
        {
            get { return hudVisibility; }
            set
            {
                hudVisibility = value;
                OnPropertyChanged();
            }
        }

        private Haircut LastSide;

        private bool IsRoundEnded = false;

        private OverlaySettings OverlaySettings;

        private RoutingHelper Router;

        public event Action NewRoundStarted;

        public HaircutMinigameVM()
        {
            Shape = GetRandomShape();

            CameraOrientation = new CameraOrientation(Orientation.Top);
            CameraOrientation.PropertyChanged += OnCameraOrientationChanged;

            HaircutState = new HaircutState(Shape);
            HaircutState.PropertyChanged += OnHaircutStateChanged;

            CurrentSide.PropertyChanged += OnCurrentSideChanged;
            LastSide = CurrentSide;

            HUDVisibility = Visibility.Visible;
        }

        public void InjectOverlaySettings(OverlaySettings overlaySettings)
        {
            OverlaySettings = overlaySettings;
        }

        public void InjectRouter(RoutingHelper routingHelper)
        {
            Router = routingHelper;
        }

        public void Dispose()
        {
            DisposePropertyChangedEvent();
            CameraOrientation.Dispose();
            HaircutState.Dispose();
            NewRoundStarted = null;
        }

        public async void EndRound()
        {
            HUDVisibility = Visibility.Hidden;
            if (!IsRoundEnded)
            {
                IsRoundEnded = true;
                GameEndPayload payload = GetGameResult();
                SaveLoader.Save.SelectedProfile.Money += payload.EarnedMoney;
                GameEndModalContent modalContent = new GameEndModalContent(payload);
                var result = await OverlaySettings.OpenedModals.OpenModal(new Modal<GameEndUserDecision>(modalContent));
                switch (result.Payload)
                {
                    case GameEndUserDecision.BackToMenu:
                        Router.ChangeCurrentPage(new MinigameSelectionPage());
                        break;

                    case GameEndUserDecision.NewRound:
                        NewRoundStarted?.Invoke();
                        break;

                    default:
                        break;
                }
            }
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

        private GameEndPayload GetGameResult()
        {
            Dictionary<string, string> failTexts = new Dictionary<string, string>()
            {
                { "hu-HU", "Elrontottad Franklin álomfrizuráját! Tényleg azt hitted hogy ezért fizetni fog egy fillért is?" },
                { "en-US", "You messed up Franklin's haircut! You really think he would pay a cent for this?" }
            };

            Dictionary<string, string> incompleteTexts = new Dictionary<string, string>() 
            {
                { "hu-HU", "Be se fejezted a frizurát, nyilván nem fog Franklin ezért fizetni!" },
                { "en-US", "You didn't even complete the haircut, he won't pay you anything obviously!" }
            };

            Dictionary<string, string> didntPayTexts = new Dictionary<string, string>() 
            {
                { "hu-HU", "Franklin kirohant a boltból fizetés nélkül. Naiv gondolat, hogy azt hitted fizetni fog a munkádért!" },
                { "en-US", "Franklin stormed out of the shop. How naive you are! You really thought he would pay anything for his haircut?" }
            };

            Dictionary<string, string> franklinPaidTexts = new Dictionary<string, string>()
            {
                { "hu-HU","Franklin kifizette a munkádat! Nem sokat adott, de szerintem érd be ennyivel!" },
                { "en-US", "Franklin actually paid for his haircut! He didn't give much, but you shouldn't complain!" }
            };


            bool isFailed = HaircutState.FailPercent >= 40;
            string langCode = LanguageManager.CurrentCulture.IetfLanguageTag;

            if (isFailed)
            {
                return new GameEndPayload(0, HaircutState.FailPercent, failTexts[langCode], isFailed, Shape.Name);
            }

            if (HaircutState.CompletePercent < 98)
            {
                return new GameEndPayload(0, HaircutState.FailPercent, incompleteTexts[langCode],isFailed, Shape.Name);
            }

            Random r = new Random();
            bool franklinPaid = r.Next(1, 101) > 60;

            if (!franklinPaid)
            {
                return new GameEndPayload(0, HaircutState.FailPercent, didntPayTexts[langCode], isFailed, Shape.Name);
            }

            return new GameEndPayload(2000- r.Next(0, 501), HaircutState.FailPercent, franklinPaidTexts[langCode], isFailed, Shape.Name);
        }

        private DesiredShape GetRandomShape()
        {
            int rnd = new Random().Next(0, DesiredShape.Shapes.Count);
            return DesiredShape.Shapes.ToList()[rnd].Value;
        }

    }
}
