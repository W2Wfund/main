using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using W2W.ModelKBT.Entities;

namespace WayWealth.Areas.lk.ViewModels.Home
{
    public class StructureView
    {
        public uint? RootId { get; set; }
        public string SearchText { get; set; }

        public StructureViewType ViewType { get; set; }
        public MarketingTypeView MarketingType { get; set; }
    }
}