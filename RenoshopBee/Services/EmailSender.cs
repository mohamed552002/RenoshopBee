using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace RenoshopBee.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var fromMail = "renoshopebee@outlook.com";
            var fromPassword = "Renooshopbee@123";

            var message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(email);
            message.Body = $"<html><body>{htmlMessage}</body></html>";
            message.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.outlook.com")
            {

                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true

            };
            smtpClient.Send(message);
            smtpClient.Dispose();
        }
    }
}
