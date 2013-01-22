using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Model;
using TrackMe.Model.Interfaces;

namespace TrackMe.Data.Repositories
{
    public class UserDeviceRepository : BaseRepository<UserDevice>, IUserDeviceRepository
    {
        public UserDeviceRepository(IDataContext context)
            : base(context)
        { }

    }
}
