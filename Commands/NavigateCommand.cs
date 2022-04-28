using MVVM_SocialContractProject.Services;
using MVVM_SocialContractProject.Stores;
using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_SocialContractProject.Commands
{
    public class NavigateCommand : CommandBase
    {

        private readonly NavigationService _navigationService;
        private readonly StudentInfoViewModel _student;

        public NavigateCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public NavigateCommand(NavigationService navigationService,StudentInfoViewModel student)
        {
            _student = student;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate(_student);
        }

    }
}
