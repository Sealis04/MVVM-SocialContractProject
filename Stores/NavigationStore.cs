using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.Stores
{
   public class NavigationStore
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public StudentInfoViewModel CurrentStudent { get; set; }

        public UserInfoViewModel CurrentUser { get; set; }

        public bool IsAdmin { get; set; }
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        public event Action CurrentViewModelChanged;

    }
}
