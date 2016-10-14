using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RestaurantManagementClientApp.ViewModel;

namespace RestaurantManagementClientApp.Views
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : UserControl
    {
        public ReportView()
        {
            InitializeComponent();
            DataContext = new ViewModel.ReportViewModel();
            Messenger.Default.Register<NotificationMessage<ViewModel.ReportViewModel>>(this, MessageNotification);
        }

        private void MessageNotification(NotificationMessage<ReportViewModel> obj)
        {
            if(obj.Notification == "Detail")
            {
                new OrderInfoView(obj.Content).Show();
            }
        }
    }
}
