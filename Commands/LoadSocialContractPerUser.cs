using MVVM_SocialContractProject.Models;
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
    public class LoadSocialContractPerUser : CommandBase
    {
        private readonly SocialContractMonitoringSystem scSystem;
        private readonly NavigationService navigationService;

        public LoadSocialContractPerUser(SocialContractMonitoringSystem scSystem, NavigationService navigationService)
        {
            this.scSystem = scSystem;
            this.navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            try
            {
                StudentInfoViewModel listView = parameter as StudentInfoViewModel;
                if(listView != null)
                {
                    navigationService.Navigate(listView);
                }
            }catch (NullReferenceException e)
            {
                MessageBox.Show("message" + e);
            }
           
        }
    }
}
