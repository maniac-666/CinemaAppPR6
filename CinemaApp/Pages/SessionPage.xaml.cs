using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CinemaApp.Pages
{
    public partial class SessionPage : Page
    {
        private Sessions session;
        private Films film;
        private int selRow = -1;
        private int selSeat = -1;
        private Button selBtn = null;
        private List<Tuple<int, int>> occupied;

        public SessionPage(Sessions s, Films f)
        {
            InitializeComponent();
            session = s;
            film = f;

            TbInfo.Text = film.Title + " | " +
                ((DateTime)session.SessionDate).ToString("dd.MM.yyyy") + " " +
                session.SessionTime + " | Зал " + session.Halls.Number +
                " (" + session.Halls.Classification + ") | " + session.Price + " руб.";

            occupied = Core.Context.Tickets
                .Where(t => t.SessionId == session.Id)
                .ToList()
                .Select(t => Tuple.Create(t.SeatRow, t.SeatNumber))
                .ToList();

            int rows = session.Halls.RowCount ?? 5;
            int seats = session.Halls.SeatsPerRow ?? 8;

            for (int r = 1; r <= rows; r++)
            {
                StackPanel rowPanel = new StackPanel();
                rowPanel.Orientation = Orientation.Horizontal;
                rowPanel.Margin = new Thickness(0, 2, 0, 2);

                TextBlock lbl = new TextBlock();
                lbl.Text = "Ряд " + r + "  ";
                lbl.Width = 60;
                lbl.VerticalAlignment = VerticalAlignment.Center;
                rowPanel.Children.Add(lbl);

                for (int s2 = 1; s2 <= seats; s2++)
                {
                    Button btn = new Button();
                    btn.Width = 35;
                    btn.Height = 30;
                    btn.Margin = new Thickness(2);
                    btn.Content = s2.ToString();
                    btn.Tag = r + "," + s2;

                    bool taken = occupied.Any(x => x.Item1 == r && x.Item2 == s2);
                    if (taken)
                    {
                        btn.Background = Brushes.IndianRed;
                        btn.IsEnabled = false;
                    }
                    else
                    {
                        btn.Background = Brushes.LightGreen;
                        btn.Click += Seat_Click;
                    }

                    rowPanel.Children.Add(btn);
                }
                SeatsPanel.Children.Add(rowPanel);
            }
        }

        private void Seat_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string[] parts = btn.Tag.ToString().Split(',');

            if (selBtn != null)
                selBtn.Background = Brushes.LightGreen;

            btn.Background = Brushes.Yellow;
            selBtn = btn;
            selRow = int.Parse(parts[0]);
            selSeat = int.Parse(parts[1]);
            BtnBook.IsEnabled = true;
        }

        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            if (selRow < 0) return;
            NavigationService.Navigate(new BookingPage(session, film, selRow, selSeat));
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FilmPage(film));
        }
    }
}
