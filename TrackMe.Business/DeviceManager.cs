using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Business.Interfaces;
using TrackMe.Model.Interfaces;
using TrackMe.Model;
using System.IO;
using TrackMe.Model.DTOs;
using TrackMe.AWS.Interface;
using System.Transactions;

namespace TrackMe.Business
{
    public class DeviceManager : BaseManager, IDeviceManager
    {
        public DeviceManager(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<DeviceDTO> GetAllDevices()
        {
            return UnitOfWork.DeviceRepository.GetAllDevices();
        }

        public IList<DeviceType> GetAllDeviceTypes()
        {
            return UnitOfWork.DeviceTypeRepository.GetAll().ToList();
        }

        public void CreateDevice(string name, string description, int deviceTypeId, string fileName, string fileExtension, Stream image)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Device device = new Device()
                {
                    Description = description,
                    DeviceTypeId = deviceTypeId,
                    Name = name
                };

                UnitOfWork.DeviceRepository.Insert(device);
                UnitOfWork.Save();

                if (image != null)
                {
                    string imagenId = string.Format("{0:000}", device.DeviceId);
                    string key = string.Format("device_{0}{1}", imagenId, fileExtension);

                    Image img = new Image()
                    {
                        FileName = key,
                        ImageKey = key
                    };

                    UnitOfWork.ImageRepository.Insert(img);

                    DeviceImage dImg = new DeviceImage()
                    {
                        Device = device,
                        Image = img
                    };

                    UnitOfWork.DeviceImageRepository.Insert(dImg);

                    UnitOfWork.Save();

                    AWSManager.Instance.UploadFile(image, key);
                }

                ts.Complete();
            }
        }

        public void DeleteDevice(int deviceId)
        {
            Device device = UnitOfWork.DeviceRepository.GetByID(deviceId);

            IList<DeviceImage> dImgs = device.DeviceImages.ToList();
            IList<Image> images = device.DeviceImages.Select(p => p.Image).ToList();

            UnitOfWork.DeviceImageRepository.Delete(dImgs);
            UnitOfWork.ImageRepository.Delete(images);
            UnitOfWork.DeviceRepository.Delete(device);

            UnitOfWork.Save();

            foreach (var img in images)
            {
                AWSManager.Instance.DeleteFile(img.ImageKey);
            }
        }

        public void EnableDisableDevice(int deviceId)
        {
            Device device = UnitOfWork.DeviceRepository.GetByID(deviceId);
            device.IsDisabled = !device.IsDisabled;
            UnitOfWork.DeviceRepository.Update(device);

            UnitOfWork.Save();
        }

        public IList<Device> GeDevicesByUserId(int userId)
        {
            return UnitOfWork.UserDeviceRepository.Get(p => p.UserId == userId).Select(p => p.Device).ToList();
        }

        public IList<Device> GeUnAssignedDevices()
        {
            return UnitOfWork.DeviceRepository.GetUnAssignedDevices();
        }

        public void UnAssignDevice(int deviceId)
        {
            IList<UserDevice> userDevices = UnitOfWork.UserDeviceRepository.Get(p => p.DeviceId == deviceId).ToList();
            UnitOfWork.UserDeviceRepository.Delete(userDevices);
            UnitOfWork.Save();
        }

        public void AssignDevices(int userId, IList<int> deviceIds)
        {
            User user = UnitOfWork.UserRepository.GetByID(userId);
            foreach (var id in deviceIds)
            {
                Device device = UnitOfWork.DeviceRepository.GetByID(id);

                UserDevice userDevice = new UserDevice()
                {
                    User = user,
                    Device = device
                };

                UnitOfWork.UserDeviceRepository.Insert(userDevice);
            }

            UnitOfWork.Save();
        }

        public void ResetDevice(int deviceId)
        {
            IList<Track> tracks = UnitOfWork.TrackRepository.GetAllByDeviceId(deviceId);
            UnitOfWork.TrackRepository.Delete(tracks);
            UnitOfWork.Save();
        }
    }
}
