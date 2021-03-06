﻿using System;
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
    /// Interaction logic for OrderInfoView.xaml
    /// </summary>
    public partial class OrderInfoView : Window
    {
        public OrderInfoView(ViewModel.ReportViewModel viewModel)
        {
            InitializeComponent();
            DataContext = new ViewModel.OrderInfoViewModel(viewModel);
        }
    }
}
