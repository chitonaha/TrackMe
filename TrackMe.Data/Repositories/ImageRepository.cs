using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Model;
using TrackMe.Model.Interfaces;

namespace TrackMe.Data.Repositories
{
    public class ImageRepository: BaseRepository<Image>, IImageRepository
    {
        public ImageRepository(IDataContext context)
            : base(context)
        { }
    }
}
