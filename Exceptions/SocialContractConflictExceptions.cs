using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MVVM_SocialContractProject.Models;
namespace MVVM_SocialContractProject.Exceptions
{
    class SocialContractConflictExceptions : Exception
    {
        public SocialContract ExistingSocialContract { get; }
        public SocialContract IncomingSocialContract { get; }

        public SocialContractConflictExceptions()
        {
        }

        public SocialContractConflictExceptions(string message, SocialContract existingSocialContract = null, SocialContract incomingSocialContract = null) : base(message)
        {
            ExistingSocialContract = existingSocialContract;
            IncomingSocialContract = incomingSocialContract;
        }

        public SocialContractConflictExceptions(string message, Exception innerException, SocialContract existingSocialContract, SocialContract incomingSocialContract) : base(message, innerException)
        {
            ExistingSocialContract = existingSocialContract;
            IncomingSocialContract = incomingSocialContract;
        }

        protected SocialContractConflictExceptions(SerializationInfo info, StreamingContext context, SocialContract existingSocialContract = null, SocialContract incomingSocialContract = null) : base(info, context)
        {
            ExistingSocialContract = existingSocialContract;
            IncomingSocialContract = incomingSocialContract;
        }

        public SocialContractConflictExceptions(SocialContract existingSocialContract, SocialContract incomingSocialContract)
        {
            ExistingSocialContract = existingSocialContract;
            IncomingSocialContract = incomingSocialContract;
        }
    }
}
