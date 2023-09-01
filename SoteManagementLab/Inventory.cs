using MySql.Data.MySqlClient;
using SoteManagementLab.SQL;
using System.Globalization;
using OfficeOpenXml;
using System.Security.Cryptography.X509Certificates;

namespace SoteManagementLab
{
    internal class Inventory
    {
        private DataGridView view;
        private List<Product> products;
        private BindingSource bindingSource;

        public Inventory(DataGridView _view) 
        {
            view = _view;
            products = new List<Product>();
            bindingSource = new BindingSource();
            bindingSource.DataSource = products;
            view.DataSource = bindingSource;
            LoadDataFromDB();
        }

        public Product GetProductByID(int id)
        {
            foreach (Product product in products)
            {
                if (product.Id == id)
                    return product;
            }
            return null;
        }

        #region SORTING

        public void SortBindingSourceByIdDesc()
        {
            products.Sort((p1, p2) => p2.Id.CompareTo(p1.Id));
            bindingSource.ResetBindings(false);
        }
        
        public void SortBindingSourceByIdAsc()
        {
            products.Sort((p1, p2) => p1.Id.CompareTo(p2.Id));
            bindingSource.ResetBindings(false);
        }
        
        public void SortBindingSourceByStockDesc()
        {
            products.Sort((p1, p2) => p2.Stock.CompareTo(p1.Stock));
            bindingSource.ResetBindings(false);
        }
        
        public void SortBindingSourceByStockAsc()
        {
            products.Sort((p1, p2) => p1.Stock.CompareTo(p2.Stock));
            bindingSource.ResetBindings(false);
        }
        
        public void SortBindingSourceByPriceAsc()
        {
            products.Sort((p1, p2) => p1.Price.CompareTo(p2.Price));
            bindingSource.ResetBindings(false);
        }
        
        public void SortBindingSourceByPriceDesc()
        {
            products.Sort((p1, p2) => p2.Price.CompareTo(p1.Price));
            bindingSource.ResetBindings(false);
        }
        
        public void SortBindingSourceByPromoPercentDesc()
        {
            products.Sort((p1, p2) => p2.PromoPercent.CompareTo(p1.PromoPercent));
            bindingSource.ResetBindings(false);
        }

        public void SortBindingSourceByPromoPercentAsc()
        {
            products.Sort((p1, p2) => p1.PromoPercent.CompareTo(p2.PromoPercent));
            bindingSource.ResetBindings(false);
        }
        
        public void SortBindingSourceByTaxPercentAsc()
        {
            products.Sort((p1, p2) => p1.TaxPercent.CompareTo(p2.TaxPercent));
            bindingSource.ResetBindings(false);
        }
        
        public void SortBindingSourceByTaxPercentDesc()
        {
            products.Sort((p1, p2) => p2.TaxPercent.CompareTo(p1.TaxPercent));
            bindingSource.ResetBindings(false);
        }

        public void SortBindingSourceByNameAsc()
        {
            products.Sort((p1, p2) => string.Compare(p1.Name, p2.Name));
            bindingSource.ResetBindings(false);
        }
        
        public void SortBindingSourceByServiceNameAsc()
        {
            products.Sort((p1, p2) => string.Compare(p1.ServiceName, p2.ServiceName));
            bindingSource.ResetBindings(false);
        }

        public void SortBindingSourceByServiceNameDesc()
        {
            products.Sort((p1, p2) => string.Compare(p2.ServiceName, p1.ServiceName));
            bindingSource.ResetBindings(false);
        }

        public void SortBindingSourceByNameDesc()
        {
            products.Sort((p1, p2) => string.Compare(p2.Name, p1.Name));
            bindingSource.ResetBindings(false);
        }

        #endregion

        public void DeleteProductById(int id)
        {
            products.Remove(GetProductByID(id));
        }

        private void LoadDataFromDB()
        {
            MySqlConnection c = SqlConnection.Connect();

            string query = "SELECT product.*, service.name AS 'serviceName' FROM `product` JOIN service ON product.service_id=service.service_id;";

            using (MySqlCommand command = new MySqlCommand(query, c))
            {

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Product p = new Product(reader.GetInt32("id"), reader.GetString("serviceName"), reader.GetInt32("promo_percent"), reader.GetInt32("price"), reader.GetInt32("tax_percent"), reader.GetString("name"), reader.GetInt32("stock"));
                        products.Add(p);
                    }
                        
                }
            }

            bindingSource.ResetBindings(false);
            c.Close();
        }

        private string GetWorkbookName(int x=0)
        {
            string baseName = "products";
            string extension = ".xlsx";

            if (x != 0)
                baseName += x;

            string name = baseName + extension;

            if (File.Exists(name))
                return GetWorkbookName(x + 1);
            else
                return name;
                
        }
        
        public void ExportInventoryToCsv()
        {
            string filename = GetWorkbookName();

            using (var package = new ExcelPackage(new FileInfo(filename)))
            {
                var worksheet = package.Workbook.Worksheets.Add("Products");

                // Add header row
                worksheet.Cells["A1"].Value = "Product ID";
                worksheet.Cells["B1"].Value = "Name";
                worksheet.Cells["C1"].Value = "Price";
                worksheet.Cells["D1"].Value = "Promo %";
                worksheet.Cells["E1"].Value = "Tax %";
                worksheet.Cells["F1"].Value = "Stock";
                worksheet.Cells["G1"].Value = "Service";

                // Apply header formatting
                using (var headerRange = worksheet.Cells["A1:G1"])
                {
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                // Populate data
                for (int i = 0; i < products.Count; i++)
                {
                    //TODO: Dans les paramètres de l'appli (dans la bdd) rajouter un seuil de stock presque vide, dans le fichier excel si le stock est 
                    // est plus petit que le seuil mettre la colonne en rouge

                    worksheet.Cells[i + 2, 1].Value = products[i].Id;
                    worksheet.Cells[i + 2, 2].Value = products[i].Name;
                    worksheet.Cells[i + 2, 3].Value = products[i].Price;
                    worksheet.Cells[i + 2, 4].Value = products[i].PromoPercent;
                    worksheet.Cells[i + 2, 5].Value = products[i].TaxPercent;
                    worksheet.Cells[i + 2, 6].Value = products[i].Stock;
                    worksheet.Cells[i + 2, 7].Value = products[i].ServiceName;
                }

                // Auto-fit columns
                worksheet.Cells.AutoFitColumns();

                // Save the Excel file
                package.Save();
                MessageBox.Show($"File successfully exported to CSV as '{filename}'", "Export to CSV", MessageBoxButtons.OK);
            }
        }
    }
}
