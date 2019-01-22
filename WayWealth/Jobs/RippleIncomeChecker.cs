using Common.Web.Mail;
using KBTech.Integration.Merchant.Ripple;
using KBTech.Integration.Merchant.Ripple.Enums;
using KBTech.Integration.Merchant.Ripple.Request;
using KBTech.Integration.Merchant.Ripple.Response;
using Ninject;
using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using W2W.ModelKBT;
using WayWealth.Areas.lk.Services;
using WayWealth.Infrastructure.Mail;

namespace WayWealth.Jobs
{
    public class RippleIncomeChecker : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            PaymentService ps = new PaymentService(
                ds: new KbtDataService(),
                ms: new MailService(),
                logger: LogManager.GetCurrentClassLogger());

            ps.CheckRipple(ConfigurationManager.AppSettings["wallet.ripple"]);

            //"


            //using (MailMessage message = new MailMessage("admin@yandex.ru", "user@yandex.ru"))
            //{
            //    message.Subject = "Новостная рассылка";
            //    message.Body = "Новости сайта: бла бла бла";
            //    using (SmtpClient client = new SmtpClient
            //    {
            //        EnableSsl = true,
            //        Host = "smtp.yandex.ru",
            //        Port = 25,
            //        Credentials = new NetworkCredential("admin@yandex.ru", "password")
            //    })
            //    {
            //        await client.SendMailAsync(message);
            //    }
            //}
        }



    }
}