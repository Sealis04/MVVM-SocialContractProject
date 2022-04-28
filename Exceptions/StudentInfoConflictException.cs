using MVVM_SocialContractProject.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.Exceptions
{
    class StudentInfoConflictException : Exception
    {
        public StudentInfoConflictException()
        {
        }

        public StudentInfoConflictException(StudentInfo existingStudentInfo, StudentInfo conflictingStudentInfo)
        {
            ExistingStudentInfo = existingStudentInfo;
            ConflictingStudentInfo = conflictingStudentInfo;
        }

        public StudentInfoConflictException(string message, StudentInfo existingStudentInfo, StudentInfo conflictingStudentInfo) : base(message)
        {
            ExistingStudentInfo = existingStudentInfo;
            ConflictingStudentInfo = conflictingStudentInfo;
        }

        public StudentInfoConflictException(string message, Exception innerException, StudentInfo existingStudentInfo, StudentInfo conflictingStudentInfo) : base(message, innerException)
        {
            ExistingStudentInfo = existingStudentInfo;
            ConflictingStudentInfo = conflictingStudentInfo;
        }

        protected StudentInfoConflictException(SerializationInfo info, StreamingContext context, StudentInfo existingStudentInfo = null, StudentInfo conflictingStudentInfo = null) : base(info, context)
        {
            ExistingStudentInfo = existingStudentInfo;
            ConflictingStudentInfo = conflictingStudentInfo;
        }

        public StudentInfo ExistingStudentInfo { get; }
        public StudentInfo ConflictingStudentInfo { get; }
    }
}
