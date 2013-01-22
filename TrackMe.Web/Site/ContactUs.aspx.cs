using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackMe.Web.Support;
using TrackMe.Support.Email;
using TrackMe.Support.Configuration;
using System.Text;
using TrackMe.Web.Templates;
using TrackMe.Support.SafeCast;
using TrackMe.Web.Pages;

namespace TrackMe.Web.Site
{
    public partial class ContactUs : GeneralPage
    {
        private static Random _random;

        private static Random Random
        {
            get
            {
                if (_random == null)
                    _random = new Random();
                return _random;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitializeSum();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                TemplatesManager templatesManager = new TemplatesManager();
                string content = templatesManager.GetTemplate("ContactEmail.htm");
                content = content.Replace("{Name}", txtName.Text);
                content = content.Replace("{Email}", txtEmail.Text);
                content = content.Replace("{Telephone}", txtTelephone.Text);
                content = content.Replace("{Comment}", txtComment.Text);

                EmailManager.Instance.SendEmail(txtEmail.Text, new string[] { AppSettingsManager.Instance.ContactEmail }, "Contacto", content);

                lblMessage.Visible = true;
                ClearFields();
            }
        }

        private void ClearFields()
        {
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            txtComment.Text = string.Empty;
        }

        private void InitializeSum()
        {
            int num1 = Random.Next(0, 9);
            int num2 = Random.Next(0, 9);
            hdnNum1.Value = num1.ToString();
            hdnNum2.Value = num2.ToString();
            lblSum.Text = string.Format("{0} + {1} =", num1, num2);
        }

        protected void cvCheckSum_CheckSum(object source, ServerValidateEventArgs args)
        {
            int num1 = SafeCast.ToInteger(hdnNum1.Value);
            int num2 = SafeCast.ToInteger(hdnNum2.Value);
            int suma = SafeCast.ToInteger(txtSum.Text);
            args.IsValid = (num1 + num2) == suma;
        }
    }
}