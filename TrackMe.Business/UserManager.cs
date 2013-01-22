using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Business.Interfaces;
using TrackMe.Model.Interfaces;
using System.Web.Security;
using TrackMe.Model;
using TrackMe.Model.Enums;
using TrackMe.Model.DTOs;

namespace TrackMe.Business
{
    public class UserManager : BaseManager, IUserManager
    {
        public UserManager(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void CreateUser(string userName, string firstName, string lastName, string email, string password, bool isAdminUser)
        {
            try
            {
                if (Membership.GetUser(userName) == null && string.IsNullOrEmpty(Membership.GetUserNameByEmail(email)))
                {
                    MembershipUser memUser = Membership.CreateUser(userName, password, email);
                    User user = new User()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        MembershipId = (Guid)memUser.ProviderUserKey,
                    };
                    if (isAdminUser)
                    {
                        string role = isAdminUser ? RoleType.Admin.ToString() : RoleType.User.ToString();
                        Roles.AddUserToRole(userName, role);
                    }
                    else
                    {
                        string role = isAdminUser ? RoleType.User.ToString() : RoleType.User.ToString();
                        Roles.AddUserToRole(userName, role);
                    }

                    UnitOfWork.UserRepository.Insert(user);

                    UnitOfWork.Save();
                }
            }
            catch(Exception ex)
            {
                Membership.DeleteUser(userName);
                throw ex;
            }
        }

        public User GetUserByMembershipId(string memberhipId)
        {
            return UnitOfWork.UserRepository.Get(p => p.MembershipId == new Guid(memberhipId)).FirstOrDefault();
        }

        public IList<UserDTO> GetAllUsers()
        {
            return UnitOfWork.UserRepository.GetAllUsers();
        }

        public IList<UserDTO> GetUsersInRole(RoleType roleType)
        {
            return UnitOfWork.UserRepository.GetAllUsersInRole(roleType);
        }

        public void EnableDisableUser(int userId)
        {
            User user = UnitOfWork.UserRepository.GetByID(userId);
            MembershipUser memUser = Membership.GetUser((object)user.MembershipId);
            memUser.IsApproved = !memUser.IsApproved;
            Membership.UpdateUser(memUser);
        }

        public void DeleteUser(int userId)
        {
            User user = UnitOfWork.UserRepository.GetByID(userId);
            MembershipUser memUser = Membership.GetUser((object)user.MembershipId);
            
            Membership.DeleteUser(memUser.UserName, true); //Borrado en cascada esta habilitado, por eso se borra la entidad User tambien
        }
    }
}
