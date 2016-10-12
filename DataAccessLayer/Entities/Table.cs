using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Table:EntityBase
    {
        public Table(int id)
        {
            Id = id;
        }
        public string TableInfo { get; set; }
    }
}
