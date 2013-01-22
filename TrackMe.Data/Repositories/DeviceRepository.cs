using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Model;
using TrackMe.Model.Interfaces;
using TrackMe.Model.DTOs;

namespace TrackMe.Data.Repositories
{
    public class DeviceRepository : BaseRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(IDataContext context)
            : base(context)
        { }

        public IList<DeviceDTO> GetAllDevices()
        {
            IList<DeviceDTO> devices = (from d in context.Devices
                                       select new DeviceDTO()
                                       {
                                           Description = d.Description,
                                           DeviceId = d.DeviceId,
                                           DeviceTypeId = d.DeviceTypeId,
                                           DeviceType = d.DeviceType.Description,
                                           Name = d.Name,
                                           IsDisabled = d.IsDisabled
                                       }).ToList();

            foreach(DeviceDTO device in devices)
            {
                DeviceImage di = context.DeviceImages.FirstOrDefault(p => p.DeviceId == device.DeviceId);
                if (di != null)
                {
                    device.ImageFileName = di.Image.FileName;
                    device.ImageKey = di.Image.ImageKey;
                }
            }

            return devices;
        }

        public IList<DeviceDTO> GetAllByUserId(int userId)
        {
            return context.UserDevices.Where(ud => ud.UserId == userId && !ud.Device.IsDisabled).Select(ud => new DeviceDTO()
                                       {
                                           Description = ud.Device.Description,
                                           DeviceId = ud.Device.DeviceId,
                                           DeviceTypeId = ud.Device.DeviceTypeId,
                                           DeviceType = ud.Device.DeviceType.Description,
                                           Name = ud.Device.Name,
                                           IsDisabled = ud.Device.IsDisabled
                                       }).ToList();;
        }

        public IList<Device> GetUnAssignedDevices()
        {
            IList<int> assignedDevices = context.UserDevices.Select(p => p.DeviceId).Distinct().ToList();

            return (from i in context.Devices
                    where !assignedDevices.Contains(i.DeviceId)
                    select i).ToList();
        }
    }
}
