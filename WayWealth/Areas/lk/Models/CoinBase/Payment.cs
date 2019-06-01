using KBTech.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WayWealth.Areas.lk.Models;

namespace KBTech.Integration.Merchant.CoinBase
{
    public class Block
    {
        public string hash { get; set; }
        public int height { get; set; }
        public int confirmations { get; set; }
        public int confirmations_required { get; set; }
    }

    public enum Moneys
    {
        [Description("USD")]
        local,

        [Description("CRYPTO")]
        crypto,

        [Description("BTC")]
        bitcoin,

        [Description("BCH")]
        bitcoincash,

        [Description("ETH")]
        ethereum,

        [Description("LTC")]
        litecoin,

        [Description("USDC")]
        usdc

    }
    public class Balance
    {
        public decimal amount { get; set; }
        public string currency { get; set; }
    }

    public class Payment
    {
        public Block block { get; set; }
        public Dictionary<Moneys, Balance> value { get; set; }
        public string status { get; set; }
        public string network { get; set; }
        public string transaction_id { get; set; }
    }
}