using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CinemaApp.Pages
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = TbLogin.Text.Trim();
            string password = PbPassword.Password;

            if (login == "" || password == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            var user = Core.Context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user != null)
            {
                Core.CurrentUser = user;
                MessageBox.Show("Добро пожаловать, " + user.FullName);
                NavigationService.Navigate(new MainPage());
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
