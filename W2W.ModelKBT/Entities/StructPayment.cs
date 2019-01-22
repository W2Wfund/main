using System;
using KBTech.Lib.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "СтруктурныйПлатеж")]
    public class StructPayment: InnerTransfer
    {
        [Property(Name = "Глубина")]
        public int? Level { get; set; }

        [Property(Name = "СсылкаНаМесто")]
        public uint? PlaceId { get; set; }
    }
}
