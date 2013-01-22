using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Model.DTOs;

namespace TrackMe.Model.Interfaces
{
    public interface IDeviceRepository : IBaseRepository<Device>
    {
        IList<DeviceDTO> GetAllDevices();
        IList<Device> GetUnAssignedDevices();
        IList<DeviceDTO> GetAllByUserId(int userId);
    }
}
