using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackMe.Web.Support;

namespace TrackMe.Web.Pages
{
    public class AdminPage: BasePage
    {
        public override void InitializeLogoAndSecondBanner()
        {
            ((SiteMaster)this.Master).ShowSecondBanner(SecondBannerType.Admin);
            ((SiteMaster)this.Master).SetLogoImage("~/Content/Images/trackme_logo_admin.png");
        }
    }
}