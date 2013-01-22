using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Support.Context;

namespace TrackMe.Web.Support
{
    public class BaseMasterPage : System.Web.UI.MasterPage
    {
        public UserContext UserContext
        {
            get
            {
                UserContext userContext = null;
                BasePage page = this.Page as BasePage;

                if (page != null)
                    userContext = page.UserContext;

                return userContext;
            }
        }
    }
}
