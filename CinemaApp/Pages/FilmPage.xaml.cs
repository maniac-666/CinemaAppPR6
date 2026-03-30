using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CinemaApp.Pages
{
    public partial class FilmPage : Page
    {
        private Films film;

        public FilmPage(Films f)
        {
            InitializeComponent();
            film = f;

            TbTitle.Text = film.Title;
            TbInfo.Text = "Рейтинг: " + film.Rating + " | " + film.AgeRating + " | " + film.Genre;
            TbDescription.Text = film.Description;

            LbSessions.ItemsSource = Core.Context.Sessions
                .Where(s => s.FilmId == film.Id).ToList();
        }

        private void LbSessions_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Sessions session = LbSessions.SelectedItem as Sessions;
            if (session == null) return;

            if (Core.CurrentUser == null)
            {
                MessageBox.Show("Сначала войдите в аккаунт!");
                NavigationService.Navigate(new LoginPage());
                return;
            }

            NavigationService.Navigate(new SessionPage(session, film));
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
