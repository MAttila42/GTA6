using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            new Order("The Balls and Rings", 1490),
            new Order("The Farmer's Surprise", 3990),
            new Order("The Fowl Burger", 1590),
            new Order("The Mighty Cluck", 3990),
            new Order("The V. Eggy", 1190),
            new Order("The Veggie Wings", 2390),
            new Order("The Wing Piece", 1290)
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
            Random r = new Random();
            lbNote.Content = "";
            Orders.Add(Menus[r.Next(0, Menus.Length)]);

            var a = new List<int>();
            for (int i = 0; i < r.Next(1, Foods.Length); i++)
            {
                var cr = r.Next(0, Foods.Length);
                if (!a.Contains(cr))
                    Orders.Add(Foods[cr]);
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
                    Orders.Add(Drinks[cr]);
                a.Add(cr);
            }

            Orders.Last().Text = $"and {Orders.Last().Text}";

            lbOrder.Content = Orders[0].Text;
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
                    btnStart.IsEnabled = true;
                    Idle = true;
                }
            }
        }
    }
}
