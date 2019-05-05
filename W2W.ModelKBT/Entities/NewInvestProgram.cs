using System;
using KBTech.Lib.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "ИнвестиционнаяПрограммаНовая")]
    public class NewInvestProgram : BaseObject
    {
        [Property(Name = "ПроцентнаяСтавка")]
        public decimal YearPercent { get; set; }

        [Property(Name = "Сумма")]
        public decimal Sum { get; set; }

        [Property(Name = "Название")]
        public string Name { get; set; }

        [Property(Name = "ФиксированнаяСумма")]
        public bool isFixedSum { get; set; }

        [Property(Name = "ЛимитСутки")]
        public uint DayLimit { get; set; }

        [Property(Name = "ЛимитМесяц")]
        public uint MonthLimit { get; set; }

        [Property(Name = "ЛимитПриглашенного")]
        public uint? ReferalLimit { get; set; }

        [Property(Name = "МаксЛимит")]
        public uint? MaxLimit { get; set; }
    }
}
