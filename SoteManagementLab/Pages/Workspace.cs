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

        public Workspace()
        {
            InitializeComponent();
        }

        private void Workspace_Load(object sender, EventArgs e)
        {
            connectedUserType = Program.connectedUserType;
            connectedUsername = Program.connectedUsername;
            this.Shown += Workspace_Shown;
        }

        private void Workspace_Shown(object sender, EventArgs e)
        {
            if (connectedUserType == 5)
                tabControl1.TabPages.Remove(adminPage);

            inventoryObj = new Inventory(_inventoryDataGridView);
        }
    }
}
