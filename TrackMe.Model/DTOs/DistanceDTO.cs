using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackMe.Model.DTOs
{
    public class DistanceDTO
    {
        public int DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public IList<IList<TrackDTO>> Origins { get; set; }
        public IList<IList<TrackDTO>> Destinations { get; set; }
    }
}
