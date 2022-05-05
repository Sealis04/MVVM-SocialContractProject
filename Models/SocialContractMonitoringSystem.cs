using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.Models
{
   public class SocialContractMonitoringSystem
    {
        private readonly SocialContractRecords _socialContractRecord;

        private readonly StudentRecords _studentRecords;

        private readonly UserInfoRecords _userInfoRecord;

        private readonly PDFInfoRecords _pdfInfo;

        private readonly UserInfoRecords _userInfo;

        public SocialContractMonitoringSystem()
        {
            _socialContractRecord = new SocialContractRecords();
            _userInfoRecord = new UserInfoRecords();
            _studentRecords = new StudentRecords();
            _pdfInfo = new PDFInfoRecords();
            _userInfo = new UserInfoRecords();
        }

        public IEnumerable<SocialContract> GetSocialContractForUser(StudentInfo StudentID)
        {
            return _socialContractRecord.GetSocialRecords(StudentID);
        }
        public bool CreateSocialContract(SocialContract socialcontract)
        {
           return _socialContractRecord.AddSocialContract(socialcontract);
        }
        public void UpdateSocialContract(StudentInfo studentID, SocialContract socialContract)
        {
           _socialContractRecord.UpdateSocialContract(studentID, socialContract);
        }

        public IEnumerable<StudentInfo> GetStudentInfo(string StudentID)
        {
            return _studentRecords.GetStudentinfo(StudentID);
        }

        public void CreateStudentInfo(StudentInfo studentID, SocialContract socialContract, SocialContractMonitoringSystem scSystem)
        {
            _studentRecords.AddStudentInfo(studentID, socialContract, scSystem);
        }

        public IEnumerable<UserInfo> GetUserInfo(string Username)
        {
            return _userInfoRecord.GetUserInfo(Username);
        }

        public void CreateUserInfo(UserInfo user)
        {
            _userInfoRecord.AddUserInfo(user);
        }

        public void UpdateUserInfo(UserInfo user)
        {
            _userInfoRecord.UpdateUserInfo(user);
        }

        public IEnumerable<StudentInfo> GetAllStudentInfo(string searchQuery, int page)
        {
            return _studentRecords.GetAllStudentInfo(searchQuery, page);
        }

        public IEnumerable<StudentInfo> SearchForUser(string StudentID)
        {
            return _studentRecords.SearchForStudentInfo(StudentID);
        }

        public void CreateEventPdf(PDFInfo pdf)
        {
            _pdfInfo.AddPDFEvent(pdf);
        }

        public IEnumerable<PDFInfo> GetAllPDF(string SearchQuery)
        {
            return _pdfInfo.GetAllEvents(SearchQuery);
        }

        public IEnumerable<UserInfo> GetAllUsers(string searchQuery, int page)
        {
            return _userInfo.GetAllUserInfo(searchQuery, page);
        }




    }
}
