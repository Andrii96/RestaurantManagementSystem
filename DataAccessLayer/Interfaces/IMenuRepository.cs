using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IMenuRepository
    {
        List<Menu> GetAllMenuItems();
        void InsertRecord(Menu menu);
        void UpdateMenuItemPrice(Menu menuItem, double newPrice);
        void DeleteMenuItem(Menu menuItem);
        List<Menu> GetMenuItemsByGroup(string groupName);
    }
}
