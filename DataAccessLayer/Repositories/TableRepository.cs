using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataBaseAccess
{
    public class TableRepository : RepositoryBase
    {
        public TableRepository(string connectionString) : base(connectionString) { }

        public List<Table> GetTableByStatus(string status)
        {
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@status"] = status;

            return (List<Table>)GetAllRecords("sp_GetTableByStatus", parametrs);
        }

        public void UpdateTableStatus(int id, string status)
        {
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@id"] = id;
            parametrs["@info"] = status;

            Execute("sp_UpdateTableInfo", parametrs);
        }

        public Table GetTableByNumber(int number)
        {
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@id"] = number;

            return (Table)GetAllRecords("sp_GetTableByNumber", parametrs).First();
        }

        protected override EntityBase Map(IDataRecord record)
        {
            Table table = new Table((int)record["table_num"]);

            table.TableInfo = record["table_info"].ToString();

            return table;
        }
    }
}
