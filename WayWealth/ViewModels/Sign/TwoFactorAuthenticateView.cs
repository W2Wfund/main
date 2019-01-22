using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WayWealth.ViewModels.Sign
{
    public class TwoFactorAuthenticateView
    {
        [Display(Name = "CodeDigit", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                     ErrorMessageResourceName = "CodeDigitRequired")]
        public string CodeDigit { get; set; }

    }
}