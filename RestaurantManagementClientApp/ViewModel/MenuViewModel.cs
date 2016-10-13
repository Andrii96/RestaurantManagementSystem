using DataAccessLayer.DataBaseAccess;
using DataAccessLayer.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagementClientApp.ViewModel
{
    public class MenuViewModel:ViewModelBase
    {
        private List<Menu> _menuItemsList;
        private List<string> _groupsItemsNames;
        private MenuRepository _menuRepository;
        private GroupRepository _groupRepository;
        private string _selectedGroup;
        private string _connectionString = "Server=ANDRIIPC;Database=Restaurant;Trusted_Connection=True; ";
        public MenuViewModel()
        {
            _menuRepository = new MenuRepository(_connectionString);
            _groupRepository = new GroupRepository(_connectionString);
            MenuItemsList = Convert(_menuRepository.GetAllRecords("sp_GetAllMenuItems"));
            GroupsItemsNames = GetGroupItemsNames();
            SelectedGroup = GroupsItemsNames[0];

        }

        public  string SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;
                RaisePropertyChanged("SelectedGroup");
            }
        }

        public List<string> GroupsItemsNames
        {
            get { return _groupsItemsNames; }
            set
            {
                _groupsItemsNames = value;
                RaisePropertyChanged("GroupsItemsNames");
            }
        }

        public List<Menu> MenuItemsList
        {
            get { return _menuItemsList; }
            set
            {
                _menuItemsList = value;
                RaisePropertyChanged("MenuItemsList");
            }
        }

        public ICommand GetMenuItemsByGroup
        {
            get {
                return new RelayCommand(() =>
                  {
                      if(SelectedGroup != "All")
                      {
                          MenuItemsList = _menuRepository.GetMenuItemsByGroup(SelectedGroup);
                      }else
                      {
                          MenuItemsList = Convert(_menuRepository.GetAllRecords("sp_GetAllMenuItems"));
                      }
                     
                  });
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Add"));
                });
            }
        }

        private List<Menu> Convert(IList<DataAccessLayer.EntityBase> list)
        {
            List<Menu> menuItems = new List<Menu>();

            foreach (var item in list)
            {
                menuItems.Add((Menu)item);
            }
            return menuItems;
        }

        private List<string> GetGroupItemsNames()
        {
            var groups = _groupRepository.GetAllRecords("sp_GetAllGroups");
            List<string> groupNamesList = new List<string>() { "All" };

            foreach (var item in groups)
            {
                var group = item as Group;
                groupNamesList.Add(group.GroupName);
            }

            return groupNamesList;

        }
    }
}
