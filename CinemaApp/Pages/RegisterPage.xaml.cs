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
            if (TbFullName.Text == "" || TbLogin.Text == "" || PbPassword.Password == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            Users newUser = new Users
            {
                Login = TbLogin.Text.Trim(),
                Password = PbPassword.Password,
                FullName = TbFullName.Text.Trim()
            };

            Core.Context.Users.Add(newUser);
            Core.Context.SaveChanges();

            Core.CurrentUser = newUser;
            MessageBox.Show("Готово!");
            NavigationService.Navigate(new MainPage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}
