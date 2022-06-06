using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.Services;
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
    public class SubmitPDFEventCommand : CommandBase
    {
        SocialContractMonitoringSystem scSystem;
        private readonly NavigationService navService;
        private readonly CreatePDFEventViewModel pdfVM;

        public SubmitPDFEventCommand(SocialContractMonitoringSystem scSystem, NavigationService navService, CreatePDFEventViewModel pdfVM)
        {
            this.scSystem = scSystem;
            this.navService = navService;
            this.pdfVM = pdfVM;
            pdfVM.PropertyChanged += OnViewPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(pdfVM.EventName) &&
                !string.IsNullOrEmpty(pdfVM.EventSupervisor) &&
                !string.IsNullOrEmpty(pdfVM.EventVenue) &&
                !string.IsNullOrEmpty(pdfVM.ImageSource) &&
                base.CanExecute(parameter);
        }
        public override void Execute(object parameter)
        {
            try
            {
                scSystem.CreateEventPdf(new PDFInfo("0",pdfVM.EventName, pdfVM.EventSupervisor, pdfVM.ImageSource, pdfVM.EventVenue,pdfVM.EventDate));
                navService.Navigate();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error" + e, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CreatePDFEventViewModel.EventName) ||
                e.PropertyName == nameof(CreatePDFEventViewModel.EventSupervisor) ||
                e.PropertyName == nameof(CreatePDFEventViewModel.EventVenue) ||
                e.PropertyName == nameof(CreatePDFEventViewModel.ImageSource))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
