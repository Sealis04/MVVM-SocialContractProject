using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_SocialContractProject.Models
{
    public class UserInfo
    {
        public UserInfo(string username, string password, string Salt, int Type)
        {
            Username = username;
            Password = password;
            this.Salt = Salt;
            type = Type;
        }

        public string Username { get; }
        public string Password { get; }

        public int type { get; }

        public string Salt { get; }

        internal bool Conflicts (UserInfo user)
        {
            return user.Username == Username;
        }

        public override bool Equals(object obj)
        {
            return obj is UserInfo user &&
                Username == user.Username;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(UserInfo user1, UserInfo user2)
        {
            if (user1 is null && user2 is null)
            {
                return true;
            }
            return !(user1 is null) && user1.Equals(user2);
        }
        public static bool operator !=(UserInfo user1, UserInfo user2)
        {
            return user1 != user2;
        }

    }
}
