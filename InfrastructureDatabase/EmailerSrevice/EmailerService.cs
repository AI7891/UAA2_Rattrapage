using ApplicationCoreBusiness.Interfaces.IEmailer;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace InfrastructureDatabase.EmailerSrevice
{
    public class EmailerService : IEmailerService
    {
        private readonly SmtpClient _client;

        public EmailerService()
        {
            _client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("yourEmail@gmail.com", "yourAppPassword"),
                EnableSsl = true
            };
        }
        public void SendWelcomeEmail(string toEmail, string memberName)
        {
            var mail = new MailMessage(
                from: "yourEmail@gmail.com",
                to: toEmail,
                subject: "Welcome to the Platform!",
                body: $"Hello {memberName},\n\nYour account has been successfully created.\n\nRegards,\nYour Team"
            );

            _client.Send(mail); // synchronous
        }
    }
}
