using System;
using System.Windows;
using System.Windows.Controls;

namespace CinemaApp.Pages
{
    public partial class BookingPage : Page
    {
        private Sessions session;
        private Films film;
        private int row;
        private int seat;
        //
        public BookingPage(Sessions s, Films f, int r, int seatNum)
        {
            InitializeComponent();
            session = s;
            film = f;
            row = r;
            seat = seatNum;

            TbFilm.Text = film.Title;
            TbHall.Text = "Зал " + session.Halls.Number + " (" + session.Halls.Classification + ")";
            TbDateTime.Text = ((DateTime)session.SessionDate).ToString("dd.MM.yyyy") + " " + session.SessionTime;
            TbSeat.Text = "Ряд " + row + ", Место " + seat;
            TbPrice.Text = session.Price + " руб.";
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Tickets ticket = new Tickets
            {
                UserId = Core.CurrentUser.Id,
                SessionId = session.Id,
                SeatRow = row,
                SeatNumber = seat,
                Price = session.Price,
                PurchaseDate = DateTime.Now
            };

            Core.Context.Tickets.Add(ticket);
            Core.Context.SaveChanges();

            MessageBox.Show("Билет куплен!");
            NavigationService.Navigate(new MainPage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
