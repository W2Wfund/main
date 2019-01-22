using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using System.Web;
using Common.Web.Mail;
using MimeKit;
using MailKit;
using System.Net.Mail;
using System.Net;

namespace WayWealth.Infrastructure.Mail
{
    public class MailService : Common.Web.Mail.IMailService
    {
        private readonly string _host;
        private readonly string _userName;
        private readonly string _password;
        private readonly bool _useSsl;
        private readonly int _port;

        public MailService()
        {
            _host = ConfigurationManager.AppSettings["smtphost"];
            _userName = ConfigurationManager.AppSettings["smtpusername"];
            _password = ConfigurationManager.AppSettings["smtppassword"];
            _useSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["smtpusessl"]);
            _port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpport"]);
        }

        public void SendMessage(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_userName));
            message.To.Add(new MailboxAddress(to));
            message.Subject = subject;

            //var bodyBuilder = new BodyBuilder { TextBody = body };
            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            message.Body = bodyBuilder.ToMessageBody();


            #region Standart smtp

            //MailAddress mailfrom = new MailAddress(_userName);
            //MailAddress mailto = new MailAddress(to);
            //MailMessage message = new MailMessage(mailfrom, mailto);
            //message.Subject = subject;
            //message.Body = body;
            //message.IsBodyHtml = true;

            //var client = new SmtpClient()
            //{
            //    Host = "smtp.zoho.com",
            //    EnableSsl = true,
            //    Port = 587,
            //    //Port = 465,
            //    //DeliveryMethod = SmtpDeliveryMethod.Network,
            //    //UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(_userName, _password)
            //};
            //client.Send(message);

            #endregion

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(_host, _port, _useSsl);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_userName, _password);
                client.Send(message);
                client.Disconnect(true);
            }
        }


    }
}