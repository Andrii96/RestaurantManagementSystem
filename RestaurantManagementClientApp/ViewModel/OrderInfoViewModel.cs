using DataAccessLayer.DataBaseAccess;
using DataAccessLayer.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementClientApp.ViewModel
{
    public class OrderInfoViewModel : ViewModelBase
    {
        #region Fields
        private ReportViewModel _reportViewModel;
        private int _orderNumber;
        private double _total;
        private List<OrderDetail> _orderList;
        private string _connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        private OrderDetailRepository _orderDetailRepositary;
        #endregion

        #region Constructor
        public OrderInfoViewModel(ReportViewModel reportViewModel)
        {
            _reportViewModel = reportViewModel;
            _orderDetailRepositary = new OrderDetailRepository(_connectionString);
            OrderList = _orderDetailRepositary.GetOrderDetail(_reportViewModel.SelectedBill.Order);
            OrderNumber = _reportViewModel.SelectedBill.Id;
            Total = _reportViewModel.SelectedBill.Total;
        }
        #endregion

        #region Methods

        public int OrderNumber
        {
            get { return _orderNumber; }
            set
            {
                _orderNumber = value;
                RaisePropertyChanged("OrderNumber");
            }
        }

        public double Total
        {
            get { return _total; }
            set
            {
                _total = value;
                RaisePropertyChanged("Total");
            }
        }

        public List<OrderDetail> OrderList
        {
            get { return _orderList; }
            set
            {
                _orderList = value;
                RaisePropertyChanged("OrderList");
            }
        }

        #endregion

    }
}
