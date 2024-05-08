namespace WarehouseAccounting.Domain.Data
{
    public class Warehouse
    {
        public int Id { get; set; }
        private string name;
        public string Name
        {
            get => name.Normalize().Trim();
            set => name = value;
        }
        private string responsible;
        public string Responsible
        {
            get => responsible.Normalize().Trim();
            set => responsible = value;
        }
        private string address;
        public string Address
        {
            get => address.Normalize().Trim();
            set => address = value;
        }
    }
}
