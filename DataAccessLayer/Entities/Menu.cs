using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Menu: EntityBase
    {
        public Menu(int id)
        {
            Id = id;
        }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Group Group { get; set; }

   
    }
}
