using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CinemaApp.Pages
{
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            bool result = Register(TbFullName.Text, TbLogin.Text, PbPassword.Password);

            if (result)
            {
                MessageBox.Show("Регистрация успешна!");
                NavigationService.Navigate(new MainPage());
            }
        }

        public bool Register(string fullName, string login, string password)
        {
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                return false;

            using (var db = new CinemaDBEntities())
            {
                var existing = db.Users.FirstOrDefault(u => u.Login == login);
                if (existing != null)
                    return false;

                Users newUser = new Users
                {
                    Login = login.Trim(),
                    Password = password,
                    FullName = fullName.Trim()
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                Core.CurrentUser = newUser;
                return true;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}