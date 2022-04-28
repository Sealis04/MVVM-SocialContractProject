using MVVM_SocialContractProject.Commands;
using MVVM_SocialContractProject.Models;
using MVVM_SocialContractProject.Services;
using MVVM_SocialContractProject.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVM_SocialContractProject.ViewModels
{
    public class EncodeSCViewModel : ViewModelBase
    {
        private readonly StudentInfoViewModel studentInfo;

        private string _filePath;

        private string _firstName;

        private string _middleName;

        private string _lastName;

        private int _batchNo;

        private string _course;

        private int _schoolYear = DateTime.Today.Year;

        private int _firstSem;

        private int _secondSem;

        private int _summer;

        private string _studentID;
        public string ImageSource
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        public string Firstname
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(Firstname));
            }
        }

        public string Middlename
        {
            get
            {
                return _middleName;
            }
            set
            {
                _middleName = value;
                OnPropertyChanged(nameof(Middlename));
            }
        }

        public string Lastname
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(Lastname));
            }
        }

        public int BatchNo
        {
            get
            {
                return _batchNo;
            }
            set
            {
                _batchNo = value;
                OnPropertyChanged(nameof(BatchNo));
            }
        }

        public string Course
        {
            get
            {
                return _course;
            }
            set
            {
                _course = value;
                OnPropertyChanged(nameof(Course));
            }
        }

        public int SchoolYear
        {
            get
            {
                return _schoolYear;
            }
            set
            {
                _schoolYear = value;
                OnPropertyChanged(nameof(SchoolYear));
            }
        }

        public int FirstSem
        {
            get
            {
                return _firstSem;
            }
            set
            {
                _firstSem = value;
                OnPropertyChanged(nameof(FirstSem));
            }
        }

        public int SecondSem
        {
            get
            {
                return _secondSem;
            }
            set
            {
                _secondSem = value;
                OnPropertyChanged(nameof(SecondSem));
            }
        }

        public int Summer
        {
            get
            {
                return _summer;
            }
            set
            {
                _summer = value;
                OnPropertyChanged(nameof(Summer));
            }
        }
        public string StudentID
        {
            get
            {
                return _studentID;
            }
            set
            {
                _studentID = value;
                if (!MatchQuery(_studentID))
                {
                    SearchQuery(_studentID);
                }
                OnPropertyChangeSearch(nameof(StudentID));
            }
        }

        public NavigationService navService;
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public ICommand _textChange;

        public ICommand TextChange
        {
            get
            {
                if(_textChange == null)
                {
                    _textChange = new NavigateCommand(navService);
                }
                return _textChange;
            }
        }
        public ICommand _uploadImage;
        public ICommand UploadImage
        {
            get
            {
                if(_uploadImage == null)
                {
                    _uploadImage = new UploadPdfCommand(this);
                }
                return _uploadImage;
            }
        }
        private bool isEnabled1;
        private bool isEnabled2;
        private bool isEnabled3;
        private bool isEnabled4;
        private bool isEnabled5;
        public bool FNEnabled
        {
            get { return isEnabled1; }
            set
            {
               
                if (isEnabled1 == value)
                {
                    return;
                }
                isEnabled1 = value;
                OnPropertyChanged(nameof(FNEnabled));
            }
        }
        public bool MNEnabled
        {
            get { return isEnabled2; }
            set
            {

                if (isEnabled2 == value)
                {
                    return;
                }
                isEnabled2 = value;
                OnPropertyChanged(nameof(MNEnabled));
            }
        }
        public bool LNEnabled
        {
            get { return isEnabled3; }
            set
            {

                if (isEnabled3 == value)
                {
                    return;
                }
                isEnabled3 = value;
                OnPropertyChanged(nameof(LNEnabled));
            }
        }
        public bool BNEnabled
        {
            get { return isEnabled4; }
            set
            {

                if (isEnabled4 == value)
                {
                    return;
                }
                isEnabled4 = value;
                OnPropertyChanged(nameof(BNEnabled));
            }
        }
        public bool CEnabled
        {
            get { return isEnabled5; }
            set
            {

                if (isEnabled5 == value)
                {
                    return;
                }
                isEnabled5 = value;
                OnPropertyChanged(nameof(CEnabled));
            }
        }
        ///
        public EncodeSCViewModel(SocialContractMonitoringSystem _scSystem, NavigationService navigationService, 
            NavigationStore _navigationStore, NavigationService SCViewModel)
        {
            EnableTBs();
            scSystem = _scSystem;
            navService = navigationService;
            studentInfo = _navigationStore.CurrentStudent;
            if (studentInfo != null)
            {
                this.StudentID = studentInfo.StudentID;
                this.Firstname = studentInfo.FirstName;
                this.Middlename = studentInfo.MiddleName;
                this.Lastname = studentInfo.LastName;
                this.BatchNo = studentInfo.BatchNo;
                this.Course = studentInfo.Course;
                DisableTBs();
                CancelCommand = new NavigateCommand(SCViewModel, studentInfo);
            }
            else
            {
                EnableTBs();
                CancelCommand = new NavigateCommand(navigationService);
            }
            SubmitCommand = new EncodeSCCommand(this, _scSystem, navigationService);

            //dropdown
            studentIDCollection = new ObservableCollection<string>();
        }

        public void SearchQuery(string StudentID)
        {
            studentIDCollection.Clear();
            foreach (StudentInfo stu in scSystem.SearchForUser(StudentID))
            {
                studentIDCollection.Add(stu.StudentID);
            }
        }

        public bool MatchQuery(string StudentID)
        {
            EnableTBs();
            int count = 0;
            foreach (StudentInfo stu in scSystem.GetStudentInfo(StudentID))
            {
                count++;
                StudentID = stu.StudentID;
                Firstname = stu.FirstName;
                Middlename = stu.MiddleName;
                Lastname = stu.LastName;
                BatchNo = stu.BatchNo;
                Course = stu.Course;
                DisableTBs();
            }
            if (count > 0)
            {
                return true;
            }
            else
            {
                this.Firstname = "";
                this.Middlename = "";
                this.Lastname = "";
                this.BatchNo = 0;
                this.Course = "";
            }

            return false;
        }
        // Dropdown part
        private readonly SocialContractMonitoringSystem scSystem;
        public ObservableCollection<string> studentIDCollection { get; set; }
        private string _selectedID;
        public string SelectedID
        {
            get
            {
                return _selectedID;
            }
            set
            {
                if (value != _selectedID)
                {
                    _selectedID = value;
                    OnPropertyChanged(_selectedID);
                }
            }
        }

        private void DisableTBs()
        {
            FNEnabled = false;
            MNEnabled = false;
            LNEnabled = false;
            BNEnabled = false;
            CEnabled = false;
        }
        private void EnableTBs()
        {
            FNEnabled = true;
            MNEnabled = true;
            LNEnabled = true;
            BNEnabled = true;
            CEnabled = true;
        }

    }
}
