using DataAccessLayer.DataBaseAccess;
using DataAccessLayer.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RestaurantManagementClientApp.ViewModel
{
    public class OrderViewModel:ViewModelBase
    {
        #region Fields
            private ViewModelBase _currentViewModel;
            private MainViewModel _mainViewModel;
            private OrderRepository _orderRepository;
            private OrderDetailRepository _orderDetailRepository;
            private TableRepository _tableRepository;
            private BillRepository _billRepositary;
            private Order _currentOrder;
            private List<OrderDetail> _currentOrderDetail;
            private DataAccessLayer.Models.Menu _orderedItem;
            private List<DataAccessLayer.Models.Menu> _orderedList = new List<DataAccessLayer.Models.Menu>();
            private int _amount;
            private int _orderNumber;
            private int _tableNumber;
            private double _total;
            private double _previousTotal;
            private bool _isDiscount;
            private double _cash;
            private double _rest;
            private string _connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        private Button _currentTable;
        #endregion

        #region Constructor
            public OrderViewModel(MainViewModel mainViewModel)
            {
                CurrentViewModel = new GroupItemsViewModel(this);
                _mainViewModel = mainViewModel;
                _orderDetailRepository = new OrderDetailRepository(_connectionString);
                _orderRepository = new OrderRepository(_connectionString);
                _tableRepository = new TableRepository(_connectionString);
                _billRepositary = new BillRepository(_connectionString);
           
            }
        #endregion

        #region Properties
        public ViewModelBase CurrentViewModel
            {
                get { return _currentViewModel; }
                set
                {
                    _currentViewModel = value;
                    RaisePropertyChanged("CurrentViewModel");
                }
            }

        public int OrderNumber
            {
                get { return _orderNumber; }
                set
                {
                    _orderNumber = value;
                    RaisePropertyChanged("OrderNumber");
                }
            }

        public int TableNumber
            {
                get { return _tableNumber; }
                set
                {
                    _tableNumber = value;
                    RaisePropertyChanged("TableNumber");
                }
            }

        public int Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                RaisePropertyChanged("Amount");
            }
        }

        public Order CurrentOrder
        {
            get { return _currentOrder; }
            set
            {
                _currentOrder = value;
                RaisePropertyChanged("CurrentOrder");
            }
        }

        public List<OrderDetail> CurrentOrderDetail
        {
            get { return _currentOrderDetail; }
            set
            {
                _currentOrderDetail = value;
                RaisePropertyChanged("CurrentOrderDetail");
            }
        }

        public DataAccessLayer.Models.Menu OrderedItem
        {
            get { return _orderedItem; }
            set
            {
                _orderedItem = value;
                RaisePropertyChanged("OrderedItem");
            }

        }

        public List<DataAccessLayer.Models.Menu> OrderedList
        {
            get { return _orderedList; }
            set
            {
                _orderedList = value;
                RaisePropertyChanged("OrderedList");
            }
        }

        public double Total
        {
            get
            {
                return _total;
            }

            set
            {
                _total = value;
                RaisePropertyChanged("Total");
            }
        }

        public bool IsDiscount
        {
            get { return _isDiscount; }
            set
            {
                _isDiscount = value;
                RaisePropertyChanged("IsDiscount");
            }
        }

        public double Cash
        {
            get { return _cash; }
            set
            {
                //double.TryParse(value,out _cash);
                _cash = value;
                RaisePropertyChanged("Cash");
            }
        }

        public double Rest
        {
            get { return _rest; }
            set
            {
                _rest = value;
                RaisePropertyChanged("Rest");
            }
        }

        public ICommand Submit
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if(OrderNumber == 0)
                    {
                        MessageBox.Show("Please, select table.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }else if(Cash == 0 || Cash < Total)
                    {
                        MessageBox.Show("Wrong cash number.");
                        return;
                    }

                    Rest = Cash - Total;
                    Bill bill = new Bill(OrderNumber);
                    bill.Date = DateTime.Now;
                    bill.Order = CurrentOrder;
                    bill.Total = Total;
                    _billRepositary.InsertBillRecord(bill);

                    MessageBox.Show($"The rest is {Rest}","REST");
                   
                    _tableRepository.UpdateTableStatus(TableNumber, "Free");
                     Total = 0;
                     Cash = 0;
                    OrderNumber = 0;
                    CurrentOrderDetail = null;
                    _currentTable.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF5FB424");
                     
                });
            }
        }

        public ICommand Discount
        {
            get
            {
                return new RelayCommand<CheckBox>(c =>
                {
                    if (c.IsChecked == true)
                    {
                        _previousTotal = Total;
                         Total = Total - (_previousTotal * 0.2);
                    }else
                    {
                        Total = _previousTotal;
                    }
                    _previousTotal = 0;
                });
            }
        }

        public ICommand TableSelected
            {
                get
                {
                    return new RelayCommand<Button>(b =>
                    {                  
                        TableNumber = int.Parse(b.Content.ToString());
                        _currentTable = b;
                       
                        if(_tableRepository.GetTableByNumber(TableNumber).TableInfo == "Free")
                        {
                            OrderNumber = _orderRepository.GetAllOrders().Count + 1;
                            CurrentOrder = new DataAccessLayer.Models.Order(OrderNumber);
                            CurrentOrder.Date = DateTime.Now;
                            CurrentOrder.TableNumber = TableNumber;
                            CurrentOrder.Waiter = _mainViewModel.Casher;
                            _orderRepository.InsertOrderRecord(CurrentOrder);
                            _tableRepository.UpdateTableStatus(TableNumber, "Busy");
                            b.Background = Brushes.Red;
                        }
                        else
                        {
                            var orders = _orderRepository.GetOrdersByTableNumber(TableNumber);

                            foreach (var item in orders)
                            {
                                if(_billRepositary.GetBillByOrder(item) == null)
                                {
                                    OrderNumber = item.Id;
                                    CurrentOrder = item;
                                    break;
                                }
                            }
                                            
                        }
                        CurrentOrderDetail = _orderDetailRepository.GetOrderDetail(CurrentOrder);
                        Total = CountTotal(CurrentOrderDetail);
                        IsDiscount = false;

                    });
                }
            }
        #endregion

        #region Methods
        public void SubmitOrderDetail()
        {
            if(OrderNumber == 0)
            {
                MessageBox.Show("Please, select table.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int id = _orderDetailRepository.GetAllDetailedOrderItems().Count + 1;

            OrderDetail orderDetail = new OrderDetail(id);
            orderDetail.Amount = Amount;
            orderDetail.MenuItem = OrderedItem;
            orderDetail.Order = CurrentOrder;

            _orderDetailRepository.InsertOrderDetailItem(orderDetail);

            CurrentOrderDetail = _orderDetailRepository.GetOrderDetail(CurrentOrder);
            Total = CountTotal(CurrentOrderDetail);
            
        }

        private double CountTotal(List<OrderDetail> orderedItems)
        {
            double sum = 0;

            foreach (var item in orderedItems)
            {
                sum += item.Amount * item.MenuItem.Price;
            }

            return sum;
        }
        #endregion

    }
}
