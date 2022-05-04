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

namespace MVVM_SocialContractProject.ViewModels
{
    public class SocialContractRecordsViewModel : ViewModelBase
    {
        //Collection for the student info
        private int TotalHours;
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

        public ICommand CreatePDF { get; }

        public ICommand PrintSCCommand { get; }
        public ICommand Logout  { get; } 
        public SocialContractRecordsViewModel(SocialContractMonitoringSystem SCSystem, NavigationService encodeSCNavigationView,
            NavigationService encodeUserNavigationView, NavigationService viewUserNavigationView, NavigationService createPDFNavigationView
            ,NavigationService LogOutView)
        {
            PrintSCCommand = new PrintSocialContract();
            _navigationService = viewUserNavigationView;
             _scSystem = SCSystem;
            EncodeSCCommand = new NavigateCommand(encodeSCNavigationView);
            CreateUser = new NavigateCommand(encodeUserNavigationView);
            CreatePDF = new NavigateCommand(createPDFNavigationView);
            Logout = new NavigateCommand(LogOutView);
            _studentInfo = new ObservableCollection<StudentInfoViewModel>();
            _socialContract = new ObservableCollection<SocialContractViewModel>();
            UpdateReservations(null);
        }

        private void UpdateReservations(string searchQuery)
        {
            _studentInfo.Clear();
            _socialContract.Clear();
            foreach (StudentInfo student in _scSystem.GetAllStudentInfo(searchQuery))
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
                    UpdateReservations(null);
                }
                UpdateReservations(_searchText);
                OnPropertyChanged(nameof(_searchText));
            }
        }
    }
}
