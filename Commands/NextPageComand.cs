using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_SocialContractProject.Commands
{
    public class NextPageComand:CommandBase
    {
        private SocialContractRecordsViewModel viewModel;
        private ViewUsersListViewModel viewModel2;
        private ViewPDFEventsViewModel viewModel3;
        public NextPageComand(SocialContractRecordsViewModel viewModel)
        {
            this.viewModel = viewModel;
            viewModel.PropertyChanged += OnViewPropertyChanged;
        }

        public NextPageComand(ViewUsersListViewModel viewModel)
        {
            viewModel2 = viewModel;
            viewModel2.PropertyChanged += OnViewPropertyChanged2;
        }

        public NextPageComand(ViewPDFEventsViewModel viewModel)
        {
            viewModel3 = viewModel;
            viewModel3.PropertyChanged += OnViewPropertyChanged3;
        }

        public override bool CanExecute(object parameter)
        {
            if(viewModel != null)
            {
                return viewModel.TotalPages - 1 > viewModel.CurrentPageIndex;
                
            }else if (viewModel2 != null)
            {
                return viewModel2.TotalPages - 1 > viewModel2.CurrentPageIndex;
            }
            else
            {
                return viewModel3.TotalPages - 1 > viewModel3.CurrentPageIndex;
            }
          
        }

        public override void Execute(object parameter)
        {
            if (viewModel != null)
            {
                viewModel.ShowNextPage();
            }
            else if(viewModel2 != null)
            {
                viewModel2.ShowNextPage();
            }
            else
            {
                viewModel3.ShowNextPage();
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
