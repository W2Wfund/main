using System;
using KBTech.Lib.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "ИнвестиционнаяПрограмма")]
    public class InvestProgram : BaseObject
    {
        [Property(Name = "ПроцентнаяСтавка")]
        public decimal YearPercent { get; set; }

        [Property(Name = "МинимальнаяСумма")]
        public decimal MinSum { get; set; }

        [Property(Name = "МаксимальнаяСумма")]
        public decimal MaxSum { get; set; }

        [Property(Name = "ШагСуммы")]
        public decimal Step { get; set; }
    }
}
