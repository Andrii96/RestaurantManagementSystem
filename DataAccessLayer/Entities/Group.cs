using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Group:EntityBase
    {
        public Group(int id)
        {
            Id = id;
        }
        public string GroupName { get; set; }
    }
}
