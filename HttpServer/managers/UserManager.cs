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
        public static bool CreateUser(User user)
        {
            if (UserManager.Exists(user))
            {
                return false;
            }

            IDbDataParameter paramUsername = WebSite.Database.CreateParameter("@Username", user.Username);
            IDbDataParameter paramPassword = WebSite.Database.CreateParameter("@Password", user.Password);

            WebSite.Database.ExecuteNonQuery("INSERT INTO users (Username, Password) VALUES (@Username, @Password)", paramUsername, paramPassword);

            return true;
        }

        public static User GetUser(string username)
        {
            IDbDataParameter parameter = WebSite.Database.CreateParameter("@Username", username);
            DataTable table = WebSite.Database.ExecuteQuery("SELECT Username, Password FROM users WHERE Username=@Username", parameter);

            if (table == null)
            {
                return null;
            }

            if (table.Rows.Count < 1)
            {
                return null;
            }

            DataRow dataRow = table.Rows[0];

            return new User((string)dataRow[User.USERNAME_FIELD], (string)dataRow[User.PASSWORD_FIELD]);
        }

        public static bool Exists(User user)
        {
            IDbDataParameter paramUsername = WebSite.Database.CreateParameter("@Username", user.Username);
            object result = WebSite.Database.ExecuteScalar("SELECT COUNT(*) FROM users WHERE Username=@Username", paramUsername);

            int count = 0;

            int.TryParse(result.ToString(), out count);

            return (count > 0);
        }
    }
}
