using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CinemaApp.Pages
{
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();

            TbName.Text = "ФИО: " + Core.CurrentUser.FullName;
            TbLogin.Text = "Логин: " + Core.CurrentUser.Login;

            LbTickets.ItemsSource = Core.Context.Tickets
                .Where(t => t.UserId == Core.CurrentUser.Id).ToList();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            Core.CurrentUser = null;
            MessageBox.Show("Вы вышли");
            NavigationService.Navigate(new MainPage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
