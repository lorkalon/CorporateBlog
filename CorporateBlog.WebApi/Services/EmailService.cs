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
            var fromAddress = new MailAddress("annalorkalon@gmail.com");
            var toAddress = new MailAddress("lorkalon@mail.ru", "To Name");
            const string fromPassword = "jarbusha91";
            const string subject = "Hello!";
            const string body = "Body";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };


            await Task.Run(() => smtp.Send(new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            }));


            //var myMessage = new SendGridMessage();

            //myMessage.AddTo(message.Destination);
            //myMessage.From = new System.Net.Mail.MailAddress("hanna.shviatsova@itechart-group.com", "Hanna Shviatsova");
            //myMessage.Subject = message.Subject;
            //myMessage.Text = message.Body;
            //myMessage.Html = message.Body;

            //var credentials = new NetworkCredential(ConfigurationManagerService.EmailServiceAccount,
            //                                        ConfigurationManagerService.EmailServicePassword);
            //// Create a Web transport for sending email.
            //var transportWeb = new Web(credentials);


        }
    }
}