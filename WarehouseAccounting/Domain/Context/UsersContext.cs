using Microsoft.EntityFrameworkCore;
using WarehouseAccounting.Domain.Data;

namespace WarehouseAccounting.Domain.Context
{
    public class UsersContext : CustomContext
    {
        public DbSet<User> Users { get; set; }
    }
}
