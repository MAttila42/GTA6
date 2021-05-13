using GTA6Game.Helpers;
using GTA6Game.Pages.HaircutMinigame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GTA6Game.UserControls
{
    /// <summary>
    /// Interaction logic for FranklinDisplay.xaml
    /// </summary>
    public partial class FranklinDisplay : UserControl
    {
        private bool IsCuttingInProgress = false;

        private int Radius = 5;

        public FranklinDisplay()
        {
            InitializeComponent();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var vm = (HaircutMinigameVM)DataContext;
            vm.PropertyChanged += OnViewModelPropertyChanged;
            AttachHairCanvas();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var vm = (HaircutMinigameVM)DataContext;
            if (e.PropertyName == nameof(vm.CurrentSide))
            {
                OnCurrentSideChanged(sender, e);
            }
        }

        private void OnCurrentSideChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(e is NestedPropertyChangedEventArgs))
            {
                AttachHairCanvas();
            }
        }

        private void AttachHairCanvas()
        {
            var vm = (HaircutMinigameVM)DataContext;
            vm.CurrentSide.Hair.SetCanvas(HairCanvas);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                IsCuttingInProgress = true;
            }

        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                IsCuttingInProgress = false;
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsCuttingInProgress)
            {
                Grid grid = (Grid)sender;
                var vm = (HaircutMinigameVM)DataContext;

                var position = e.GetPosition(grid);
                int x = Math.Min((int)position.X, vm.CurrentSide.Hair.Bmp.Width - 1);
                int y = Math.Min((int)position.Y, vm.CurrentSide.Hair.Bmp.Width - 1);

                var points = GetPointsOfCircle(Radius, new System.Drawing.Point(x, y));

                vm.CurrentSide.Cut(points);
            }
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            IsCuttingInProgress = false;
        }

        private List<System.Drawing.Point> GetPointsOfCircle(int radius, System.Drawing.Point origo)
        {
            List<System.Drawing.Point> points = new List<System.Drawing.Point>();
            for (int i = 0; i <= radius; i++)
            {
                for (int j = 0; j <= radius - i; j++)
                {
                    points.Add(new System.Drawing.Point(origo.X + i, origo.Y + j));

                    if (i != 0)
                    {
                        points.Add(new System.Drawing.Point(origo.X - i, origo.Y + j));
                    }

                    if (j != 0)
                    {
                        points.Add(new System.Drawing.Point(origo.X + i, origo.Y - j));
                    }

                    if (i != 0 && j != 0)
                    {
                        points.Add(new System.Drawing.Point(origo.X - i, origo.Y - j));
                    }
                }
            }

            return points;
        }

      
    }
}
