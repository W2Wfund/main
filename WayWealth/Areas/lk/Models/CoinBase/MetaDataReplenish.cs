using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBTech.Integration.Merchant.CoinBase
{
    public class MetaDataReplenish
    {
        public uint partner { get; set; }
        public uint order { get; set; }
        public int account { get; set; }
    }
}