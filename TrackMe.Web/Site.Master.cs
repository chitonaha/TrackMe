using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackMe.Web.Support;
using TrackMe.Model.Enums;
using TrackMe.Web.Pages;
using TrackMe.Business.Interfaces;
using TrackMe.Support.IoC;
using System.Web.Security;
using TrackMe.Model;

namespace TrackMe.Web
{
    public partial class SiteMaster : BaseMasterPage
    {
        IUserManager _userManager = IOCContainerFactory.Instance.CurrentContainer.Resolve<IUserManager>();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (this.Page.User.Identity.IsAuthenticated)
            {
                ((BasePage)this.Page).InitializeLogoAndSecondBanner();
            }
        }

        protected string GetUserFullName()
        {
            MembershipUser mUser = Membership.GetUser();
            User user = _userManager.GetUserByMembershipId(mUser.ProviderUserKey.ToString());

            return user.FullName;
        }

        public void ShowSecondBanner(SecondBannerType secondBannerType)
        {
            pnlGeneral.Visible = secondBannerType == SecondBannerType.General;
            pnlAdmin.Visible = secondBannerType == SecondBannerType.Admin;
            pnlUser.Visible = secondBannerType == SecondBannerType.User;
        }

        public void SetLogoImage(string image)
        {
            imgLogo.ImageUrl = image;
        }
    }
}
