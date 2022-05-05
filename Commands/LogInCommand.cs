using MVVM_SocialContractProject.Exceptions;
using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.Models.Database;
using MVVM_SocialContractProject.Services;
using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_SocialContractProject.Commands
{
    public class LogInCommand : CommandBase
    {
        private readonly LogInViewModel _loginVM;
        private readonly SocialContractMonitoringSystem scSystem;
        private readonly NavigationService navigationService;
        private readonly DatabaseQueries databaseQueries;

        public LogInCommand(LogInViewModel loginVM, SocialContractMonitoringSystem scSystem, NavigationService navigationService)
        {
            _loginVM = loginVM;
            this.scSystem = scSystem;
            this.navigationService = navigationService;
            databaseQueries = new DatabaseQueries();
            _loginVM.PropertyChanged += OnViewPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return
               !string.IsNullOrEmpty(_loginVM.Username) &&
               _loginVM.SecurePassword.Length > 0 &&
               !string.IsNullOrWhiteSpace(_loginVM.SecurePassword.ToString()) &&
               base.CanExecute(parameter);
        }
        public override void Execute(object parameter)
        {
            bool isAdmin = false;
            try
            {
                foreach (UserInfo user in scSystem.GetUserInfo(_loginVM.Username))
                {
                    byte[] salt = Convert.FromBase64String(user.Salt);
                    byte[] pass = Convert.FromBase64String(user.Password);
                    var input = databaseQueries.DeriveKey(_loginVM.SecurePassword, salt);
                    if (input.Length != pass.Length)
                    {
                        throw new UserLoginInvalidException();
                    }
                    else
                    {
                        for (int i = 0; i < input.Length; i++)
                        {
                            if (input[i] != pass[i])
                            {
                                throw new UserLoginInvalidException();
                            }
                        }
                    }
                    isAdmin = user.type == 0 ? false : true;
                }
                MessageBox.Show("Login Successful", "Success",
                   MessageBoxButton.OK, MessageBoxImage.Information);
                navigationService.Navigate(isAdmin);
            }
            catch (UserLoginInvalidException)
            {
                MessageBox.Show("Invalid Username and/or Password", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName == nameof(_loginVM.Username) ||
                e.PropertyName == nameof(_loginVM.SecurePassword))
            {
                OnCanExecuteChanged();
            }

        }
    }
}
