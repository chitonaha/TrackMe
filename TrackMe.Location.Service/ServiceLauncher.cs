using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;

namespace TrackMe.Location.Service
{
    internal static class ServiceLauncher
    {
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
                                {
                                    new LocationService()
                                };

            ServiceBase.Run(ServicesToRun);
        }
    }
}
