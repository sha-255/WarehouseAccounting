using OfficeOpenXml;
using System.Windows;
using System.Windows.Controls;
using WarehouseAccounting.Common;
using WarehouseAccounting.Domain.Context;
using WarehouseAccounting.Domain.Data;

namespace WarehouseAccounting.Pages
{
    /// <summary>
    /// Interaction logic for MaterialsForTheManufactureOfFurniture.xaml
    /// </summary>
    public partial class MaterialsForTheManufactureOfFurniture : Page
    {
        MaterialsForFurnitureContext cntx = new();

        public MaterialsForTheManufactureOfFurniture()
        {
            InitializeComponent();
            ApplyView();
            Add.Click += (s, e) => OnAdd();
            Update.Click += (s, e) => OnUpdate();
            Remove.Click += (s, e) => OnRemove();
            NGReport.Click += (s, e) => Report();
            TBNameSearch.TextChanged += (s, e) => OnSerchTextChanged();

            NMain.Click += (s, e) => NavigationService.Navigate(new Main());
            NMFTC.Click += (s, e) => NavigationService.Navigate(new MaterialsForTheCountertop());
        }

        private void OnSerchTextChanged()
        {
            if (TBNameSearch.Text == "")
            {
                ApplyView();
                return;
            }
            ApplyAccessoriesView(cntx.MaterialsForFurniture.Where(el => el.Name.Contains(TBNameSearch.Text)).ToList());
        }

        private void Report()
        {
            try
            {
                var path = @$"{Folders.GetPath(Folder.Downloads)}\{nameof(cntx.MaterialsForFurniture)}.xlsx";
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var ls = cntx.MaterialsForFurniture.ToList();
                if (TBNameSearch.Text != "")
                {
                    ls = cntx.MaterialsForFurniture.Where(el => el.Name.Contains(TBNameSearch.Text)).ToList();
                }
                using (var package = new ExcelPackage())
                {
                    package.Workbook.Worksheets.Add(nameof(Report)).Cells[1, 1].LoadFromCollection(ls, true);
                    package.SaveAs(new System.IO.FileInfo(path));
                }
                var argument = "/select, \"" + path + "\"";
                System.Diagnostics.Process.Start("explorer.exe", argument);
            }
            catch
            {
                MessageBox.Show("Ошибка генерации отчёта", "Складской учёт", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ApplyView() => ApplyAccessoriesView(cntx.MaterialsForFurniture.ToList());

        private void ApplyAccessoriesView(List<MaterialsForFurniture> readers)
            => AccessoriesView.ItemsSource = readers;

        private async void OnAdd()
        {
            try
            {
                var dto = new MaterialsForFurniture()
                {
                    Id = null,
                    Name = AddName.Text,
                    Material = AddMaterial.SelectedValue.ToString(),
                    Price = int.Parse(AddPrice.Text),
                    Quantity = int.Parse(AddQuantity.Text),
                    Warehouse = AddWarehouse.Text,
                };
                await cntx.MaterialsForFurniture.AddAsync(dto);
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
                AddMaterial.SelectedIndex = -1;
                AddPrice.Clear();
                AddQuantity.Clear();
                AddWarehouse.Clear();
            }
        }

        private void OnUpdate()
        {
            try
            {
                var dto = new MaterialsForFurniture()
                {
                    Id = int.Parse(UpdateId.Text),
                    Name = UpdateName.Text,
                    Material = UpdateMaterial.SelectedValue.ToString(),
                    Price = int.Parse(UpdatePrice.Text),
                    Quantity = int.Parse(UpdateQuantity.Text),
                    Warehouse = UpdateWarehouse.Text,
                };
                cntx.ChangeTracker.Clear();
                cntx.MaterialsForFurniture.Update(dto);
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
                UpdateMaterial.SelectedIndex = -1;
                UpdatePrice.Clear();
                UpdateQuantity.Clear();
                UpdateWarehouse.Clear();
            }
        }

        private void OnRemove()
        {
            try
            {
                cntx.MaterialsForFurniture.Remove(cntx.MaterialsForFurniture.Find(Math.Abs(int.Parse(RemoveId.Text))));
                cntx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неверно заполнены данные:" + ex.Message, "Складской учёт", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                RemoveId.Clear();
            }
        }
    }
}
