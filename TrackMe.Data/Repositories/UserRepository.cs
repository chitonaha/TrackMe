using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Model;
using TrackMe.Model.Interfaces;
using TrackMe.Model.DTOs;
using System.Web.Security;
using TrackMe.Model.Enums;

namespace TrackMe.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDataContext context)
            : base(context)
        { }

        public IList<UserDTO> GetAllUsers()
        {
            IList<UserDTO> result = new List<UserDTO>();
            IList<User> users = context.Users.ToList();

            foreach (var user in users)
            {
                MembershipUser memUser = Membership.GetUser((object)user.MembershipId);

                result.Add(new UserDTO()
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = memUser.Email,
                    UserName = memUser.UserName,
                    IsDisabled = !memUser.IsApproved
                });
            }

            return result;
        }

        public IList<UserDTO> GetAllUsersInRole(RoleType roleType)
        {
            IList<UserDTO> users = new List<UserDTO>();
            string[] userNames = Roles.GetUsersInRole(roleType.ToString());
            foreach(string userName in userNames)
            {
                MembershipUser memUser = Membership.GetUser(userName);
                User user = context.Users.FirstOrDefault(p => p.MembershipId == (Guid)memUser.ProviderUserKey);
                users.Add(new UserDTO()
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = memUser.Email,
                    UserName = memUser.UserName,
                    IsDisabled = !memUser.IsApproved
                });
            }

            return users;
        }
    }

}
