using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Order:EntityBase
    {
        public Order(int id)
        {
            Id = id;
        }
        public Casher Waiter { get; set; }
        public int TableNumber { get; set; }
        public DateTime Date { get; set; }
    }
}
