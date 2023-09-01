namespace SoteManagementLab.Pages
{
    partial class Workspace
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            adminPage = new TabPage();
            inventory = new TabPage();
            splitContainer1 = new SplitContainer();
            _operationsGroupBox = new GroupBox();
            _sortComboBox = new ComboBox();
            label1 = new Label();
            _exportToCsvBtn = new Button();
            _inventoryDataGridView = new DataGridView();
            tabControl1.SuspendLayout();
            inventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            _operationsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_inventoryDataGridView).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(adminPage);
            tabControl1.Controls.Add(inventory);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 450);
            tabControl1.TabIndex = 1;
            // 
            // adminPage
            // 
            adminPage.Location = new Point(4, 24);
            adminPage.Name = "adminPage";
            adminPage.Padding = new Padding(3);
            adminPage.Size = new Size(792, 422);
            adminPage.TabIndex = 0;
            adminPage.Text = "admin";
            adminPage.UseVisualStyleBackColor = true;
            // 
            // inventory
            // 
            inventory.Controls.Add(splitContainer1);
            inventory.Location = new Point(4, 24);
            inventory.Name = "inventory";
            inventory.Padding = new Padding(3);
            inventory.Size = new Size(792, 422);
            inventory.TabIndex = 1;
            inventory.Text = "inventory";
            inventory.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(_operationsGroupBox);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(_inventoryDataGridView);
            splitContainer1.Size = new Size(786, 416);
            splitContainer1.SplitterDistance = 262;
            splitContainer1.TabIndex = 0;
            // 
            // _operationsGroupBox
            // 
            _operationsGroupBox.Controls.Add(_sortComboBox);
            _operationsGroupBox.Controls.Add(label1);
            _operationsGroupBox.Controls.Add(_exportToCsvBtn);
            _operationsGroupBox.Dock = DockStyle.Fill;
            _operationsGroupBox.Location = new Point(0, 0);
            _operationsGroupBox.Name = "_operationsGroupBox";
            _operationsGroupBox.Size = new Size(262, 416);
            _operationsGroupBox.TabIndex = 0;
            _operationsGroupBox.TabStop = false;
            _operationsGroupBox.Text = "Operations";
            // 
            // _sortComboBox
            // 
            _sortComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _sortComboBox.FormattingEnabled = true;
            _sortComboBox.Location = new Point(67, 16);
            _sortComboBox.Name = "_sortComboBox";
            _sortComboBox.Size = new Size(121, 23);
            _sortComboBox.TabIndex = 2;
            _sortComboBox.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 1;
            label1.Text = "Sort by: ";
            label1.Click += label1_Click;
            // 
            // _exportToCsvBtn
            // 
            _exportToCsvBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _exportToCsvBtn.Location = new Point(181, 387);
            _exportToCsvBtn.Name = "_exportToCsvBtn";
            _exportToCsvBtn.Size = new Size(75, 23);
            _exportToCsvBtn.TabIndex = 0;
            _exportToCsvBtn.Text = "Export CSV";
            _exportToCsvBtn.UseVisualStyleBackColor = true;
            _exportToCsvBtn.Click += ExportToCsvBtn_Click;
            // 
            // _inventoryDataGridView
            // 
            _inventoryDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _inventoryDataGridView.Dock = DockStyle.Fill;
            _inventoryDataGridView.Location = new Point(0, 0);
            _inventoryDataGridView.Name = "_inventoryDataGridView";
            _inventoryDataGridView.RowTemplate.Height = 25;
            _inventoryDataGridView.Size = new Size(520, 416);
            _inventoryDataGridView.TabIndex = 0;
            // 
            // Workspace
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Name = "Workspace";
            Text = "Workspace";
            Load += Workspace_Load;
            tabControl1.ResumeLayout(false);
            inventory.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            _operationsGroupBox.ResumeLayout(false);
            _operationsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_inventoryDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabControl1;
        private TabPage adminPage;
        private TabPage inventory;
        private SplitContainer splitContainer1;
        private DataGridView _inventoryDataGridView;
        private Button _exportToCsvBtn;
        private GroupBox _operationsGroupBox;
        private Label label1;
        private ComboBox _sortComboBox;
    }
}