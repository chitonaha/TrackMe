using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackMe.Model.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        ITrackRepository TrackRepository { get; }
        IUserRepository UserRepository { get; }
        IDeviceRepository DeviceRepository { get; }
        IUserDeviceRepository UserDeviceRepository { get; }
        IDeviceImageRepository DeviceImageRepository { get; }
        IImageRepository ImageRepository { get; }
        IDeviceTypeRepository DeviceTypeRepository { get; }
     }
}
