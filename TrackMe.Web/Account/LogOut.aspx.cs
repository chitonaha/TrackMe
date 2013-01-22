using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackMe.Web.Support;
using System.Web.Security;

namespace TrackMe.Web.Account
{
    public partial class LogOut : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ResetUserContext();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect(FormsAuthentication.DefaultUrl);
        }
    }
}