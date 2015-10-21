using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using SendGrid;

namespace CorporateBlog.WebApi.Services
{
    public class EmailService :IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var myMessage = new SendGridMessage();

            myMessage.AddTo(message.Destination);
            myMessage.From = new System.Net.Mail.MailAddress("hanna.shviatsova@itechart-group.com", "Hanna Shviatsova");
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;

            var credentials = new NetworkCredential(ConfigurationManagerService.EmailServiceAccount,
                                                    ConfigurationManagerService.EmailServicePassword);
            try
            {
                // Create a Web transport for sending email.
                var transportWeb = new Web(credentials);

                // Send the email.
                if (transportWeb != null)
                {
                    await transportWeb.DeliverAsync(myMessage);
                }
                else
                {
                    //Trace.TraceError("Failed to create Web transport.");
                    await Task.FromResult(0);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}