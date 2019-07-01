using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using W2W.ModelKBT.Entities;

namespace WayWealth.Areas.lk.ViewModels.Home
{
    public class SettingsView
    {
        public int PersonalId { get; set; }

        [Display(Name = "Login", ResourceType = typeof(Resources.Resource))]
        public string Login { get; set; }
        public string Image { get; set; }
        public string VerificationStatus { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "FirstNameRequired")]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "ValidRequired")]
        public string LastName { get; set; }

        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string SocNetworkLink { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        public string Password { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$",
            ErrorMessageResourceName = "PassErrMess",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public string NewPassword { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$",
            ErrorMessageResourceName = "PassErrMess",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public string PasswordRepeat { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource),
        //         ErrorMessageResourceName = "EmailRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "EmailRequired2")]
        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        // Адреса крипто-кошельков
        [Display(Name = "Bitcoin")]
        public string AccountBitcoin { get; set; }

        [Display(Name = "Ethereum")]
        public string AccountEthereum { get; set; }

        [Display(Name = "Litecoin")]
        public string AccountLitecoin { get; set; }

        [Display(Name = "Bitcoin Cash")]
        public string AccountBitcoinCash { get; set; }

        [Display(Name = "Ripple")]
        public string AccountRipple { get; set; }

        [Display(Name = "Usdt")]
        public string AccountUsdt { get; set; }

        // Остатки на балансах

        // Лицевой
        public decimal? BalanceInner { get; set; }
        // Проценты
        public decimal? BalancePercents { get; set; }
        // Инвестиции
        public decimal? BalanceInvestments { get; set; }
        // Вознаграждения
        public decimal? BalanceRewards { get; set; }


        [Display(Name = "PassportHomepage", ResourceType = typeof(Resources.Resource))]
        public string Passport1 { get; set; }

        [Display(Name = "PassportReg", ResourceType = typeof(Resources.Resource))]
        public string Passport2 { get; set; }

        [Display(Name = "PassportSelfi", ResourceType = typeof(Resources.Resource))]
        public string Passport3 { get; set; }

    }
}