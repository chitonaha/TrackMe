using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Support.Context;
using TrackMe.Model.Enums;
using System.Web.Security;
using TrackMe.Business.Interfaces;
using TrackMe.Support.IoC;

namespace TrackMe.Web.Support
{
    public class BasePage : System.Web.UI.Page
    {
        public UserContext UserContext
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["trackme.usercontext"] == null)
                {
                    System.Web.HttpContext.Current.Session["trackme.usercontext"] = new UserContext();

                    if (User.Identity.IsAuthenticated)
                    {
                        MembershipUser memUser = Membership.GetUser();

                        if (memUser != null)
                        {
                            IUserManager manager = IOCContainerFactory.Instance.CurrentContainer.Resolve<IUserManager>();
                            TrackMe.Model.User user = manager.GetUserByMembershipId(memUser.ProviderUserKey.ToString());
                            int roleId = (int)GetRole(Roles.GetRolesForUser(memUser.UserName));
                            UserContext.Initialize(user.UserId, roleId, memUser.UserName, memUser.Email);
                        }
                    }
                }

                return (UserContext)System.Web.HttpContext.Current.Session["trackme.usercontext"];
            }
        }

        protected void ResetUserContext()
        {
            System.Web.HttpContext.Current.Session["trackme.usercontext"] = null;
        }

        private RoleType GetRole(string[] roles)
        {
            RoleType result = RoleType.User;
            if (roles.Any())
            {
                string role = roles[0]; //Should have just one role associated
                switch (role)
                {
                    case "Admin":
                        result = RoleType.Admin;
                        break;
                    case "User":
                        result = RoleType.User;
                        break;
                }
                return result;
            }

            throw new Exception("User has no role assigned.");
        }

        public virtual void InitializeLogoAndSecondBanner()
        {
 
        }
    }
}
