using MVVM_SocialContractProject.Stores;
using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_SocialContractProject.Services
{
    public class NavigationService
    {
        private readonly NavigationStore _navigationStore;

        private readonly Func<ViewModelBase> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }
        public void Navigate()
        {
            _navigationStore.CurrentStudent = null;
            _navigationStore.CurrentViewModel = _createViewModel();
        }

        public void Navigate (StudentInfoViewModel Student)
        {
            _navigationStore.CurrentStudent = Student;
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
