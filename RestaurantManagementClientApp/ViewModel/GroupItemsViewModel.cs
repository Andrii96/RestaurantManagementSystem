using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RestaurantManagementClientApp.ViewModel
{
    public class GroupItemsViewModel:ViewModelBase
    {
        private OrderViewModel _orderViewModel;
        private string _selectedGroupName;

        public GroupItemsViewModel(OrderViewModel orderViewModel)
        {
            _orderViewModel = orderViewModel;
        }

        public OrderViewModel OrderViewModel
        {
            get { return _orderViewModel; }
        }

        public string SelectedGroupName
        {
            get { return _selectedGroupName; }
            set
            {
                _selectedGroupName = value;
                RaisePropertyChanged("SelectedGroupName");
            }
        }

        public ICommand MenuGroupClick
        {
            get
            {
                return new RelayCommand<Label>(l =>
                {
                     SelectedGroupName = l.Content as string;
                    _orderViewModel.CurrentViewModel = new MenuItemsByGroupViewModel(this);
                });
            }
        }
    }
}
