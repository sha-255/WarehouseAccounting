using Microsoft.EntityFrameworkCore;
using WarehouseAccounting.Common;

namespace WarehouseAccounting.Domain.Context
{
    public class CustomContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(Settings.SqlConnection);
    }
}
