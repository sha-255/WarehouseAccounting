using Microsoft.EntityFrameworkCore;
using WarehouseAccounting.Domain.Data;

namespace WarehouseAccounting.Domain.Context
{
    public class WarehousesContext : CustomContext
    {
        public DbSet<Warehouse> Warehouses { get; set; }
    }
}
