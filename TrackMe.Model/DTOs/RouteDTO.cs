using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackMe.Model.DTOs
{
    public class RouteDTO
    {
        public int DeviceId { get; set; }
        public string DeviceType { get; set; }
        public TrackDTO Origin { get; set; }
        public TrackDTO Destination { get; set; }
        public IList<TrackDTO> WayPoints { get; set; }
    }
}
