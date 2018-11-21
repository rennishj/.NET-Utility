using RJ.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace RJ.MVC.Data {
    public class SqlConnectionFactory {
        private readonly IConfigurationSource _configSource;

        public SqlConnectionFactory(IConfigurationSource configSource) {
            _configSource = configSource;
        }

        public DbConnection CreateConnection(string name) {
            return new SqlConnection(_configSource.GetConnectionString(name).ConnectionString);
        }
    }
}