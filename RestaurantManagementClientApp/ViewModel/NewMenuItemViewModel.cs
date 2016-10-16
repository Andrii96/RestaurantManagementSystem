using DataAccessLayer.DataBaseAccess;
using DataAccessLayer.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagementClientApp.ViewModel
{
    public class NewMenuItemViewModel:ViewModelBase
    {
        #region Fields
        private string _itemName;
        private string _selectedGroup;
        private string _price;
        private string _description;
        private List<string> _groups;
        private MenuViewModel _menuViewModel;
        #endregion

        #region Constructor
        public NewMenuItemViewModel(MenuViewModel menuViewModel)
        {
            _menuViewModel = menuViewModel;
            Groups = _menuViewModel.GetGroupItemsNames();
            
        }
        #endregion

        #region Properties
        public string ItemName
        {
            get { return _itemName; }
            set
            {
                _itemName = value;
                RaisePropertyChanged("ItemName");
            }
        }

        public string SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;
                RaisePropertyChanged("SelectedGroup");
            }
        }

        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                RaisePropertyChanged("Price");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        public List<string> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                RaisePropertyChanged("Groups");
            }
        }

        public ICommand InsertNewRecord
        {
            get
            {
                return new RelayCommand(() =>
                {                   
                    double price;
                    double.TryParse(Price, out price);

                    if(price ==0 || ItemName == string.Empty || SelectedGroup == string.Empty)
                    {
                        System.Windows.MessageBox.Show("Please, enter all fields correct.","Error",System.Windows.MessageBoxButton.OK,System.Windows.MessageBoxImage.Error);
                        return;
                    }

                    Menu item = new Menu(_menuViewModel.MenuRepository.GetAllMenuItems().Last().Id + 1);
                    item.ItemName = ItemName;
                    item.Group = new Group(Groups.FindIndex(s => s == SelectedGroup) + 1) { GroupName = SelectedGroup };

                    item.Price = price;
                    if(Description == null)
                    {
                        Description = string.Empty;
                    }
                    item.Description = Description;
                   _menuViewModel.MenuRepository.InsertRecord(item);
                    _menuViewModel.MenuItemsList = _menuViewModel.MenuRepository.GetAllMenuItems();
                    System.Windows.MessageBox.Show($"The item {item.ItemName} has successfully been added.", "Result", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    Clear();
                });
            }
        }

        private void Clear()
        {
            ItemName = string.Empty;
            SelectedGroup = string.Empty;
            Price = string.Empty;
            Description = string.Empty;
        }

        public ICommand Cancel
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Clear();
                });
            }
        }

        #endregion
    }
}
