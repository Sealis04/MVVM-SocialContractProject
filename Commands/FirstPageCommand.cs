using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.Commands
{
    public class FirstPageCommand : CommandBase
    {
        private SocialContractRecordsViewModel viewModel;
        private ViewUsersListViewModel viewModel2;

        public FirstPageCommand(SocialContractRecordsViewModel viewModel)
        {
            this.viewModel = viewModel;
            viewModel.PropertyChanged += OnViewPropertyChanged;
        }

        public FirstPageCommand(ViewUsersListViewModel viewModel)
        {
            viewModel2 = viewModel;
            viewModel.PropertyChanged += OnViewPropertyChanged2;
        }

        public override bool CanExecute(object parameter)
        {
            if(viewModel == null)
            {
                return viewModel2.CurrentPageIndex != 0;
            }
            return viewModel.CurrentPageIndex != 0;
        }

        public override void Execute(object parameter)
        {
            if (viewModel == null)
            {
                viewModel2.ShowFirstPage();
            }
            else
            {
                viewModel.ShowFirstPage();
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
