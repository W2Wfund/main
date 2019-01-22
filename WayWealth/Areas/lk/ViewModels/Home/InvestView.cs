using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WayWealth.Areas.lk.ViewModels.Home
{
    public class InvestView
    {
        [Display(Name = "Amount", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                   ErrorMessageResourceName = "ValidRequired")]
        [Range(1, 100000, ErrorMessageResourceName = "ValidRange",
             ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal Sum { get; set; }

        [Display(Name = "IsProlonged", ResourceType = typeof(Resources.Resource))]
        public bool IsProlonged { get; set; }

        [Display(Name = "IsCreateNewPlace", ResourceType = typeof(Resources.Resource))]
        public bool IsCreateNewPlace { get; set; }
    }
}