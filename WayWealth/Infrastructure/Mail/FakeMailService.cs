using Common.Web.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WayWealth.Infrastructure.Mail
{
    public class FakeMailService : IMailService
    {
        public void SendMessage(string to, string subject, string body)
        {

        }
    }
}