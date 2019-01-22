using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WayWealth.Areas.lk.Models.CoinBase
{
    public class Rate
    {
        public string Currency { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}