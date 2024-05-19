using System.ComponentModel.DataAnnotations;

namespace WarehouseAccounting.Domain.Data
{
    public class User
    {
        private string login;
        [Key]
        public string Login
        {
            get => login.Normalize().Trim();
            set => login = value;
        }

        private string password;
        public string Password
        {
            get => password.Normalize().Trim();
            set => password = value;
        }
    }
}
