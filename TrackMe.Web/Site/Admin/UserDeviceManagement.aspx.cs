using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackMe.Web.Support;
using TrackMe.Business.Interfaces;
using TrackMe.Support.IoC;
using TrackMe.Model.DTOs;
using TrackMe.Model;
using TrackMe.Support.SafeCast;
using TrackMe.Model.Enums;

namespace TrackMe.Web.Site.Admin
{
    public partial class UserDeviceManagement : BasePage
    {
        IDeviceManager _deviceManager = IOCContainerFactory.Instance.CurrentContainer.Resolve<IDeviceManager>();
        IUserManager _userManager = IOCContainerFactory.Instance.CurrentContainer.Resolve<IUserManager>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers();
                LoadDevices();
            }
        }


        private int UserId { get { return SafeCast.ToInteger(cboUsers.SelectedValue); } }
        private IList<int> DeviceIds { get { return GetAllSelectedDevices(); } }

        private void LoadUserDevices()
        {
            IList<Device> devices = _deviceManager.GeDevicesByUserId(UserId);
            grUserDevices.DataSource = devices;
            grUserDevices.DataBind();
            lblUserDevicesMessage.Visible = !devices.Any();
        }

        private void LoadUnAssignedDevices()
        {
            IList<Device> devices = _deviceManager.GeUnAssignedDevices();
            grUnnassignDevices.DataSource = devices;
            grUnnassignDevices.DataBind();
            lblUnassignedDevicesMessage.Visible = !devices.Any();
        }

        private void LoadUsers()
        {
            IList<UserDTO> users = _userManager.GetUsersInRole(RoleType.User);
            cboUsers.DataSource = users;
            cboUsers.DataBind();
        }

        protected void cboUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUserDevices();
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            _deviceManager.AssignDevices(UserId, DeviceIds);
            LoadDevices();
        }

        private IList<int> GetAllSelectedDevices()
        {
            IList<int> ids = new List<int>();
            foreach (GridViewRow row in grUnnassignDevices.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("chkSelect");
                HiddenField hdnDeviceId = (HiddenField)row.FindControl("hdnDeviceId");
                if (cb != null && cb.Checked)
                    ids.Add(SafeCast.ToInteger(hdnDeviceId.Value));
            }

            return ids;
        }

        protected void grUserDevices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int deviceId = SafeCast.ToInteger(e.CommandArgument);

            switch (e.CommandName)
            {
                case "UnAssign":
                    _deviceManager.UnAssignDevice(deviceId);
                    LoadDevices();
                    break;
            }
        }

        private void LoadDevices()
        {
            LoadUnAssignedDevices();
            LoadUserDevices();
        }
    }
}