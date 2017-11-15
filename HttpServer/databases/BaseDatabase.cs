using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.databases
{
    public abstract class BaseDatabase : IDatabase
    {
        public string ConnectionString { get; set; }

        public BaseDatabase(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public abstract IDbCommand CreateCommand(string query);
        public abstract IDbConnection CreateConnection();
        public abstract IDbDataParameter CreateParameter(string parameterName, object value);
    }
}
