using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using W2W.ModelKBT.Entities;

namespace WayWealth.Areas.lk.ViewModels.Home
{
    public class HistoryView
    {
        // filters
        public string account { get; set; }
        public DateTime? begin { get; set; }
        public DateTime? end { get; set; }

        public string article { get; set; }

        
        public PageInfo PageInfo { get; set; }
        public IEnumerable<InnerTransfer> Items { get; set; }

        public decimal TotalSumPrihod { get; set; }
        public decimal TotalSumRashod { get; set; }
        public decimal Saldo => this.TotalSumPrihod - this.TotalSumRashod;
    }
}