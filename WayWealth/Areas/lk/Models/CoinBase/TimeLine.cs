using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBTech.Integration.Merchant.CoinBase
{
    public enum TimeLineStatuses
    {
        NEW, PENDING, COMPLETED, EXPIRED, UNRESOLVED, RESOLVED
    }

    public enum TimeLineContextes
    {
        UNDERPAID, OVERPAID, DELAYED, MULTIPLE, MANUAL, OTHER
    }

    public class TimeLine
    {
        public TimeLineStatuses Status { get; set; }
        public DateTime Time { get; set; }
        public TimeLineContextes? Context { get; set; }
    }
}