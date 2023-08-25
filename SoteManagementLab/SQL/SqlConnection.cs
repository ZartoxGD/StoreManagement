using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoteManagementLab.SQL
{
    internal class SqlConnection
    {
        //TODO: Faire des retry pour la connection et lors de l'envoi d'une commande (threading, x sec d'écart entre chaque retry)

        private static readonly string connectionString = "Server=localhost;Database=storemanagementlab;Uid=root;Pwd=;";
        private static readonly int msBetweenRetries = 1000;
        private static readonly int maxRetries = 3;

        public static MySqlConnection Connect(int numberOfTries = 0)
        {

            int tries = numberOfTries + 1;

            if (tries >= maxRetries)
            {
                MessageBox.Show("Max reties reached... ");
                return null;
            }

            try
            {
                MySqlConnection c = new MySqlConnection(connectionString);
                c.Open();
                return c;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return Connect(tries);
        }

        public static bool IsConnected(MySqlConnection c, int numberOfTries = 0)
        {
            int tries = numberOfTries + 1;

            if (tries >= maxRetries)
            {
                MessageBox.Show("Max reties reached... ");
                return false;
            }

            try
            {
                if (c.Ping()) //TODO: Cause une erreur lorsque la bdd est éteinte et qu'on appelle cette ligne
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Thread.Sleep(msBetweenRetries);
            return IsConnected(c, tries);
        }

        public static void Deconnect(MySqlConnection c)
        {
            c.Close();
        }
    }
}
