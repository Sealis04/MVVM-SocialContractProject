using MVVM_SocialContractProject.Exceptions;
using MVVM_SocialContractProject.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.Models
{
    public class UserInfoRecords
    {
        private readonly List<UserInfo> _userInfo;
        private readonly DatabaseQueries _dbQueries;

        public UserInfoRecords()
        {
            _userInfo = new List<UserInfo>();
            _dbQueries = new DatabaseQueries();
        }

        public IEnumerable<UserInfo> GetUserInfo(string Username)
        {
            ///DBquery to add userrecords to _userinfo variable;
            ///
            _userInfo.Clear();
            _dbQueries.GetUserInfo(_userInfo);
            return _userInfo.Where(r=> r.Username== Username);
        }

        public void AddUserInfo(UserInfo user)
        {
            foreach(UserInfo existingUser in _userInfo)
            {
                if (existingUser.Conflicts(user))
                {
                    throw new UserLoginConflictException();
                }
                
            }
            _dbQueries.CreateUserInfo(user);
        }
    }
}
