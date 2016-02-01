using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HttpServer
{
    public static class UserManager
    {
        public static void CreateUser(User user)
        {

        }

        public static User GetUser(string username)
        {
            IDbDataParameter parameter = WebSite.Database.CreateParameter("@Username", username);
            DataTable table = WebSite.Database.ExecuteQuery("SELECT Username, Password FROM users WHERE Username=@Username", parameter);

            return null;
        }

        public static bool Exists(User user)
        {
            return false;
        }
    }
}
