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
            Auth(TbLogin.Text, PbPassword.Password);
        }

        public bool Auth(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните все поля!");
                return false;
            }

            using (var db = new CinemaDBEntities())
            {
                var user = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

                if (user == null)
                {
                    MessageBox.Show("Неверный логин или пароль!");
                    return false;
                }

                Core.CurrentUser = user;
                MessageBox.Show("Добро пожаловать, " + user.FullName);
                return true;
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