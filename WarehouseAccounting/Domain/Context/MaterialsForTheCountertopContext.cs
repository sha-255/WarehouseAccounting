using Microsoft.EntityFrameworkCore;
using WarehouseAccounting.Domain.Data;

namespace WarehouseAccounting.Domain.Context
{
    public class MaterialsForTheCountertopContext : CustomContext
    {
        public DbSet<MaterialsForTheCountertop> MaterialsForTheCountertops { get; set; }
    }
}
