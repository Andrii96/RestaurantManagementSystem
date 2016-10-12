using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataBaseAccess
{
    public class GroupRepository : RepositoryBase
    {
        public GroupRepository(string connectionString) : base(connectionString) { }

        public void InsertRecord(Group group)
        {
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@id"] = group.Id;
            parametrs["@group_name"] = group.GroupName;

            Execute("sp_InsertGroupRecord", parametrs);
        }

        public void DeleteRecord(Group group)
        {
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@group_id"] = group.Id;

            Execute("sp_DeleteGroupRecord", parametrs);
        }

        protected override EntityBase Map(IDataRecord record)
        {
            Group group = new Group((int)record["group_id"]);
            group.GroupName = record["group_name"].ToString();

            return group;
        }
    }
}
