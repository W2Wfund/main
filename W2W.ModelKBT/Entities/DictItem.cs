using System;
using KBTech.Lib.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "ЭлементСправочника")]
    public class DictItem:BaseObject
    {
        [Property(Name = "Значение")]
        public string Value { get; set; }

        [Property(Name = "СсылкаНаСправочник")]
        public uint? DictId { get; set; }
    }
}
