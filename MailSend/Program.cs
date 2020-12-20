using System;
using System.Net;
using System.Net.Mail;

namespace MailSend
{
    class Program
    {
        static void Main(string[] args)
        {


                var fromAddress = new MailAddress("alexeyfekson@gmail.com", "From Name");
                var toAddress = new MailAddress("alexeyfekson@example.com", "To Name");
                const string fromPassword = "Mm99ggqq";
                const string subject = "Subject";
                const string body = "BEst Body";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            
        }
    }
}
