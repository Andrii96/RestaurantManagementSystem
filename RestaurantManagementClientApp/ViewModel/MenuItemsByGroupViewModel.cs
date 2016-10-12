using DataAccessLayer.DataBaseAccess;
using DataAccessLayer.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using RestaurantManagementClientApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagementClientApp.ViewModel
{
    public class MenuItemsByGroupViewModel:ViewModelBase
    {
        private GroupItemsViewModel _groupItemsViewModel;
        private List<Menu> _itemsByGroup;
        private string _groupName;
        private Menu _selectedItem;
        private string _amount;
        private string _connectionString = "Server=ANDRIIPC;Database=Restaurant;Trusted_Connection=True;";
        private MenuRepository _menuRepository;
        private OrderDetailRepository _orderDetailRepository;

        public MenuItemsByGroupViewModel(GroupItemsViewModel groupItemsViewModel)
        {
            _groupItemsViewModel = groupItemsViewModel;
            _menuRepository = new MenuRepository(_connectionString);
            _orderDetailRepository = new OrderDetailRepository(_connectionString);
            GroupName = _groupItemsViewModel.SelectedGroupName;
            ItemsByGroup = _menuRepository.GetMenuItemsByGroup(GroupName);
            Amount = "1";
            
        }

        public List<Menu> ItemsByGroup
        {
            get { return _itemsByGroup; }
            set
            {
                _itemsByGroup = value;
                RaisePropertyChanged("ItemsByGroup");
            }
        }

        public string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                RaisePropertyChanged("GroupName");
            }
        }

        public Menu SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                RaisePropertyChanged("Amount");
            }
        }

        public ICommand BackToGroups
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var s = SelectedItem;
                    _groupItemsViewModel.OrderViewModel.CurrentViewModel = new GroupItemsViewModel(_groupItemsViewModel.OrderViewModel);
                });
            }
        }

        public ICommand Submit
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if(SelectedItem == null)
                    {
                        System.Windows.MessageBox.Show("Please, select any item", "Error");

                    }else
                    {
                        _groupItemsViewModel.OrderViewModel.OrderedItem = SelectedItem;
                        _groupItemsViewModel.OrderViewModel.Amount = int.Parse(Amount);
                        _groupItemsViewModel.OrderViewModel.SubmitOrderDetail();
                    }
                   
                });
            }
        }
    }
}
