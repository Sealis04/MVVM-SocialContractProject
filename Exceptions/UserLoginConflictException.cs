using MVVM_SocialContractProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.Exceptions
{
    public class UserLoginConflictException : Exception
    {
        public UserLoginConflictException()
        {
        }

        public UserLoginConflictException(string message, UserInfo existingUserInfo = null, UserInfo conflictingUserInfo = null) : base(message)
        {
            ExistingUserInfo = existingUserInfo;
            ConflictingUserInfo = conflictingUserInfo;
        }

        public UserLoginConflictException(UserInfo existingUserInfo, UserInfo conflictingUserInfo)
        {
            ExistingUserInfo = existingUserInfo;
            ConflictingUserInfo = conflictingUserInfo;
        }

        public UserLoginConflictException(string message, Exception innerException, UserInfo existingUserInfo, UserInfo conflictingUserInfo) : base(message, innerException)
        {
            ExistingUserInfo = existingUserInfo;
            ConflictingUserInfo = conflictingUserInfo;
        }

        protected UserLoginConflictException(SerializationInfo info, StreamingContext context, UserInfo existingUserInfo = null, UserInfo conflictingUserInfo = null) : base(info, context)
        {
            ExistingUserInfo = existingUserInfo;
            ConflictingUserInfo = conflictingUserInfo;
        }

        public UserInfo ExistingUserInfo { get; }
        public UserInfo ConflictingUserInfo { get; }
    }
}
