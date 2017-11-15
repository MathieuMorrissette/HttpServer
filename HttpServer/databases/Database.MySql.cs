using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace HttpServer.databases
{
    class DatabaseMySql : BaseDatabase
    {
        public DatabaseMySql(string connectionString) : base(connectionString)
        {
        }

        public override IDbConnection CreateConnection()
        {
            return new MySqlConnection(this.ConnectionString);
        }

        public override IDbDataParameter CreateParameter(string parameterName, object value)
        {
            return new MySqlParameter(parameterName, value);
        }

        public override IDbCommand CreateCommand(string query)
        {
            return new MySqlCommand(query);
        }
    }
}
