using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Model.DTOs;

namespace TrackMe.Model.Interfaces
{
    public interface ITrackRepository : IBaseRepository<Track>
    {
        IList<Track> GetAllByDeviceId(int deviceId);
        TrackDTO GetLastTrackByDeviceId(int deviceId);
        TrackDTO GetFirstTrackByDeviceId(int deviceId);
        IList<TrackDTO> GetAllTracksByDeviceId(int deviceId);
        IList<TrackDTO> GetAllTracksByTrackFromAndTrackTo(int trackFrom, int trackTo, int deviceId);
    }
}
