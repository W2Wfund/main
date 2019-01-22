using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WayWealth.ViewModels.Sign
{
    public class RestoreView
    {
        [Display(Name = "LoginEmailIDRequired", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), 
            ErrorMessageResourceName = "LoginEmailIDRequired")]
        public string Login { get; set; }
    }
}