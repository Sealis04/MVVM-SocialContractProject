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

        public FirstPageCommand(SocialContractRecordsViewModel viewModel)
        {
            this.viewModel = viewModel;
            viewModel.PropertyChanged += OnViewPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return viewModel.CurrentPageIndex != 0;
        }

        public override void Execute(object parameter)
        {
            viewModel.ShowFirstPage();
        }
        private void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SocialContractRecordsViewModel.CurrentPageIndex))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
