using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataBaseAccess
{
    //public class WaiterRepository : RepositoryBase
    //{
    //    public WaiterRepository(string connectionString) : base(connectionString) { }

    //    public void InsertWaiterRecord(Waiter waiter)
    //    {
    //        Dictionary<string, object> parametrs = new Dictionary<string, object>();
    //        parametrs["@id"] = waiter.Id;
    //        parametrs["@waiter_name"] = waiter.Name;
    //        parametrs["@waiter_surname"] = waiter.Surname;

    //        Execute("sp_InsertWaiterRecord", parametrs);
    //    }

    //    public void DeleteWaiterRecord(Waiter waiter)
    //    {
    //        Dictionary<string, object> parametrs = new Dictionary<string, object>();
    //        parametrs["@waiter_id"] = waiter.Id;
    //        Execute("sp_DeleteWaiterRecord", parametrs);
    //    }

    //    protected override EntityBase Map(IDataRecord record)
    //    {
    //        Waiter waiter = new Waiter((int)record["waiter_id"]);

    //        waiter.Name = record["waiter_name"].ToString();
    //        waiter.Surname = record["waiter_surname"].ToString();

    //        return waiter;
    //    }
    //}
}
