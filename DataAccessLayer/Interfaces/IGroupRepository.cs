using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IGroupRepository
    {
        List<Group> GetAllGroups();
        void InsertRecord(Group group);
        void DeleteRecord(Group group);
    }
}
