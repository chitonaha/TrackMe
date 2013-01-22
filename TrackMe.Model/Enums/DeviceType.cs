using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TrackMe.Model.Enums
{
    public enum DeviceType
    {
        [Description("DRIVING")]
        DRIVING = 1,
        [Description("WALKING")]
        WALKING  = 2,
        [Description("BICYCLING")]
        BICYCLING  = 3
    }
}
