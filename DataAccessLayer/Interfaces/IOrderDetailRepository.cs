using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IOrderDetailRepository
    {
        List<OrderDetail> GetAllDetailedOrderItems();
        void InsertOrderDetailItem(OrderDetail item);
        void UpdateOrderDetail(OrderDetail item, int newAmount);
        List<OrderDetail> GetOrderDetail(Order order);
    }
}
