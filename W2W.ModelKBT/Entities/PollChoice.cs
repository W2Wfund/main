using System;
using KBTech.Lib.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "Голос")]
    public class PollChoice:BaseObject
    {
        [Property(Name = "СсылкаНаОпрос")]
        public uint? PollId { get; set; }

        [Property(Name = "СсылкаНаВариант")]
        public uint? PollVariantId { get; set; }

        [Property(Name = "СсылкаНаКонтрагента")]
        public uint? PartnerId { get; set; }
    }
}
