using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MVVM_SocialContractProject.Commands;
using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.Services;
using MVVM_SocialContractProject.Stores;

namespace MVVM_SocialContractProject.ViewModels
{
    public class SocialContractPerUserViewModel : ViewModelBase
    {
        private readonly SocialContractMonitoringSystem _SCSystem;
        private readonly ObservableCollection<SocialContractViewModel> _socialContract;
        private readonly StudentInfoViewModel _student;


        public string Firstname
        {
            get
            {
                return Student.FirstName;
            }
        }

        public string Middlename
        {
            get
            {
                return Student.MiddleName;
            }
        }

        public string Lastname
        {
            get
            {
                return Student.LastName;
            }
        }

        public string StudentName => Firstname + " " + Middlename + " " + Lastname;
        public int BatchNo
        {
            get
            {
                return Student.BatchNo;
            }
        }
        public string Course
        {
            get
            {
                return Student.Course;
            }
        }


        public int CurrentHours
        {
            get
            {
                return Student.CurrentHours;
            }
        }

        public int LackingHours => CurrentHours > 160 ? 0 : 160 - CurrentHours;
        public ICommand Return { get; }
        public ICommand Encode { get; }

        public ICommand PrintCommand { get; }

        public ICommand _headerClick;
        public ICommand HeaderClick
        {
            get
            {
                if (_headerClick == null)
                {
                    _headerClick = new SortViewCommand(_SCSystem, this);
                }
                return _headerClick;
            }
        }
        public ICommand RemoveCommand { get; }
        public IEnumerable<SocialContractViewModel> SocialContract => _socialContract;
        public SocialContractPerUserViewModel(SocialContractMonitoringSystem sCSystem, 
            NavigationService navigationService, 
            NavigationStore navigation, 
            NavigationService encodeSCModel,
            NavigationService thisModel)
        {
            
            _SCSystem = sCSystem;
            _socialContract = new ObservableCollection<SocialContractViewModel>();
            _student = navigation.CurrentStudent;
            PrintCommand = new PrintCommandForContract(_student);
            RemoveCommand = new RemoveSocialContract(thisModel, _student);
            LoadSocialContractInfo(_student,StudentQuery,Direction);
            Return = new NavigateCommand(navigationService);
            Encode = new NavigateCommand(encodeSCModel, _student);
        }
        public int StudentQuery { get; set; }
        public bool Direction { get; set; }

        public StudentInfoViewModel Student => _student;

        public void LoadSocialContractInfo(StudentInfoViewModel _student, int query, bool direction)
        {
            try
            {
                _socialContract.Clear();
                foreach (SocialContract SC in _SCSystem.GetSocialContractForUser(new StudentInfo(_student.StudentID, _student.FirstName, _student.MiddleName, _student.LastName, _student.BatchNo, _student.Course), query, direction))
                {
                    SocialContractViewModel contractInfo = new SocialContractViewModel(SC);

                    _socialContract.Add(contractInfo);
                }
            } catch(NullReferenceException e)
            {
                
            }
            
        }

    }
}
