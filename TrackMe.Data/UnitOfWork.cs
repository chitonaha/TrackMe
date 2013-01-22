using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Data.Repositories;
using TrackMe.Model;
using TrackMe.Model.Interfaces;

namespace TrackMe.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDataContext context;
        private ITrackRepository trackRepository;
        private IUserRepository userRepository;
        private IDeviceRepository deviceRepository;
        private IUserDeviceRepository userDeviceRepository;
        private IDeviceImageRepository deviceImageRepository;
        private IImageRepository imageRepository;
        private IDeviceTypeRepository deviceTypeRepository;

        public UnitOfWork(IDataContext dataContext)
        {
            this.context = dataContext;
        }

        public ITrackRepository TrackRepository
        {
            get { return trackRepository ?? (trackRepository = new TrackRepository(context)); }
        }

        public IUserRepository UserRepository
        {
            get { return userRepository ?? (userRepository = new UserRepository(context)); }
        }

        public IDeviceRepository DeviceRepository
        {
            get { return deviceRepository ?? (deviceRepository = new DeviceRepository(context)); }
        }

        public IUserDeviceRepository UserDeviceRepository
        {
            get { return userDeviceRepository ?? (userDeviceRepository = new UserDeviceRepository(context)); }
        }

        public IDeviceImageRepository DeviceImageRepository 
        {
            get { return deviceImageRepository ?? (deviceImageRepository = new DeviceImageRepository(context)); }
        }

        public IImageRepository ImageRepository
        {
            get { return imageRepository ?? (imageRepository = new ImageRepository(context)); }
        }

        public IDeviceTypeRepository DeviceTypeRepository
        {
            get { return deviceTypeRepository ?? (deviceTypeRepository = new DeviceTypeRepository(context)); }
        }


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
