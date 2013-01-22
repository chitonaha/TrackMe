using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackMe.Web.Support;
using TrackMe.Model.Enums;

namespace TrackMe.Web.Site
{
    public partial class ForkFromLogin : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (UserContext.Role)
            {
                case (int)RoleType.Admin:
                    Response.Redirect("~/Site/Admin/UserManagement.aspx");
                    break;
                case (int)RoleType.User:
                    Response.Redirect("~/Site/User/CurrentLocation.aspx");
                    break;
            }
        }
    }
}