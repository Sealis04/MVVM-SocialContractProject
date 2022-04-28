using MVVM_SocialContractProject.Commands;
using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_SocialContractProject.ViewModels
{
    public class ViewPDFEventsViewModel : ViewModelBase
    {
        private readonly ObservableCollection<EventsPDFViewModel> _pdfInfo;
        private readonly SocialContractMonitoringSystem ScSystem;
        private readonly NavigationService addPDFNav;
        private readonly NavigationService viewStudentService;

        public IEnumerable<EventsPDFViewModel> EventsPDF => _pdfInfo;

        public ICommand AddPDFCommand { get; }
        public ICommand ReturnCommand { get; }

        public ICommand PrintCommand { get; }
        public ViewPDFEventsViewModel(SocialContractMonitoringSystem ScSystem, NavigationService AddPDFNav, NavigationService ViewStudentService)
        {
          
            this.ScSystem = ScSystem;
            addPDFNav = AddPDFNav;
            PrintCommand = new PrintPDFCommand();
            viewStudentService = ViewStudentService;
            _pdfInfo = new ObservableCollection<EventsPDFViewModel>();
            AddPDFCommand = new NavigateCommand(addPDFNav);
            ReturnCommand = new NavigateCommand(viewStudentService);
            UpdatePDFTable(null);
        }

        public void UpdatePDFTable(string searchText)
        {
            _pdfInfo.Clear();
           foreach (PDFInfo pdf in ScSystem.GetAllPDF(searchText))
            {
                EventsPDFViewModel pdfvm = new EventsPDFViewModel(pdf);
                _pdfInfo.Add(pdfvm);
            }
        }

        private string _searchText;

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                if (_searchText == "")
                {
                    UpdatePDFTable(null);
                }
                UpdatePDFTable(_searchText);
                OnPropertyChanged(nameof(_searchText));
            }
        }
    }
}
