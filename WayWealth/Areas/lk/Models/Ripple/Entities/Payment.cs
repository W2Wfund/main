using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBTech.Integration.Merchant.Ripple.Entities
{
    public class Payment
    {
        public decimal Amount { get; set; }
        public decimal Delivered_amount { get; set; }
        public BalanceChange[] Destination_balance_changes { get; set; }
        public BalanceChange[] Source_balance_changes { get; set; }
        public decimal Transaction_cost { get; set; }
        public int Destination_tag { get; set; }
        public int Source_tag { get; set; }
        public string Currency { get; set; }
        public string Destination { get; set; }
        public DateTime Executed_time { get; set; }
        public uint Ledger_index { get; set; }
        public string Source { get; set; }
        public string Source_currency { get; set; }
        public string Tx_hash { get; set; }
    }
}