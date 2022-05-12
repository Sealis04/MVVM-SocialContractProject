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
    public class ViewUsersListViewModel:ViewModelBase
    {
        private readonly ObservableCollection<UserInfoViewModel> _userInfo;
        public ObservableCollection<UserInfoViewModel> UserInfo => _userInfo;
        private readonly SocialContractMonitoringSystem ScSystem;
        private readonly DatabaseQueries dbQueries;
        public ICommand CreateUser { get; }
        public ICommand PreviousCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand FirstCommand { get; private set; }
        public ICommand LastCommand { get; private set; }
        public ICommand ReturnCommand { get; }

        public ICommand EditCommand { get; }
        public ICommand RemoveCommand { get; }

        private string _searchText;

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
        public ViewUsersListViewModel(SocialContractMonitoringSystem scSystem, NavigationService Return, NavigationService CreateUser, NavigationService ReturnThis)
        {
            EditCommand = new NavigateCommand(CreateUser, true);
            RemoveCommand = new RemoveUserInfoCommand(scSystem, ReturnThis);
            dbQueries = new DatabaseQueries();
            Start = 0;
            ScSystem = scSystem;
            CurrentPageChosen = _currentPageIndex;
            _userInfo = new ObservableCollection<UserInfoViewModel>();
            this.CreateUser = new NavigateCommand(CreateUser);
            ReturnCommand = new NavigateCommand(Return);
            FirstCommand = new FirstPageCommand(this);
            LastCommand = new LastPageCommand(this);
            NextCommand = new NextPageComand(this);
            PreviousCommand = new PreviousPageCommand(this);
            UpdateUserList(null, Start);
        }

        private void UpdateUserList(string SearchQuery, int page)
        {
            totalItems = dbQueries.GetAllUserCount(SearchQuery);
            _userInfo.Clear();
            foreach (UserInfo user in ScSystem.GetAllUsers(SearchQuery,page))
            {
                _userInfo.Add(new UserInfoViewModel(new UserInfo(user.Username, user.Password, user.Salt, user.type)));
            }
            CalculateTotalPages(totalItems);
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
                    UpdateUserList(_searchText, Start);
                }
                Start = 0;
                UpdateUserList(_searchText, Start);
                OnPropertyChanged(nameof(SearchText));
            }
        }


        public int Start { get; set; }

        public void ShowNextPage()
        {
            CurrentPageIndex++;
            Start += itemPerPage;
            UpdateUserList(_searchText, Start);
        }

        public void ShowPreviousPage()
        {
            CurrentPageIndex--;
            Start -= itemPerPage;
            UpdateUserList(_searchText, Start);
        }

        public void ShowFirstPage()
        {
            CurrentPageIndex = 0;
            Start = 0;
            UpdateUserList(_searchText, Start);
        }

        public void ShowLastPage()
        {
            CurrentPageIndex = TotalPages - 1;
            Start = (TotalPages * itemPerPage) - itemPerPage;
            UpdateUserList(_searchText, Start);
        }

    }
}
