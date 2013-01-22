using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackMe.Web.Support;
using System.IO;
using TrackMe.Support.Configuration;
using TrackMe.Model.DTOs;
using TrackMe.Business.Interfaces;
using TrackMe.Support.IoC;
using TrackMe.Support.SafeCast;
using TrackMe.Model;

namespace TrackMe.Web.Site.Admin
{
    public partial class DeviceManagement : BasePage
    {
        private const string ImageTypePng = "image/png";
        private const string ImageTypeJpeg = "image/jpeg";
        private const string ImageTypePjpeg = "image/pjpeg";
        private const string ImageTypeGif = "image/gif";
        private const string ImageTypeBmp = "image/bmp";
        private const string ImageTypeXpng = "image/x-png";

        IDeviceManager _deviceManager = IOCContainerFactory.Instance.CurrentContainer.Resolve<IDeviceManager>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDevices();
                LoadDeviceTypes();
            }
        }

        private void LoadDevices()
        {
            IList<DeviceDTO> users = _deviceManager.GetAllDevices();
            grDevices.DataSource = users;
            grDevices.DataBind();
        }

        private void LoadDeviceTypes()
        {
            IList<DeviceType> types = _deviceManager.GetAllDeviceTypes();
            cboDeviceType.DataSource = types;
            cboDeviceType.DataBind();
        }

        private Stream GetPhoto(FileUpload fileUpload)
        {
            Stream result = null;
            if (fileUpload.HasFile && fileUpload.PostedFile.ContentLength < AppSettingsManager.Instance.PhotoMaxSize
                && (fileUpload.PostedFile.ContentType == ImageTypeBmp
                || fileUpload.PostedFile.ContentType == ImageTypeGif
                || fileUpload.PostedFile.ContentType == ImageTypeJpeg
                || fileUpload.PostedFile.ContentType == ImageTypePjpeg
                || fileUpload.PostedFile.ContentType == ImageTypePng
                || fileUpload.PostedFile.ContentType == ImageTypeXpng))
            {
                result = fileUpload.FileContent;
            }

            return result;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Stream fileContent = GetPhoto(fuImage);
            string fileExtension = System.IO.Path.GetExtension(fuImage.FileName);
            _deviceManager.CreateDevice(txtName.Text, txtDescription.Text, SafeCast.ToInteger(cboDeviceType.SelectedValue), fuImage.FileName, fileExtension, fileContent);
            LoadDevices();
        }

        protected void grDevices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int deviceId = SafeCast.ToInteger(e.CommandArgument);

            switch (e.CommandName)
            {
                case "DeleteDevice":
                    _deviceManager.DeleteDevice(deviceId);
                    break;
                case "EnableDisableDevice":
                    _deviceManager.EnableDisableDevice(deviceId);
                    break;
            }
            LoadDevices();
        }

    }
}