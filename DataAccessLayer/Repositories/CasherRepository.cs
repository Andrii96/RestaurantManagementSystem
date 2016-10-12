using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataBaseAccess
{
    public class CasherRepository : RepositoryBase
    {
        public CasherRepository (string connectionString): base(connectionString) { }

        public void DeleteCasher(Casher casher)
        {
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@casher_id"] = casher.Id;

            Execute("sp_DeleteCasherRecord", parametrs);
        }

        public Casher GetCasherById(int id)
        {
            Dictionary<string, object> parametrs = new Dictionary<string, object>();

            parametrs["@casher_id"] = id;

            return (Casher)GetAllRecords("sp_GetCasherById", parametrs).First();
            
        }

        public Casher GetCasherByEmailAndPassword(string email, string pass)
        {
            Dictionary<string, object> parametrs = new Dictionary<string, object>();

            parametrs["@login"] = email;
            parametrs["@pass"] = pass;

            return (Casher)GetAllRecords("sp_GetCasherByLoginAndPass", parametrs).First();

        }

        protected override EntityBase Map(IDataRecord record)
        {
            Casher casher = new Casher((int)record["casher_id"]);

            casher.Name = record["casher_name"] as string;
            casher.Surname = record["casher_surname"] as string;

            return casher;
        }
    }
}
