using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Casher:EntityBase
    {
        public Casher(int id)
        {
            Id = id;
        }
        public string Name { get; set; }
        public string Surname { get; set;}
    }
}
