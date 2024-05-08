using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using WarehouseAccounting.Domain.Context;
using WarehouseAccounting.Domain.Data;

namespace WarehouseAccounting.Pages
{
    public partial class Main : Page
    {
        AccessoriesContext cntx = new();

        public Main()
        {
            InitializeComponent();
            ApplyView();
            Add.Click += (s, e) => OnAdd();
            Update.Click += (s, e) => OnUpdate();
            Remove.Click += (s, e) => OnRemove();
        }

        private void ApplyView() => ApplyAccessoriesView(cntx.Accessories.ToListAsync());

        private async void ApplyAccessoriesView(Task<List<Accessorie>> readers)
            => AccessoriesView.ItemsSource = await readers;

        private async void OnAdd()
        {
            try
            {
                var dto = new Accessorie()
                {
                    Id = null,
                    Name = AddName.Text,
                    Material = AddMaterial.Text,
                    Price = int.Parse(AddPrice.Text),
                    Quantity = int.Parse(AddQuantity.Text),
                    Warehouse = AddWarehouse.Text,
                };
                await cntx.Accessories.AddAsync(dto);
                await cntx.SaveChangesAsync();
                ApplyView();
            }
            catch
            {
                MessageBox.Show("Неверно заполнены данные", "Складской учёт", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                AddName.Clear();
                AddMaterial.Clear();
                AddPrice.Clear();
                AddQuantity.Clear();
                AddWarehouse.Clear();
            }
        }

        private void OnUpdate()
        {
            try
            {
                var dto = new Accessorie()
                {
                    Id = int.Parse(UpdateId.Text),
                    Name = UpdateName.Text,
                    Material = UpdateMaterial.Text,
                    Price = int.Parse(UpdatePrice.Text),
                    Quantity = int.Parse(UpdateQuantity.Text),
                    Warehouse = UpdateWarehouse.Text,
                };
                cntx.ChangeTracker.Clear();
                cntx.Accessories.Update(dto);
                cntx.SaveChanges();
                ApplyView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неверно заполнены данные" + ex.Message, "Складской учёт", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                UpdateId.Clear();
                UpdateName.Clear();
                UpdateMaterial.Clear();
                UpdatePrice.Clear();
                UpdateQuantity.Clear();
                UpdateWarehouse.Clear();
            }
        }

        private void OnRemove()
        {
            try
            {
                cntx.Accessories.Remove(cntx.Accessories.Find(Math.Abs(int.Parse(RemoveId.Text))));
                cntx.SaveChangesAsync();
                AccessoriesView.ItemsSource = cntx.Accessories.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неверно заполнены данные" + ex.Message, "Складской учёт", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                RemoveId.Clear();
            }
        }
    }
}
