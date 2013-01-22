using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Model.DTOs;
using TrackMe.Model.Enums;

namespace TrackMe.Model.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        IList<UserDTO> GetAllUsers();
        IList<UserDTO> GetAllUsersInRole(RoleType roleType);
    }
}
