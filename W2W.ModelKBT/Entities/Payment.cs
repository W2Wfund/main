using System;
using KBTech.Lib.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "Платеж")]
    public class Payment : BaseObject
    {
        [Property(Name = "ДатаПлатежа")]
        public DateTime? PaymentDate { get; set; }

        [Property(Name = "НаправлениеПлатежа")]
        public string PaymentDirection { get; set; }

        [Property(Name = "СпособОплаты")]
        public string PaymentMethod { get; set; }

        [Property(Name = "СсылкаНаДоговор")]
        public uint? OrderId { get; set; }

        [Property(Name = "СсылкаНаДоговор/Название")]
        public string OrderObjectName { get; set; }

        [Property(Name = "СсылкаНаДоговор/СсылкаНаПрограмму")]
        public uint? OrderProgramId { get; set; }

        [Property(Name = "СсылкаНаДоговор/СсылкаНаПрограмму/Название")]
        public string OrderProgramObjectName { get; set; }

        [Property(Name = "СсылкаНаДоговор/СсылкаНаМаркетинг")]
        public uint? OrderMarketingId { get; set; }

        [Property(Name = "СсылкаНаДоговор/Логин")]
        public string OrderLogin { get; set; }

        [Property(Name = "СсылкаНаДоговор/ПерсональныйНомерКонтрагента")]
        public string OrderPersonalId { get; set; }

        [Property(Name = "СсылкаНаКонтрагента")]
        public uint? PartnerId { get; set; }

        [Property(Name = "СсылкаНаКонтрагента/Название")]
        public string PartnerObjectName { get; set; }

        [Property(Name = "СтатусПлатежа")]
        public string PaymentStatus { get; set; }

        [Property(Name = "СтатьяДвиженияДенежныхСредств")]
        public string PaymentArticle { get; set; }

        [Property(Name = "СуммаОстатокНаСчете")]
        public decimal? AccountBalance { get; set; }

        [Property(Name = "СуммаПлатежа")]
        public decimal? PaymentSum { get; set; }

        [Property(Name = "СуммаПриход")]
        public decimal? DebetSum { get; set; }

        [Property(Name = "СуммаРасход")]
        public decimal? CreditSum { get; set; }

        [Property(Name = "НомерПлатежа")]
        public int? PaymentNumber { get; set; }

        [Property(Name = "КомиссияЗаВывод")]
        public decimal? PaymentComission { get; set; }
    }
}
