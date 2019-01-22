using System;
using KBTech.Lib.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "Вариант_ответа")]
    public class PollVariant:BaseObject
    {
        [Property(Name = "СсылкаНаОпрос")]
        public uint? PollId { get; set; }

        [Property(Name = "КоличествоГолосов")]
        public int? Count { get; set; }

        [Property(Name = "НазваниеАнгл")]
        public string ObjectNameEng { get; set; }
    }
}
