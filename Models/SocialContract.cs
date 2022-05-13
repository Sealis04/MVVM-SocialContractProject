using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.Models
{
    public class SocialContract
    {
        public SocialContract(int SocialContractID, StudentInfo studentID, int firstSemester, int secondSemester, int summer, int schoolYear, string SocialContractimage)
        {
            StudentID = studentID;
            FirstSemester = firstSemester;
            SecondSemester = secondSemester;
            Summer = summer;
            SchoolYear = schoolYear;
            this.SocialContractimage = SocialContractimage;
            this.SocialContractID = SocialContractID;
        }

        public StudentInfo StudentID { get; }

        public int SocialContractID { get; }
        public string SocialContractimage { get; }
        public int SchoolYear { get; }
        public int FirstSemester { get; }
        public int SecondSemester { get; }
        public int Summer { get; }
        //public UserAccount UserID { get; }
        public int TotalHours => FirstSemester + SecondSemester + Summer;
   
        public bool IDConflict(StudentInfo student)
        {
            return student.StudentID == StudentID.StudentID;
        }
        public bool Conflicts(SocialContract socialContract)
        {
            return socialContract.SchoolYear == SchoolYear;
        }

        public override bool Equals(object obj)
        {
            return obj is SocialContract useracc &&
                StudentID == useracc.StudentID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
