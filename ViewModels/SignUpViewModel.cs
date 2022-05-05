using MVVM_SocialContractProject.Commands;
using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.Services;
using MVVM_SocialContractProject.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_SocialContractProject.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private string _userName;

        private SecureString _password;

        private SecureString _confirmPassword;


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

        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public SecureString ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public SignUpViewModel(SocialContractMonitoringSystem scSystem, NavigationStore nav, NavigationService navigationService)
        {
            if(nav != null)
            {
                _userName = nav.CurrentUser.UserName;
            }
            SubmitCommand = new EncodeUserInfoCommand(this, scSystem, navigationService);
            CancelCommand = new NavigateCommand(navigationService);
        }

        public ICommand SubmitCommand { get; }

        public ICommand CancelCommand { get; }

    }
}
