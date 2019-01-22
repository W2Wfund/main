using KBTech.Integration.Merchant.Ripple.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBTech.Integration.Merchant.Ripple.Response
{
    public class AccountPaymentsResponse
    {
        public string Result { get; set; }
        public int Count { get; set; }
        public string Marker { get; set; }
        public Payment[] Payments { get; set; }
    }
}