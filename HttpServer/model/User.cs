using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public class User
    {
        public const string USERNAME_FIELD = "Username";
        public const string PASSWORD_FIELD = "Password";

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        
        public string Username { get; private set; }
        public string Password { get; private set; }
    }
}
