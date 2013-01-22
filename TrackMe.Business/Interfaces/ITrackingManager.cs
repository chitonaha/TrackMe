using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Model;
using TrackMe.Model.DTOs;

namespace TrackMe.Business.Interfaces
{
    public interface ITrackingManager
    {
        void AddTrack(Track track);
        void DeleteAllTracksByDeviceId(int deviceId);
        IList<TrajectoryDTO> GetLastTrackByDeviceIdAndUserId(int deviceId, int userId);
        IList<TrackDTO> GetAllTracksByDeviceId(int deviceId);
        IList<TrackDTO> GetAllTracksByTrackFromAndTrackTo(int trackFrom, int trackTo, int deviceId);
        IList<RouteDTO> GetRoutesByTrackFromAndTrackTo(int trackFrom, int trackTo, int deviceId);
    }
}
