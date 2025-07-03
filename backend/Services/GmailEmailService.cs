using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
namespace backend.Services
{
    public class GmailEmailService
    {
        private readonly string _emailFrom = "graceisforyou9@gmail.com";
        private readonly string _appPassword = "xsjnkxbaizqrkdfn";

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Grace Academy Support", _emailFrom));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart("plain") { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailFrom, _appPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
