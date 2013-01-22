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
using TrackMe.Support.SafeCast;

namespace TrackMe.Web.Site.Admin
{
    public partial class UserManagement : BasePage
    {
        IUserManager _userManager = IOCContainerFactory.Instance.CurrentContainer.Resolve<IUserManager>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers();
            }

        }

        private void LoadUsers()
        {
            IList<UserDTO> users = _userManager.GetAllUsers();
            grUsers.DataSource = users;
            grUsers.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            _userManager.CreateUser(txtUserName.Text, txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPassword.Text, false);
            LoadUsers();
        }

        protected void grUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int userId = SafeCast.ToInteger(e.CommandArgument);

            switch (e.CommandName)
            {
                case "DeleteUser":
                    _userManager.DeleteUser(userId);
                    break;
                case "EnableDisableUser":
                    _userManager.EnableDisableUser(userId);
                    break;
            }

            LoadUsers();
        }
    }
}