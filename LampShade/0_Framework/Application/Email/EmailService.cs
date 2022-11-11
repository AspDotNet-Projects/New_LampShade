using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace _0_Framework.Application.Email
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string title, string messageBody, string destination)
        {
            var message = new MimeMessage();

            ////  MailboxAddress("CG digital", "test@atriya.com");  CG digital hast name mostaare ma
            var from = new MailboxAddress("CG Digital", "ali.company.cg@gmail.com");
            message.From.Add(from);

            var to = new MailboxAddress("User", destination);
            message.To.Add(to);

            message.Subject = title;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"<h1>" + $"{messageBody}</h1>",
            };

            message.Body = bodyBuilder.ToMessageBody();

            var client = new SmtpClient();
            client.Connect("smtp.gmail.com",587, SecureSocketOptions.StartTls);
            client.Authenticate("ali.company.cg@gmail.com", "qjnuyzryzczozbhu");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}