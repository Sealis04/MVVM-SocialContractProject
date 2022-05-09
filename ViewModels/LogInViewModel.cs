using MVVM_SocialContractProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using MVVM_SocialContractProject.Services;
using MVVM_SocialContractProject.Commands;
using System.Windows.Input;
using System.Windows;

namespace MVVM_SocialContractProject.ViewModels
{
    public class LogInViewModel : ViewModelBase
    {
        private string _userName;

        private SecureString _password;

        public string Username
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public SecureString SecurePassword
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(SecurePassword));
            }
        }
        
        public ICommand LoginCommand { get; }

        public ICommand connectToDB { get; }
        
        public LogInViewModel(SocialContractMonitoringSystem scSystem, NavigationService navigate, NavigationService ConnectToDB)
        {
            LoginCommand = new LogInCommand(this,scSystem, navigate);
            connectToDB = new NavigateCommand(ConnectToDB);
        }
    }
}
