using MySql.Data.MySqlClient;
using SoteManagementLab.Pages;
using SoteManagementLab.SQL;
using SoteManagementLab.Utils.Security;

namespace SoteManagementLab
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private bool CheckPassword(string password, string username, MySqlConnection connection)
        {
            Cryptography c = new Cryptography();
            string hashedPasswordFromDb = "";

            string query = "SELECT password FROM user WHERE (username = @username OR mail = @username);";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        hashedPasswordFromDb = reader.GetString(0);
                }
            }

            if (c.IsArgon2PasswordEqual(password, hashedPasswordFromDb))
                return true;

            return false;
        }

        private int GetUserType(string username, MySqlConnection connection)
        {
            string userType;

            string query = "SELECT user_type_id FROM user WHERE (username = @username OR mail = @username);";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        label1.Text = reader.GetInt32(0).ToString();
                        return reader.GetInt32(0);
                }
            }

            return 0;
        }

        private void _loginButton_Click(object sender, EventArgs e)
        {
            _wrongCredentialsLabel.Visible = false;

            string username = _usernameTextBox.Text;
            string password = _passwordTextBox.Text;

            MySqlConnection c = SqlConnection.Connect();

            if (CheckCredentials(username, password, c))
            {
                int userType = GetUserType(username, c);

                if(userType != 1 && userType != 5 && userType != 6)
                {
                    UnauthorizedUser();
                    return;
                }

                Program.connectedUsername = username;
                Program.connectedUserType = userType;

                SqlConnection.Deconnect(c);
                _wrongCredentialsLabel.Visible = false;
                Hide();
                Workspace workspace = new Workspace();
                workspace.FormClosed += WorkspaceForm_FormClosed;
                workspace.Show();
            }
            else
            {
                SqlConnection.Deconnect(c);
                BadPasswordOrUsername();
                return;
            }
        }

        private void WorkspaceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Show the login form again when the workspace form is closed
            Close();
        }

        private void BadPasswordOrUsername()
        {
            _passwordTextBox.Text = "";
            _wrongCredentialsLabel.Text = "Mauvais username/password";
            _wrongCredentialsLabel.Visible = true;
            //TODO: faire une message box à la place du label (avec retry comme bouton)
        }
        
        private void UnauthorizedUser()
        {
            _passwordTextBox.Text = "";
            _wrongCredentialsLabel.Text = "Non autorisé !";
            _wrongCredentialsLabel.Visible = true;
            //TODO: faire une message box à la place du label (avec retry comme bouton)
        }

        private bool CheckCredentials(string username, string password, MySqlConnection c)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            if (SqlConnection.IsConnected(c))
            {
                string query = "SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END FROM user WHERE (username = @username OR mail = @username);";

                using (MySqlCommand command = new MySqlCommand(query, c))
                {
                    command.Parameters.AddWithValue("@username", username);

                    int count = (int)command.ExecuteScalar();

                    if (!(count > 0 && count < 2))
                        return false;
                }

                return CheckPassword(password, username, c);
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _passwordTextBox.UseSystemPasswordChar = !_passwordTextBox.UseSystemPasswordChar;
        }
    }
}