using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Common.Web.Auth;
using Common.Web.Mail;
using KBTech.Lib;
using Ninject;
using NLog;
using W2W.ModelKBT;
using WayWealth.Helpers;
using WayWealth.Infrastructure.Auth;
using W2W.Marketing;

namespace WayWealth.Controllers
{
    public abstract class BaseController : Controller
    {
        public Logger Logger = LogManager.GetCurrentClassLogger();

        [Inject]
        public AuthenticationService AuthenticationService { get; set; }

       [Inject]
       public IDataService DataService { get; set; }

        [Inject]
        public IMailService MailService { get; set; }

        protected new virtual CustomPrincipal User => HttpContext.User as CustomPrincipal;

        public IEnumerable<T> GetPage<T>(IEnumerable<T> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

       
        protected bool IsAllowCreateNewPlace()
        {
            if (this.User == null || this.User.id_object == 0)
                return false;

            try
            {
                /*return false;*/ var marketing = new Service1();
                return marketing.IsAllowCreateNewPlace(93, this.User.id_object);
                /*using (var marketing = new Marketing.Service1Client())
                 {
                     return marketing.IsAllowCreateNewPlace(93, this.User.id_object);
                 }*/
            }
            catch
            {
                return false;
            }
        }

        protected string CreateUrl(string email, string pass)
        {
            // generate payload
            var payload = new Dictionary<string, object>();
            payload.Add("email", email);
            payload.Add("password", pass);
            //payload.Add("id_client", client.id_object);
            //payload.Add("action", "remind");
            payload.Add("iat", DateTime.Now.ConvertToUnixTimestamp());
            payload.Add("exp", DateTime.Now.AddDays(1).ConvertToUnixTimestamp());

            JwtHelper jwt = new JwtHelper();
            var secret = ConfigurationManager.AppSettings["jwt:secret"];
            string token = jwt.Encode(payload, secret);

            return Url.AbsoluteRouteUrl("default", new { controller = "sign", action = "reset", t = token });
        }

        protected string CreateUrl2(string email)
        {
            // generate payload
            var payload = new Dictionary<string, object>();
            payload.Add("email", email);
            //payload.Add("action", "remind");
            payload.Add("iat", DateTime.Now.ConvertToUnixTimestamp());
            payload.Add("exp", DateTime.Now.AddDays(1).ConvertToUnixTimestamp());

            JwtHelper jwt = new JwtHelper();
            var secret = ConfigurationManager.AppSettings["jwt:secret"];
            string token = jwt.Encode(payload, secret);

            return Url.AbsoluteRouteUrl("default", new { controller = "sign", action = "confirm", t = token });
        }

        protected string RandomString(int length)
        {
            Random r = new Random();
            const string chars = "abcdefghihklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[r.Next(s.Length)]).ToArray());
        }
    }
}