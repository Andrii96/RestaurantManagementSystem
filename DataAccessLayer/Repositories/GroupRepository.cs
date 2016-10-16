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
    public class GroupRepository : RepositoryBase, IGroupRepository
    {
        #region Constructor
        public GroupRepository(string connectionString) : base(connectionString) { }
        #endregion

        #region Methods
        public List<Group> GetAllGroups()
        {
            Connection.Open();

            List<Group> groupList = new List<Group>();

            foreach (var item in GetAllRecords("sp_GetAllGroups"))
            {
                groupList.Add((Group)item);
            }

            Connection.Close();

            return groupList;
        }

        public void InsertRecord(Group group)
        {
            Connection.Open();

            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@id"] = group.Id;
            parametrs["@group_name"] = group.GroupName;

            Execute("sp_InsertGroupRecord", parametrs);

            Connection.Close();
        }

        public void DeleteRecord(Group group)
        {
            Connection.Open();

            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@group_id"] = group.Id;

            Execute("sp_DeleteGroupRecord", parametrs);

            Connection.Close();
        }

        protected override EntityBase Map(IDataRecord record)
        {           
            Group group = new Group((int)record["group_id"]);
            group.GroupName = record["group_name"].ToString();
            
            return group;
        }
        #endregion
    }
}
