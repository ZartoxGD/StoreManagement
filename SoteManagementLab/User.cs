using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoteManagementLab
{
    internal class User
    {
        public int UserId;
        public string Username;

        public User(int userId, string username)
        {
            UserId = userId;
            Username = username;
        }
    }
}
