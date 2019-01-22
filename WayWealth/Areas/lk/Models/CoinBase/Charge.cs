using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBTech.Integration.Merchant.CoinBase
{
    public enum ChargePricingTypes
    {
        no_price, fixed_price
    }
    public enum ChargeAddresses
    {
        bitcoin,
        bitcoincash,
        ethereum,
        litecoin
    }

    public class Charge<T>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo_url { get; set; }
        public string Hosted_url { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Expires_at { get; set; }
        public DateTime? Confirmed_at { get; set; }
        public object Checkout { get; set; }
        public TimeLine[] Timeline { get; set; }
        public T Metadata { get; set; }
        public ChargePricingTypes Pricing_type { get; set; } 
        public Dictionary<Moneys, Balance> Pricing { get; set; }
        public Payment[] Payments { get; set; }
        public Dictionary<ChargeAddresses, string> Addresses { get; set; }
    }
}