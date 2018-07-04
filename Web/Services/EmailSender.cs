using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Web.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private IConfiguration Configuration { get; set; }

        public EmailSender(IConfiguration config)
        {
            Configuration = config;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            MailAddress from = new MailAddress("vlad.starovoitov.1994@mail.ru", "Admin");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Body = message;

            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 2525);
            smtp.Credentials = new NetworkCredential("vlad.starovoitov.1994@mail.ru", "123456789q");
            smtp.EnableSsl = true;

            await smtp.SendMailAsync(m);
        }
    }
}
