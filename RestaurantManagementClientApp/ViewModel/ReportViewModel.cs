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
using Microsoft.Office.Interop.Excel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;
using System.Configuration;

namespace RestaurantManagementClientApp.ViewModel
{
    public class ReportViewModel:ViewModelBase
    {
        #region Fields
        private string _reportInfo;
        private double _total;
        private BillRepository _billRepository;
        private OrderRepository _orderRepository;
        private string _connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        private Bill _selectedBill;
        private List<Bill> _billList;
        private DateTime _selectedDate;
        #endregion

        #region Constructor
        public ReportViewModel()
        {
            _billRepository = new BillRepository(_connectionString);
            _orderRepository = new OrderRepository(_connectionString);
            SelectedDate = DateTime.Now;
        }
        #endregion

        #region Properties
        public string ReportInfo
        {
            get { return _reportInfo; }
            set
            {
                _reportInfo = value;
                RaisePropertyChanged("ReportInfo");
            }
        }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged("SelectedDate");
            }
        }

        public Bill SelectedBill
        {
            get { return _selectedBill; }
            set
            {
                _selectedBill = value;
                RaisePropertyChanged("SelectedBill");
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

        public List<Bill> BillList
        {
            get { return _billList; }
            set
            {
                _billList = value;
                RaisePropertyChanged("BillList");
            }
        }

        public ICommand OrderDetailInfo
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (SelectedBill != null)
                    {
                        new Views.OrderInfoView(this).Show();
                        SelectedBill = null;
                    }
                });
            }
        }

        public ICommand ReportForDay
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if(SelectedDate != new DateTime())
                    {
                        
                        var orders = _orderRepository.GetOrdersByDate(SelectedDate);
                        BillList = GetBillsFromOrders(orders);
                        ReportInfo = SelectedDate.Date.ToShortDateString();
                        //SelectedDate = new DateTime();
                    }
                    
                });
            }
        }

        public ICommand ReportForMonth
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ReportInfo = SelectedDate.ToString("MMMM", System.Globalization.CultureInfo.InvariantCulture);
                    var orders = _orderRepository.GetOrdersByMonth(SelectedDate.Month, SelectedDate.Year);
                    
                    BillList = GetBillsFromOrders(orders);
                    //SelectedDate = new DateTime();

                });
            }
        }

        public ICommand OpenInExcel
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        ExportToExcel();
                    }
                    catch (Exception e)
                    {
                        System.Windows.MessageBox.Show("Can't open table in Excel.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    }
                    
                });
            }
        }
        #endregion

        #region Methods
        private void ExportToExcel()
        {

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            object misValue = System.Reflection.Missing.Value;

            xlApp.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet workSheet = xlApp.ActiveSheet;
            

            workSheet.Cells[1, "A"] = "Order Number";
            workSheet.Cells[1, "B"] = "Waiter Name";
            workSheet.Cells[1, "C"] = "Waiter Surname";
            workSheet.Cells[1, "D"] = "Date";
            workSheet.Cells[1, "E"] = "Total";

            int row = 1;

            foreach (var item in BillList)
            {
                row++;
                workSheet.Cells[row, "A"] = item.Id;
                workSheet.Cells[row, "B"] = item.Order.Waiter.Name;
                workSheet.Cells[row, "C"] = item.Order.Waiter.Surname;
                workSheet.Cells[row, "D"] = item.Date.ToString();
                workSheet.Cells[row, "E"] = item.Total;
            }
            workSheet.Cells[++row, "D"] = "TOTAL:";
            workSheet.Cells[row, "E"] = Total;

            workSheet.Range["A1"].AutoFormat(XlRangeAutoFormat.xlRangeAutoFormatList1);
            xlApp.Visible = true;

        }

        private List<Bill> GetBillsFromOrders(List<Order> orders)
        {
            List<Bill> bills= new List<Bill>();
            double total = 0;
            foreach (var item in orders)
            {
                try
                {
                    var bill = _billRepository.GetBillByOrder(item);
                    total += bill.Total;
                    bills.Add(bill);
                }
                catch(NullReferenceException e)
                {
                    break;
                }
                
            }
            Total = total;
            return bills;
        }
        #endregion
    }
}
