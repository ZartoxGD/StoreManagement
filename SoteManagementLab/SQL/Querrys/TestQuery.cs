using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoteManagementLab.SQL.Querrys
{
    internal class TestQuery : SqlConnection
    {
        public void SendQuery()
        {
            MySqlConnection c = Connect();

            if (IsConnected(c))
            {
                string query = "SELECT * FROM user;";

                using (MySqlCommand command = new MySqlCommand(query, c))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MessageBox.Show($"{reader.GetInt32(0)} {reader.GetInt32(1)} {reader.GetString(2)} {reader.GetString(3)} {reader.GetString(4)} {reader.GetString(5)} {reader.GetString(6)} {reader.GetString(7)} ");
                        }
                    }
                }
            }

            Deconnect(c);
        }
    }
}
