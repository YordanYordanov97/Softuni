using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace WebServer.Application.Models
{
    public class SendEmail
    {
        private const string SendEmailHtml = "sendEmail";
        private const string SenderEmail = "tester11195.kaad@gmail.com";
        private const string SenderPassword = "Tester12";

        public void SendEmailTo(string email, string subject, string text)
        {
            var decodeSubject = WebUtility.HtmlDecode(subject);
            var decodeText = WebUtility.HtmlDecode(text);
            var fromAddress = new MailAddress(SenderEmail);
            var toAddress = new MailAddress(email);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, SenderPassword)
            };
            using (var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = decodeSubject,
                Body = decodeText
            })
            {
                smtp.Send(mailMessage);
            }
        }
    }
}
