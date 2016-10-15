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
        #region Constructor
        public MenuRepository(string connectionString) : base(connectionString) { }
        #endregion

        #region Methods
        public List<Menu> GetAllMenuItems()
        {
            Connection.Open();

            List<Menu> menuList = new List<Menu>();

            foreach (var item in GetAllRecords("sp_GetAllMenuItems"))
            {
                menuList.Add((Menu)item);
            }

            Connection.Close();

            return menuList;
        }

        public void InsertRecord (Menu menu)
        {
            Connection.Open();

            Dictionary<string, object> paramets = new Dictionary<string, object>();

            paramets["@id"] = menu.Id;
            paramets["@item_name"] = menu.ItemName;
            paramets["@item_description"] = menu.Description;
            paramets["@item_price"] = menu.Price;
            paramets["@item_group"] = menu.Group.Id;

            Execute("sp_InsertMenuItem", paramets);

            Connection.Close();
        }

        public void UpdateMenuItemPrice(Menu menuItem, double newPrice)
        {
            Connection.Open();

            Dictionary<string, object> paramets = new Dictionary<string, object>();

            paramets["@item_id"] = menuItem.Id;
            paramets["@item_price"] = menuItem.Price = newPrice;

            Execute("sp_UpdateMenuItemPrice", paramets);

            Connection.Close();

        }

        public void DeleteMenuItem (Menu menuItem)
        {
            Connection.Open();

            Dictionary<string, object> paramets = new Dictionary<string, object>();
            paramets["@item_id"] = menuItem.Id;

            Execute("sp_DeleteMenuItem", paramets);

            Connection.Close();
        }

        public List<Menu> GetMenuItemsByGroup(string groupName)
        {
            return (from item in GetAllMenuItems() where item.Group.GroupName == groupName select item).ToList<Menu>();
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
        #endregion

        #region Helpers
        private Group GetGroup(int id, string name)
        {
            return new Group(id) { GroupName = name };
        }
        #endregion
    }
}
