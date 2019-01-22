using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBTech.Integration.Merchant.CoinBase
{
    public class Response<T>
    {
        public Charge<T> Data { get; set; }
    }
}