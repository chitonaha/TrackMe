using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackMe.Model.DTOs
{
    public class TrajectoryDTO
    {
        public IList<RouteDTO> Routes { get; set; }
        public TrackDTO LastTrack { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
