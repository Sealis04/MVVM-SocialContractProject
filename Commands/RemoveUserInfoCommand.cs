using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.Models.Database;
using MVVM_SocialContractProject.Services;
using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_SocialContractProject.Commands
{
    public class RemoveUserInfoCommand : CommandBase
    {
        private readonly DatabaseQueries database;
        public RemoveUserInfoCommand(SocialContractMonitoringSystem scSystem, NavigationService navigate)
        {
            database = new DatabaseQueries();
            ScSystem = scSystem;
            Navigate = navigate;
        }

        public SocialContractMonitoringSystem ScSystem { get; }
        public NavigationService Navigate { get; }

        public override void Execute(object parameter)
        {
            UserInfoViewModel vm = parameter as UserInfoViewModel;
            MessageBoxResult result = MessageBox.Show("Confirm Removal of User?", "Remove User", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                //Db query
                database.RemoveUser(vm.UserName);
                var x = new NavigateCommand(Navigate);
                x.Execute(null);
            }
            else if (result == MessageBoxResult.Cancel)
            {
                return;
            }
        }
    }
}
