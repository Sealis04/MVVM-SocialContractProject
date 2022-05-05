using MVVM_SocialContractProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.ViewModels
{
    public class UserInfoViewModel:ViewModelBase    
    {
        private readonly UserInfo _userInfo;

        public UserInfoViewModel(UserInfo userInfo)
        {
            _userInfo = userInfo;
        }

        public string UserName => _userInfo.Username;
        public string Password => _userInfo.Password;
        public string Type => _userInfo.type == 1 ? "Is Admin" : "Is user";
        public string Salt => _userInfo.Salt;
    }
}
