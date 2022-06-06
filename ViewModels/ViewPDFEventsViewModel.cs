using MVVM_SocialContractProject.Commands;
using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.Models.Database;
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
        private readonly DatabaseQueries dbQueries;
        public IEnumerable<EventsPDFViewModel> EventsPDF => _pdfInfo;

        public ICommand AddPDFCommand { get; }
        public ICommand ReturnCommand { get; }
        public ICommand PreviousCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand FirstCommand { get; private set; }
        public ICommand LastCommand { get; private set; }
        public ICommand PrintCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand _headerClick;
        public ICommand HeaderClick
        {
            get
            {
                if (_headerClick == null)
                {
                    _headerClick = new SortViewCommand(ScSystem, this);
                }
                return _headerClick;
            }
        }
        private int itemPerPage = 20;
        private int _currentPageIndex;
        public int CurrentPageIndex
        {
            get { return _currentPageIndex; }
            set
            {
                _currentPageIndex = value;
                OnPropertyChanged(nameof(CurrentPageIndex));
                OnPropertyChanged(nameof(CurrentPageChosen));
            }
        }
        public int CurrentPageChosen
        {
            get { return _currentPageIndex + 1; }
            set
            {
                _currentPageIndex = value;
                OnPropertyChanged(nameof(CurrentPageIndex));
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
        public ViewPDFEventsViewModel(SocialContractMonitoringSystem ScSystem, NavigationService AddPDFNav, NavigationService ViewStudentService
            ,NavigationService thisModel)
        {
            Start = 0;
            CurrentPageChosen = _currentPageIndex;
            this.ScSystem = ScSystem;
            addPDFNav = AddPDFNav;
            FirstCommand = new FirstPageCommand(this);
            LastCommand = new LastPageCommand(this);
            NextCommand = new NextPageComand(this);
            PreviousCommand = new PreviousPageCommand(this);
            dbQueries = new DatabaseQueries();
            PrintCommand = new PrintPDFCommand();
            RemoveCommand = new RemovePDFCommand(thisModel, ScSystem);
            viewStudentService = ViewStudentService;
            _pdfInfo = new ObservableCollection<EventsPDFViewModel>();
            AddPDFCommand = new NavigateCommand(addPDFNav);
            ReturnCommand = new NavigateCommand(viewStudentService);
            UpdatePDFTable(null, Start, StudentQuery,Direction);
        }
        public int Start { get; set; }
        public int StudentQuery { get; set; }
        public bool Direction { get; set; }
        public void UpdatePDFTable(string searchText, int page, int intQuery, bool direction)
        {
            totalItems = dbQueries.GetAllPDFCount(searchText);
            _pdfInfo.Clear();
           foreach (PDFInfo pdf in ScSystem.GetAllPDF(searchText, (0 + (page * 20)), intQuery,direction))
            {
                EventsPDFViewModel pdfvm = new EventsPDFViewModel(pdf);
                _pdfInfo.Add(pdfvm);
            }
            CalculateTotalPages(totalItems);
        }

        private void CalculateTotalPages(int totalItems)
        {
            if (totalItems == 0)
            {
                TotalPages = (totalItems / itemPerPage) + 1;
            }
            else
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
                    UpdatePDFTable(null, Start, StudentQuery, Direction);
                }
                Start = 0;
                CurrentPageIndex = 0;
                UpdatePDFTable(_searchText, Start, StudentQuery, Direction);
                OnPropertyChanged(nameof(_searchText));
            }
        }

        public void ShowNextPage()
        {
            CurrentPageIndex++;
            Start += itemPerPage;
            UpdatePDFTable(_searchText, Start, StudentQuery, Direction);
        }

        public void ShowPreviousPage()
        {
            CurrentPageIndex--;
            Start -= itemPerPage;
            UpdatePDFTable(_searchText, Start, StudentQuery, Direction);
        }

        public void ShowFirstPage()
        {
            CurrentPageIndex = 0;
            Start = 0;
            UpdatePDFTable(_searchText, Start, StudentQuery, Direction);
        }

        public void ShowLastPage()
        {
            CurrentPageIndex = TotalPages - 1;
            Start = (TotalPages * itemPerPage) - itemPerPage;
            UpdatePDFTable(_searchText, Start, StudentQuery, Direction);
        }
    }
}
