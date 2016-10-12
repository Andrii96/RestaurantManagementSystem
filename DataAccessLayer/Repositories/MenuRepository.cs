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
    public class MenuRepository : RepositoryBase
    {
        public MenuRepository(string connectionString) : base(connectionString) { }

        public void InsertRecord (Menu menu)
        {
            Dictionary<string, object> paramets = new Dictionary<string, object>();

            paramets["@id"] = menu.Id;
            paramets["@item_name"] = menu.ItemName;
            paramets["@item_description"] = menu.Description;
            paramets["@item_price"] = menu.Price;
            paramets["@item_group"] = menu.Group.Id;

            Execute("sp_InsertMenuItem", paramets);
        }

        public void UpdateMenuItemPrice(Menu menuItem, double newPrice)
        {
            Dictionary<string, object> paramets = new Dictionary<string, object>();

            paramets["@item_id"] = menuItem.Id;
            paramets["@item_price"] = menuItem.Price = newPrice;

            Execute("sp_UpdateMenuItemPrice", paramets);

        }

        public void DeleteMenuItem (Menu menuItem)
        {
            Dictionary<string, object> paramets = new Dictionary<string, object>();

            paramets["@item_id"] = menuItem.Id;

            Execute("sp_DeleteMenuItem", paramets);
        }

        public List<Menu> GetMenuItemsByGroup(string groupName)
        {
            Dictionary<string, object> parametrs = new Dictionary<string, object>();

            parametrs["@group_name"] = groupName;

            var list =  GetAllRecords("sp_GetMenuItemsByGroup", parametrs);
            List<Menu> menuItems = new List<Menu>();
            foreach(var item in list)
            {
                menuItems.Add((Menu)item);
            }

            return menuItems;

        }

        protected override EntityBase Map(IDataRecord record)
        {
            Menu menuItem = new Menu((int)record["item_id"]);
            menuItem.ItemName = record["item_name"] as string;
            menuItem.Description = record["item_description"] as string;
            menuItem.Price = (double)record["item_price"];
            menuItem.Group = GetGroup((int)record["group_id"], record["group_name"].ToString());

            return menuItem;
            
        }

        private Group GetGroup(int id, string name)
        {
            return new Group(id) { GroupName = name };
        }

    }
}
