using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataBaseAccess
{
    public class BillRepository : RepositoryBase
    {
        public BillRepository(string connectionString) : base(connectionString) { }

        public void InsertBillRecord(Bill bill)
        {
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@id"] = bill.Id;
            parametrs["@order_id"] = bill.Order.Id;
            parametrs["@total"] = bill.Total;
            parametrs["@bill_date"] = bill.Date;

            Execute("sp_InsertBillrecord", parametrs);
        }

        public void DeleteBillRecord(Bill bill)
        {
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@bill_id"] = bill.Id;
            Execute("sp_DeleteBillRecord", parametrs);
        }

        public Bill GetBillByOrder(Order order)
        {
            using (var command = new SqlCommand("sp_GetBillByOrder", Connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@order_id", order.Id));
              
                using (var reader = command.ExecuteReader())
                {                   
                    if (reader.Read())
                    {
                        Bill bill = new Bill((int)reader["bill_number"]);
                        bill.Order = order;
                        bill.Total = (double)reader["total"];
                        bill.Date = (DateTime)reader["bill_date"];
                        return bill;
                    }

                    return null;
                }
            }

        }

        protected override EntityBase Map(IDataRecord record)
        {
            Bill bill = new Bill((int)record["bill_number"]);
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@order_id"] = (int)record["order_id"];
            var order = (Order)GetAllRecords("sp_GetOrderById", parametrs).First();
            bill.Order = order;

            bill.Total = (double)record["total"];
            bill.Date = (DateTime)record["bill_date"];

            return bill;
            
        }

    }
}
