using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        void InsertOrderRecord(Order order);
        void DeleteOrderRecord(Order order);
        List<Order> GetOrdersByDate(DateTime date);
        List<Order> GetOrdersByTableNumber(int number);
        List<Order> GetOrdersByMonth(int monthNumber, int year);
    }
}
