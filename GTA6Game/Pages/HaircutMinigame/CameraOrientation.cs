using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GTA6Game.Pages.HaircutMinigame
{
    public class CameraOrientation : INotifyPropertyChanged, IDisposable
    {
        private static List<Orientation> HorizontalOrientations = new List<Orientation>
        {
            Orientation.Front,
            Orientation.Left,
            Orientation.Rear,
            Orientation.Right
        };

        private Orientation currentOrientation;

        public Orientation CurrentOrientation
        {
            get { return currentOrientation; }
            set
            {
                if (value == currentOrientation)
                {
                    return;
                }
                Orientation previousValue = currentOrientation;
                currentOrientation = value;
                OnPropertyChanged();
                if (currentOrientation != Orientation.Top)
                {
                    IsUpEnabled = true;
                    IsDownEnabled = false;
                    IsLeftEnabled = true;
                    IsRightEnabled = true;
                }
                else
                {
                    IsUpEnabled = false;
                    IsDownEnabled = true;
                    IsLeftEnabled = false;
                    IsRightEnabled = false;
                }
                if (previousValue != previousOrientation)
                {
                    PreviousOrientation = previousValue;
                }
            }
        }

        private Orientation previousOrientation;

        public Orientation PreviousOrientation
        {
            get { return previousOrientation; }
            private set
            {
                if (value == previousOrientation)
                {
                    return;
                }
                previousOrientation = value;
                OnPropertyChanged();
            }
        }

        private bool isUpEnabled;

        public bool IsUpEnabled
        {
            get { return isUpEnabled; }
            set
            {
                if (value == isUpEnabled)
                {
                    return;
                }
                isUpEnabled = value; 
                OnPropertyChanged();
            }
        }

        private bool isDownEnabled;

        public bool IsDownEnabled
        {
            get { return isDownEnabled; }
            set
            {
                if (value == isDownEnabled)
                {
                    return;
                }
                isDownEnabled = value; 
                OnPropertyChanged();
            }
        }

        private bool isLeftEnabled;

        public bool IsLeftEnabled
        {
            get { return isLeftEnabled; }
            set
            {
                if (value == isLeftEnabled)
                {
                    return;
                }
                isLeftEnabled = value; 
                OnPropertyChanged();
            }
        }

        private bool isRightEnabled;

        public bool IsRightEnabled
        {
            get { return isRightEnabled; }
            set
            {
                if (value == isRightEnabled)
                {
                    return;
                }
                isRightEnabled = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CameraOrientation(Orientation initialOrientation)
        {
            IsUpEnabled = false;
            IsDownEnabled = true;
            IsLeftEnabled = false;
            IsRightEnabled = false;
            CurrentOrientation = initialOrientation;
            PreviousOrientation = initialOrientation;
        }

        public void MoveCamera(Direction direction)
        {

            bool isUpViolated = !IsUpEnabled && direction == Direction.Up;
            bool isDownViolated = !IsDownEnabled && direction == Direction.Down;
            bool isLeftViolated = !IsLeftEnabled && direction == Direction.Left;
            bool isRightViolated = !IsRightEnabled && direction == Direction.Right;
            if (isUpViolated || isDownViolated || isLeftViolated || isRightViolated)
            {
                return;
            }

            if (direction == Direction.Up || direction == Direction.Down)
            {
                PerformVerticalMovement(direction);
            }
            else
            {
                PerformHorizontalMovement(direction);
            }
        }

        public void Dispose()
        {
            PropertyChanged = null;
        }

        private void PerformVerticalMovement(Direction direction)
        {
            if (direction == Direction.Up)
            {
                CurrentOrientation = Orientation.Top;
            }
            else
            {
                var previous = PreviousOrientation == Orientation.Top ? Orientation.Front : PreviousOrientation;
                CurrentOrientation = previous;
            }
        }

        private void PerformHorizontalMovement(Direction direction)
        {
            int currentIndex = HorizontalOrientations.FindIndex(x => x == CurrentOrientation);
            int directionIndex = direction == Direction.Left ? 1 : -1;

            int i = currentIndex + directionIndex;

            if (currentIndex == 0 && directionIndex == -1)
            {
                i = 3;
            }

            if (currentIndex == 3 && directionIndex == 1)
            {
                i = 0;
            }

            CurrentOrientation = HorizontalOrientations[i];
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
