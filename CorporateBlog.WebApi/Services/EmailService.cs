using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Net.Mail;

namespace CorporateBlog.WebApi.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var fromAddress = new MailAddress(ConfigurationManagerService.SmtpServiceEmail);
            var toAddress = new MailAddress(message.Destination);
            var password = ConfigurationManagerService.SmtpServicePassword;

            var smtp = new SmtpClient
            {
                Host = ConfigurationManagerService.SmtpServiceHost,
                Port = ConfigurationManagerService.SmtpServicePort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, password)
            };
            
            await Task.Run(() => smtp.Send(new MailMessage(fromAddress, toAddress)
            {
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            }));

        }
    }
}