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
using TrackMe.Support.SafeCast;
using System.Web.Services;
using TrackMe.Web.Pages;

namespace TrackMe.Web.Site.User
{
    public partial class Background : UserPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDevices();
            }
        }
                
        protected void ddlAllDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            int deviceId = SafeCast.ToInteger(ddlAllDevices.SelectedValue);

            IList<TrackDTO> tracks = new List<TrackDTO>();

            gvTracks.Visible = false;

            if (deviceId > 0)
            {
                ITrackingManager trackingManager = IOCContainerFactory.Instance.CurrentContainer.Resolve<ITrackingManager>();
                tracks = trackingManager.GetAllTracksByDeviceId(deviceId);
            }

            ddlFrom.DataSource = tracks;
            ddlFrom.DataBind();

            ddlTo.DataSource = tracks;
            ddlTo.DataBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "changeDateTrack", "changeDateTrack()", true);
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            int trackFrom = SafeCast.ToInteger(ddlFrom.SelectedValue);
            int trackTo = SafeCast.ToInteger(ddlTo.SelectedValue);
            int deviceId = SafeCast.ToInteger(ddlAllDevices.SelectedValue);

            ITrackingManager trackingManager = IOCContainerFactory.Instance.CurrentContainer.Resolve<ITrackingManager>();
            IList<TrackDTO> tracks = trackingManager.GetAllTracksByTrackFromAndTrackTo(trackFrom, trackTo, deviceId);

            gvTracks.DataSource = tracks;
            gvTracks.DataBind();
            gvTracks.Visible = true;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "getDistance", "getDistance()", true);
        }

        private void LoadDevices()
        {
            IDeviceManager manager = IOCContainerFactory.Instance.CurrentContainer.Resolve<IDeviceManager>();

            //UserContext.UserId
            IList<Device> devices = manager.GeDevicesByUserId(6);

            ddlAllDevices.DataSource = devices;
            ddlAllDevices.DataBind();
            ddlAllDevices.Items.Insert(0, new ListItem("Seleccione un dispositivo", "0"));

            gvTracks.Visible = false;
        }
    }
}