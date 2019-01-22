using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBTech.Integration.Merchant.CoinBase
{
    public class Event<T>
    {
        public string id { get; set; }
        public string type { get; set; }
        public string api_version { get; set; }
        public DateTime created_at { get; set; }
        public Charge<T> Data { get; set; }
    }
}