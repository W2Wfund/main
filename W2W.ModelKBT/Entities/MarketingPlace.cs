using System;
using KBTech.Lib.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "МаркетинговоеМесто")]
    public class MarketingPlace : BaseObject
    {
        [Property(Name = "Активно")]
        public bool? IsActive { get; set; }

        [Property(Name = "Алиас")]
        public string Alias { get; set; }

        [Property(Name = "Ранг")]
        public string Rank { get; set; }

        [Property(Name = "СсылкаНаКонтрагента")]
        public uint? PartnerId { get; set; }

        [Property(Name = "СсылкаНаМаркетинг")]
        public uint? MarketingId { get; set; }

        [Property(Name = "СуммаВхода")]
        public decimal? EntrySum { get; set; }

        [Property(Name = "ЗанятоМест")]
        public int? ChildCount { get; set; }

        [Property(Name = "СсылкаНаКонтрагента/Паспорт.Имя")]
        public string PartnerFirstName { get; set; }

        [Property(Name = "СсылкаНаКонтрагента/Паспорт.Фамилия")]
        public string PartnerLastName { get; set; }

        [Property(Name = "СсылкаНаКонтрагента/Паспорт.Отчество")]
        public string PartnerMiddleName { get; set; }

        [Property(Name = "СсылкаНаКонтрагента/ПерсональныйНомерКонтрагента")]
        public int? PartnerPersonalId { get; set; }

        [Property(Name = "СсылкаНаКонтрагента/СсылкаНаСпонсора")]
        public uint? PartnerInviterId { get; set; }

        [Property(Name = "СсылкаНаКонтрагента/Остаток.ИнвестиционныйСчет")]
        public decimal? PartnerBalanceInvestments { get; set; }

        [Property(Name = "СсылкаНаКонтрагента/Логин")]
        public string PartnerLogin { get; set; }

        [Property(Name = "СуммаЛевоеПлечо")]
        public decimal? PartnerLeftShoulderInvestSum { get; set; }

        [Property(Name = "СуммаПравоеПлечо")]
        public decimal? PartnerRightShoulderInvestSum { get; set; }

        [Property(Name = "БинарЛевоеПлечо")]
        public decimal? PartnerBinaryLeftShoulderSum { get; set; }

        [Property(Name = "БинарПравоеПлечо")]
        public decimal? PartnerBinaryRightShoulderSum { get; set; }
    }
}
