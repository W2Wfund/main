using System;
using KBTech.Lib.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "Опрос")]
    public class Poll : BaseObject
    {
        [Property(Name = "ДатаПубликации")]
        public DateTime? ActualDate { get; set; }

        [Property(Name = "НазваниеАнгл")]
        public string ObjectNameEng { get; set; }
    }
}
