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
        private int itemPerPage = 20;
        private int _currentPageIndex;
        public int CurrentPageIndex
        {
            get { return _currentPageIndex; }
            set
            {
                _currentPageIndex = value;
                OnPropertyChanged(nameof(CurrentPageIndex));
            }
        }
        public int CurrentPageChosen
        {
            get { return _currentPageIndex + 1; }
            set
            {
                _currentPageIndex = value;
                OnPropertyChanged(nameof(CurrentPageChosen));
            }
        }

        private int totalItems;
        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            private set
            {
                _totalPages = value;
                OnPropertyChanged(nameof(TotalPages));
            }
        }
        public ViewPDFEventsViewModel(SocialContractMonitoringSystem ScSystem, NavigationService AddPDFNav, NavigationService ViewStudentService)
        {
            Start = 0;
            this.ScSystem = ScSystem;
            addPDFNav = AddPDFNav;
            PrintCommand = new PrintPDFCommand();
            viewStudentService = ViewStudentService;
            _pdfInfo = new ObservableCollection<EventsPDFViewModel>();
            AddPDFCommand = new NavigateCommand(addPDFNav);
            ReturnCommand = new NavigateCommand(viewStudentService);
            UpdatePDFTable(null, Start);
        }
        public int Start { get; set; }
        public void UpdatePDFTable(string searchText, int page)
        {
            _pdfInfo.Clear();
           foreach (PDFInfo pdf in ScSystem.GetAllPDF(searchText, page))
            {
                EventsPDFViewModel pdfvm = new EventsPDFViewModel(pdf);
                _pdfInfo.Add(pdfvm);
            }
        }

        private void CalculateTotalPages(int totalItems)
        {
            if (totalItems % itemPerPage == 0)
            {
                TotalPages = (totalItems / itemPerPage);
            }
            else
            {
                TotalPages = (totalItems / itemPerPage) + 1;
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
                    UpdatePDFTable(null, Start);
                }
                Start = 0;
                UpdatePDFTable(_searchText, Start);
                OnPropertyChanged(nameof(_searchText));
            }
        }

        public void ShowNextPage()
        {
            CurrentPageIndex++;
            Start += itemPerPage;
            UpdatePDFTable(_searchText, Start);
        }

        public void ShowPreviousPage()
        {
            CurrentPageIndex--;
            Start -= itemPerPage;
            UpdatePDFTable(_searchText, Start);
        }

        public void ShowFirstPage()
        {
            CurrentPageIndex = 0;
            Start = 0;
            UpdatePDFTable(_searchText, Start);
        }

        public void ShowLastPage()
        {
            CurrentPageIndex = TotalPages - 1;
            Start = (TotalPages * itemPerPage) - itemPerPage;
            UpdatePDFTable(_searchText, Start);
        }
    }
}
