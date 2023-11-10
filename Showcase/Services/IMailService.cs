using Showcase.Models;
using Showcase.Properties;

namespace Showcase.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
