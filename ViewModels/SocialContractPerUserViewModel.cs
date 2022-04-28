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
                return _student.FirstName;
            }
        }

        public string Middlename
        {
            get
            {
                return _student.MiddleName;
            }
        }

        public string Lastname
        {
            get
            {
                return _student.LastName;
            }
        }

        public string StudentName => Firstname + " " + Middlename + " " + Lastname;
        public int BatchNo
        {
            get
            {
                return _student.BatchNo;
            }
        }
        public string Course
        {
            get
            {
                return _student.Course;
            }
        }


        public int CurrentHours
        {
            get
            {
                return _student.CurrentHours;
            }
        }

        public int LackingHours => 160 - CurrentHours;
        public ICommand Return { get; }
        public ICommand Encode { get; }

        public ICommand PrintCommand { get; }

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
            LoadSocialContractInfo(_student);
            Return = new NavigateCommand(navigationService);
            Encode = new NavigateCommand(encodeSCModel, _student);
        }

        private void LoadSocialContractInfo(StudentInfoViewModel _student)
        {
            try
            {
                _socialContract.Clear();
                foreach (SocialContract SC in _SCSystem.GetSocialContractForUser(new StudentInfo(_student.StudentID, _student.FirstName, _student.MiddleName, _student.LastName, _student.BatchNo, _student.Course)))
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
