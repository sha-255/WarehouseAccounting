using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System.Windows;
using WarehouseAccounting.Domain.Context;
using WarehouseAccounting.Domain.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace WarehouseAccounting.Pages
{
    public partial class Main : System.Windows.Controls.Page
    {
        AccessoriesContext cntx = new();

        public Main()
        {
            InitializeComponent();
            ApplyView();
            Add.Click += (s, e) => OnAdd();
            Update.Click += (s, e) => OnUpdate();
            Remove.Click += (s, e) => OnRemove();
            NGReport.Click += (s, e) => Report();
            TBNameSearch.TextChanged += (s, e) => OnSerchTextChanged();

            NMFF.Click += (s, e) => NavigationService.Navigate(new MaterialsForTheManufactureOfFurniture());
            NMFTC.Click += (s, e) => NavigationService.Navigate(new MaterialsForTheCountertop());
        }

        private void OnSerchTextChanged()
        {
            if (TBNameSearch.Text == "")
            {
                ApplyView();
                return;
            }
            ApplyAccessoriesView(cntx.Accessories.Where(el => el.Name.Contains(TBNameSearch.Text)).ToListAsync());
        }

        private void Report()
        {
            try
            {
                var path = @$"C:\Users\_\Downloads\{nameof(cntx.Accessories)}.xlsx";
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var list = new List<ReportData>
                {
                    new ReportData
                    {
                        Price = "123",
                        Quantity = "321",
                    },
                    new ReportData
                    {
                        Price = "1234",
                        Quantity = "3214",
                    },
                    new ReportData
                    {
                        Price = "1235",
                        Quantity = "3215",
                    },
                };
                var ls = cntx.Accessories.ToList();
                if (TBNameSearch.Text != "")
                {
                    ls = cntx.Accessories.Where(el => el.Name.Contains(TBNameSearch.Text)).ToList();
                }
                using (var package = new ExcelPackage())
                {
                    package.Workbook.Worksheets.Add("Report").Cells[1, 1].LoadFromCollection(ls, true);
                    package.SaveAs(new System.IO.FileInfo(path));
                }
            }
            catch
            {
                MessageBox.Show("Не удалось найти установленный Microsoft Office Excel", "Складской учёт", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                    Material = AddMaterial.SelectedValue.ToString(),
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
                var dto = new Accessorie()
                {
                    Id = int.Parse(UpdateId.Text),
                    Name = UpdateName.Text,
                    Material = UpdateMaterial.SelectedValue.ToString(),
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

public static class CollectionsExtension
{
    public static void DisplayInExcel(this IEnumerable<Accessorie> data)
    {
        var excelApp = new Excel.Application();
        excelApp.Visible = true;
        excelApp.Workbooks.Add();
        _Worksheet workSheet = (Worksheet)excelApp.ActiveSheet;
        void DrawHeaders()
        {
            workSheet.Cells[1, "A"] = "Id";
            workSheet.Cells[1, "B"] = "Name";
            workSheet.Cells[1, "C"] = "Material";
            workSheet.Cells[1, "D"] = "Price";
            workSheet.Cells[1, "E"] = "Quantity";
            workSheet.Cells[1, "F"] = "Warehouse";
        }
        DrawHeaders();
        var row = 2;
        foreach (var _ in data)
        {
            row++;
            workSheet.Cells[row, "A"] = _.Id;
            workSheet.Cells[row, "B"] = _.Name;
            workSheet.Cells[row, "C"] = _.Material;
            workSheet.Cells[row, "D"] = _.Price;
            workSheet.Cells[row, "E"] = _.Quantity;
            workSheet.Cells[row, "F"] = _.Warehouse;
        }
        workSheet.Columns.AutoFit();
        workSheet.SaveAs(AppDomain.CurrentDomain.BaseDirectory);
        excelApp.Quit();
    }
}

public class ReportData
{
    public string Price { get; set; }
    public string Quantity { get; set; }
}