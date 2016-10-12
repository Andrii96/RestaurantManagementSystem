using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class OrderDetail:EntityBase
    {
        public OrderDetail(int id)
        {
            Id = id;
        }
        public Menu MenuItem { get; set; }
        public int Amount { get; set; }
        public Order Order { get; set; }
    }
}
