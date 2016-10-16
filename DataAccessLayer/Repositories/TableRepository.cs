using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataBaseAccess
{
    public class TableRepository : RepositoryBase, ITableRepository
    {
        #region Constructor
        public TableRepository(string connectionString) : base(connectionString) { }
        #endregion

        #region Methods
        public List<Table> GetTableByStatus(string status)
        {
            Connection.Open();
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@status"] = status;
            Connection.Close();
            return (List<Table>)GetAllRecords("sp_GetTableByStatus", parametrs);

        }

        public void UpdateTableStatus(Table table, string status)
        {
            Connection.Open();
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@id"] = table.Id;
            parametrs["@info"] = status;

            Execute("sp_UpdateTableInfo", parametrs);
            Connection.Close();
        }

        public Table GetTableByNumber(int number)
        {
            Connection.Open();
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@id"] = number;
            var table = (Table)GetAllRecords("sp_GetTableByNumber", parametrs).First();
            Connection.Close();
            return table;
        }

        protected override EntityBase Map(IDataRecord record)
        {
            
            Table table = new Table((int)record["table_num"]);

            table.TableInfo = record["table_info"].ToString();
            
            return table;
        }
        #endregion
    }
}
