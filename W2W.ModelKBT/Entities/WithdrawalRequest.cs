using System;
using KBTech.Lib.Model.Attributes;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "ЗаявкаНаВыводСредств")]
    public class WithdrawalRequest: InnerTransfer
    {
        [Property(Name = "Tag")]
        public string Tag { get; set; }

        [Property(Name = "ВылютаВывода")]
        public string Currency { get; set; }

        [Property(Name = "ДатаОбработки")]
        public DateTime? ProcessedDate { get; set; }

        [Property(Name = "СтатусОбработки")]
        public string ProcessedStatus { get; set; }

        [Property(Name = "НомерКошелька")]
        public string WalletAddress { get; set; }

        [Property(Name = "ПлатежнаяСистема")]
        public string PaymentSystem { get; set; }

        [Property(Name = "СуммаВВалюте")]
        public decimal? CurrencySum { get; set; }
    }
}
