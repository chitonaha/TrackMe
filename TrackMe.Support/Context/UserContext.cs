using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace TrackMe.Support.Context
{
    [Serializable]
    public class UserContext
    {
        public UserContext()
        {
        }

        public int UserId { get; private set; }
        public int Role { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }

        public void Initialize(int userId, int roleId, string userName, string email)
        {
            this.UserId = userId;
            this.Role = roleId;
            this.UserName = UserName;
            this.Email = email;
        }
    }
}
