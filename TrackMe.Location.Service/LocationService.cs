using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using TrackMe.Support.Configuration;
using TrackMe.Model;
using TrackMe.Location.Service.Import;
using TrackMe.Business.Interfaces;
using TrackMe.Support.IoC;

namespace TrackMe.Location.Service
{
    public class LocationService : ServiceBase
    {
        private bool IsRunInProgress { get; set; }

        private IDictionary<int, Track> _tracks = new Dictionary<int, Track>();
        private IList<Track> _tracksDevice2 = new List<Track>();
        private int _index = 1;
        private int _index2 = 0;
        private ITrackingManager _manager;
        
        public LocationService()
        {
            _manager = IOCContainerFactory.Instance.CurrentContainer.Resolve<ITrackingManager>();
            LoadTracksByDevice1();
            LoadTracksByDevice2();
        }

        private static Timer ServiceTimer { get; set; }

        private void Run()
        {
            ServiceTimer.Enabled = false;
            try
            {
                IsRunInProgress = true;

                //Device 1
                if (_index > 18)
                {
                    _manager.DeleteAllTracksByDeviceId(1);
                    _index = 1;
                }

                Track track = _tracks[_index];
                track.TrackDate = DateTime.Now;
                _manager.AddTrack(track);

                _index++;

                //Device 2
                if (_index2 > 117)
                {
                    _manager.DeleteAllTracksByDeviceId(2);
                    _index2 = 1;
                }

                Track trackDevice2 = _tracksDevice2[_index2];
                trackDevice2.TrackDate = DateTime.Now;
                _manager.AddTrack(trackDevice2);

                _index2++;

                //IList<Track> tracks = new TrackDataImporter(AppSettingsManager.Instance.TrackFilePath).Import();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                IsRunInProgress = false;
            }

            ServiceTimer.Enabled = true;
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                StartService(AppSettingsManager.Instance.ServiceWaitTime);
            }
            catch (Exception)
            {
                  }
        }

        private void StartService(double serviceWaitTimeInSeconds)
        {
            if (double.MaxValue / 1000 < serviceWaitTimeInSeconds)
            {
                throw new ArgumentOutOfRangeException("serviceWaitTimeInSeconds");
            }

            double waitTimeInMilliseconds = serviceWaitTimeInSeconds * 1000;
            ServiceTimer = new Timer(waitTimeInMilliseconds);
            ServiceTimer.Elapsed += ServiceTimerOnElapsed;
            ServiceTimer.Start();
        }

