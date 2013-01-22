using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackMe.Model.DTOs;
using TrackMe.Business.Interfaces;
using TrackMe.Support.IoC;
using TrackMe.Support.SafeCast;
using TrackMe.Web.Support;
using TrackMe.Model;

namespace TrackMe.Web.Site.User
{
    public partial class Administration : BasePage
    {
        IDeviceManager _deviceManager = IOCContainerFactory.Instance.CurrentContainer.Resolve<IDeviceManager>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDevices();
            }
        }

        protected void grDevices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int deviceId = SafeCast.ToInteger(e.CommandArgument);

            switch (e.CommandName)
            {
                case "ResetDevice":
                    _deviceManager.ResetDevice(deviceId);
                    break;
            }
            LoadDevices();
        }

        private void LoadDevices()
        {
            IList<Device> devices = _deviceManager.GeDevicesByUserId(UserContext.UserId);
            grDevices.DataSource = devices;
            grDevices.DataBind();
        }
    }
}