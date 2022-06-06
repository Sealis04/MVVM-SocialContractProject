using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.Models.Database;
using MVVM_SocialContractProject.Services;
using MVVM_SocialContractProject.Stores;
using MVVM_SocialContractProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.ServiceProcess;
using System.Net.Sockets;

namespace MVVM_SocialContractProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly SocialContractMonitoringSystem _SCSystem;
        private readonly NavigationStore _navigationStore;
        private readonly DatabaseQueries _dbQueries;
        public App()
        {
            _SCSystem = new SocialContractMonitoringSystem();
            _navigationStore = new NavigationStore();
            _dbQueries = new DatabaseQueries();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            String thisprocessname = Process.GetCurrentProcess().ProcessName;

            if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1)
            return;
            ConnectDB();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
        private void ConnectDB()
        {
            bool x = _dbQueries.RunConnectionCheck();
            if (x)
            {
                _navigationStore.CurrentViewModel = LogInViewModel();
            }
            else
            {
                _navigationStore.CurrentViewModel = ConnectDBViewModel();
            }
        }
        private ConnectToDBViewModel ConnectDBViewModel()
        {
            return new ConnectToDBViewModel(new NavigationService(_navigationStore, LogInViewModel));
        }
        private EncodeSCViewModel EncodeSocialContractViewModel()
        {
            return new EncodeSCViewModel(_SCSystem, new NavigationService(_navigationStore, ViewStudentRecords),_navigationStore, 
                new NavigationService(_navigationStore, SCViewModel));
        }

        private SocialContractRecordsViewModel ViewStudentRecords()
        {
            return new SocialContractRecordsViewModel(_SCSystem, 
                _navigationStore,
                new NavigationService(_navigationStore, EncodeSocialContractViewModel),
                new NavigationService(_navigationStore, ViewUserListVM), 
                new NavigationService(_navigationStore, SCViewModel),
                new NavigationService(_navigationStore, PDFViewModel),
                new NavigationService(_navigationStore, LogInViewModel));
        }

        private LogInViewModel LogInViewModel()
        {
            return new LogInViewModel(_SCSystem, new NavigationService(_navigationStore, ViewStudentRecords)
                , new NavigationService(_navigationStore, ConnectDBViewModel));
        }

        private SignUpViewModel SignUpVM()
        {
            return new SignUpViewModel(_SCSystem, _navigationStore,  new NavigationService(_navigationStore, ViewUserListVM));
        }

        private SocialContractPerUserViewModel SCViewModel()
        {
            return new SocialContractPerUserViewModel(_SCSystem, new NavigationService(_navigationStore, ViewStudentRecords), _navigationStore, 
                new NavigationService(_navigationStore, EncodeSocialContractViewModel)
                ,new NavigationService(_navigationStore, SCViewModel));
        }

        private CreatePDFEventViewModel CreatePDFViewModel()
        {
            return new CreatePDFEventViewModel(_SCSystem, new NavigationService(_navigationStore, PDFViewModel));
        }

        private ViewPDFEventsViewModel PDFViewModel()
        {
            return new ViewPDFEventsViewModel(_SCSystem, new NavigationService(_navigationStore, CreatePDFViewModel), 
                                            new NavigationService(_navigationStore, ViewStudentRecords),
                                            new NavigationService(_navigationStore,PDFViewModel);
        }

        private ViewUsersListViewModel ViewUserListVM()
        {
            return new ViewUsersListViewModel(_SCSystem, new NavigationService(_navigationStore, ViewStudentRecords)
                , new NavigationService(_navigationStore, SignUpVM)
                , new NavigationService(_navigationStore, ViewUserListVM));
        }
    }
}