        private void ServiceTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (IsRunInProgress == false)
            {
                Run();
            }
        }

        protected override void OnStop()
        {
            
        }

        private void LoadTracksByDevice1()
        {
            Track track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -37.3243576m,
                Longitude = -59.1345161m
            };

            _tracks.Add(1, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -37.316181857118636m,
                Longitude = -59.12120819091797m
            };

            _tracks.Add(2, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -37.31136927830865m,
                Longitude = -59.114084243774414m
            };

            _tracks.Add(3, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -37.30631744573363m,
                Longitude = -59.120821952819824m
            };

            _tracks.Add(4, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -37.299046307144216m,
                Longitude = -59.12974834442139m
            };

            _tracks.Add(5, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -37.2901697861369m,
                Longitude = -59.14657115936279m
            };

            _tracks.Add(6, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -37.286516452124125m,
                Longitude = -59.17219161987305m
            };

            _tracks.Add(7, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -37.26124554878956m,
                Longitude = -59.14296627044678m
            };

            _tracks.Add(8, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -37.23125096160605m,
                Longitude = -59.0983772277832m
            };

            _tracks.Add(9, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -37.1996378291534m,
                Longitude = -59.06528949737549m
            };

            _tracks.Add(10, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -37.14701106323107m,
                Longitude = -59.00228977203369m
            };

            _tracks.Add(11, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -37.07935292783438m,
                Longitude = -58.99924278259277m
            };

            _tracks.Add(12, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -37.000290649078835m,
                Longitude = -59.002933502197266m
            };

            _tracks.Add(13, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -36.91040652759201m,
                Longitude = -58.98726940155029m
            };

            _tracks.Add(14, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -36.84655567801436m,
                Longitude = -58.98516654968262m
            };

            _tracks.Add(15, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -36.800831799453846m,
                Longitude = -59.04284477233887m
            };

            _tracks.Add(16, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -36.770792396445m,
                Longitude = -59.08327102661133m
            };

            _tracks.Add(17, track);

            track = new Track
            {
                DeviceId = 1,
                TrackDate = DateTime.Now,
                Latitude = -36.77477999721947m,
                Longitude = -59.08842086791992m
            };

            _tracks.Add(18, track);

            // Track track = new Track
            // {
            //     DeviceId = 1,
            //     TrackDate = DateTime.Now,
            //     Latitude = -37.3243576m,
            //     Longitude = -59.1345161m
            // };

            // _tracks.Add(1, track);

            // track = new Track
            // {
            //     DeviceId = 1,
            //     TrackDate = DateTime.Now,
            //     Latitude = -37.32220198213998m,
            //     Longitude = -59.1373872756958m
            // };

            // _tracks.Add(2, track);

            // track = new Track
            // {
            //     DeviceId = 1,
            //     TrackDate = DateTime.Now,
            //     Latitude = -37.32278642081179m,
            //     Longitude = -59.138814210891724m
            // };

            // _tracks.Add(3, track);

            // track = new Track
            // {
            //     DeviceId = 1,
            //     TrackDate = DateTime.Now,
            //     Latitude = -37.32324287705121m,
            //     Longitude = -59.14018213748932m
            // };

            // _tracks.Add(4, track);

            // track = new Track
            // {
            //     DeviceId = 1,
            //     TrackDate = DateTime.Now,
            //     Latitude = -37.323720660051535m,
            //     Longitude = -59.14142668247223m
            // };

            // _tracks.Add(5, track);

            // track = new Track
            // {
            //     DeviceId = 1,
            //     TrackDate = DateTime.Now,
            //     Latitude = -37.32427095992151m,
            //     Longitude = -59.1428804397583m
            // };

            // _tracks.Add(6, track);

            // track = new Track
            // {
            //     DeviceId = 1,
            //     TrackDate = DateTime.Now,
            //     Latitude = -37.324778597313426m,
            //     Longitude = -59.14435029029846m
            // };

            // _tracks.Add(7, track);

            // track = new Track
            // {
            //     DeviceId = 1,
            //     TrackDate = DateTime.Now,
            //     Latitude = -37.32536728175928m,
            //     Longitude = -59.145787954330444m
            // };

            // _tracks.Add(8, track);

            // track = new Track
            // {
            //     DeviceId = 1,
            //     TrackDate = DateTime.Now,
            //     Latitude = -37.32587064596106m,
            //     Longitude = -59.1471666097641m
            // };

            // _tracks.Add(9, track);

            // track = new Track
            // {
            //     DeviceId = 1,
            //     TrackDate = DateTime.Now,
            //     Latitude = -37.32643799309546m,
            //     Longitude = -59.14870083332062m
            // };

            // _tracks.Add(10, track);

            // track = new Track
            // {
            //     DeviceId = 1,
            //     TrackDate = DateTime.Now,
            //     Latitude = -37.32697974162404m,
            //     Longitude = -59.15017068386078m
            // };

            //_tracks.Add(11, track);

            // track = new Track
            // {
            //     DeviceId = 1,
            //     TrackDate = DateTime.Now,
            //     Latitude = -37.327530017626756m,
            //     Longitude = -59.15167272090912m
            // };

            // _tracks.Add(12, track);

            // track = new Track
            // {
            //     DeviceId = 1,
            //     TrackDate = DateTime.Now,
            //     Latitude = -37.32820825924808m,
            //     Longitude = -59.153496623039246m
            // };

            // _tracks.Add(13, track);
        }



        private void LoadTracksByDevice2()
        {
            _tracksDevice2 = new TrackDataImporter(@"D:\Sistemas\TrackMe\Tracks\Suarez_colon.txt", ",").Import();
        }
    }
}
