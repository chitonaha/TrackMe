using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackMe.Web.Support;
using TrackMe.Model.Enums;

namespace TrackMe.Web.Pages
{
    public class GeneralPage : BasePage
    {
        public override void InitializeLogoAndSecondBanner()
        {
            if (this.Page.User.Identity.IsAuthenticated)
            {
                switch (UserContext.Role)
                {
                    case (int)RoleType.Admin:
                        ((SiteMaster)this.Master).ShowSecondBanner(SecondBannerType.Admin);
                        break;
                    case (int)RoleType.User:
                        ((SiteMaster)this.Master).ShowSecondBanner(SecondBannerType.User);
                        break;
                }
            }
            else
            {
                ((SiteMaster)this.Master).ShowSecondBanner(SecondBannerType.General);
            }

            ((SiteMaster)this.Master).SetLogoImage("~/Content/Images/trackme_logo.png");
        }
    }
}