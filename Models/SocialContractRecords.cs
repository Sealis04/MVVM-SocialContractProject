using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_SocialContractProject.Exceptions;
using MVVM_SocialContractProject.Models.Database;

namespace MVVM_SocialContractProject.Models
{

    class SocialContractRecords
    {
        //private readonly SocialContract _socialContract;

        private readonly List<SocialContract> _socialContract;
        private readonly DatabaseQueries _dbQueries;
        public SocialContractRecords()
        {
            _socialContract = new List<SocialContract>();
            _dbQueries = new DatabaseQueries();
        }

        public IEnumerable<SocialContract> GetSocialRecords(StudentInfo StudentID, int query, bool direction)
        {
            _socialContract.Clear();
            _dbQueries.LoadSocialContractInfo(StudentID,_socialContract, query, direction);
            return _socialContract.Where(r => r.StudentID == StudentID);
        }

        public IEnumerable<SocialContract> GetSCInfo(StudentInfo StudentID)
        {
            _socialContract.Clear();
            _dbQueries.GetSCInfo(StudentID, _socialContract);
            return _socialContract.Where(r => r.StudentID == StudentID);
        }
        public bool AddSocialContract (SocialContract socialContract, StudentInfo StudentID)
        {
            
            foreach (SocialContract existingSocialContract in _socialContract)
            {
                if (existingSocialContract.IDConflict(StudentID))
                {
                    if (existingSocialContract.Conflicts(socialContract))
                    {
                        return true;
                        throw new SocialContractConflictExceptions(existingSocialContract, socialContract);
                        ///Update reservation on DB code to be updated
                    }
                }
            }
            _dbQueries.InsertSocialContract(socialContract);
            _socialContract.Add(socialContract);
            return false;
        }
        public void UpdateSocialContract(StudentInfo student,SocialContract socialContract)
        {
            _dbQueries.UpdateSocialContract(student, socialContract);
            _socialContract.Clear();
        }

    }
}
