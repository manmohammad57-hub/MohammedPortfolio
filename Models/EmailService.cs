using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace MohammedPortfolio.Models
{
    public class EmailService
    {
        private readonly string MyEmail;
        private readonly string EPassword;
        private readonly string SmtpHost;
        private readonly int SmtpPort;

        public EmailService(IConfiguration config)
        {
            // قراءة من appsettings.json
            MyEmail = config["EmailSettings:Email"];
            EPassword = config["EmailSettings:Password"];
            SmtpHost = config["EmailSettings:Host"];
            SmtpPort = int.Parse(config["EmailSettings:Port"]);
        }

        public async Task SendEmailAsync(string to, string subject, string body, string? fromEmail = null, string? fromName = null)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Mohammed_Portfolio", MyEmail));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            if (!string.IsNullOrEmpty(fromEmail))
                email.ReplyTo.Add(new MailboxAddress(fromName ?? "Visitor", fromEmail));

            var builder = new BodyBuilder { HtmlBody = body };
            email.Body = builder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(SmtpHost, SmtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(MyEmail, EPassword);
            await client.SendAsync(email);
            await client.DisconnectAsync(true);
        }

        public async Task SendEmailProjectAsync(string to, string subject, string body, string? fromEmail = null, string? fromName = null)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Mohammed_Portfolio", MyEmail));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

        }
    }
}
