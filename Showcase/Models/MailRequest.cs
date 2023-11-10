using System.ComponentModel.DataAnnotations;

namespace Showcase.Models
{
    public class MailRequest
    {
        public string Token { get; set; }

        public string ToEmail = "mailservertestdev@gmail.com";
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }
}
