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
    public class UpdateViewModel:ViewModelBase
    {
        #region Fields
        private MenuViewModel _menuViewModel;
        private string _itemName;
        private string _itemOldPrice;
        private string _itemNewPrice;
        #endregion

        #region Constructor
        public UpdateViewModel(MenuViewModel menuViewModel)
        {
            _menuViewModel = menuViewModel;
            ItemName = _menuViewModel.SelectedMenuItem.ItemName;
            _itemOldPrice = _menuViewModel.SelectedMenuItem.Price.ToString();
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

        public string OldPrice
        {
            get { return _itemOldPrice; }
            set
            {
                _itemOldPrice = value;
                RaisePropertyChanged("OldPrice");
            }
        }

        public string NewPrice
        {
            get { return _itemNewPrice; }
            set
            {
                _itemNewPrice = value;
                RaisePropertyChanged("NewPrice");
            }
        }

        public ICommand Update
        {
            get
            {
                return new RelayCommand(() =>
                {
                    double price;
                    double.TryParse(NewPrice, out price);
                    if(price == 0)
                    {
                        System.Windows.MessageBox.Show("Please, enter correct price.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                        return;
                    }

                    _menuViewModel.MenuRepository.UpdateMenuItemPrice(_menuViewModel.SelectedMenuItem, price);
                    _menuViewModel.MenuItemsList = _menuViewModel.MenuRepository.GetAllMenuItems();
                    System.Windows.MessageBox.Show($"You have just updated {ItemName} price from {OldPrice} to {NewPrice}.", "Result", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    NewPrice = string.Empty;
                });
            }
        }
        #endregion
    }
}
