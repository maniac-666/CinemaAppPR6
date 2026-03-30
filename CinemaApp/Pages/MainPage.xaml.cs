using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CinemaApp.Pages
{
    public partial class MainPage : Page
    {
        private List<Films> allFilms;

        public MainPage()
        {
            InitializeComponent();
            allFilms = Core.Context.Films.ToList();
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            List<Films> result = allFilms;

            string search = TbSearch.Text.Trim().ToLower();
            if (search != "")
                result = result.Where(f => f.Title.ToLower().Contains(search)).ToList();

            if (CbSort.SelectedIndex == 1)
                result = result.OrderBy(f => f.Title).ToList();
            else if (CbSort.SelectedIndex == 2)
                result = result.OrderByDescending(f => f.Rating).ToList();

            LbFilms.ItemsSource = result;
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (allFilms != null) ApplyFilters();
        }

        private void LbFilms_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Films film = LbFilms.SelectedItem as Films;
            if (film != null)
                NavigationService.Navigate(new FilmPage(film));
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            if (Core.CurrentUser == null)
                NavigationService.Navigate(new LoginPage());
            else
                NavigationService.Navigate(new ProfilePage());
        }
    }
}
