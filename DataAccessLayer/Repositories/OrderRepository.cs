using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataBaseAccess
{
    public class OrderRepository : RepositoryBase
    {
        public OrderRepository(string connectionString) : base(connectionString) { }

        public List<Order> GetAllOrders()
        {
            Connection.Open();
            List<Order> itemsList = new List<Order>();
            foreach (var item in GetAllRecords("sp_GetAllOrders"))
            {
                itemsList.Add((Order)item);
            }
            Connection.Close();
            return itemsList;
        }

        public void InsertOrderRecord(Order order)
        {
            Connection.Open();
            Dictionary<string, object> parametrs = new Dictionary<string, object>();

            parametrs["@id"] = order.Id;
            parametrs["@waiter_id"] = order.Waiter.Id;
            parametrs["@table_number"] = order.TableNumber;
            parametrs["date"] = order.Date;

            Execute("sp_InsertOrderRecord", parametrs);
            Connection.Close();
        }

        public void DeleteOrderRecord(Order order)
        {
            Connection.Open();
            Dictionary<string, object> parametrs = new Dictionary<string, object>();

            parametrs["@id"] = order.Id;

            Execute("sp_DeleteOrderRecord", parametrs);
            Connection.Close();

        }

        public List<Order> GetOrdersByDate(DateTime date)
        {
            Connection.Open();
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@date"] = date;
            var list = GetAllRecords("sp_GetOrdersByDate", parametrs);
            List<Order> orders = new List<Order>();
            foreach (var item in list)
            {
                orders.Add((Order)item);
            }
            Connection.Close();
            return orders;
        }

        public List<Order> GetOrdersByTableNumber(int number)
        {
            Connection.Open();
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@table_num"] = number;

            var list = GetAllRecords("sp_GetOrderByTable", parametrs);
            List<Order> orders = new List<Order>();
            foreach (var item in list)
            {
                orders.Add((Order)item);
            }
            Connection.Close();
            return orders;
        }

        public List<Order> GetOrdersByMonth(int monthNumber,int year)
        {
            Connection.Open();
            if (monthNumber < 1 || monthNumber > 12)
            {
                throw new ArgumentException("invalid month number");
            }

            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@month"] = monthNumber;
            parametrs["@year"] = year;

            var list = GetAllRecords("sp_GetOrdersByMonth", parametrs); 
            List<Order> orders = new List<Order>();
            foreach (var item in list)
            {
                orders.Add((Order)item);
            }
            Connection.Close();
            return orders;
        }

        protected override EntityBase Map(IDataRecord record)
        {
            
            Order order = new Order((int)record["order_id"]);

            var waiter = new Casher((int)record["waiter"]);
            waiter.Name = record["casher_name"].ToString();
            waiter.Surname = record["casher_surname"].ToString();
            order.Waiter = waiter;

            order.TableNumber = (int)record["table_number"];
            order.Date = (DateTime)record["order_date"];
           
            return order;
        }
    }
}
