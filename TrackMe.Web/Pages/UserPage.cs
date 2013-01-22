using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackMe.Web.Support;

namespace TrackMe.Web.Pages
{
    public class UserPage : BasePage
    {
        public override void InitializeLogoAndSecondBanner()
        {
            ((SiteMaster)this.Master).ShowSecondBanner(SecondBannerType.User);
            ((SiteMaster)this.Master).SetLogoImage("~/Content/Images/trackme_logo_panel_control.png");
        }
    }
}