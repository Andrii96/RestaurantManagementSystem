using DataAccessLayer.Models;
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
using System.Windows.Shapes;

namespace RestaurantManagementClientApp.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(Casher casher)
        {
            InitializeComponent();
            this.DataContext = new ViewModel.MainViewModel(casher);
            Messenger.Default.Register<NotificationMessage>(this, MessageNotification);
            
        }

        private void MessageNotification(NotificationMessage obj)
        {
            if(obj.Notification == "Close")
            {
                Messenger.Default.Unregister<NotificationMessage>(this);
                var result = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if(MessageBoxResult.Yes == result)
                {
                    App.Current.Shutdown();
                }
                
            }
        }
    }
}
