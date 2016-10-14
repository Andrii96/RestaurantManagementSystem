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
        private Menu _selectedItem;

        public MenuViewModel()
        {
            _menuRepository = new MenuRepository(_connectionString);
            _groupRepository = new GroupRepository(_connectionString);
            MenuItemsList = Convert(_menuRepository.GetAllRecords("sp_GetAllMenuItems"));
            GroupsItemsNames = GetGroupItemsNames();
            GroupsItemsNames.Add("All");
            SelectedGroup = GroupsItemsNames.Last();

        }

        public GroupRepository GroupRepository
        {
            get { return _groupRepository; }
        }

        public MenuRepository MenuRepository
        {
            get { return _menuRepository; }
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

        public Menu SelectedMenuItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedMenuItem");
            }
        }

        public ICommand RemoveMenuItem
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if(SelectedMenuItem == null)
                    {
                        System.Windows.MessageBox.Show("Please, select menu item you want to delete.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);

                    }else
                    {
                        var result = System.Windows.MessageBox.Show($"Are you sure you want to delete {SelectedMenuItem.ItemName}?", "Delete", System.Windows.MessageBoxButton.OKCancel, System.Windows.MessageBoxImage.Question);
                        if (result == System.Windows.MessageBoxResult.OK)
                        {
                            _menuRepository.DeleteMenuItem(SelectedMenuItem);
                            MenuItemsList = Convert(_menuRepository.GetAllRecords("sp_GetAllMenuItems"));
                            SelectedGroup = GroupsItemsNames.Last();
                            SelectedMenuItem = null;
                        }
                    }
                    
                });
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
                    Messenger.Default.Send<NotificationMessage<MenuViewModel>>(new NotificationMessage<MenuViewModel>(this, "Insert"));

                });
            }
        }

        public ICommand UpdatePrice
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if(SelectedMenuItem == null)
                    {
                        System.Windows.MessageBox.Show("Please, select menu item you want to update.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);

                    }else
                    {
                        var result = System.Windows.MessageBox.Show($"Do you want to update {SelectedMenuItem.ItemName} price?", "Update", System.Windows.MessageBoxButton.OKCancel, System.Windows.MessageBoxImage.Question);
                        if (result == System.Windows.MessageBoxResult.OK)
                        {
                            Messenger.Default.Send<NotificationMessage<MenuViewModel>>(new NotificationMessage<MenuViewModel>(this, "Update"));
                        }
                    }
                    

                });
            }
        }

        public List<Menu> Convert(IList<DataAccessLayer.EntityBase> list)
        {
            List<Menu> menuItems = new List<Menu>();

            foreach (var item in list)
            {
                menuItems.Add((Menu)item);
            }
            return menuItems;
        }

        public List<string> GetGroupItemsNames()
        {
            var groups = _groupRepository.GetAllRecords("sp_GetAllGroups");
            List<string> groupNamesList = new List<string>();

            foreach (var item in groups)
            {
                var group = item as Group;
                groupNamesList.Add(group.GroupName);
            }

            return groupNamesList;

        }
    }
}
