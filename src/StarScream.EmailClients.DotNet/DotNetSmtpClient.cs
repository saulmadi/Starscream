using System;
using System.Configuration;
using System.Net.Mail;
using AcklenAvenue.Email;

namespace StarScream.EmailClients.DotNet
{
    public class DotNetSmtpClient : ISmtpClient
    {
        #region ISmtpClient Members

        public void Send(string emailAddress, string subject, string body)
        {
            using (var client = new SmtpClient())
            {
                string from = ConfigurationManager.AppSettings["email_from"];
                if (string.IsNullOrEmpty(from))
                    throw new Exception("You need to include an app setting \"email_from\" in your web.config.");

                var message = new MailMessage(from, emailAddress, subject, body) {IsBodyHtml = true};
                client.Send(message);
            }
        }

        #endregion
    }
}