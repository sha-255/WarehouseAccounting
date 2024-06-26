﻿using OfficeOpenXml;
using System.Windows;
using System.Windows.Controls;
using WarehouseAccounting.Common;
using WarehouseAccounting.Domain.Context;

namespace WarehouseAccounting.Pages
{
    /// <summary>
    /// Interaction logic for MaterialsForTheCountertop.xaml
    /// </summary>
    public partial class MaterialsForTheCountertop : Page
    {
        MaterialsForTheCountertopContext cntx = new();

        public MaterialsForTheCountertop()
        {
            InitializeComponent();
            ApplyView();
            Add.Click += (s, e) => OnAdd();
            Update.Click += (s, e) => OnUpdate();
            Remove.Click += (s, e) => OnRemove();
            NGReport.Click += (s, e) => Report();
            TBNameSearch.TextChanged += (s, e) => OnSerchTextChanged();

            NMain.Click += (s, e) => NavigationService.Navigate(new Main());
            NMFF.Click += (s, e) => NavigationService.Navigate(new MaterialsForTheManufactureOfFurniture());
        }

        private void OnSerchTextChanged()
        {
            if (TBNameSearch.Text == "")
            {
                ApplyView();
                return;
            }
            ApplyAccessoriesView(cntx.MaterialsForTheCountertop.Where(el => el.Name.Contains(TBNameSearch.Text)).ToList());
        }

        private void Report()
        {
            try
            {
                var path = @$"{Folders.GetPath(Folder.Downloads)}\{nameof(cntx.MaterialsForTheCountertop)}.xlsx";
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var ls = cntx.MaterialsForTheCountertop.ToList();
                if (TBNameSearch.Text != "")
                {
                    ls = cntx.MaterialsForTheCountertop.Where(el => el.Name.Contains(TBNameSearch.Text)).ToList();
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

        private void ApplyView() => ApplyAccessoriesView(cntx.MaterialsForTheCountertop.ToList());

        private void ApplyAccessoriesView(List<Domain.Data.MaterialsForTheCountertop> readers)
            => AccessoriesView.ItemsSource = readers;

        private async void OnAdd()
        {
            try
            {
                var dto = new Domain.Data.MaterialsForTheCountertop()
                {
                    Id = null,
                    Name = AddName.Text,
                    Material = AddMaterial.SelectedValue.ToString(),
                    Price = int.Parse(AddPrice.Text),
                    Quantity = int.Parse(AddQuantity.Text),
                    Warehouse = AddWarehouse.Text,
                };
                await cntx.MaterialsForTheCountertop.AddAsync(dto);
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
                var dto = new Domain.Data.MaterialsForTheCountertop()
                {
                    Id = int.Parse(UpdateId.Text),
                    Name = UpdateName.Text,
                    Material = UpdateMaterial.SelectedValue.ToString(),
                    Price = int.Parse(UpdatePrice.Text),
                    Quantity = int.Parse(UpdateQuantity.Text),
                    Warehouse = UpdateWarehouse.Text,
                };
                cntx.ChangeTracker.Clear();
                cntx.MaterialsForTheCountertop.Update(dto);
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
                cntx.MaterialsForTheCountertop.Remove(cntx.MaterialsForTheCountertop.Find(Math.Abs(int.Parse(RemoveId.Text))));
                cntx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неверно заполнены данные:" + ex.Message, "Складской учёт", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ApplyView();
                RemoveId.Clear();
            }
        }
    }
}
