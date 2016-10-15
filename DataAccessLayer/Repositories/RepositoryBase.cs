using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
   public abstract class RepositoryBase
    {
        #region Fields
         private SqlConnection _connection;
        #endregion

        #region Properties
        public SqlConnection Connection
        {
            get { return _connection; }
            private set { _connection = value; }
        }
        #endregion

        #region Constructor

        public RepositoryBase(string connectionString)
        {
              Connection = (SqlConnection)new ConfigConnectionFactory(connectionString).Create();
        }

        #endregion

        #region Methods

        protected IList<EntityBase> GetAllRecords(string sp_name, Dictionary<string,object> parametrs=null)
        {
            using (var command = new SqlCommand(sp_name, Connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                if(parametrs != null)
                {
                    foreach(var key in parametrs.Keys)
                    {
                        command.Parameters.Add(new SqlParameter(key, parametrs[key]));
                    }
                }

                using (var reader = command.ExecuteReader())
                {
                    List<EntityBase> list = new List<EntityBase>();

                    while (reader.Read())
                    {
                        EntityBase entity = new EntityBase();                       
                        list.Add(Map(reader));
                    }

                    return list;
                }
            }
        }

        protected void Execute(string sp_name, Dictionary<string, object> parametrs)
        {
            using (var command = new SqlCommand(sp_name, Connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                foreach(var key in parametrs.Keys)
                {
                    command.Parameters.Add(new SqlParameter(key, parametrs[key]));                   
                }
                command.ExecuteNonQuery();
            }
        }

        protected abstract EntityBase Map(IDataRecord record);

        #endregion
    }

}
