using System;
using KBTech.Lib.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "ИнвестиционныйДоговор")]
    public class Investment:BaseObject
    {
        [Property(Name = "Номер")]
        public  int? Number { get; set; }

        [Property(Name = "ДатаОформления")]
        public  DateTime? StartDate { get; set; }

        [Property(Name = "ДатаЗавершения")]
        public  DateTime? EndDate { get; set; }

        [Property(Name = "Статус")]
        public  string Status { get; set; }

        [Property(Name = "СуммаДоговора")]
        public decimal? Sum { get; set; }

        [Property(Name = "СуммаВыплаченная")]
        public decimal? SumIncome { get; set; }

        [Property(Name = "СсылкаНаКонтрагента")]
        public uint? PartnerId { get; set; }

        [Property(Name = "ПерерасчетСумма")]
        public decimal? RecalcSum { get; set; }

        [Property(Name = "ДатаИзменения")]
        public DateTime? StatusChangedDate { get; set; }

        [Property(Name = "СсылкаНаПрограмму/Название")]
        public string ProgramName { get; set; }

        [Property(Name = "Пролонгация")]
        public bool? IsProlonged { get; set; }
    }
}
