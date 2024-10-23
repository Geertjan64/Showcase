using System.ComponentModel.DataAnnotations;

namespace Showcase.Models
{
    public class MailRequest
    {
        public string ToEmail = "mailservertestdev@gmail.com";
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Subject { get; private set; }
        public string FromEmail { get; private set; }
        public string Mobile { get; private set; }
        public string Body { get; private set; }

        public MailRequest(string firstName, string lastName, string subject, string fromEmail, string mobile, string body)
        {
            FirstName = firstName;
            LastName = lastName;
            Subject = subject;
            FromEmail = fromEmail;
            Mobile = mobile;
            Body = body;
        }

        private MailRequest() { }
    }
}
