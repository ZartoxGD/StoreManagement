using OfficeOpenXml;
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

        private string connectedUsername;
        private int connectedUserType;
        private Inventory inventoryObj;

        private object cellOldValue; //_inventoryGridView cell's old value when modified

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

            _inventoryDataGridView.CellDoubleClick += _inventoryDataGridView_CellDoubleClick;
            _inventoryDataGridView.CellBeginEdit += _inventoryDataGridView_CellBeginEdit;
            _inventoryDataGridView.CellEndEdit += _inventoryDataGridView_CellEndEdit;
        }

        private void _inventoryDataGridView_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = _inventoryDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            object cellNewValue = cell.Value;
            MessageBox.Show($"{cellOldValue} : {cellNewValue}");
        }

        private void _inventoryDataGridView_CellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewCell cell = _inventoryDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            cellOldValue = cell.Value;
        }

        private void _inventoryDataGridView_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 0) // 0 -> ID column's index
            {
                MessageBox.Show("You cannot modify the ID of the product", "Wrong operation", MessageBoxButtons.OK);
                _inventoryDataGridView.CurrentCell = null;
            }
        }

        private void ExportToCsvBtn_Click(object sender, EventArgs e)
        {
            inventoryObj.ExportInventoryToCsv();
        }
    }
}
