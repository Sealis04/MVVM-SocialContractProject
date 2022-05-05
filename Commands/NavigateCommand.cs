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
        private readonly bool _isEdit;

        public NavigateCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public NavigateCommand(NavigationService navigationService,StudentInfoViewModel student)
        {
            _student = student;
            _navigationService = navigationService;
        }

        public NavigateCommand(NavigationService navigationService,bool IsEdit)
        {
            _navigationService = navigationService;
            _isEdit = IsEdit;
        }

        public override void Execute(object parameter)
        {
            if (_isEdit)
            {
                UserInfoViewModel vm = parameter as UserInfoViewModel;
                _navigationService.Navigate(vm);
            }
            _navigationService.Navigate(_student);
        }

    }
}
