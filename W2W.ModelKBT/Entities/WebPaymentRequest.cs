using System;
using KBTech.Lib.Model.Attributes;
namespace W2W.ModelKBT.Entities
{
    [Type(Name = "ЗапросНаВебПлатеж")]
    public class WebPaymentRequest:InnerTransfer
    {
        [Property(Name = "ВременныйАдрес")]
        public string TempAddress { get; set; }

        [Property(Name = "ВылютаВывода")]
        public string Currency { get; set; }

        [Property(Name = "ПлатежнаяСистема")]
        public string PaymentSystem { get; set; }

        [Property(Name = "СуммаВВалюте")]
        public decimal? CurrencySum { get; set; }

        [Property(Name = "Транзакция")]
        public string TransactionId { get; set; }

        //[Property(Name = "Tag")]
        //public string Tag { get; set; }
    }
}
