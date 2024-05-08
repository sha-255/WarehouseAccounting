using System.ComponentModel.DataAnnotations;

namespace WarehouseAccounting.Domain.Data
{
    public class Accessorie
    {
        [Key]
        public int? Id { get; set; }

        private string? name;
        public string Name
        {
            get => name.Normalize().Trim();
            set => name = value;
        }

        private string? material;
        public string Material
        {
            get => material.Normalize().Trim();
            set => material = value;
        }

        public int Price { get; set; }

        public int Quantity { get; set; }

        private string? warehouse;
        public string Warehouse
        {
            get => warehouse.Normalize().Trim();
            set => warehouse = value;
        }
    }
}
