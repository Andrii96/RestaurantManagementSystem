using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ITableRepository
    {
        List<Table> GetTableByStatus(string status);
        void UpdateTableStatus(Table table, string status);
        Table GetTableByNumber(int number);
    }
}
