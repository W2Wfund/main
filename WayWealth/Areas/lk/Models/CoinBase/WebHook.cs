using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBTech.Integration.Merchant.CoinBase
{
    public class WebHook<T>
    {
        public string id { get; set; }
        public DateTime scheduled_for { get; set; }
        public int attempt_number { get; set; }
        public Event<T> Event { get; set; }
    }
}