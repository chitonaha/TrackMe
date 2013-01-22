using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackMe.Business.Interfaces;
using TrackMe.Support.IoC;
using TrackMe.Model;
using TrackMe.Web.Support;
using TrackMe.Model.DTOs;
using TrackMe.Web.Pages;

namespace TrackMe.Web.Site.User
{
    public partial class CurrentLocation : UserPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IDeviceManager manager = IOCContainerFactory.Instance.CurrentContainer.Resolve<IDeviceManager>();

            //this.UserContext.UserId
            IList<Device> devices = manager.GeDevicesByUserId(6);

            rptDevices.DataSource = devices;
            rptDevices.DataBind();

            //ddlAllDevices.DataSource = devices;
            //ddlAllDevices.DataBind();
            //ddlAllDevices.Items.Insert(0, new ListItem("Seleccione un dispositivo", "0"));
        }
    }
}