using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Model;
using TrackMe.Model.Interfaces;

namespace TrackMe.Data.Repositories
{
    public class DeviceImageRepository: BaseRepository<DeviceImage>, IDeviceImageRepository
    {
        public DeviceImageRepository(IDataContext context)
            : base(context)
        { }
    }
}
