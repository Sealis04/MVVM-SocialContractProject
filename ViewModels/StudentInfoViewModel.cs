using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_SocialContractProject.Models;

namespace MVVM_SocialContractProject.ViewModels
{
    public class StudentInfoViewModel:ViewModelBase
    {
        private readonly StudentInfo _studentInfo;
        public string StudentID => _studentInfo.StudentID?.ToString();
        public string StudentName => _studentInfo.FirstName.ToString() + " " + _studentInfo.MiddleName.ToString() + " " + _studentInfo.LastName.ToString();
        public string FirstName => _studentInfo.FirstName.ToString();
        public string MiddleName => _studentInfo.MiddleName.ToString();
        public string LastName => _studentInfo.LastName.ToString();
        public int BatchNo => _studentInfo.BatchNo;
        public string Course => _studentInfo.Course.ToString();
        public int CurrentHours { get; set; }

        public int LackingHours => 160 - CurrentHours;

        public StudentInfoViewModel(StudentInfo studentRecord)
        {
            _studentInfo = studentRecord;
        }
    }
}
