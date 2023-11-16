using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Showcase.Models;
using Showcase.Properties;

namespace Showcase.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;

            // Stel de body van de e-mail in met alle gegevens
            var emailBody = $"Naam: {mailRequest.FirstName} {mailRequest.LastName}\n" +
                            $"E-mail: {mailRequest.FromEmail}\n" +
                            $"Telefoonnummer: {mailRequest.Mobile}\n\n" +
                            $"Onderwerp: {mailRequest.Subject}\n\n" +
                            $"Bericht:\n{mailRequest.Body}";

            var builder = new BodyBuilder();
            builder.HtmlBody = emailBody;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
