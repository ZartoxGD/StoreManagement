using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoteManagementLab
{
    internal class User
    {
        private int userId;
        private int userTypeId;
        private string username;
        private string lastName;
        private string firstName;
        private string mail;
        private string password;
        private string phone;
        private int balance;

        public int UserId { get => userId; set => userId = value; }
        public int UserTypeId { get => userTypeId; set => userTypeId = value; }
        public string Username { get => username; set => username = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Password { get => password; set => password = value; }
        public string Phone { get => phone; set => phone = value; }
        public int Balance { get => balance; set => balance = value; }

        public User(int userId, int userTypeId ,string username, string lastName, string firstName, string mail, string password, string phone, int balance)
        {
            this.userId = userId;
            this.userTypeId = userTypeId;
            this.username = username;
            this.lastName = lastName;
            this.firstName = firstName;
            this.mail = mail;
            this.password = password;
            this.phone = phone;
            this.balance = balance;
        }
    }
}
