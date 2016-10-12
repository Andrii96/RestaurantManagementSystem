using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataBaseAccess
{
    public class OrderDetailRepository : RepositoryBase
    {
        public OrderDetailRepository(string connectionString) : base(connectionString) { }

        public void InsertOrderDetailItem(OrderDetail item)
        {
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@id"] = item.Id;
            parametrs["@item_id"] = item.MenuItem.Id;
            parametrs["@amount"] = item.Amount;
            parametrs["@order_id"] = item.Order.Id;

            Execute("sp_InsertOrderDetailItem", parametrs);
        }

        public void UpdateOrderDetail(OrderDetail item, int newAmount)
        {
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@order_id"] = item.Order.Id;
            parametrs["@item_id"] = item.MenuItem.Id;
            parametrs["@amount"] = newAmount;

            Execute("sp_UpdateOrderInfo", parametrs);
        }

        public List<OrderDetail> GetOrderDetail (Order order)
        {
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@order_id"] = order.Id;

            return Convert(GetAllRecords("sp_GetOrderDetail", parametrs));
        }

        protected override EntityBase Map(IDataRecord record)
        {
            OrderDetail orderDetail = new OrderDetail((int)record["id"]);

            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@item_id"] = (int)record["item_id"];

            MenuRepository menuRepo = new MenuRepository(Connection.ConnectionString);

            var menu_item = (Menu)menuRepo.GetAllRecords("sp_GetMenuItemById", parametrs).First();
            orderDetail.MenuItem = menu_item;
            parametrs.Clear();

            orderDetail.Amount = (int)record["amount"];

            parametrs["@order_id"] = record["order_id"];
            OrderRepository orderRepo = new OrderRepository(Connection.ConnectionString);
            var order = (Order)orderRepo.GetAllRecords("sp_GetOrderById", parametrs).First();
            orderDetail.Order = order;

            return orderDetail;
            

        }

        private List<OrderDetail> Convert (IEnumerable<EntityBase> items)
        {
            List<OrderDetail> list = new List<OrderDetail>();
            foreach (var item in items)
            {
                list.Add((OrderDetail)item);
            }
            return list;
        }
    }
}
