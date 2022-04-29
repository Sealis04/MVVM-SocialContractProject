using MVVM_SocialContractProject.Commands;
using MVVM_SocialContractProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_SocialContractProject.ViewModels
{
    public class ConnectToDBViewModel : ViewModelBase
    {
        private string server;
        private string database;
        private string username;
        private string password;

        public string Server
        {
            get
            {
                return server;
            }
            set
            {
                server = value;
                OnPropertyChanged(nameof(Server));
            }
        }

        public string Database
        {
            get
            {
                return database;
            }
            set
            {
                database = value;
                OnPropertyChanged(nameof(Database));
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand ConnectDB { get; }

        public ConnectToDBViewModel(NavigationService loginViewModel)
        {
            ConnectDB = new CheckDBCommand(this, loginViewModel);
        }
    }
}
