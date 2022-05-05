using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.Commands
{
    public class LastPageCommand : CommandBase
    {
        private SocialContractRecordsViewModel viewModel;

        private ViewUsersListViewModel viewModel2;
        public LastPageCommand(SocialContractRecordsViewModel viewModel)
        {
            this.viewModel = viewModel;
            viewModel.PropertyChanged += OnViewPropertyChanged;
        }

        public LastPageCommand(ViewUsersListViewModel viewModel)
        {
            viewModel2 = viewModel;
            viewModel2.PropertyChanged += OnViewPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            if(viewModel == null)
            {
                return viewModel2.CurrentPageIndex != viewModel2.TotalPages - 1;
            }
            return viewModel.CurrentPageIndex != viewModel.TotalPages - 1;
        }

        public override void Execute(object parameter)
        {
            if (viewModel == null)
            {
                viewModel2.ShowLastPage();
            }
            else
            {
                viewModel.ShowLastPage();
            }
            
        }
        private void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SocialContractRecordsViewModel.CurrentPageIndex))
            {
                OnCanExecuteChanged();
            }
        }

        private void OnViewPropertyChanged2(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewUsersListViewModel.CurrentPageIndex))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
