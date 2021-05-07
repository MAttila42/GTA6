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

        public HaircutState()
        {
            Top = new Haircut(Orientation.Top);
            Top.PropertyChanged += OnTopChanged;

            Front = new Haircut(Orientation.Front);
            Front.PropertyChanged += OnFrontChanged;

            Left = new Haircut(Orientation.Left);
            Left.PropertyChanged += OnLeftChanged;

            Rear = new Haircut(Orientation.Rear);
            Rear.PropertyChanged += OnRearChanged;

            Right = new Haircut(Orientation.Right);
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
            OnPropertyChanged(nameof(Top) + "." + e.PropertyName);
        }

        private void OnFrontChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Front) + "." + e.PropertyName);
        }

        private void OnLeftChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Left) + "." + e.PropertyName);
        }

        private void OnRearChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Rear) + "." + e.PropertyName);
        }

        private void OnRightChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Right) + "." + e.PropertyName);
        }
    }
}
