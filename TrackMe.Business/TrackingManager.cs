using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Model.Interfaces;
using TrackMe.Business.Interfaces;
using TrackMe.Model;
using System.Web.Security;
using TrackMe.Model.DTOs;

namespace TrackMe.Business
{
    public class TrackingManager : BaseManager, ITrackingManager
    {
        public TrackingManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void AddTrack(Track track)
        {
            UnitOfWork.TrackRepository.Insert(track);
            UnitOfWork.Save();
        }

        public void DeleteAllTracksByDeviceId(int deviceId)
        {
            IList<Track> tracks = UnitOfWork.TrackRepository.GetAllByDeviceId(deviceId).ToList();

            foreach (Track track in tracks)
            {
                UnitOfWork.TrackRepository.Delete(track);
            }

            UnitOfWork.Save();
        }

        public IList<TrackDTO> GetAllTracksByDeviceId(int deviceId)
        {
            return UnitOfWork.TrackRepository.GetAllTracksByDeviceId(deviceId);
        }

        public IList<TrajectoryDTO> GetLastTrackByDeviceIdAndUserId(int deviceId, int userId)
        {
            IList<TrajectoryDTO> trajectories = new List<TrajectoryDTO>();
            TrajectoryDTO trajectory = null;

            if (deviceId == 0)
            {
                IList<DeviceDTO> devices = UnitOfWork.DeviceRepository.GetAllByUserId(userId);

                foreach (DeviceDTO device in devices)
                {
                    TrackDTO lastTrack = UnitOfWork.TrackRepository.GetLastTrackByDeviceId(device.DeviceId);
                    TrackDTO firstTrack = UnitOfWork.TrackRepository.GetFirstTrackByDeviceId(device.DeviceId);

                    if (lastTrack != null && lastTrack != null)
                    {
                        trajectory = new TrajectoryDTO
                        {
                            LastTrack = lastTrack,
                            From = firstTrack.TrackDateFormatted,
                            To = lastTrack.TrackDateFormatted,
                            Routes = GetRouteByDeviceId(device.DeviceId)
                        };
                        
                        trajectories.Add(trajectory);
                    }
                }
            }
            else
            {
                TrackDTO lastTrack = UnitOfWork.TrackRepository.GetLastTrackByDeviceId(deviceId);
                TrackDTO firstTrack = UnitOfWork.TrackRepository.GetFirstTrackByDeviceId(deviceId);

                if (lastTrack != null && lastTrack != null)
                {
                    trajectory = new TrajectoryDTO
                    {
                        LastTrack = lastTrack,
                        From = firstTrack.TrackDateFormatted,
                        To = lastTrack.TrackDateFormatted,
                        Routes = GetRouteByDeviceId(deviceId)
                    };
                    
                    trajectories.Add(trajectory);
                }
            }

            return trajectories; 
        }

        public IList<RouteDTO> GetRoutesByTrackFromAndTrackTo(int trackFrom, int trackTo, int deviceId)
        {
            IList<RouteDTO> routes = new List<RouteDTO>();

            IList<TrackDTO> tracks = UnitOfWork.TrackRepository.GetAllTracksByTrackFromAndTrackTo(trackFrom, trackTo, deviceId);

            TrackDTO destination = tracks.LastOrDefault();
            string deviceType = TrackMe.Support.Helpers.EnumHelper.GetDescription(destination.DeviceType);
            
            RouteDTO route = new RouteDTO();

            int index = 0;
            foreach (TrackDTO track in tracks)
            {
                if (index % 8 == 0)
                {
                    if (index > 0)
                        route.Destination = track;

                    route = new RouteDTO();
                    route.DeviceId = deviceId;
                    route.DeviceType = deviceType;
                    route.Origin = track;
                    route.WayPoints = new List<TrackDTO>();

                    routes.Add(route);
                }
                else
                {
                    if (index < tracks.Count)
                        route.WayPoints.Add(track);
                }

                index++;
            }

            if (route.Destination == null)
                route.Destination = destination;

            return routes;
        }

        public IList<TrackDTO> GetAllTracksByTrackFromAndTrackTo(int trackFrom, int trackTo, int deviceId)
        {
            return UnitOfWork.TrackRepository.GetAllTracksByTrackFromAndTrackTo(trackFrom, trackTo, deviceId);
        }

        #region Private Methods

        private IList<RouteDTO> GetRouteByDeviceId(int deviceId)
        {
            IList<RouteDTO> routes = new List<RouteDTO>();

            IList<TrackDTO> tracks = UnitOfWork.TrackRepository.GetAllTracksByDeviceId(deviceId);
            Device device = UnitOfWork.DeviceRepository.GetByID(deviceId);
            string deviceType = device != null ? TrackMe.Support.Helpers.EnumHelper.GetDescription((TrackMe.Model.Enums.DeviceType)device.DeviceTypeId) : string.Empty;

            TrackDTO origin = tracks.FirstOrDefault();
            TrackDTO destination = tracks.LastOrDefault();

            RouteDTO route = new RouteDTO();

            int index = 0;
            foreach (TrackDTO track in tracks)
            {
                if (index % 8 == 0)
                {
                    if (index > 0)
                        route.Destination = track;

                    route = new RouteDTO();
                    route.DeviceId = deviceId;
                    route.DeviceType = deviceType;
                    route.Origin = track;
                    route.WayPoints = new List<TrackDTO>();

                    routes.Add(route);
                    //
                }

                route.WayPoints.Add(track);

                index++;
            }

            if (route.Destination == null)
                route.Destination = destination;

            return routes;
        }

        private DistanceDTO FactionateOriginsAndDestinations(DistanceDTO distance, IList<TrackDTO> origins, IList<TrackDTO> destinations)
        {
            int count = origins.Count;

            IList<TrackDTO> originsResult = new List<TrackDTO>();
            IList<TrackDTO> destinationsResult = new List<TrackDTO>();

            for (int i = 0; i < count; i++)
            {
                if (i % 6 == 0)
                {
                    originsResult = new List<TrackDTO>();
                    destinationsResult = new List<TrackDTO>();

                    distance.Origins.Add(originsResult);
                    distance.Destinations.Add(destinationsResult);
                }

                originsResult.Add(origins[i]);
                destinationsResult.Add(destinations[i]);
            }

            return distance;
        }

        #endregion
    }
}
