using KBTech.Integration.Merchant.Ripple.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBTech.Integration.Merchant.Ripple.Request
{
    public class AccountPaymentRequest : IRequest
    {
        public string Path => "/v2/accounts/{address}/payments";
        public string Method => "GET";

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public AccountPaymentType Type { get; set; }
        public string Currency { get; set; }
        public string Issuer { get; set; }
        public int Source_tag { get; set; }
        public int Destination_tag { get; set; }
        public int Limit { get; set; }
        public string Marker { get; set; }
        public AccountPaymentFormat Format { get; set; }
    }
}