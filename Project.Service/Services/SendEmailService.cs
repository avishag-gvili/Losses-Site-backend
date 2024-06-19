using MimeKit;
using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public class SendEmailService:ISendEmailService
    {
        public void SendEmail(string to, string name, string htmlBody, string Subject)
        {
            var email = new MimeMessage();
            // הגדרת השולח
            email.From.Add(new MailboxAddress("project", "prwyyqtsly@gmail.com"));
            // הגדרת הנמען
            email.To.Add(new MailboxAddress(name, to));

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = htmlBody; // הגדרת התוכן של המייל כ- HTML

            // הגדרת התוכן של המייל וכותרת המייל
            email.Body = bodyBuilder.ToMessageBody();
            email.Subject = Subject;

            using (var s = new MailKit.Net.Smtp.SmtpClient())
            {
                s.CheckCertificateRevocation = false;
                s.Connect("smtp.gmail.com", 587, false);
                s.Authenticate("prwyyqtsly@gmail.com", "z k w x m l w a m j e n i j q p"); // התחברות לשרת הSMTP
                s.Send(email); // שליחת המייל
                s.Disconnect(true);
            }
        }
    }
}
