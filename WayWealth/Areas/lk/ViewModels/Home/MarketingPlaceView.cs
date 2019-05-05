using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WayWealth.Areas.lk.ViewModels.Home
{
    public class MarketingPlaceView
    {
        public uint id_object { get; set; }
        public uint id_parent { get; set; }
        public string hash { get; set; }

        public string ObjectName { get; set; }
        public int? SortPosition { get; set; }
        public int? ChildCount { get; set; }

        public uint? PartnerId { get; set; }

        public string PartnerFirstName { get; set; }
        public string PartnerLastName { get; set; }
        public string PartnerMiddleName { get; set; }
        public decimal? PartnerBalanceInvestments { get; set; }


        public string ShortName
        {
            get
            {
                var name = ObjectName;
                if (!string.IsNullOrWhiteSpace(PartnerFirstName) ||
                    !string.IsNullOrWhiteSpace(PartnerLastName) ||
                    !string.IsNullOrWhiteSpace(PartnerMiddleName))
                {
                    name = "";
                    if (!string.IsNullOrWhiteSpace(PartnerLastName))
                        name += PartnerLastName + " ";
                    if (!string.IsNullOrWhiteSpace(PartnerFirstName))
                        name += PartnerFirstName.Substring(0, 1) + ".";
                    if (!string.IsNullOrWhiteSpace(PartnerMiddleName))
                        name += PartnerMiddleName.Substring(0, 1) + ".";
                }
                return name;
            }
        }

        public string Rank { get; set; }

        // заполнено уровней
        public int FilledLevels { get; set; }

        public int PartnerPersonalId { get; set; }
        public string PartnerLogin { get; set; }
        
        public uint? PartnerInviterId { get; set; }

        public decimal? PartnerLeftShoulderInvestSum { get; set; }

        public decimal? PartnerRightShoulderInvestSum { get; set; }

        public decimal? PartnerBinaryLeftShoulderSum { get; set; }

        public decimal? PartnerBinaryRightShoulderSum { get; set; }
    }
}