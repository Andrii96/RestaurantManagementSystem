using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Bill:EntityBase
    {
        public Bill(int id)
        {
            Id = id;
        }

        public Order Order { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }

    }
}
