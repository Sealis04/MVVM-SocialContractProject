using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.Commands
{
    public class NextPageComand:CommandBase
    {
        private SocialContractRecordsViewModel viewModel;
        private ViewUsersListViewModel viewModel2;
        public NextPageComand(SocialContractRecordsViewModel viewModel)
        {
            this.viewModel = viewModel;
            viewModel.PropertyChanged += OnViewPropertyChanged;
        }

        public NextPageComand(ViewUsersListViewModel viewModel)
        {
            viewModel2 = viewModel;
            viewModel2.PropertyChanged += OnViewPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            if(viewModel == null)
            {
                return viewModel2.TotalPages - 1 > viewModel2.CurrentPageIndex;
            }
            return viewModel.TotalPages - 1 > viewModel.CurrentPageIndex;
        }

        public override void Execute(object parameter)
        {
            if (viewModel == null)
            {
                viewModel2.ShowNextPage();
            }
            else
            {
                viewModel.ShowNextPage();
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
