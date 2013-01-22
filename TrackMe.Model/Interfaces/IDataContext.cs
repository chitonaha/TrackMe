using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace TrackMe.Model.Interfaces
{
    public interface IDataContext
    {
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void Dispose();
        DbSet<Device> Devices { get; set; }
        DbSet<Direction> Directions { get; set; }
        DbSet<UserDevice> UserDevices { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Track> Tracks { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<DeviceImage> DeviceImages { get; set; }
    }
}
