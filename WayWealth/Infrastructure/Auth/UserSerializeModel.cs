using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WayWealth.Infrastructure.Auth
{
    public class UserSerializeModel
    {
        public uint id_object { get; set; }
        public string Email { get; set; }

        public decimal BalanceInner { get; set; }
        public decimal BalancePercents { get; set; }
        public decimal BalanceInvestments { get; set; }
        public decimal BalanceRewards { get; set; }
        public decimal BlockedResidue { get; set; }
        public decimal MarketOutputResidue { get; set; }

        // Адреса крипто-кошельков
        public string AccountBitcoin { get; set; }
        public string AccountEthereum { get; set; }
        public string AccountLitecoin { get; set; }
        public string AccountBitcoinCash { get; set; }
        public string AccountRipple { get; set; }
        public string AccountUsdt { get; set; }

        public string Image { get; set; }

        //public string Review { get; set; }
        public string ReviewStatus { get; set; }
        public DateTime? ReviewDate { get; set; }

        public int PersonalId { get; set; }
        public string Login { get; set; }
        public string ObjectName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string GoogleAuthKey { get; set; }
        public bool GoogleAuthEnabled { get; set; }
        public string VerificationStatus { get; set; }
    }
}