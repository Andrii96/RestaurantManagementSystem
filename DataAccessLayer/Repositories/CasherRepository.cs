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
    public class CasherRepository : RepositoryBase, ICasherRepository
    {
        #region Constructor

        public CasherRepository (string connectionString): base(connectionString) { }
        
        #endregion

        #region Methods

        public Casher GetCasherByEmailAndPassword(string email, string pass)
        {
            Connection.Open();
            Dictionary<string, object> parametrs = new Dictionary<string, object>();

            parametrs["@login"] = email;
            parametrs["@pass"] = pass;
            var casher = (Casher)GetAllRecords("sp_GetCasherByLoginAndPass", parametrs).First();

            Connection.Close();
            return casher;

        }

        protected override EntityBase Map(IDataRecord record)
        {
            Casher casher = new Casher((int)record["casher_id"]);

            casher.Name = record["casher_name"] as string;
            casher.Surname = record["casher_surname"] as string;
            return casher;
        }

        #endregion
    }
}
