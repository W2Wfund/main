using System;
using KBTech.Lib.Model.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.ModelKBT.Entities
{
    [Type(Name = "Контрагент")]
    public class Partner : BaseObject
    {
        [Property(Name = "ПерсональныйНомерКонтрагента")]
        public int? PersonalId { get; set; }

        [Property(Name = "СсылкаНаСпонсора")]
        public uint? InviterId { get; set; }

        [Property(Name = "Логин")]
        public string Login { get; set; }

        [Property(Name = "Пароль")]
        public string Password { get; set; }

        [Property(Name = "Контакты.Email")]
        public string Email { get; set; }

        [Property(Name = "Контакты.Email.Подтвержден")]
        public bool? IsEmailConfirmed { get; set; }

        [Property(Name = "Паспорт.Имя")]
        public string FirstName { get; set; }

        [Property(Name = "Паспорт.Фамилия")]
        public string LastName { get; set; }

        [Property(Name = "Паспорт.Отчество")]
        public string MiddleName { get; set; }

        [Property(Name = "Страна")]
        public string Country { get; set; }

        [Property(Name = "Город")]
        public string City { get; set; }

        [Property(Name = "Паспорт.ДатаРождения")]
        public DateTime? BirthDate { get; set; }

        [Property(Name = "Контакты.ТелефонКонтактный")]
        public string PhoneNumber { get; set; }

        [Property(Name = "СоцСети")]
        public string SocNetworkLink { get; set; }

        [Property(Name = "Кошелек.Bitcoin")]
        public string AccountBitcoin { get; set; }

        [Property(Name = "Кошелек.Ethereum")]
        public string AccountEthereum { get; set; }

        [Property(Name = "Кошелек.Litecoin")]
        public string AccountLitecoin { get; set; }

        [Property(Name = "Кошелек.BitcoinCash")]
        public string AccountBitcoinCash { get; set; }

        [Property(Name = "Кошелек.Ripple")]
        public string AccountRipple { get; set; }

        [Property(Name = "Остаток.ВнутреннийСчет")]
        public decimal? BalanceInner { get; set; }

        [Property(Name = "Остаток.Проценты")]
        public decimal? BalancePercents { get; set; }

        [Property(Name = "Остаток.ИнвестиционныйСчет")]
        public decimal? BalanceInvestments { get; set; }

        [Property(Name = "Остаток.Вознаграждения")]
        public decimal? BalanceRewards { get; set; }

        [Property(Name = "Пол")]
        public string Gender { get; set; }

        [Property(Name = "СтатусВерификации")]
        public string VerificationStatus { get; set; }

        [Property(Name = "Отзыв")]
        public string Review { get; set; }

        [Property(Name = "СтатусОтзыва")]
        public string ReviewStatus { get; set; }

        [Property(Name = "ДатаПубликацииОтзыва")]
        public DateTime? ReviewDate { get; set; }

        [Property(Name = "Изображение")]
        public string Image { get; set; }

        [Property(Name = "ИзображениеПаспорт1")]
        public string Passport1 { get; set; }

        [Property(Name = "ИзображениеПаспорт2")]
        public string Passport2 { get; set; }

        [Property(Name = "ИзображениеПаспорт3")]
        public string Passport3 { get; set; }

        [Property(Name = "Настройки.ПроцентПриВыводе")]
        public decimal? Settings_WithdrawalPercent { get; set; }

        [Property(Name = "Настройки.ПроцентПриПополнении")]
        public decimal? Settings_ReplenishmentPercent { get; set; }

        [Property(Name = "Настройки.НомерРабочегоДняРасчет")]
        public int? Settings_StartPayWorkDay { get; set; }

        [Property(Name = "GoogleAuthKey")]
        public string GoogleAuthKey { get; set; }

        [Property(Name = "GoogleAuthEnabled")]
        public bool? GoogleAuthEnabled { get; set; }

        [Property(Name = "ЕстьМаркетинговоеМесто")]
        public bool? IsHasMarketPlace { get; set; }
        
        [Property(Name = "СуммаЛевоеПлечо")]
        public decimal? SumLeftTreeSide { get; set; }

        [Property(Name = "СуммаПравоеПлечо")]
        public decimal? SumRightTreeSide { get; set; }
    }
}
