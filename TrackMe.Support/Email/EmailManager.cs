using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using TrackMe.Support.Configuration;

namespace TrackMe.Support.Email
{
    public class EmailManager
    {
        private static readonly EmailManager instance = new EmailManager();


        private EmailManager()
        {
        }

        /// <summary>
        /// EmailManager instance.
        /// </summary>
        public static EmailManager Instance
        {
            get
            {
                return instance;
            }
        }

        public void SendEmail(string from, string[] to, string subject, string body)
        {
            MailMessage msg = new MailMessage();
            if (string.IsNullOrEmpty(from))
                msg.From = new MailAddress(AppSettingsManager.Instance.EmailFrom);
            else
                msg.From = new MailAddress(from);
            
            foreach (var email in to)
            {
                msg.To.Add(email);
            }
            msg.Subject = subject;
            msg.Body = body.ToString();
            msg.IsBodyHtml = true;
            msg.BodyEncoding = Encoding.Unicode;

            SmtpClient smtp = new SmtpClient();
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }
    }
}
