using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.Models
{
   public class StudentInfo
    {
        public StudentInfo(string studentID, string firstName, string middleName, string lastName, int batchNo, string course)
        {
            StudentID = studentID;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            BatchNo = batchNo;
            Course = course;
        }

        public override String ToString()
        {
            return $"{StudentID}{BatchNo}{CurrentHours}";
        }

        public string StudentID { get; }
        public string FirstName { get; }
        public string MiddleName { get; }
        public string LastName { get; }
        public int BatchNo { get; }
        public string Course { get; }
        public int CurrentHours { get; set; }

        public int LackingHours => 160 - CurrentHours; 
        internal bool Conflicts(StudentInfo StudentInfo)
        {
            return StudentInfo.StudentID == StudentID;
        }

        
        public override bool Equals(object obj)
        {
            return obj is StudentInfo useracc &&
                StudentID == useracc.StudentID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(StudentInfo student1, StudentInfo student2)
        {
            if(student1 is null && student2 is null)
            {
                return true;
            }
            return !(student1 is null) && student1.Equals(student2);
        }
        public static bool operator !=(StudentInfo student1, StudentInfo student2)
        {
            return student1 != student2;
        }
    }
}
