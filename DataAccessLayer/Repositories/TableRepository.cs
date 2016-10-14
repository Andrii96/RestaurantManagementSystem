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

        public List<Table> GetAllTables()
        {
            List<Table> itemsList = new List<Table>();
            foreach (var item in GetAllRecords(""))
            {
                itemsList.Add((Table)item);
            }
            return itemsList;
        }

        public List<Table> GetTableByStatus(string status)
        {
            Connection.Open();
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@status"] = status;
            Connection.Close();
            return (List<Table>)GetAllRecords("sp_GetTableByStatus", parametrs);

        }

        public void UpdateTableStatus(int id, string status)
        {
            Connection.Open();
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@id"] = id;
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
    }
}
