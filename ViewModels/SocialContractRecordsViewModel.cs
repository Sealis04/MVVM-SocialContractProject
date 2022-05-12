using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MVVM_SocialContractProject.Models;
using MySql.Data.MySqlClient;
using MVVM_SocialContractProject.Commands;
using MVVM_SocialContractProject.Services;
using System.Windows;
using Prism.Commands;
using System.Windows.Controls;
using MVVM_SocialContractProject.Models.Database;
using MVVM_SocialContractProject.Stores;

namespace MVVM_SocialContractProject.ViewModels
{
    public class SocialContractRecordsViewModel : ViewModelBase
    {
        //Collection for the student info
        private int TotalHours;
        private readonly DatabaseQueries _dbQueries;
        private readonly NavigationService _navigationService;
        private readonly ObservableCollection<StudentInfoViewModel> _studentInfo;
        public IEnumerable<StudentInfoViewModel> StudentInfo => _studentInfo;

        //Collection for the social contract per sem
        private readonly ObservableCollection<SocialContractViewModel> _socialContract;

        public IEnumerable<SocialContractViewModel> SocialContract => _socialContract;

        private readonly SocialContractMonitoringSystem _scSystem;

        //Command for Button Interaction
        public ICommand EncodeSCCommand { get; }
        public ICommand CreateUser { get; }

        public ICommand _doubleClick;
        private string _searchText;

        public ICommand DoubleClick {
            get
            {
                if (_doubleClick == null)
                {
                   _doubleClick = new LoadSocialContractPerUser(_scSystem, _navigationService);
                }
                return _doubleClick;
            }
        }
        public ICommand _headerClick;
        public ICommand HeaderClick
        {
            get
            {
                if (_headerClick == null)
                {
                    MessageBox.Show("ASDASDSAd");
                }
                return _headerClick;
            }
        }

        public ICommand CreatePDF { get; }

        public ICommand PrintSCCommand { get; }
        public ICommand Logout  { get; }

        //Pagination
        public ICommand PreviousCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand FirstCommand { get; private set; }
        public ICommand LastCommand { get; private set; }

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

        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public SocialContractRecordsViewModel(SocialContractMonitoringSystem SCSystem, NavigationStore navstore, NavigationService encodeSCNavigationView,
            NavigationService encodeUserNavigationView, NavigationService viewUserNavigationView, NavigationService createPDFNavigationView
            ,NavigationService LogOutView)
        {
            IsEnabled = true;
            if(!navstore.IsAdmin)
            {
                IsEnabled = false;
            }



            Start = 0;
            _dbQueries = new DatabaseQueries();
            FirstCommand = new FirstPageCommand(this);
            LastCommand = new LastPageCommand(this);
            NextCommand = new NextPageComand(this);
            PreviousCommand = new PreviousPageCommand(this);
            PrintSCCommand = new PrintSocialContract();
            _navigationService = viewUserNavigationView;
             _scSystem = SCSystem;
            EncodeSCCommand = new NavigateCommand(encodeSCNavigationView);
            CreateUser = new NavigateCommand(encodeUserNavigationView);
            CreatePDF = new NavigateCommand(createPDFNavigationView);
            Logout = new NavigateCommand(LogOutView);
            _studentInfo = new ObservableCollection<StudentInfoViewModel>();
            _socialContract = new ObservableCollection<SocialContractViewModel>();
            UpdateReservations(null, Start);
        }

        private void UpdateReservations(string searchQuery, int page)
        {
            totalItems = _dbQueries.GetStudentCount(searchQuery);
            _studentInfo.Clear();
            _socialContract.Clear();
            CurrentPageChosen = _currentPageIndex;
            foreach (StudentInfo student in _scSystem.GetAllStudentInfo(searchQuery,page))
            {
                TotalHours = 0;
                foreach (SocialContract contract in _scSystem.GetSocialContractForUser(student))
                {
                    SocialContractViewModel scModel = new SocialContractViewModel(contract);
                    TotalHours += scModel.TotalHours;
                    _socialContract.Add(scModel);
                }
                StudentInfoViewModel studentInfoViewModel = new StudentInfoViewModel(student)
                {
                    CurrentHours = TotalHours
                };
                _studentInfo.Add(studentInfoViewModel);
            }
            CalculateTotalPages(totalItems);
        }

        private int totalItems;
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
                    UpdateReservations(null, Start);
                }
                Start = 0;
                CurrentPageIndex = 0;
                UpdateReservations(_searchText, Start);
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public int Start { get; set; }

        public void ShowNextPage()
        {
            CurrentPageIndex++;
            Start += itemPerPage;
            UpdateReservations(_searchText, Start);
        }

        public void ShowPreviousPage()
        {
            CurrentPageIndex--;
            Start -= itemPerPage;
            UpdateReservations(_searchText, Start);
        }

        public void ShowFirstPage()
        {
            CurrentPageIndex = 0;
            Start = 0;
            UpdateReservations(_searchText, Start);
        }

        public void ShowLastPage()
        {
            CurrentPageIndex = TotalPages - 1;
            Start = (TotalPages * itemPerPage) - itemPerPage;
            UpdateReservations(_searchText, Start);
        }


        //Sorting


    }
}
