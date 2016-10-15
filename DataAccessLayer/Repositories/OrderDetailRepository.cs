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
        #region Constructor
        public OrderDetailRepository(string connectionString) : base(connectionString) { }
        #endregion

        #region Methods
        public List<OrderDetail> GetAllDetailedOrderItems()
        {
            Connection.Open();
            var list =  Convert(GetAllRecords("sp_GetAllOrderDetail"));
            Connection.Close();
            return list;
        }

        public void InsertOrderDetailItem(OrderDetail item)
        {
            Connection.Open();

            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@id"] = item.Id;
            parametrs["@item_id"] = item.MenuItem.Id;
            parametrs["@amount"] = item.Amount;
            parametrs["@order_id"] = item.Order.Id;

            Execute("sp_InsertOrderDetailItem", parametrs);

            Connection.Close();
        }

        public void UpdateOrderDetail(OrderDetail item, int newAmount)
        {
            Connection.Open();

            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@order_id"] = item.Order.Id;
            parametrs["@item_id"] = item.MenuItem.Id;
            parametrs["@amount"] = newAmount;

            Execute("sp_UpdateOrderInfo", parametrs);

            Connection.Close();
        }

        public List<OrderDetail> GetOrderDetail (Order order)
        {
            Connection.Open();
            Dictionary<string, object> parametrs = new Dictionary<string, object>();
            parametrs["@order_id"] = order.Id;
            var list = Convert(GetAllRecords("sp_GetOrderDetail", parametrs));
            Connection.Close();
            return list;
            
        }

        protected override EntityBase Map(IDataRecord record)
        {           
            OrderDetail orderDetail = new OrderDetail((int)record["id"]);

            MenuRepository menuRepo = new MenuRepository(Connection.ConnectionString);

            var menu_item = from m in menuRepo.GetAllMenuItems() where m.Id == (int)record["item_id"] select m;
            orderDetail.MenuItem = menu_item.First();

            orderDetail.Amount = (int)record["amount"];
         
            OrderRepository orderRepo = new OrderRepository(Connection.ConnectionString);

            var order = from o in orderRepo.GetAllOrders() where o.Id == (int)record["order_id"] select o;
            orderDetail.Order = order.First();
            
            return orderDetail;
            
        }
        #endregion

        #region Helpers
        private List<OrderDetail> Convert (IEnumerable<EntityBase> items)
        {
            List<OrderDetail> list = new List<OrderDetail>();

            foreach (var item in items)
            {
                list.Add((OrderDetail)item);
            }
            return list;
        }
        #endregion
    }
}
