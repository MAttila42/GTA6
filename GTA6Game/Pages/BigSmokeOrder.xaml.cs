﻿using GTA6Game.PlayerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace GTA6Game.Pages
{
    class Order
    {
        public string Text { get; set; }
        public int Price { get; set; }

        public Order(string text, int price)
        {
            Text = text;
            Price = price;
        }
    }
    /// <summary>
    /// Interaction logic for BigSmokeOrder.xaml
    /// </summary>
    public partial class BigSmokeOrder : PageBase
    {
        public BigSmokeOrder()
        {
            InitializeComponent();
        }

        static Order[] Menus =
        {
            new Order("The Big Meal", 2990),
            new Order("The Huge Meal", 4490),
            new Order("The Balls and Rings Meal", 1490),
            new Order("The Farmer's Surprise Meal", 3990),
            new Order("The Fowl Burger Meal", 1590),
            new Order("The Mighty Cluck Meal", 3990),
            new Order("The V. Eggy Meal", 1190),
            new Order("The Veggie Wings Meal", 2390),
            new Order("The Wing Piece Meal", 1290)
        };

        static Order[] Foods =
        {
            new Order("Number 1", 290),
            new Order("Number 1 Large", 490),
            new Order("Number 2", 550),
            new Order("Number 2 Large", 750),
            new Order("Number 3", 430),
            new Order("Number 3 Large", 730),
            new Order("Number 4", 380),
            new Order("Number 4 Large", 570),
            new Order("Number 5", 310),
            new Order("Number 5 Large", 590),
            new Order("Number 6", 490),
            new Order("Number 6 Large", 690),
            new Order("Number 7", 540),
            new Order("Number 7 Large", 940),
            new Order("Number 8", 190),
            new Order("Number 8 Large", 790),
            new Order("Number 9", 390),
            new Order("Number 9 Large", 590),
            new Order("Number 12", 610),
            new Order("Number 12 Large", 710),
            new Order("Number 29", 590),
            new Order("Number 29 Large", 680),
            new Order("Number 33", 240),
            new Order("Number 33 Large", 450),
            new Order("Number 34", 360),
            new Order("Number 34 Large", 590),
            new Order("Number 45", 370),
            new Order("Number 45 Large", 560),
            new Order("Number 49", 440),
            new Order("Number 49 Large", 630),
            new Order("Number 50", 510),
            new Order("Number 50 Large", 1290)
        };
        static Order[] Extras =
        {
            new Order(" with Clucksauce", 290),
            new Order(" with cheese", 90),
            new Order(" with extra dip", 60)
        };

        static Order[] Drinks =
        {
            new Order("a Big Soda", 490),
            new Order("a Large Soda", 590),
            new Order("a Bowl of Ice Cream", 690)
        };

        static bool Idle = true;
        static List<Order> Orders = new List<Order>();

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            Idle = false;
            Orders.Clear();
            FullPrice = 0;
            Random r = new Random();
            lbNote.Content = "";
            Orders.Add(Menus[r.Next(0, Menus.Length)]);

            var a = new List<int>();
            for (int i = 0; i < r.Next(1, Foods.Length); i++)
            {
                var cr = r.Next(0, Foods.Length);
                if (!a.Contains(cr))
                    Orders.Add(new Order(Foods[cr].Text, Foods[cr].Price));
                a.Add(cr);
            }

            for (int i = 1; i < Orders.Count; i++)
            {
                if (r.Next(0, 3) == 0)
                {
                    Orders[i].Text = $"2 {Orders[i].Text}s";
                    Orders[i].Price *= 2;
                }
                else
                    Orders[i].Text = $"a {Orders[i].Text}";
            }

            for (int i = 1; i < Orders.Count; i++)
            {
                if (r.Next(0, 3) == 0)
                {
                    int cr = r.Next(0, Extras.Length);
                    Orders[i].Text += Extras[cr].Text;
                    Orders[i].Price += Orders[i].Text.StartsWith("2") ? 2 * Extras[cr].Price : Extras[cr].Price;
                }
            }

            a = new List<int>();
            for (int i = 0; i < r.Next(1, Drinks.Length); i++)
            {
                var cr = r.Next(0, Drinks.Length);
                if (!a.Contains(cr))
                    Orders.Add(new Order(Drinks[cr].Text, Drinks[cr].Price));
                a.Add(cr);
            }

            Orders.Last().Text = $"and {Orders.Last().Text}";

            lbOrder.Content = Orders[0].Text;
            
            Timer(Orders.Sum(x => x.Text.Length) * 350);
        }

        DispatcherTimer _timer;
        TimeSpan _time;

        private void Timer(int ms)
        {
            _time = TimeSpan.FromSeconds(ms / 1000);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                lbTimer.Content = _time.ToString(@"mm\:ss");
                if (_time == TimeSpan.Zero)
                {
                    lbTimer.Content = "00:00";
                    lbOrder.Content = "";
                    lbNote.Content = "";
                    btnStart.IsEnabled = true;
                    Idle = true;
                    PopupWindow("Vesztettél!", "Majd legközelebb.");
                    _timer.Stop();
                }
                else if (Idle)
                {
                    lbTimer.Content = "00:00";
                    _timer.Stop();
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }

        static int FullPrice = 0;
        private void tbInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Idle)
            {
                try
                {
                    if (tbInput.Text == Orders[0].Text)
                    {
                        tbInput.Text = "";
                        lbNote.Content += $"{Orders[0].Text} - {Orders[0].Price} Ft\n";
                        FullPrice += Orders[0].Price;
                        Orders.RemoveAt(0);
                        lbOrder.Content = Orders[0].Text;
                    }
                }
                catch (Exception)
                {
                    lbOrder.Content = "";
                    lbNote.Content += $"\nÖsszesen: {FullPrice} Ft";
                    UpdateMoney(FullPrice);
                    PopupWindow("Nyertél!", $"Nyeremény: {FullPrice} Ft");
                    btnStart.IsEnabled = true;
                    Idle = true;
                }
            }
        }

        private void PopupWindow(string title, string desc)
        {
            frBackgroundDisabler.Visibility = Visibility.Visible;
            frPopupBackground.Visibility = Visibility.Visible;
            lbPopupTitle.Visibility = Visibility.Visible;
            lbPopupDesc.Visibility = Visibility.Visible;
            btnOk.Visibility = Visibility.Visible;
            lbPopupTitle.Content = title;
            lbPopupDesc.Content = desc;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Router.ChangeCurrentPage(new MinigameSelectionPage());
        }

        private void UpdateMoney(int plusz)
        {
            SaveLoader.Save.SelectedProfile.Money += plusz;
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMoney(0);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            frBackgroundDisabler.Visibility = Visibility.Hidden;
            frPopupBackground.Visibility = Visibility.Hidden;
            lbPopupTitle.Visibility = Visibility.Hidden;
            lbPopupDesc.Visibility = Visibility.Hidden;
            btnOk.Visibility = Visibility.Hidden;
        }
    }
}
