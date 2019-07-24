using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SZPD.Models
{
    public class EmailService
    {
        public async static Task SendEmail(string email, string subject, string content)
        {
            try
            {
                var _email = "szpd@onet.pl";
                var _pass = ConfigurationManager.AppSettings["EmailPassword"];
                var _name = "Mail rejestracyjny";
                MailMessage message = new MailMessage();
                message.To.Add(email);
                message.From = new MailAddress(_email, _name);
                message.Subject = subject;
                message.Body = content;
                message.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.poczta.onet.pl";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_email, _pass);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    await smtp.SendMailAsync(message);
                }

            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}