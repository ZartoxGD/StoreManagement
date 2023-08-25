using MySql.Data.MySqlClient;
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

        private void _loginButton_Click(object sender, EventArgs e)
        {
            _wrongCredentialsLabel.Visible = false;

            string username = _usernameTextBox.Text;
            string password = _passwordTextBox.Text;

            if (CheckCredentials(username, password))
            {
                //TODO: Creer un user et le stocker
                _passwordTextBox.Text = "";
                _wrongCredentialsLabel.Visible = false;
            }
            else
            {
                BadPasswordOrUsername();
                return;
            }
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

            Clipboard.SetText(c.GetArgon2HashedPassword(password));

            if (c.IsArgon2PasswordEqual(password, hashedPasswordFromDb))
                return true;

            return false;
        }

        private void BadPasswordOrUsername()
        {
            _passwordTextBox.Text = "";
            _wrongCredentialsLabel.Visible = true;
            //TODO: faire une message box à la place du label (avec retry comme bouton)
        }

        private bool CheckCredentials(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            MySqlConnection c = SqlConnection.Connect();

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

            SqlConnection.Deconnect(c);
            return false;
        }
    }
}