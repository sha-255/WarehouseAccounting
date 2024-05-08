using Microsoft.EntityFrameworkCore;
using WarehouseAccounting.Domain.Data;

namespace WarehouseAccounting.Domain.Context
{
    public class WarehouseContext : CustomContext
    {
        public DbSet<Warehouse> Warehouses { get; set; }
    }
}
