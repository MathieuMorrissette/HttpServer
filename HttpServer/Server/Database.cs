using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace HttpServer.managers
{
    public class Database
    {
        private string connectionString;

        public Database(string connectionString)
        {
            this.connectionString = connectionString;            
        }

        public int ExecuteNonQuery(string query)
        {
            int rowsAffected = -1;

            using (MySqlConnection mysqlConnection = new MySqlConnection(this.connectionString))
            {
                using (MySqlCommand mysqlCommand = new MySqlCommand(query, mysqlConnection))
                {
                    mysqlConnection.Open();
                    rowsAffected = mysqlCommand.ExecuteNonQuery();
                    mysqlConnection.Close();
                }
            }

            return rowsAffected;
        }
    }
}
