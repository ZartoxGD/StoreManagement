using MySql.Data.MySqlClient;
using OfficeOpenXml;
using SoteManagementLab.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoteManagementLab.Pages
{
    public partial class Workspace : Form
    {

        //TODO: Gérer l'évenement delete lorsqu'on sélectionne une ligne dans le data grid view et qu'on appuie sur delete
        //Demander une vérification si l'utilisateur veut supprimer une liste

        private string connectedUsername;
        private int connectedUserType;
        private Inventory inventoryObj;
        private BindingSource sortComboBoxBindingSource;

        private object cellOldValue; //_inventoryGridView cell's old value when modified

        private enum SortType
        {
            ID_ASC,
            ID_DESC,
            NAME_ASC,
            NAME_DESC,
            STOCK_ASC,
            STOCK_DESC,
            PRICE_ASC,
            PRICE_DESC,
            PROMO_ASC,
            PROMO_DESC,
            TAX_ASC,
            TAX_DESC,
            SERVICE_ASC,
            SERVICE_DESC
        }

        public Workspace()
        {
            InitializeComponent();
        }

        private void Workspace_Load(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            connectedUserType = Program.connectedUserType;
            connectedUsername = Program.connectedUsername;
            this.Shown += Workspace_Shown;
        }

        private void Workspace_Shown(object sender, EventArgs e)
        {
            if (connectedUserType == 5)
                tabControl1.TabPages.Remove(adminPage);

            inventoryObj = new Inventory(_inventoryDataGridView);

            sortComboBoxBindingSource = new BindingSource();
            sortComboBoxBindingSource.DataSource = Enum.GetValues(typeof(SortType));
            _sortComboBox.DataSource = sortComboBoxBindingSource;

            _inventoryDataGridView.CellBeginEdit += _inventoryDataGridView_CellBeginEdit;
            _inventoryDataGridView.CellEndEdit += _inventoryDataGridView_CellEndEdit;
            _inventoryDataGridView.DataError += _inventoryDataGridView_DataError;
            _inventoryDataGridView.UserDeletingRow += _inventoryDataGridView_UserDeletingRow;
        }

        private void _inventoryDataGridView_UserDeletingRow(object? sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true; // Annule la suppression de la ligne
            }
            else
            {
                int productId = Convert.ToInt32(e.Row.Cells[0].Value);
                string query = $"DELETE FROM product WHERE id={productId};";
                MySqlConnection c = SqlConnection.Connect();

                using (MySqlCommand command = new MySqlCommand(query, c))
                {
                    command.ExecuteNonQuery();
                }

                c.Close();
                //inventoryObj.DeleteProductById(productId);
            }
        }

        private void _inventoryDataGridView_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            MessageBox.Show("Can't have an empty cell", "Wrong operation", MessageBoxButtons.OK);
            _inventoryDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cellOldValue;
            _inventoryDataGridView.CurrentCell = null;
        }

        private void _inventoryDataGridView_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = _inventoryDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string cellNewValue = "";

            if (cell.Value != null)
            {
                cellNewValue = cell.Value.ToString();
            }
            else
            {
                MessageBox.Show("Can't have an empty cell", "Wrong operation", MessageBoxButtons.OK);
                cell.Value = cellOldValue;
                _inventoryDataGridView.CurrentCell = null;
                return;
            }

            int productId = (int)Convert.ToInt32(_inventoryDataGridView.Rows[e.RowIndex].Cells[0].Value);

            MySqlConnection c = SqlConnection.Connect();

            string query = GetRightModificationQuery(e.ColumnIndex, productId, cellNewValue);

            using (MySqlCommand command = new MySqlCommand(query, c))
            {
                command.ExecuteNonQuery();
            }

            c.Close();
        }

        private string GetRightModificationQuery(int columnIndex, int productId, string newValue)
        {
            if (columnIndex == 1)
                return $"UPDATE product SET name='{newValue}' WHERE id={productId}";

            if (columnIndex == 2)
                return $"UPDATE product SET stock='{newValue}' WHERE id={productId}";

            if (columnIndex == 3)
                return $"UPDATE product SET price='{newValue}' WHERE id={productId}";

            if (columnIndex == 4)
                return $"UPDATE product SET promo_percent='{newValue}' WHERE id={productId}";

            if (columnIndex == 5)
                return $"UPDATE product SET tax_percent='{newValue}' WHERE id={productId}";

            if (columnIndex == 6)
            {
                int serviceId = GetServiceIdByServiceName(newValue);
                return $"UPDATE product SET service_id={serviceId} WHERE id={productId}";
            }

            return "";
        }

        private int GetServiceIdByServiceName(string name)
        {//TODO: tester la fonction en cas de mauvaise entrée (surement erreur) Creer dans la bd une valeur par défaut et l'assigner dans ce cas
            MySqlConnection c = SqlConnection.Connect();

            string query = $"SELECT service_id FROM service WHERE name='{name}';";
            int toReturn = 0;
            using (MySqlCommand command = new MySqlCommand(query, c))
            {

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            toReturn = reader.GetInt32(0);
                        }
                    }
                }
            }
            c.Close();
            return toReturn;
        }

        private void _inventoryDataGridView_CellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0) // 0 -> ID column's index
            {
                MessageBox.Show("You cannot modify the ID of the product", "Wrong operation", MessageBoxButtons.OK);
                _inventoryDataGridView.CurrentCell = null;
                return;
            }

            DataGridViewCell cell = _inventoryDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            cellOldValue = cell.Value;
        }

        private void ExportToCsvBtn_Click(object sender, EventArgs e)
        {
            inventoryObj.ExportInventoryToCsv();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = _sortComboBox.SelectedIndex;
            switch (i)
            {
                case 0:
                    inventoryObj.SortBindingSourceByIdAsc();
                    break;
                case 1:
                    inventoryObj.SortBindingSourceByIdDesc();
                    break;
                case 2:
                    inventoryObj.SortBindingSourceByNameAsc();
                    break;
                case 3:
                    inventoryObj.SortBindingSourceByNameDesc();
                    break;
                case 4:
                    inventoryObj.SortBindingSourceByStockAsc();
                    break;
                case 5:
                    inventoryObj.SortBindingSourceByStockDesc();
                    break;
                case 6:
                    inventoryObj.SortBindingSourceByPriceAsc();
                    break;
                case 7:
                    inventoryObj.SortBindingSourceByPriceDesc();
                    break;
                case 8:
                    inventoryObj.SortBindingSourceByPromoPercentAsc();
                    break;
                case 9:
                    inventoryObj.SortBindingSourceByPromoPercentDesc();
                    break;
                case 10:
                    inventoryObj.SortBindingSourceByTaxPercentAsc();
                    break;
                case 11:
                    inventoryObj.SortBindingSourceByTaxPercentDesc();
                    break;
                case 12:
                    inventoryObj.SortBindingSourceByServiceNameAsc();
                    break;
                case 13:
                    inventoryObj.SortBindingSourceByServiceNameDesc();
                    break;
                default:
                    inventoryObj.SortBindingSourceByIdAsc();
                    break;
            }
        }
    }
}
