using MySql.Data.MySqlClient;
using SoteManagementLab.SQL;
using System.Linq.Expressions;

namespace SoteManagementLab
{
    internal static class Program
    {

        public static string connectedUsername;
        public static int connectedUserType;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Login loginPage = new Login();

            Application.Run(loginPage);
        }
    }
}