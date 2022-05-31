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
        private ViewPDFEventsViewModel viewModel3;
        public LastPageCommand(SocialContractRecordsViewModel viewModel)
        {
            this.viewModel = viewModel;
            viewModel.PropertyChanged += OnViewPropertyChanged;
        }

        public LastPageCommand(ViewUsersListViewModel viewModel)
        {
            viewModel2 = viewModel;
            viewModel2.PropertyChanged += OnViewPropertyChanged2;
        }
        public LastPageCommand(ViewPDFEventsViewModel viewModel)
        {
            viewModel3 = viewModel;
            viewModel3.PropertyChanged += OnViewPropertyChanged3;
        }

        public override bool CanExecute(object parameter)
        {
            if (viewModel != null)
            {
                return viewModel.CurrentPageIndex != (viewModel.TotalPages - 1 <0 ? 0 : viewModel.TotalPages - 1);

            }
            else if (viewModel2 != null)
            {
                return viewModel2.CurrentPageIndex != (viewModel2.TotalPages - 1 < 0 ? 0 : viewModel2.TotalPages - 1);
            }
            else
            {
                return viewModel3.CurrentPageIndex != (viewModel3.TotalPages - 1 < 0 ? 0 : viewModel3.TotalPages - 1);
            }
        }

        public override void Execute(object parameter)
        {
            if (viewModel != null)
            {
                viewModel.ShowLastPage();
            }
            else if (viewModel2 != null)
            {
                viewModel2.ShowLastPage();
            }
            else
            {
                viewModel3.ShowLastPage();
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

        private void OnViewPropertyChanged3(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewPDFEventsViewModel.CurrentPageIndex))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
