using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Model;
using System.IO;
using TrackMe.Model.DTOs;

namespace TrackMe.Business.Interfaces
{
    public interface IDeviceManager
    {
        IList<DeviceDTO> GetAllDevices();
        IList<DeviceType> GetAllDeviceTypes();
        void CreateDevice(string name, string description, int deviceTypeId, string fileName, string fileExtension, Stream image);
        void DeleteDevice(int deviceId);
        void EnableDisableDevice(int deviceId);
        IList<Device> GeDevicesByUserId(int userId);
        IList<Device> GeUnAssignedDevices();
        void UnAssignDevice(int deviceId);
        void AssignDevices(int userId, IList<int> deviceIds);
        void ResetDevice(int deviceId);
    }
}
