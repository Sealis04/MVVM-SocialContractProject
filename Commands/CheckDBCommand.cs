
using MVVM_SocialContractProject.Models.Database;
using MVVM_SocialContractProject.Services;
using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.Commands
{
    public class CheckDBCommand : CommandBase
    {

        private readonly ConnectToDBViewModel _db;
        private readonly DatabaseQueries databaseQueries;
        public CheckDBCommand(ConnectToDBViewModel db, NavigationService loginVm)
        {
            _db = db;
            LoginVm = loginVm;
            databaseQueries = new DatabaseQueries();
            _db.PropertyChanged += OnViewPropertyChanged;
        }

        public NavigationService LoginVm { get; }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_db.Server) &&
                   !string.IsNullOrEmpty(_db.Username) &&
                   !string.IsNullOrEmpty(_db.Database) &&
                   !string.IsNullOrEmpty(_db.TCPIP)&&
                    base.CanExecute(parameter);
        }
        public override void Execute(object parameter)
        {
            Properties.Settings.Default.Server = _db.Server;
            Properties.Settings.Default.Username = _db.Username;
            Properties.Settings.Default.Database = _db.Database;
            Properties.Settings.Default.Password = (_db.Password == null) ? "" : _db.Password;
            Properties.Settings.Default.TCPIP = _db.TCPIP;
            Properties.Settings.Default.Save();
            databaseQueries.RunConnectionCheck();
            LoginVm.Navigate();
        }

        private void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_db.Server) ||
                e.PropertyName == nameof(_db.Username) ||
                e.PropertyName == nameof(_db.Database)||
                e.PropertyName == nameof(_db.TCPIP))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
