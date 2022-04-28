using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.Exceptions
{
    public class UserLoginInvalidException : Exception

    {
        public UserLoginInvalidException()
        {
        }

        public UserLoginInvalidException(string message) : base(message)
        {
        }

        public UserLoginInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserLoginInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
