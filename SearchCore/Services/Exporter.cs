using Newtonsoft.Json;
using SearcCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace SearcCore.Services
{
    public class Exporter : IExporter
    {


        public string ExportToFile(IEnumerable<AppartmentModel> items, string filePath, string param, bool byKey)
        {
            string folder = filePath;
            Directory.CreateDirectory(folder);
            folder = folder + "\\" + (byKey ? "byKey" : "byValue");
            Directory.CreateDirectory(folder);
            folder = folder + "\\" + string.Format("{0}", param.Trim().Replace(" ", "_"));
            Directory.CreateDirectory(folder);
            string fileName = DateTime.Now.Date.ToString("dd'-'MM'-'yyyy");
            string fullPath = folder + "\\" + fileName + ".txt";
            using (StreamWriter outputFile = new StreamWriter(fullPath))
            {
                var line = JsonConvert.SerializeObject(items);
                outputFile.WriteLine(line);
            }
            return fileName;
        }

        public void sendToMail(string from, string to)
        {
            var fromAddress = new MailAddress("from@gmail.com", "From Name");
            var toAddress = new MailAddress("to@example.com", "To Name");
            const string fromPassword = "fromPassword";
            const string subject = "Subject";
            const string body = "Body";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
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
