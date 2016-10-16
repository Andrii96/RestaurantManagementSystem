using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    interface IBillRepository
    {
        void InsertBillRecord(Bill bill);
        void DeleteBillRecord(Bill bill);
        Bill GetBillByOrder(Order order);
    }
}
