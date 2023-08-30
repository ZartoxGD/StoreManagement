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

        public void ModifyExistingProduct(int product_id)
        {

        }
    }
}
