using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
   public class ConfigConnectionFactory
   {
        private readonly string _connectionString;

        public ConfigConnectionFactory(string connectionString)
        {
            if(connectionString == null)
            {
                throw new ArgumentNullException("Connection string can not be null.");
            }

            _connectionString = connectionString;
        }

        public IDbConnection Create()
        {
            var connection = new SqlConnection(_connectionString);
            if(connection == null)
            {
                throw new NullReferenceException("Connection is null value.");
            }
            connection.Open();
            return connection;
        }
            
   }
}
