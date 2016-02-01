using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace HttpServer.managers
{
    public class DatabaseManager
    {
        private string connectionString;
        public IDatabase DatabaseType { get; set; }

        public DatabaseManager(string connectionString, IDatabase database)
        {
            this.connectionString = connectionString;
            this.DatabaseType = database;      
        }

        public IDbDataParameter CreateParameter(string parameterName, object value)
        {
            return this.DatabaseType.CreateParameter(parameterName, value);
        }

        public int ExecuteNonQuery(string query)
        {
            int rowsAffected = -1;

            using (IDbConnection connection = this.DatabaseType.CreateConnection(this.connectionString))
            {
                using (IDbCommand command = this.DatabaseType.CreateCommand(query))
                {
                    command.Connection = connection;
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        public DataTable ExecuteQuery(string query, params IDbDataParameter[] parameters)
        {
            DataTable dataTable = new DataTable();

            using (IDbConnection connection = this.DatabaseType.CreateConnection(this.connectionString))
            {
                using (IDbCommand command = this.DatabaseType.CreateCommand(query))
                {
                    command.Connection = connection;
                    connection.Open();

                    foreach (IDbDataParameter param in parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    using (IDataReader dataReader = command.ExecuteReader())
                    {
                        dataTable.Load(dataReader);
                    }
                }
            }

            return dataTable;
        }
    }
}
