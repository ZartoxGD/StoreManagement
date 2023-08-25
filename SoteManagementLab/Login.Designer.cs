namespace SoteManagementLab
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            _wrongCredentialsLabel = new Label();
            _usernameTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            _passwordTextBox = new TextBox();
            _loginButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(133, 9);
            label1.Name = "label1";
            label1.Size = new Size(78, 30);
            label1.TabIndex = 0;
            label1.Text = "LOGIN";
            // 
            // _wrongCredentialsLabel
            // 
            _wrongCredentialsLabel.AutoSize = true;
            _wrongCredentialsLabel.Font = new Font("Arial Narrow", 9.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            _wrongCredentialsLabel.ForeColor = Color.Red;
            _wrongCredentialsLabel.Location = new Point(93, 49);
            _wrongCredentialsLabel.Name = "_wrongCredentialsLabel";
            _wrongCredentialsLabel.Size = new Size(158, 16);
            _wrongCredentialsLabel.TabIndex = 1;
            _wrongCredentialsLabel.Text = "mauvais username/password";
            _wrongCredentialsLabel.Visible = false;
            // 
            // _usernameTextBox
            // 
            _usernameTextBox.Location = new Point(60, 149);
            _usernameTextBox.Name = "_usernameTextBox";
            _usernameTextBox.Size = new Size(213, 23);
            _usernameTextBox.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ActiveBorder;
            label2.Location = new Point(60, 130);
            label2.Name = "label2";
            label2.Size = new Size(86, 18);
            label2.TabIndex = 3;
            label2.Text = "Username";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ActiveBorder;
            label3.Location = new Point(60, 190);
            label3.Name = "label3";
            label3.Size = new Size(84, 18);
            label3.TabIndex = 5;
            label3.Text = "Password";
            // 
            // _passwordTextBox
            // 
            _passwordTextBox.Location = new Point(60, 209);
            _passwordTextBox.Name = "_passwordTextBox";
            _passwordTextBox.Size = new Size(213, 23);
            _passwordTextBox.TabIndex = 4;
            // 
            // _loginButton
            // 
            _loginButton.BackColor = Color.Green;
            _loginButton.Font = new Font("Arial", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            _loginButton.Location = new Point(68, 328);
            _loginButton.Name = "_loginButton";
            _loginButton.Size = new Size(183, 38);
            _loginButton.TabIndex = 6;
            _loginButton.Text = "LOGIN";
            _loginButton.UseVisualStyleBackColor = false;
            _loginButton.Click += _loginButton_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(328, 450);
            Controls.Add(_loginButton);
            Controls.Add(label3);
            Controls.Add(_passwordTextBox);
            Controls.Add(label2);
            Controls.Add(_usernameTextBox);
            Controls.Add(_wrongCredentialsLabel);
            Controls.Add(label1);
            Name = "Login";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Label label1;
        private Label _wrongCredentialsLabel;
        private TextBox _usernameTextBox;
        private Label label2;
        private Label label3;
        private TextBox _passwordTextBox;
        private Button _loginButton;
    }
}