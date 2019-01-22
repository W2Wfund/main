using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBTech.Integration.Merchant.Ripple.Entities
{
    public class Balance
    {
        public string Counterparty { get; set; }
        public string Currency { get; set; }
        public decimal Value { get; set; }
    }
}