using MySql.Data.MySqlClient;
using SoteManagementLab.SQL;

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
                        Product p = new Product(reader.GetInt32("id"), reader.GetString("serviceName"), reader.GetInt32("promo_percent"), reader.GetInt32("price"), reader.GetInt32("tax_percent"), reader.GetString("name"));
                        products.Add(p);
                    }
                        
                }
            }

            bindingSource.ResetBindings(false);
            c.Close();
        }
    }
}
