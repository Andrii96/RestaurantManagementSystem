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

namespace RestaurantManagementClientApp.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, MessageNotification);
        }

        private void MessageNotification(NotificationMessage obj)
        {
            if (obj.Notification == "Add")
            {
                new NewMenuItem().Show();
            }
        }

        private void comboBox_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
