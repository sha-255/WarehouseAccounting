using Microsoft.EntityFrameworkCore;
using WarehouseAccounting.Domain.Data;

namespace WarehouseAccounting.Domain.Context
{
    public class MaterialsForFurnitureContext : CustomContext
    {
        public DbSet<MaterialsForFurniture> MaterialsForFurnitures { get; set; }
    }
}
