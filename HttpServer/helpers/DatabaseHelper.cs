using HttpServer.databases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.helpers
{
    public static class DatabaseHelper
    {
        public static IDbDataParameter CreateParameter(this BaseDatabase database, string parameterName, object value)
        {
            return database.CreateParameter(parameterName, value);
        }

        public static int ExecuteNonQuery(this BaseDatabase database, string query)
        {
            int rowsAffected = -1;

            using (IDbConnection connection = database.CreateConnection())
            {
                using (IDbCommand command = database.CreateCommand(query))
                {
                    command.Connection = connection;
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        public static int ExecuteNonQuery(this BaseDatabase database, string query, params IDbDataParameter[] parameters)
        {
            int rowsAffected = -1;

            using (IDbConnection connection = database.CreateConnection())
            {
                using (IDbCommand command = database.CreateCommand(query))
                {
                    command.Connection = connection;
                    connection.Open();

                    foreach (IDbDataParameter param in parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        public static object ExecuteScalar(this BaseDatabase database, string query, params IDbDataParameter[] parameters)
        {
            object result = null;

            using (IDbConnection connection = database.CreateConnection())
            {
                using (IDbCommand command = database.CreateCommand(query))
                {
                    command.Connection = connection;
                    connection.Open();

                    foreach (IDbDataParameter param in parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    result = command.ExecuteScalar();
                }
            }

            return result;
        }

        public static DataTable ExecuteQuery(this BaseDatabase database, string query, params IDbDataParameter[] parameters)
        {
            DataTable dataTable = new DataTable();

            using (IDbConnection connection = database.CreateConnection())
            {
                using (IDbCommand command = database.CreateCommand(query))
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
