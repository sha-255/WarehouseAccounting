using Microsoft.EntityFrameworkCore;
using WarehouseAccounting.Domain.Data;

namespace WarehouseAccounting.Domain.Context
{
    public class AccessoriesContext : CustomContext
    {
        public DbSet<Accessorie> Accessories { get; set; }
    }
}
