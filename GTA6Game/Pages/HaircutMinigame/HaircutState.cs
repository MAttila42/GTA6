using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GTA6Game.Helpers;

namespace GTA6Game.Pages.HaircutMinigame
{
    public class HaircutState : PropertyChangeNotifier, IDisposable
    {
        public Haircut Top { get; }

        public Haircut Front { get; }

        public Haircut Left { get; }

        public Haircut Rear { get; }

        public Haircut Right { get; }

        public int CompletePercent
        {
            get
            {
                double percent = (Top.CompletePercent + Front.CompletePercent + Left.CompletePercent + Rear.CompletePercent + Right.CompletePercent) / (double)5;
                return (int)Math.Round(percent);
            }
        }

        public int FailPercent
        {
            get
            {
                double percent = (Top.FailPercent + Front.FailPercent + Left.FailPercent + Rear.FailPercent + Right.FailPercent) / (double)5;
                return (int)Math.Round(percent);
            }
        }

        public HaircutState(DesiredShape shape)
        {
            Top = new Haircut(Orientation.Top, shape.Top);
            Top.PropertyChanged += OnTopChanged;

            Front = new Haircut(Orientation.Front, shape.Front);
            Front.PropertyChanged += OnFrontChanged;

            Left = new Haircut(Orientation.Left, shape.Left);
            Left.PropertyChanged += OnLeftChanged;

            Rear = new Haircut(Orientation.Rear, shape.Rear);
            Rear.PropertyChanged += OnRearChanged;

            Right = new Haircut(Orientation.Right, shape.Right);
            Right.PropertyChanged += OnRightChanged;
        }

        public void Dispose()
        {
            DisposePropertyChangedEvent();
            Top.Dispose();
            Front.Dispose();
            Left.Dispose();
            Rear.Dispose();
            Right.Dispose();
        }

        private void OnTopChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Haircut.CompletePercent))
            {
                OnPropertyChanged(nameof(CompletePercent));
            }
            if (e.PropertyName == nameof(Haircut.FailPercent))
            {
                OnPropertyChanged(nameof(FailPercent));
            }
            OnPropertyChanged(nameof(Top), nameof(Top) + "." + e.PropertyName);
        }

        private void OnFrontChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Haircut.CompletePercent))
            {
                OnPropertyChanged(nameof(CompletePercent));
            }
            if (e.PropertyName == nameof(Haircut.FailPercent))
            {
                OnPropertyChanged(nameof(FailPercent));
            }
            OnPropertyChanged(nameof(Front), nameof(Front) + "." + e.PropertyName);
        }

        private void OnLeftChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Haircut.CompletePercent))
            {
                OnPropertyChanged(nameof(CompletePercent));
            }
            if (e.PropertyName == nameof(Haircut.FailPercent))
            {
                OnPropertyChanged(nameof(FailPercent));
            }
            OnPropertyChanged(nameof(Left), nameof(Left) + "." + e.PropertyName);
        }

        private void OnRearChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Haircut.CompletePercent))
            {
                OnPropertyChanged(nameof(CompletePercent));
            }
            if (e.PropertyName == nameof(Haircut.FailPercent))
            {
                OnPropertyChanged(nameof(FailPercent));
            }
            OnPropertyChanged(nameof(Rear),nameof(Rear) + "." + e.PropertyName);
        }

        private void OnRightChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Haircut.CompletePercent))
            {
                OnPropertyChanged(nameof(CompletePercent));
            }
            if (e.PropertyName == nameof(Haircut.FailPercent))
            {
                OnPropertyChanged(nameof(FailPercent));
            }
            OnPropertyChanged(nameof(Right),nameof(Right) + "." + e.PropertyName);
        }
    }
}
