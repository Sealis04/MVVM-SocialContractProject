using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.Commands
{
    public class PreviousPageCommand:CommandBase
    {
        private SocialContractRecordsViewModel viewModel;

        private ViewUsersListViewModel viewModel2;
        private ViewPDFEventsViewModel viewModel3;
        public PreviousPageCommand(SocialContractRecordsViewModel viewModel)
        {
            this.viewModel = viewModel;

            viewModel.PropertyChanged += OnViewPropertyChanged;
        }

        public PreviousPageCommand(ViewUsersListViewModel viewModel)
        {
            viewModel2 = viewModel;

            viewModel2.PropertyChanged += OnViewPropertyChanged2;
        }
        public PreviousPageCommand(ViewPDFEventsViewModel viewModel)
        {
            viewModel3 = viewModel;
            viewModel3.PropertyChanged += OnViewPropertyChanged3;
        }

        public override bool CanExecute(object parameter)
        {

            if (viewModel != null)
            {
                return viewModel.CurrentPageIndex != 0;

            }
            else if (viewModel2 != null)
            {
                return viewModel2.CurrentPageIndex != 0;
            }
            else
            {
                return viewModel3.CurrentPageIndex != 0;
            }
           
        }

        public override void Execute(object parameter)
        {
            if (viewModel != null)
            {
                viewModel.ShowPreviousPage();
            }
            else if (viewModel2 != null)
            {
                viewModel2.ShowPreviousPage();
            }
            else
            {
                viewModel3.ShowPreviousPage();
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
