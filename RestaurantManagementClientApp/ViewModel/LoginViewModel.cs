using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataBaseAccess;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Ioc;
using System.Configuration;

namespace RestaurantManagementClientApp.ViewModel
{
    public class LoginViewModel:ViewModelBase
    {
        #region Fields

        private string _login;
        private string _password;
        private CasherRepository _casherRepository;
        private string _connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            _casherRepository = new CasherRepository(_connectionString);
        }
        #endregion

        #region Properties

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                RaisePropertyChanged("Login");
            }
        }

        public ICommand PasswordChangedCommand
        {
            get { return new RelayCommand<PasswordBox>(p => ExecutePasswordChangedCommand(p)); }
        }

        public ICommand Submit
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        var casher = _casherRepository.GetCasherByEmailAndPassword(Login, _password);
                        Messenger.Default.Send<DataAccessLayer.Models.Casher>(casher);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Wrong login or password. Please, try again");
                    }

                });
            }
        }

        #endregion

        #region Helpers
        private void ExecutePasswordChangedCommand(PasswordBox pbPassword)
        {
            _password = pbPassword.Password;
        }
        #endregion
    }
}
