using DataAccessLayer.DataBaseAccess;
using DataAccessLayer.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagementClientApp.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        #region Fields
        private Casher _casher;
        private string _casherInfo;
        private  ViewModelBase _viewModel;
        private List<ViewModelBase> _viewModels;
        #endregion

        #region Constructor
        public MainViewModel(Casher casher)
        {
            _casher = casher;
            CasherInfo = $"{_casher.Name} {casher.Surname}";
            _viewModels = new List<ViewModelBase>() { new OrderViewModel(this), new MenuViewModel(), new ReportViewModel() };
            CurrentViewModel = _viewModels[0];
        }
        #endregion

        #region Properties

        public Casher Casher
        {
            get { return _casher; }
        }

        public  ViewModelBase CurrentViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        public string CasherInfo
        {
            get { return _casherInfo; }
            set
            {

                _casherInfo = value;
                RaisePropertyChanged("CasherInfo");
            }
        }

        public ICommand OrderSelected
        {
            get
            {
                return new RelayCommand(() =>
                {
                    CurrentViewModel = _viewModels[0];

                });
            }
        }

        public ICommand MenuSelected
        {
            get
            {
                return new RelayCommand(() =>
                {
                    CurrentViewModel = _viewModels[1];

                });
            }
        }

        public ICommand ReportSelected
        {
            get
            {
                return new RelayCommand(() =>
                {
                    CurrentViewModel = _viewModels[2];
                });
            }
        }

        public ICommand Close
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Close"));
                });
            }
        }
        #endregion

    }
}
