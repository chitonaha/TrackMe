using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Model;
using TrackMe.Model.Interfaces;
using TrackMe.Model.DTOs;

namespace TrackMe.Data.Repositories
{
    public class TrackRepository : BaseRepository<Track>, ITrackRepository
    {
        public TrackRepository(IDataContext context)
            : base(context)
        { }

        public IList<Track> GetAllByDeviceId(int deviceId)
        {
            return context.Tracks.Where(t => t.DeviceId == deviceId).OrderBy(t => t.TrackDate).ToList();
        }

        public TrackDTO GetLastTrackByDeviceId(int deviceId)
        {
            TrackDTO track = context.Tracks
                .Where(t => t.DeviceId == deviceId)
                .OrderByDescending(t => t.TrackDate)
                .Select(t => new TrackDTO
                {
                    DeviceId = t.DeviceId,
                    DeviceName = t.Device.Name,
                    DeviceType = (TrackMe.Model.Enums.DeviceType)t.Device.DeviceTypeId,
                    Latitude = t.Latitude,
                    Longitude = t.Longitude,
                    TrackDate = t.TrackDate,
                    TrackId = t.TrackId,
                    ImagenFileName  = t.Device.DeviceImages.FirstOrDefault()!=null ? t.Device.DeviceImages.FirstOrDefault().Image.FileName : string.Empty
                }).FirstOrDefault();

            return track;
        }

        public TrackDTO GetFirstTrackByDeviceId(int deviceId)
        {
            TrackDTO track = context.Tracks
                .Where(t => t.DeviceId == deviceId)
                .OrderBy(t => t.TrackDate)
                .Select(t => new TrackDTO
                {
                    DeviceId = t.DeviceId,
                    DeviceName = t.Device.Name,
                    DeviceType = (TrackMe.Model.Enums.DeviceType)t.Device.DeviceTypeId,
                    Latitude = t.Latitude,
                    Longitude = t.Longitude,
                    TrackDate = t.TrackDate,
                    TrackId = t.TrackId,
                    ImagenFileName = t.Device.DeviceImages.FirstOrDefault() != null ? t.Device.DeviceImages.FirstOrDefault().Image.FileName : string.Empty
                }).FirstOrDefault();

            return track;
        }

        public IList<TrackDTO> GetAllTracksByDeviceId(int deviceId)
        {
            IList<TrackDTO> tracks = context.Tracks
                .Where(t => t.DeviceId == deviceId)
                .OrderBy(t => t.TrackDate)
                .Select(t => new TrackDTO {
                    DeviceId = t.DeviceId,
                    Latitude = t.Latitude,
                    Longitude = t.Longitude,
                    TrackDate = t.TrackDate,
                    TrackId = t.TrackId,
                    ImagenFileName = t.Device.DeviceImages.FirstOrDefault() != null ? t.Device.DeviceImages.FirstOrDefault().Image.FileName : string.Empty
                                     }).ToList();

            return tracks;
        }

        public IList<TrackDTO> GetAllTracksByTrackFromAndTrackTo(int trackFrom, int trackTo, int deviceId)
        {
            return context.Tracks.Where(t => t.TrackId >= trackFrom && t.TrackId <= trackTo && t.DeviceId == deviceId)
                .OrderBy(t => t.TrackDate)
                .Select(t => new TrackDTO
                {
                    DeviceId = t.DeviceId,
                    DeviceType = (TrackMe.Model.Enums.DeviceType)t.Device.DeviceTypeId,
                    Latitude = t.Latitude,
                    Longitude = t.Longitude,
                    TrackDate = t.TrackDate,
                    TrackId = t.TrackId,
                    ImagenFileName = t.Device.DeviceImages.FirstOrDefault() != null ? t.Device.DeviceImages.FirstOrDefault().Image.FileName : string.Empty
                }).ToList();
        }
    }
}
