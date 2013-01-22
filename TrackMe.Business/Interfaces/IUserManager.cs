using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Model;
using TrackMe.Model.DTOs;
using TrackMe.Model.Enums;

namespace TrackMe.Business.Interfaces
{
    public interface IUserManager
    {
        void CreateUser(string userName, string firstName, string lastName, string email, string password, bool isAdminUser);
        User GetUserByMembershipId(string memberhipId);
        IList<UserDTO> GetAllUsers();
        void EnableDisableUser(int userId);
        void DeleteUser(int userId);
        IList<UserDTO> GetUsersInRole(RoleType roleType);
    }
}
