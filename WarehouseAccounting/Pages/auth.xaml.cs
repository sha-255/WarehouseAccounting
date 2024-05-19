using System.Windows;
using System.Windows.Controls;
using WarehouseAccounting.Domain.Context;

namespace WarehouseAccounting.Pages
{
    /// <summary>
    /// Interaction logic for auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        UsersContext cntx = new();

        public Auth()
        {
            InitializeComponent();
            LogIn.Click += (s, e) =>
            {
                try
                {
                    var user = cntx.Users.Find(Login.Text);
                    if (user == null)
                    {
                        MessageBox.Show("Не существующий логин", "Складской учёт", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (user.Password != Password.Password)
                    {
                        MessageBox.Show("Пароли не совпадают", "Складской учёт", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    NavigationService.Navigate(new Main());
                }
                catch
                {
                    MessageBox.Show("Неверно заполнены данные", "Складской учёт", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    Login.Clear();
                    Password.Clear();
                }
            };
        }
    }
}
