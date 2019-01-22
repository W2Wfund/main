using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WayWealth.Areas.lk.Models.CoinBase
{
    public class Price
    {
        public string Base {get;set;}
        public string Currency { get; set; }
        public decimal Amount { get; set; }

            
    }
}