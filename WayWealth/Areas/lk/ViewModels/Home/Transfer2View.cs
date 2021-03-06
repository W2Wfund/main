﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WayWealth.Areas.lk.ViewModels.Home
{
    public class Transfer2View
    {
        [Display(Name = "Amount", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "ValidRequired")]
        [Range(1, 100000, ErrorMessageResourceName = "ValidRange",
          ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal Sum { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Resource))]
        public string Desctiption { get; set; }

        [Display(Name = "FinancialPassword", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "ValidRequired")]
        public string Password { get; set; }

        [Display(Name = "ReceiverLoginOrId",  ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "ValidRequired")]
        public string Login { get; set; }
    }
}