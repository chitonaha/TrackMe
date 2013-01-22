using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using TrackMe.Model;
using TrackMe.Support.IoC;
using TrackMe.Business.Interfaces;
using TrackMe.Model.DTOs;
using TrackMe.Web.Templates;
using TrackMe.Web.Support;

namespace TrackMe.Web.Site
{
    public partial class WebMethods : BasePage
    {
        private static BasePage _basePage = new BasePage();

        [WebMethod]
        public static IList<TrajectoryDTO> GetDeviceCoordinates(int deviceId)
        {
            TemplatesManager templatesManager = new TemplatesManager();
            string content = templatesManager.GetTemplate("MarkInfo.htm");

            ITrackingManager manager = IOCContainerFactory.Instance.CurrentContainer.Resolve<ITrackingManager>();

            // _basePage.UserContext.UserId
            IList<TrajectoryDTO> trajectories = manager.GetLastTrackByDeviceIdAndUserId(deviceId, 6);

            foreach (TrajectoryDTO trajectory in trajectories)
            {
                //trajectory.Routes = new List<RouteDTO>();
                TrackDTO track = trajectory.LastTrack;
                content = content.Replace("{Imagen}", track.ImagenUrl);
                track.Info = content.Replace("{DeviceName}", track.DeviceName);
            }

            return trajectories;
        }

        [WebMethod]
        public static IList<RouteDTO> GetRoutesByTrackFromAndTrackTo(int trackFrom, int trackTo, int deviceId)
        {
            ITrackingManager manager = IOCContainerFactory.Instance.CurrentContainer.Resolve<ITrackingManager>();
            return manager.GetRoutesByTrackFromAndTrackTo(trackFrom, trackTo, deviceId);
        }
    }
}