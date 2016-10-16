using DataAccessLayer.DataBaseAccess;
using DataAccessLayer.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagementClientApp.ViewModel
{
    public class MenuViewModel:ViewModelBase
    {
        #region Fields
        private List<Menu> _menuItemsList;
        private List<string> _groupsItemsNames;
        private MenuRepository _menuRepository;
        private GroupRepository _groupRepository;
        private string _selectedGroup;
        private string _connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        private Menu _selectedItem;
        #endregion

        #region Constructor
        public MenuViewModel()
        {
            _menuRepository = new MenuRepository(_connectionString);
            _groupRepository = new GroupRepository(_connectionString);
            MenuItemsList = _menuRepository.GetAllMenuItems();
            GroupsItemsNames = GetGroupItemsNames();
            GroupsItemsNames.Add("All");
            SelectedGroup = GroupsItemsNames.Last();

        }
        #endregion

        #region Properties
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
                            MenuItemsList = _menuRepository.GetAllMenuItems();
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
                      }else if(SelectedGroup == "All")
                      {
                          MenuItemsList = _menuRepository.GetAllMenuItems();
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
                    new Views.NewMenuItem(this).ShowDialog();
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
                            new Views.UpdateView(this).ShowDialog();
                        }
                    }
                    

                });
            }
        }
        #endregion

        #region Methods
        public List<string> GetGroupItemsNames()
        {
            var groups = _groupRepository.GetAllGroups();
            List<string> groupNamesList = new List<string>();

            foreach (var item in groups)
            {
                var group = item as Group;
                groupNamesList.Add(group.GroupName);
            }

            return groupNamesList;
        }
        #endregion

    }
}
